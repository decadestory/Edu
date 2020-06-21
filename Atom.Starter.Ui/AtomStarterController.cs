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
        [Route("Atom/AddOrEditTable")]
        public Br<long> AddOrEditTable([FromBody] AtomDbTableModel model)
        {
            var res = starter.AddOrEditTable(model);
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

    }
}
