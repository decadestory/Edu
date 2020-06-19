using Atom.Starter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Atom.Starter.DataCore
{
    internal class AStarterCore
    {
        public bool CheckOrCreateDb()
        {
            SonFact.Cur.CreateTable<AtomProject>();
            SonFact.Cur.CreateTable<AtomProjectDoc>();
            SonFact.Cur.CreateTable<AtomDbTable>();
            SonFact.Cur.CreateTable<AtomDbColumn>();
            SonFact.Cur.CreateTable<AtomStarterLog>();
            return true;
        }

        public bool AddProject(AtomProjectModel model)
        {

        }


        public enum LogLevel
        {
            Info = 1,
            Warn = 2,
            Error = 3,
            Fatal = 4,
            Monitor = 5,
            Exception = 6,
            Debug = 7,
        }



    }
}
