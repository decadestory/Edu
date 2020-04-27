/*******************************************************
*创建作者： Jerry
*类的名称： ClassesRepo
*命名空间： Edu.Repo
*创建时间： 2020/3/24
********************************************************/

using Atom.EF.Base;
using Edu.Entity;
using Edu.Model;
using Edu.Repo.Interface;
using System.Linq;
using Atom.EF.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;

namespace Edu.Repo
{
    public class ClassesRepo : BaseRepo<Classes>, IClassesRepo
    {
        public IAContextEF6 db { get; set; }


        public int AddOrEditClasses(ClassesModel c, UserTokenModel curUser)
        {
            if (c.Id > 0) goto editClass;

            var ue = c.Adapt<Classes>();
            ue.AddUserId = curUser.UserId;
            ue.EditUserId = curUser.UserId;
            ue.AddTime = DateTime.Now;
            ue.EditTime = DateTime.Now;
            db.Set<Classes>().Add(ue);
            return db.SaveChanges();

            editClass:

            var exist = db.Set<Classes>().Find(c.Id);
            exist.ClassName = c.ClassName;
            exist.ClassType = c.ClassType;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = curUser.UserId;
            exist.IsValid = c.IsValid;
            db.Entry(exist).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }


        public Tuple<List<ClassesModel>, int> Classes(ClassesModel model)
        {
            var where = new StringBuilder("1=1");

            if (!string.IsNullOrWhiteSpace(model.KeyWord))
                where.Append($" and u.ClassName like '%{model.KeyWord}%'  ");

            if (model.IsValid==true) where.Append($" and u.IsValid=1 ");

            var cols = @"select * ";
            var sql = $@" from Classes u where {where}  ";
            var cntSql = $"select count(1) {sql} ";
            var dataSql = $"{cols} {sql}  order by u.EditTime desc OFFSET ({model.Skip}) ROW FETCH NEXT {model.PageSize} rows only ";

            var cnt = db.Database.SqlQuery<int>(cntSql).First();
            var data = db.Database.SqlQuery<ClassesModel>(dataSql).ToList();

            return new Tuple<List<ClassesModel>, int>(data, cnt);
        }



        public bool AddClassUser(ClassesUserModel model, UserTokenModel curUser)
        {
            var exist = db.Set<ClassUser>().FirstOrDefault(t => t.ClassId == model.ClassId && t.UserId == model.UserId && t.IsValid);
            if (exist != null) return true;

            var en = new ClassUser
            {
                AddUserId = curUser.UserId,
                EditUserId = curUser.UserId,
                AddTime = DateTime.Now,
                EditTime = DateTime.Now,
                IsValid = true,
                ClassId = model.ClassId,
                UserId = model.UserId
            };
            db.Set<ClassUser>().Add(en);
            db.SaveChanges();
            return true;
        }

        public bool DelClassUser(ClassesUserModel model, UserTokenModel curUser)
        {
            var exist = db.Set<ClassUser>().FirstOrDefault(t => t.ClassId == model.ClassId && t.UserId == model.UserId && t.IsValid);
            if (exist == null) return true;

            exist.IsValid = false;
            exist.EditUserId = curUser.UserId;
            exist.EditTime = DateTime.Now;
            db.Entry(exist).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return true;
        }


    }
}
