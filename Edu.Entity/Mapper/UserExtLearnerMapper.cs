using Atom.EF.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Entity.Mapper
{
    public class UserExtLearnerMapper : BaseMap<UserExtLearner>
    {
        public override void Init()
        {
            ToTable("UserExtLearner");
            HasKey(m => m.Id);
        }
    }
}
