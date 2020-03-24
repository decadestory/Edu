using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edu.Entity
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? BirthDay { set; get; }
        public string HeadImg { set; get; }
        public bool? Gender { set; get; }
    }
}
