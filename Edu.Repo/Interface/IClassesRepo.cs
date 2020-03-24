/*******************************************************
*创建作者： Jerry
*类的名称： IClassesRepo
*命名空间： Edu.Repo.Interface
*创建时间： 2020/3/24
********************************************************/

using Atom.EF.Base.Interface;
using Edu.Entity;
using Edu.Model;
using System;
using System.Collections.Generic;

namespace Edu.Repo.Interface
{
	 public interface IClassesRepo :  IBaseRepo<Classes>
	 {
		int AddOrEditClasses(ClassesModel c, UserTokenModel curUser);
		Tuple<List<ClassesModel>, int> Classes(ClassesModel model);
		public bool AddClassUser(ClassesUserModel model, UserTokenModel curUser);
		public bool DelClassUser(ClassesUserModel model, UserTokenModel curUser);
	 }
}
