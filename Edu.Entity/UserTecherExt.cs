/*******************************************************
*创建作者： Jerry
*类的名称： UserTecherExt
*命名空间： Edu.Entity
*创建时间： 2020/3/23
********************************************************/
using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Edu.Entity
{
	 public class UserTecherExt : BaseEntity
	 {
		[Key]
		public int UserId { set; get; }
		public string TechHistory  {set;get;}
		 public string Certificate  {set;get;}
	 }
}
