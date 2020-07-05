using Atom.Starter.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Starter.Ui
{
    public class AtomStarterController:ControllerBase
    {
        public IAStarter starter { get; set; }

        [HttpPost("Atom/AddOrEditDoc")]
        public Br<long> AddOrEditDoc([FromBody] AtomProjectDocModel model)
        {
            var res = starter.AddOrEditDoc(model);
            return new Br<long>(res.Item1,extData:res.Item2);
        }

        [HttpPost("Atom/Docs")]
        public Br<List<AtomProjectDocModel>> Docs([FromBody] AtomProjectDocModel model)
        {
            var res = starter.Docs(model);
            return new Br<List<AtomProjectDocModel>>(res);
        }

        [HttpPost("Atom/Tables")]
        public Br<List<AtomDbTableModel>> Tables([FromBody] AtomDbTableModel model)
        {
            var res = starter.Tables(model);
            return new Br<List<AtomDbTableModel>>(res);
        }

        [HttpPost("Atom/Columns")]
        public Br<List<AtomDbColumnModel>> Columns([FromBody] AtomDbColumnModel model)
        {
            var res = starter.Columns(model);
            return new Br<List<AtomDbColumnModel>>(res);
        }

        [HttpPost]
        [Route("Atom/AddTable")]
        public Br<long> AddTable([FromBody] AtomDbTableModel model)
        {
            var res = starter.AddTable(model);
            return new Br<long>(res);
        }

        [HttpPost]
        [Route("Atom/AddColumn")]
        public Br<long> AddColumn([FromBody] AtomDbColumnModel model)
        {
            var res = starter.AddColumn(model);
            return new Br<long>(res);
        }

        [HttpPost]
        [Route("Atom/SqlQuery")]
        public Br<AtomSqlExeModel> SqlQuery([FromBody] AtomSqlExeModel model)
        {
            var res = starter.SqlQuery(model.Sql);
            return new Br<AtomSqlExeModel>(res);
        }

        [HttpPost]
        [Route("Atom/SqlExecute")]
        public Br<AtomSqlExeModel> SqlExecute([FromBody] AtomSqlExeModel model)
        {
            var res = starter.SqlExecute(model.Sql);
            return new Br<AtomSqlExeModel>(res);
        }

        [HttpGet]
        [Route("Atom/ExportCsv")]
        public FileStreamResult ExportCsv([FromQuery] string sql)
        {
            var data = starter.ExportCsv(sql);
            
            var stream = new MemoryStream(Encoding.GetEncoding("GB2312").GetBytes(data));
            stream.Position = 0;
            var result = new FileStreamResult(stream, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("text/csv"));
            result.FileDownloadName = DateTime.Now.ToLongTimeString() + "_sql.csv";
            return result;
        }

        [HttpPost]
        [Route("Atom/ASearch")]
        public Br<List<AtomSearchModel>> ASearch([FromQuery] string key)
        {
            var res = starter.ASearch(key);
            return new Br<List<AtomSearchModel>>(res);
        }

    }
}
