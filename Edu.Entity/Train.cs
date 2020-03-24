/*******************************************************
*创建作者： Jerry
*类的名称： Train
*命名空间： Edu.Entity
*创建时间： 2020/3/25
********************************************************/
using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Edu.Entity
{
	 public class Train : BaseEntity
	 {
		 public int Id  {set;get;}
		 public int ClassId  {set;get;}
		 public DateTime EndTime  {set;get;}
		 public string Remark  {set;get;}
		 public string CourseCode  {set;get;}
		 public DateTime StartTime  {set;get;}
	 }
}
