/*******************************************************
*创建作者： Jerry
*类的名称： TrainLearner
*命名空间： Edu.Entity
*创建时间： 2020/3/25
********************************************************/
using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Edu.Entity
{
	 public class TrainLearner : BaseEntity
	 {
		 public int UserId  {set;get;}
		 public int TrainId  {set;get;}
		 public int Id  {set;get;}
		 public string Remark  {set;get;}
	 }
}
