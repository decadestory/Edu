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
using Atom.Lib;

namespace Edu.Repo
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public IAContextEF6 db { get; set; }

        public UserTokenModel Auth(AuthModel user)
        {
            var u = db.Set<User>().FirstOrDefault(t => t.LoginId == user.Account);
            if (u == null) throw new Exception("用户名或密码不正确");
            var pwd = Atom.Lib.Security.CryptographyUtils.Pwd(user.Password, u.Salt);
            if (pwd.Item1 != u.Password) throw new Exception("用户名或密码不正确");
            return u.Adapt<UserTokenModel>();
        }

        public int AddOrEditUser(UserModel user, UserTokenModel curUser)
        {
            if (user.UserId > 0) goto editUser;

            var ue = user.Adapt<User>();
            ue.Password = "123456abc";
            var pwd = Atom.Lib.Security.CryptographyUtils.Pwd(ue.Password);
            ue.Password = pwd.Item1;
            ue.Salt = pwd.Item2;
            ue.LoginId = string.IsNullOrWhiteSpace(ue.LoginId) ? Guid.NewGuid().ToString("N") : user.LoginId;
            ue.AddUserId = curUser.UserId;
            ue.EditUserId = curUser.UserId;
            ue.AddTime = DateTime.Now;
            ue.EditTime = DateTime.Now;
            db.Set<User>().Add(ue);
            db.SaveChanges();

            if (user.UserType == 1)
            {
                var uex = user.Adapt<UserExtLearner>();
                uex.AddUserId = curUser.UserId;
                uex.EditUserId = curUser.UserId;
                uex.AddTime = DateTime.Now;
                uex.EditTime = DateTime.Now;
                uex.UserId = ue.UserId;
                db.Set<UserExtLearner>().Add(uex);
            }
            else if (user.UserType == 2)
            {
                var uext = user.Adapt<UserTecherExt>();
                uext.AddUserId = curUser.UserId;
                uext.EditUserId = curUser.UserId;
                uext.AddTime = DateTime.Now;
                uext.EditTime = DateTime.Now;
                uext.UserId = ue.UserId;
                db.Set<UserTecherExt>().Add(uext);
            }

            var uwr = new UserWorkRole();
            uwr.IsValid = true;
            uwr.AddUserId = curUser.UserId;
            uwr.EditUserId = curUser.UserId;
            uwr.AddTime = DateTime.Now;
            uwr.EditTime = DateTime.Now;
            uwr.RoleCode = user.UserType == 1 ? "learner" : "trainer";
            uwr.UserId = ue.UserId;
            db.Set<UserWorkRole>().Add(uwr);

            return db.SaveChanges();

            editUser:

            var exist = db.Set<User>().Find(user.UserId);
            exist.UserName = user.UserName;
            exist.MobilePhone = user.MobilePhone;
            exist.Gender = user.Gender;
            exist.HeadImg = user.HeadImg;
            exist.BirthDay = user.BirthDay;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = curUser.UserId;
            exist.IsValid = user.IsValid;
            db.Entry(exist).State = System.Data.Entity.EntityState.Modified;

            if (user.UserType == 1)
            {
                var existuex = db.Set<UserExtLearner>().FirstOrDefault(t => t.UserId == user.UserId);

                existuex.School = user.School;
                existuex.Grade = user.Grade;
                existuex.Likes = user.Likes;
                existuex.Disposition = user.Disposition;
                existuex.LikesStuff = user.LikesStuff;
                existuex.HasEn = user.HasEn;
                existuex.IsEarlyEdu = user.IsEarlyEdu;
                existuex.IsHasAllergy = user.IsHasAllergy;
                existuex.ParentName = user.ParentName;
                existuex.ParentPhone = user.ParentPhone;
                existuex.ParentGrade = user.ParentGrade;
                existuex.ParentDoing = user.ParentDoing;
                existuex.SendPeople = user.SendPeople;
                existuex.SendPhone = user.SendPhone;
                existuex.SendType = user.SendType;
                existuex.TechPeople = user.TechPeople;
                existuex.IsHasEduType = user.IsHasEduType;
                existuex.HasKnowStdudent = user.HasKnowStdudent;
                existuex.ComLearnType = user.ComLearnType;
                existuex.EditUserId = curUser.UserId;
                existuex.EditTime = DateTime.Now;
                db.Entry(existuex).State = System.Data.Entity.EntityState.Modified;
            }
            else if (user.UserType == 2)
            {
                var existuext = db.Set<UserTecherExt>().FirstOrDefault(t => t.UserId == user.UserId);
                existuext.TechHistory = user.TechHistory;
                existuext.Certificate = user.Certificate;
                existuext.EditUserId = curUser.UserId;
                existuext.EditTime = DateTime.Now;
                db.Entry(existuext).State = System.Data.Entity.EntityState.Modified;
            }
            return db.SaveChanges();
        }

        public Tuple<List<UserModel>, int> Users(UserModel model)
        {
            var where = new StringBuilder("1=1");
            if (model.UserType == 1)
                where.Append($" and  uwr.RoleCode='learner' ");
            else if (model.UserType == 2)
                where.Append($" and  uwr.RoleCode='trainer' ");


            if (!string.IsNullOrWhiteSpace(model.KeyWord))
                where.Append($" and (u.UserName like '%{model.KeyWord}%' or u.MobilePhone like '%{model.KeyWord}%') ");

            var cols = @"select u.*,  uel.School,uel.Grade,uel.Likes,uel.Disposition,uel.LikesStuff,uel.HasEn,uel.IsEarlyEdu,uel.IsHasAllergy,
                                uel.ParentName,uel.ParentPhone,uel.ParentGrade,uel.ParentDoing,uel.SendPeople,uel.SendPhone,uel.SendType,
                                uel.TechPeople,uel.IsHasEduType,uel.HasKnowStdudent,uel.ComLearnType ";

            if (model.UserType == 2) cols = @"select u.*,  utel.TechHistory,utel.Certificate ";

                var sql = $@" from [User] u 
                                left join UserExtLearner uel on uel.UserId=u.UserId
                                left join UserTecherExt utel on utel.UserId=u.UserId
                                left join UserWorkRole uwr on uwr.UserId=u.UserId
                                where {where}  ";
            var cntSql = $"select count(1) {sql} ";
            var dataSql = $"{cols} {sql}  order by u.EditTime desc OFFSET ({model.Skip}) ROW FETCH NEXT {model.PageSize} rows only ";

            var cnt = db.Database.SqlQuery<int>(cntSql).First();
            var data = db.Database.SqlQuery<UserModel>(dataSql).ToList();

            return new Tuple<List<UserModel>, int>(data, cnt);
        }


        public Tuple<List<UserModel>, int> ClassUsers(UserModel model)
        {
            var where = new StringBuilder(" u.IsValid=1 ");
                where.Append($" and  uwr.RoleCode='learner' ");

            if (model.UserType == 1)
                where.Append($" and  u.UserId not in (select UserId from ClassUser where IsValid=1 and ClassId={model.ClassId}) ");
            else if (model.UserType == 2)
                where.Append($" and  u.UserId  in (select UserId from ClassUser where IsValid=1 and ClassId={model.ClassId}) ");

            if (!string.IsNullOrWhiteSpace(model.KeyWord))
                where.Append($" and (u.UserName like '%{model.KeyWord}%' or u.MobilePhone like '%{model.KeyWord}%') ");

            var cols = @"select u.* ";
            var sql = $@" from [User] u 
                                left join UserExtLearner uel on uel.UserId=u.UserId
                                left join UserWorkRole uwr on uwr.UserId=u.UserId
                                where {where}  ";
            var cntSql = $"select count(1) {sql} ";
            var dataSql = $"{cols} {sql}  order by u.EditTime desc OFFSET ({model.Skip}) ROW FETCH NEXT {model.PageSize} rows only ";

            var cnt = db.Database.SqlQuery<int>(cntSql).First();
            var data = db.Database.SqlQuery<UserModel>(dataSql).ToList();

            return new Tuple<List<UserModel>, int>(data, cnt);
        }

        public User GetOne()
        {
            var result = db.Set<User>().FirstOrDefault();
            return result;
        }

    }
}
