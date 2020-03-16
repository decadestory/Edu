using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.Lib;
using Edu.Api.Infrastructure.Authorizes;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using Edu.Model;

namespace Edu.Api.Infrastructure.Filters
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            filterContext.HttpContext.Request.Headers.TryGetValue("Jwt-Token", out StringValues token);
            if (!token.Any())
            {
                var errorBr = new Br<string>("拒绝访问", -1, "没有身份认证");
                filterContext.Result = new JsonResult(errorBr);
                return;
            }

            var isValid = AuthorizeUtils.Validate(token);
            if (!isValid)
            {
                var errorBr = new Br<string>("拒绝访问", -1, "身份认证失败");
                filterContext.Result = new JsonResult(errorBr);
                return;
            }

            filterContext.HttpContext.Items["curUserInfo"] = AuthorizeUtils.GetCurUser<UserTokenModel>(token);

            base.OnActionExecuting(filterContext);

        }

    }
}
