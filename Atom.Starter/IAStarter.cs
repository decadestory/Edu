using Atom.Starter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;

namespace Atom.Starter
{
    public interface IAStarter
    {
        Tuple<long, bool> AddOrEditDoc(AtomProjectDocModel model);
        List<AtomProjectDocModel> Docs(AtomProjectDocModel model);
        List<AtomDbTableModel> Tables(AtomDbTableModel model);
        List<AtomDbColumnModel> Columns(AtomDbColumnModel model);

        long AddTable(AtomDbTableModel model);
        long AddColumn(AtomDbColumnModel model);

        AtomSqlExeModel SqlQuery(string sql);
        AtomSqlExeModel SqlExecute(string sql);
        string ExportCsv(string sql);

        List<AtomSearchModel> ASearch(string key);

    }

}
