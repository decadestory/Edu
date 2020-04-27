using Atom.Lib;
using System;
using System.ComponentModel.DataAnnotations;

namespace Edu.Model
{
    public class UserModel:PagerInBase
    {
		/// <summary>
		/// 0 系统用户 1学员 2 老师
		/// </summary>
        public int UserType { get; set; }
        public int UserId { get; set; }
		public string UserName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }

		public DateTime? BirthDay { set; get; }
		public string HeadImg { set; get; }
		public bool? Gender { set; get; }

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

		public DateTime AddTime { get; set; }
		public int AddUserId { get; set; }
		public DateTime EditTime { get; set; }
		public int EditUserId { get; set; }
		public bool IsValid { get; set; }
		public int ClassId { get; set; }
		public string KeyWord { set; get; }
	}

	public class TrainUserModel
	{
		public int TrainId { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string MobilePhone { get; set; }
		public string Remark { get; set; }
	}
}
