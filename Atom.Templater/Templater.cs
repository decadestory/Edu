using Orm.Son.Core;
using System;

namespace Atom.Templater
{
    public class Templater
    {
        public Templater(string connStr)
        {
            SonFact.init(connStr);
            //PermissionDataCore.InitTables();
        }
    }
}
