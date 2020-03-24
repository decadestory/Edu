/*******************************************************
*创建作者： Jerry
*类的名称： IClassesSvc
*命名空间： Edu.Svc.Interface
*创建时间： 2020/3/24
********************************************************/

using Atom.EF.Base.Interface;
using Edu.Entity;
using Edu.Model;
using System;
using System.Collections.Generic;

namespace Edu.Svc.Interface
{
	 public interface IClassesSvc :  IBaseSvc
	 {
		int AddOrEditClasses(ClassesModel c, UserTokenModel curUser);
		Tuple<List<ClassesModel>, int> Classes(ClassesModel model);
		 bool AddClassUser(ClassesUserModel model, UserTokenModel curUser);
		 bool DelClassUser(ClassesUserModel model, UserTokenModel curUser);
	}
}
