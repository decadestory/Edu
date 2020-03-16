using Atom.EF.Base;
using Edu.Entity;
using Edu.Model;
using Edu.Repo.Interface;
using Edu.Svc.Interface;
using System;

namespace Edu.Svc
{
    public class UserSvc : BaseSvc, IUserSvc
    {
        public  IUserRepo rep { get; set; }

        public UserModel Auth(AuthModel user)
        {
            return rep.Auth(user);
        }


        public int AddOrEditUser(UserModel user, UserTokenModel curUser)
        {
            var result = rep.AddOrEditUser(user,curUser);
            return result;
        }


        public User GetOne()
        {
            var result = rep.GetOne();
            return rep.GetOne();
        }

    }
}
