using Atom.Starter.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Atom.Starter.Ui
{
    public class AtomStarterController:ControllerBase
    {
        public IAStarter starter { get; set; }

        [HttpPost]
        [Route("Atom/AddTable")]
        public Br<long> AddTable([FromBody] AtomDbTableModel model)
        {
            var res = starter.AddTable(model);
            return new Br<long>(res);
        }

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

    }
}
