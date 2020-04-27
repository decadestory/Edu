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
using System.Collections.Generic;

namespace Edu.Api.Controllers
{
    public class TrainController : BaseController
    {
        public ITrainSvc svc { get; set; }
        public IALogger logger { get; set; }

        [HttpPost, CheckParams, Auth]
        public Br<bool> AddOrEditTrain(TrainModel model)
        {
            var result = svc.AddOrEditTrain(model, CurUser);
            return new Br<bool>(result);
        }

        [HttpPost, CheckParams, Auth]
        public Br<List<TrainModel>> Trains(TrainModel model)
        {
            var result = svc.Trains(model);
            return new Br<List<TrainModel>>(result.Item1,extData:result.Item2);
        }

        [HttpPost, CheckParams, Auth]
        public Br<List<TrainUserModel>> TrainAllLearners(TrainUserModel model)
        { 
            var result = svc.TrainAllLearners(model);
            return new Br<List<TrainUserModel>>(result);
        }

        [HttpPost, CheckParams, Auth]
        public Br<bool> SetLearnerRemark(TrainUserModel model)
        {
            var result = svc.SetLearnerRemark(model,CurUser);
            return new Br<bool>(result);
        }

    }
}
