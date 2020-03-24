/*******************************************************
*创建作者： Jerry
*类的名称： ClassesModel
*命名空间： Edu.Model
*创建时间： 2020/3/24
********************************************************/

using Atom.Lib;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edu.Model
{
    public class ClassesModel : PagerInBase
    {
        public bool IsValid { set; get; }
        public string ClassName { set; get; }
        public int AddUserId { set; get; }
        public DateTime AddTime { set; get; }
        public int Id { set; get; }
        public int EditUserId { set; get; }
        public DateTime EditTime { set; get; }
        public int? ClassType { set; get; }
        public string KeyWord { set; get; }
    }

    public class ClassesUserModel 
    {
        public int ClassId { set; get; }
        public int UserId { set; get; }
    }

}
