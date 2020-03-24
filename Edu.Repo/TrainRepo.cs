/*******************************************************
*创建作者： Jerry
*类的名称： TrainRepo
*命名空间： Edu.Repo
*创建时间： 2020/3/25
********************************************************/

using Atom.EF.Base;
using Edu.Entity;
using Edu.Model;
using Edu.Repo.Interface;
using System.Linq;
using Atom.EF.Core;
using System;

namespace Edu.Repo
{
	 public class TrainRepo : BaseRepo<Train>, ITrainRepo
	 {
		 public IAContext db { get; set; }

	 }
}
