/*******************************************************
*创建作者： Jerry
*类的名称： ClassesController
*命名空间： Edu.Api.Controllers
*创建时间： 2020/3/24
********************************************************/

using Atom.Lib;
 using Atom.Logger;
using Edu.Model;
using Edu.Api.Infrastructure.Filters;
 using Edu.Svc.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Edu.Api.Controllers
{
	   public class ClassesController : BaseController
	 {
		 public IClassesSvc svc { get; set; }
		 public IALogger logger { get; set; }

        /// <summary>
        /// 获取班级列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Auth]
        public Br<List<ClassesModel>> Classes(ClassesModel model)
        {
            var result = svc.Classes(model);
            return new Br<List<ClassesModel>>(result.Item1, extData: result.Item2);
        }


        /// <summary>
        /// 添加或修改班级
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckParams]
        [Auth]
        public Br<int> AddOrEditClasses(ClassesModel c)
        {
            var result = svc.AddOrEditClasses(c, CurUser);
            return new Br<int>(result);
        }

     
        [HttpPost, CheckParams,Auth]
        public Br<bool> AddClassUser(ClassesUserModel c)
        {
            var result = svc.AddClassUser(c, CurUser);
            return new Br<bool>(result);
        }

        [HttpPost, CheckParams, Auth]
        public Br<bool> DelClassUser(ClassesUserModel c)
        {
            var result = svc.DelClassUser(c, CurUser);
            return new Br<bool>(result);
        }

    }
}
