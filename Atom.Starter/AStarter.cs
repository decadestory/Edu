using Atom.Starter.DataCore;
using Orm.Son.Core;
using System;
using System.Collections.Generic;

namespace Atom.Starter
{
    public class AStarter : IAStarter
    {
        internal AStarterCore rep = new AStarterCore();
        internal string ServerSrc = null;
        public AStarter(string dbConnStr)
        {
            SonFact.init(dbConnStr);
            rep.CheckOrCreateDb();
        }

    }
}
