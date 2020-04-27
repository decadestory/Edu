using Atom.EF.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Entity.Mapper
{
    public class UserMapper : BaseMap<User>
    {
        public override void Init()
        {
            ToTable("User");
            HasKey(m => m.UserId);
            //HasRequired(m=>m.SSN);
            //Property(t => t.SSN).IsRequired();
        }
    }
}
