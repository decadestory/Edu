/*******************************************************
*创建作者： Jerry
*类的名称： TrainModel
*命名空间： Edu.Model
*创建时间： 2020/3/25
********************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edu.Model
{
	 public class TrainModel
	 {
		 public int Id  {set;get;}
		 public DateTime AddTime  {set;get;}
		 public int ClassId  {set;get;}
		 public DateTime EndTime  {set;get;}
		 public DateTime EditTime  {set;get;}
		 public string Remark  {set;get;}
		 public string CourseCode  {set;get;}
		 public int AddUserId  {set;get;}
		 public bool IsValid  {set;get;}
		 public int EditUserId  {set;get;}
		 public DateTime StartTime  {set;get;}
	 }
}
