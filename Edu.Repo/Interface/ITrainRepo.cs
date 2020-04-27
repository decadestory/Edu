/*******************************************************
*创建作者： Jerry
*类的名称： ITrainRepo
*命名空间： Edu.Repo.Interface
*创建时间： 2020/3/25
********************************************************/

using Atom.EF.Base.Interface;
using Edu.Entity;
using Edu.Model;
using System;
using System.Collections.Generic;

namespace Edu.Repo.Interface
{
	 public interface ITrainRepo :  IBaseRepo<Train>
	 {
		public bool AddOrEditTrain(TrainModel model, UserTokenModel curUser);
		public Tuple<List<TrainModel>, int> Trains(TrainModel model);
		public List<TrainUserModel> TrainAllLearners(TrainUserModel model);
		public bool SetLearnerRemark(TrainUserModel model, UserTokenModel curUser);
	 }
}
