using Atom.EF.Base.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Atom.EF.Base
{
    public class BaseEntity:IBaseEntity
    {
        public BaseEntity()
        {
            IsValid = true;
            AddTime = DateTime.Now;
            EditTime = DateTime.Now;
            AddUserId = 0;
            EditUserId = 0;
        }

        [Required]
        public DateTime AddTime { get; set; }
        [Required]
        public int AddUserId { get; set; }
        [Required]
        public DateTime EditTime { get; set; }
        [Required]
        public int EditUserId { get; set; }
        [Required]
        public bool IsValid { get; set; }
    }
}
