using Atom.EF.Base;
using Atom.EF.Core;
using Edu.Entity;
using Edu.Model;
using Edu.Repo.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edu.Repo
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public IAContext db { get; set; }

        public UserTokenModel Auth(AuthModel user)
        {
            var u = db.Set<User>().FirstOrDefault(t=>t.LoginId==user.Account);
            if (u == null) throw new Exception("用户名或密码不正确");
            var pwd = Atom.Lib.Security.CryptographyUtils.Pwd(user.Password,u.Salt);
            if (pwd.Item1 != u.Password) throw new Exception("用户名或密码不正确");
            return u.Adapt<UserTokenModel>();
        }

        public int AddOrEditUser(UserModel user, UserTokenModel curUser)
        {
            var ue = user.Adapt<User>();
            var pwd = Atom.Lib.Security.CryptographyUtils.Pwd(ue.Password);
            ue.Password = pwd.Item1;
            ue.Salt = pwd.Item2;
            ue.AddUserId = curUser.UserId;
            ue.EditUserId = curUser.UserId;

            db.IsEntityValid(ue);
            db.Set<User>().Add(ue);
            return db.SaveChanges();
        }

        public Tuple<List<UserModel>,int> Users(UserModel model) 
        {
            var where = new StringBuilder("1=1");
            if (!string.IsNullOrWhiteSpace(model.KeyWord))
                where.Append($" and (u.UserName like '%{model.KeyWord}%' or u.MobilePhone like '%{model.KeyWord}%') ");

            var cols = @"select u.*,  uel.School,uel.Grade,uel.Likes,uel.Disposition,uel.LikesStuff,uel.HasEn,uel.IsEarlyEdu,uel.IsHasAllergy,
                                uel.ParentName,uel.ParentPhone,uel.ParentGrade,uel.ParentDoing,uel.SendPeople,uel.SendPhone,uel.SendType,
                                uel.TechPeople,uel.IsHasEduType,uel.HasKnowStdudent,uel.ComLearnType ";

            var sql = $@" from [User] u 
                                left join UserExtLearner uel on uel.UserId=u.UserId
                                left join UserWorkRole uwr on uwr.UserId=u.UserId
                                where {where} uwr.RoleCode='learner' order by u.EditTime desc";
            var cntSql  = $"select count(1) {sql} ";
            var dataSql = $"{cols} {sql} OFFSET ({model.Skip}) ROW FETCH NEXT {model.PageSize} rows only ";

            //var cnt = db.SqlQuery<User,int>(cntSql) .Cast<int>().First();
            var cnt = 10;
            var data = db.Set<User>().FromSqlRaw(dataSql).Cast<UserModel>().ToList();

            return new Tuple<List<UserModel>, int>(data,cnt);

        }



        public User GetOne()
        {
            var result = db.Set<User>().FirstOrDefault();
            return result;
        }

    }
}
