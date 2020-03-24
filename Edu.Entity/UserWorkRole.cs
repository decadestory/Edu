/*******************************************************
*创建作者： Jerry
*类的名称： UserWorkRole
*命名空间： Edu.Entity
*创建时间： 2020/3/23
********************************************************/
using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Edu.Entity
{
	 public class UserWorkRole : BaseEntity
	 {
		 /// <summary>
		 /// 用户角色Id
		 /// <summary>
		 public int Id  {set;get;}
		 /// <summary>
		 /// 用户Id
		 /// <summary>
		 public int UserId  {set;get;}
		 /// <summary>
		 /// 角色代码
		 /// <summary>
		 public string RoleCode  {set;get;}
	 }
}
