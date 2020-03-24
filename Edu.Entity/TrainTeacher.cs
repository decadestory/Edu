/*******************************************************
*创建作者： Jerry
*类的名称： TrainTeacher
*命名空间： Edu.Entity
*创建时间： 2020/3/25
********************************************************/
using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Edu.Entity
{
	 public class TrainTeacher : BaseEntity
	 {
		 public int TrainId  {set;get;}
		 public int Id  {set;get;}
		 public int UserId  {set;get;}
	 }
}
