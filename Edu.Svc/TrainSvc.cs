/*******************************************************
*创建作者： Jerry
*类的名称： TrainSvc
*命名空间： Edu.Svc
*创建时间： 2020/3/25
********************************************************/

using Atom.EF.Base;
using Edu.Entity;
using Edu.Model;
using Edu.Repo.Interface;
using System.Linq;
using Edu.Svc.Interface;
using System;

namespace Edu.Svc
{
	 public class TrainSvc : BaseSvc, ITrainSvc
	 {
		 public  ITrainRepo rep { get; set; }
	 }
}
