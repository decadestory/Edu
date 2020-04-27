/*******************************************************
*创建作者： Jerry
*类的名称： TrainRepo
*命名空间： Edu.Repo
*创建时间： 2020/3/25
********************************************************/

using Atom.EF.Base;
using Edu.Entity;
using Edu.Model;
using Edu.Repo.Interface;
using System.Linq;
using Atom.EF.Core;
using System;
using Mapster;
using System.Collections.Generic;
using System.Text;
using Atom.Lib.Collection;

namespace Edu.Repo
{
    public class TrainRepo : BaseRepo<Train>, ITrainRepo
    {
        public IAContextEF6 db { get; set; }

        public bool AddOrEditTrain(TrainModel model, UserTokenModel curUser)
        {
            if (model.Id > 0) goto editLogic;

            var ue = model.Adapt<Train>();
            ue.AddUserId = curUser.UserId;
            ue.EditUserId = curUser.UserId;
            ue.AddTime = DateTime.Now;
            ue.EditTime = DateTime.Now;
            db.Set<Train>().Add(ue);
            db.SaveChanges();

            model.TeacherIds.ForEach(t =>
            {
                var te = new TrainTeacher();
                te.AddUserId = curUser.UserId;
                te.EditUserId = curUser.UserId;
                te.AddTime = DateTime.Now;
                te.EditTime = DateTime.Now;
                te.IsValid = true;
                te.TrainId = ue.Id;
                te.UserId = t;
                db.Set<TrainTeacher>().Add(te);
            });
            db.SaveChanges();

            var classUsers = db.Set<ClassUser>().Where(t=>t.IsValid && t.ClassId==model.ClassId).ToList();
            classUsers.ForEach(t =>
            {
                var te = new TrainLearner();
                te.AddUserId = curUser.UserId;
                te.EditUserId = curUser.UserId;
                te.AddTime = DateTime.Now;
                te.EditTime = DateTime.Now;
                te.IsValid = true;
                te.TrainId = ue.Id;
                te.UserId = t.UserId;
                te.Remark = "";
                db.Set<TrainLearner>().Add(te);
            });
            db.SaveChanges();

            return true;

            editLogic:

            var exist = db.Set<Train>().Find(model.Id);
            exist.StartTime = model.StartTime;
            exist.EndTime = model.EndTime;
            exist.ClassId = model.ClassId;
            exist.CourseCode = model.CourseCode;
            exist.Remark = model.Remark;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = curUser.UserId;
            exist.IsValid = model.IsValid;
            db.Entry(exist).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var existTrainTeachers = db.Set<TrainTeacher>().Where(t=>t.IsValid && t.TrainId==exist.Id).ToList();
            existTrainTeachers.ForEach(t =>
            {
                t.IsValid = false;
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            }) ;
            db.SaveChanges();
            
            model.TeacherIds.ForEach(t =>
            {
                var te = new TrainTeacher();
                te.AddUserId = curUser.UserId;
                te.EditUserId = curUser.UserId;
                te.AddTime = DateTime.Now;
                te.EditTime = DateTime.Now;
                te.IsValid = true;
                te.TrainId = exist.Id;
                te.UserId = t;
                db.Set<TrainTeacher>().Add(te);
            });
            db.SaveChanges();

            var existTrainLearners = db.Set<TrainLearner>().Where(t => t.IsValid && t.TrainId == exist.Id).ToList();
            existTrainLearners.ForEach(t =>
            {
                t.IsValid = false;
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            });
            db.SaveChanges();

            var classUserse = db.Set<ClassUser>().Where(t => t.IsValid && t.ClassId == model.ClassId).ToList();
            classUserse.ForEach(t =>
            {
                var te = new TrainLearner();
                te.AddUserId = curUser.UserId;
                te.EditUserId = curUser.UserId;
                te.AddTime = DateTime.Now;
                te.EditTime = DateTime.Now;
                te.IsValid = true;
                te.TrainId = exist.Id;
                te.UserId = t.UserId;
                te.Remark = "";
                db.Set<TrainLearner>().Add(te);
            });
            db.SaveChanges();

            return true;
        }


        public Tuple<List<TrainModel>, int> Trains(TrainModel model)
        {
            var where = new StringBuilder("1=1");

            if (!string.IsNullOrWhiteSpace(model.KeyWord))
                where.Append($" and tte.TrainTeacherNames like '%{model.KeyWord}%'  ");

            var pre = @"select t.Id,
                                (SELECT STUFF((SELECT ','+ Cast(UserId as varchar(100)) FROM  TrainTeacher (NOLOCK) where TrainId=t.Id and IsValid=1 for xml path('')),1,1,'')) TrainTeacherIdStr,
                                (SELECT STUFF((SELECT ','+ u.UserName FROM TrainTeacher tt left join [User] u on tt.userid=u.UserId where TrainId=t.Id and tt.IsValid=1 for xml path('')),1,1,'')) TrainTeacherNames
                                into #temp_trainers from Train t;";

            var cols = @"select t.*,acc.CateName,c.ClassName, tte.TrainTeacherIdStr,tte.TrainTeacherNames";

            var sql = $@"from Train t
                                    left join AtomCateConfig acc on t.CourseCode=acc.CateCode
                                    left join Classes c on c.Id=t.ClassId
                                    left join #temp_trainers tte on tte.Id=t.Id where {where}  ";

            var cntSql = $"{pre} select count(1) {sql} ";
            var dataSql = $"{pre} {cols} {sql}  order by t.EditTime desc OFFSET ({model.Skip}) ROW FETCH NEXT {model.PageSize} rows only ";

            var cnt = db.Database.SqlQuery<int>(cntSql).First();
            var data = db.Database.SqlQuery<TrainModel>(dataSql).ToList();

            data.ForEach(t=> t.TeacherIds = ListConverter.StringToListInt(t.TrainTeacherIdStr,','));

            return new Tuple<List<TrainModel>, int>(data, cnt);
        }

        public List<TrainUserModel> TrainAllLearners(TrainUserModel model)
        {
            var sql = $@" select tl.Id TrainId,u.UserId,u.UserName,u.MobilePhone,tl.Remark from TrainLearner tl
                                    left join [User] u on u.UserId=tl.UserId
                                    where  tl.IsValid=1 and  tl.TrainId={model.TrainId}  ";
            var data = db.Database.SqlQuery<TrainUserModel>(sql).ToList();
            return data;
        }


        public bool SetLearnerRemark(TrainUserModel model,UserTokenModel curUser)
        {
            var exist = db.Set<TrainLearner>().FirstOrDefault(t=>t.IsValid && t.Id==model.TrainId);
            if (exist == null) throw new Exception("学生不存在");

            exist.Remark = model.Remark;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = curUser.UserId;

            db.Entry(exist).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges()>0;
        }


    }
}
