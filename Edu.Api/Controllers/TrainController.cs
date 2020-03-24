/*******************************************************
*创建作者： Jerry
*类的名称： TrainController
*命名空间： Edu.Api.Controllers
*创建时间： 2020/3/25
********************************************************/

using Atom.Lib;
 using Atom.Logger;
using Edu.Model;
using Edu.Api.Infrastructure.Filters;
 using Edu.Svc.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Edu.Api.Controllers
{
	   public class TrainController : BaseController
	 {
		 public ITrainSvc svc { get; set; }
		 public IALogger logger { get; set; }
	 }
}
