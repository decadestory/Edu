using Atom.Starter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;

namespace Atom.Starter
{
    public interface IAStarter
    {
        long AddOrEditTable(AtomDbTableModel model);
        Tuple<long, bool> AddOrEditDoc(AtomProjectDocModel model);
        List<AtomProjectDocModel> Docs(AtomProjectDocModel model);
        List<AtomDbTableModel> Tables(AtomDbTableModel model);
        List<AtomDbColumnModel> Columns(AtomDbColumnModel model);

    }

}
