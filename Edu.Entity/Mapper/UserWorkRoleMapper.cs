using Atom.EF.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Entity.Mapper
{
    public class UserWorkRoleMapper : BaseMap<UserWorkRole>
    {
        public override void Init()
        {
            ToTable("UserWorkRole");
            HasKey(m => m.Id);
        }
    }
}
