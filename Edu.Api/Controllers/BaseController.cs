using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edu.Api.Infrastructure.Authorizes;
using Edu.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edu.Api.Controllers
{
    /// <summary>
    /// 自定义路由模版
    /// 用于解决swagger文档No operations defined in spec!问题
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public UserTokenModel CurUser
        {
            get
            {
                var cu = HttpContext.Items["curUserInfo"] as UserTokenModel;
                return cu;
            }
        }
    }
}