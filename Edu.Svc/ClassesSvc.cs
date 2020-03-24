/*******************************************************
*创建作者： Jerry
*类的名称： ClassesSvc
*命名空间： Edu.Svc
*创建时间： 2020/3/24
********************************************************/

using Atom.EF.Base;
using Edu.Entity;
using Edu.Model;
using Edu.Repo.Interface;
using System.Linq;
using Edu.Svc.Interface;
using System;
using System.Collections.Generic;

namespace Edu.Svc
{
	 public class ClassesSvc : BaseSvc, IClassesSvc
	 {
		 public  IClassesRepo rep { get; set; }
		 
		public int AddOrEditClasses(ClassesModel c, UserTokenModel curUser)
		{
			return rep.AddOrEditClasses(c,curUser);
		}

		public Tuple<List<ClassesModel>, int> Classes(ClassesModel model)
		{
			return rep.Classes(model);
		}

		public bool AddClassUser(ClassesUserModel model, UserTokenModel curUser)
		{
			return rep.AddClassUser(model,curUser);
		}

		public bool DelClassUser(ClassesUserModel model, UserTokenModel curUser)
		{
			return rep.DelClassUser(model,curUser);
		}
	}
}
