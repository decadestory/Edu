using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.Lib;
using Atom.Starter;
using Atom.Starter.Model;
using Edu.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edu.Api.Controllers
{
    public class AtomStarterController : BaseController
    {
        public IAStarter starter { get; set; }

        [HttpPost, CheckParams]
        public Br<long> AddOrEditProject(AtomProjectModel model)
        {
            var res = starter.AddOrEditProject(model);
            return new Br<long>(res);
        }

    }
}
