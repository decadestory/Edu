using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.Lib;
using Atom.Logger;
using Atom.Permissioner;
using Edu.Api.Infrastructure.Authorizes;
using Edu.Api.Infrastructure.Filters;
using Edu.Entity;
using Edu.Model;
using Edu.Svc;
using Edu.Svc.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Edu.Api.Controllers
{
    public class UserController : BaseController
    {
        public IUserSvc svc { get; set; }
        public IALogger logger { get; set; }
        public IPermissioner per { get; set; }




        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
       [Auth]
        public Br<List<UserModel>> Users(UserModel model)
        {
            var result = svc.Users(model);
            return new Br<List<UserModel>>(result.Item1, extData: result.Item2);
        }

        /// <summary>
        /// 获取班级用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Auth]
        public Br<List<UserModel>> ClassUsers(UserModel model)
        {
            var result = svc.ClassUsers(model);
            return new Br<List<UserModel>>(result.Item1, extData: result.Item2);
        }


        /// <summary>
        /// 添加或修改用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckParams]
       [Auth]
        public Br<int> AddOrEditUser(UserModel user)
        {
            var result = svc.AddOrEditUser(user, CurUser);
            return new Br<int>(result);
        }

        /// <summary>
        /// 获取一个用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CheckParams]
        public Br<User> GetOne()
        {
            var result = svc.GetOne();
            return new Br<User>(result);
        }

    }
}