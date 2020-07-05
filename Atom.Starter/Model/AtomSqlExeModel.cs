using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Atom.Starter.Model
{
    public class AtomSqlExeModel
    {
        public string Sql { get; set; }
        public int RowCnt { get; set; }
        public DataTable ResultTable { get; set; }
        public List<string> ColumnNames{ get; set; }
    }
}
