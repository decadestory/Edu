using Atom.Starter.DataCore;
using Atom.Starter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;

namespace Atom.Starter
{
    public class AStarter : IAStarter
    {
        internal AStarterCore rep = new AStarterCore();
        public AStarter(string dbConnStr,string projectName="Atom项目", string projectDesc= "Atom项目描述")
        {
            SonFact.init(dbConnStr);
            rep.CheckOrCreateDb(projectName,projectDesc);
        }

        public long AddOrEditTable(AtomDbTableModel model)
        {
            return rep.AddOrEditTable(model);
        }

        public Tuple<long, bool> AddOrEditDoc(AtomProjectDocModel model)
        {
            return rep.AddOrEditDoc(model);
        }

        public List<AtomProjectDocModel> Docs(AtomProjectDocModel model)
        {
            return rep.Docs(model);
        }

        public List<AtomDbTableModel> Tables(AtomDbTableModel model)
        {
            return rep.Tables(model);
        }

        public List<AtomDbColumnModel> Columns(AtomDbColumnModel model)
        {
            return rep.Columns(model);
        }

    }
}
