/*******************************************************
*创建作者： Jerry
*类的名称： ITrainSvc
*命名空间： Edu.Svc.Interface
*创建时间： 2020/3/25
********************************************************/

using Atom.EF.Base.Interface;
using Edu.Entity;
using Edu.Model;
using System;
using System.Collections.Generic;

namespace Edu.Svc.Interface
{
	 public interface ITrainSvc :  IBaseSvc
	 {
		public bool AddOrEditTrain(TrainModel model, UserTokenModel curUser);
		public Tuple<List<TrainModel>, int> Trains(TrainModel model);
		public List<TrainUserModel> TrainAllLearners(TrainUserModel model);
		public bool SetLearnerRemark(TrainUserModel model, UserTokenModel curUser);



	}
}
