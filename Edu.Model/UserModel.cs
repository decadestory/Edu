using Atom.Lib;
using System;
using System.ComponentModel.DataAnnotations;

namespace Edu.Model
{
    public class UserModel:PagerInBase
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage ="手机号不能为空")]
        [StringLength(11, ErrorMessage = "MobilePhone太长了")]
        public string MobilePhone { get; set; }

		public string ParentDoing { set; get; }
		public string ParentName { set; get; }
		public string HasKnowStdudent { set; get; }
		public string SendType { set; get; }
		public string School { set; get; }
		public string SendPhone { set; get; }
		public bool? IsHasEduType { set; get; }
		public string Likes { set; get; }
		public string HasEn { set; get; }
		public string Grade { set; get; }
		public string ComLearnType { set; get; }
		public string LikesStuff { set; get; }
		public bool? IsHasAllergy { set; get; }
		public string ParentGrade { set; get; }
		public string Disposition { set; get; }
		public bool? IsEarlyEdu { set; get; }
		public string TechPeople { set; get; }
		public string ParentPhone { set; get; }
		public string SendPeople { set; get; }
		public string TechHistory { set; get; }
		public string Certificate { set; get; }

		public string KeyWord { set; get; }


	}
}
