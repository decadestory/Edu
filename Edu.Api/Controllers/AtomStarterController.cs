using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.Lib;
using Atom.Starter;
using Edu.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edu.Api.Controllers
{
    public class AtomStarterController : BaseController
    {
        public IAStarter starter { get; set; }

        [HttpPost, Auth]
        public Br<bool> AddProject()
        {
            return new Br<bool>(true);
        }

    }
}
