using Atom.EF.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Entity.Mapper
{
    public class UserTecherExtMapper : BaseMap<UserTecherExt>
    {
        public override void Init()
        {
            ToTable("UserTecherExt");
            HasKey(m => m.Id);
        }
    }
}
