/*******************************************************
*创建作者： Jerry
*类的名称： ClassUser
*命名空间： Edu.Entity
*创建时间： 2020/3/24
********************************************************/
using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Edu.Entity
{
	 public class ClassUser : BaseEntity
	 {
		 public int ClassId  {set;get;}
		 public int UserId  {set;get;}
		 public int Id  {set;get;}
	 }
}
