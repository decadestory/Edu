using Atom.Starter.DataCore;
using Atom.Starter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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

        public long AddTable(AtomDbTableModel model)
        {
            return rep.AddTable(model);
        }

        public long AddColumn(AtomDbColumnModel model)
        {
            return rep.AddColumn(model);
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

        public AtomSqlExeModel SqlQuery(string sql)
        {
            return rep.SqlQuery(sql);
        }

        public AtomSqlExeModel SqlExecute(string sql)
        {
            return rep.SqlExecute(sql);
        }

        public string ExportCsv(string sql)
        {
            var res =  rep.SqlQuery(sql);
            var content = new StringBuilder();
            var rstr = new StringBuilder();

            var header = string.Join(",",res.ColumnNames);
            content.AppendLine(header);

            foreach(DataRow r in res.ResultTable.Rows)
            {
                rstr.Clear();
                for (var i = 0; i < res.ResultTable.Columns.Count; i++)
                {
                    var curValue = (r[i] + "").Replace(",", "，");
                    rstr.Append(curValue + ",");
                }
                content.AppendLine(rstr.ToString());
            }

            return content.ToString();
        }

        public List<AtomSearchModel> ASearch(string key)
        {
            return rep.ASearch(key);
        }

    }
}
