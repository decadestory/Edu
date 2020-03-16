using Atom.EF.Base.Interface;
using Edu.Entity;
using Edu.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Svc.Interface
{
    public interface IUserSvc : IBaseSvc
    {
        public int AddOrEditUser(UserModel user,UserTokenModel curUser);
        public User GetOne();
        public UserModel Auth(AuthModel user);
    }
}
