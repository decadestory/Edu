using System;
using System.ComponentModel.DataAnnotations;

namespace Edu.Model
{
    public class AuthModel
    {
        [Required(ErrorMessage ="账号不能为空")]
        public string Account { get; set; }
        [Required(ErrorMessage ="密码不能为空")]
        public string Password { get; set; }
    }
}
