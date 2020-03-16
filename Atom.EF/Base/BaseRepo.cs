using Atom.EF.Base.Interface;
using Atom.EF.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.EF.Base
{
    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
    {

    }
}
