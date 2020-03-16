using System;
using System.ComponentModel.DataAnnotations;

namespace Edu.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage ="手机号不能为空")]
        [StringLength(11, ErrorMessage = "MobilePhone太长了")]
        public string MobilePhone { get; set; }
    }
}
