using Atom.EF.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edu.Entity
{
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "UserName太长了")]
        public string UserName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "LoginId太长了")]
        public string LoginId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Password太长了")]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

        [StringLength(11, ErrorMessage = "MobilePhone太长了")]
        public string MobilePhone { get; set; }
    }
}
