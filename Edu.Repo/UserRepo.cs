using Atom.EF.Base;
using Atom.EF.Core;
using Edu.Entity;
using Edu.Model;
using Edu.Repo.Interface;
using Mapster;
using System;
using System.Linq;

namespace Edu.Repo
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public IAContext db { get; set; }

        public UserModel Auth(AuthModel user)
        {
            var u = db.Set<User>().FirstOrDefault(t=>t.LoginId==user.Account);
            if (u == null) throw new Exception("用户名或密码不正确");
            var pwd = Atom.Lib.Security.CryptographyUtils.Pwd(user.Password,u.Salt);
            if (pwd.Item1 != u.Password) throw new Exception("用户名或密码不正确");
            return u.Adapt<UserModel>();
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

        public User GetOne()
        {
            var result = db.Set<User>().FirstOrDefault();
            return result;
        }

    }
}
