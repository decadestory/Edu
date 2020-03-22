/*******************************************************
*创建作者： Jerry
*类的名称： UserExtLearner
*命名空间： Edu.Entity
*创建时间： 2020/3/23
********************************************************/
using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Edu.Entity
{
	 public class UserExtLearner : BaseEntity
	 {
		[Key]
		public int UserId { set; get; }
		public string ParentDoing  {set;get;}
		 public string ParentName  {set;get;}
		 public string HasKnowStdudent  {set;get;}
		 public string SendType  {set;get;}
		 public string School  {set;get;}
		 public string SendPhone  {set;get;}
		 public bool? IsHasEduType  {set;get;}
		 public string Likes  {set;get;}
		 public string HasEn  {set;get;}
		 public string Grade  {set;get;}
		 public string ComLearnType  {set;get;}
		 public string LikesStuff  {set;get;}
		 public bool? IsHasAllergy  {set;get;}
		 public string ParentGrade  {set;get;}
		 public string Disposition  {set;get;}
		 public bool? IsEarlyEdu  {set;get;}
		 public string TechPeople  {set;get;}
		 public string ParentPhone  {set;get;}
		 public string SendPeople  {set;get;}
	 }
}
