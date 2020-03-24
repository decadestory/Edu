/*******************************************************
*创建作者： Jerry
*类的名称： TrainLearner
*命名空间： Edu.Entity.Mapper
*创建时间： 2020/3/25
********************************************************/
using Atom.EF.Base;
using System;
namespace Edu.Entity.Mapper
{
	 public class TrainLearnerMapper : BaseMap<TrainLearner>
	 {
		public override void Init()
		{
			ToTable("TrainLearner");
			HasKey(m => m.Id);
		}
	 }
}
