using Atom.EF.Base.Interface;
using Edu.Entity;
using Edu.Model;
using System;

namespace Edu.Repo.Interface
{
    public interface IUserRepo : IBaseRepo<User>
    {
        public int AddOrEditUser(UserModel user, UserTokenModel curUser);
        public User GetOne();
        public UserModel Auth(AuthModel user);
    }
}
