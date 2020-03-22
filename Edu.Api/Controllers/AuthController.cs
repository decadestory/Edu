using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.Lib;
using Atom.Permissioner;
using Edu.Api.Infrastructure.Authorizes;
using Edu.Api.Infrastructure.Filters;
using Edu.Model;
using Edu.Svc.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edu.Api.Controllers
{
    public class AuthController : BaseController
    {
        public IUserSvc svc { get; set; }
        public IPermissioner per { get; set; }


        /// <summary>
        /// 身份认证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckParams]
        public Br<UserTokenModel> Auth(AuthModel user)
        {
            var result = svc.Auth(user);
            var token = AuthorizeUtils.Serialize(result);
            HttpContext.Response.Headers.Add("Jwt-Token", token);
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Jwt-Token");
            result.Permissions = per.GetPermission(result.UserId);
            return new Br<UserTokenModel>(result);
        }
    }
}