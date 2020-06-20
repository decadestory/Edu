using Atom.Starter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;

namespace Atom.Starter
{
    public interface IAStarter
    {
        long AddOrEditProject(AtomProjectModel model);
    }
}
