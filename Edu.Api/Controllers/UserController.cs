using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.Lib;
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

        /// <summary>
        /// 身份认证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckParams]
        public Br<UserModel> Auth(AuthModel user) 
        {
            var result = svc.Auth(user);
            var token = AuthorizeUtils.Serialize(result);
            HttpContext.Response.Headers.Add("Jwt-Token", token);
            return new Br<UserModel>(result);
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Users()
        {
            return "Usersl Id 10";
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
            var result = svc.AddOrEditUser(user,CurUser);
            return new Br<int>(result);
        }

        /// <summary>
        /// 获取一个用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Br<User> GetOne()
        {
            var result = svc.GetOne();
            return new Br<User>(result);
        }

    }
}