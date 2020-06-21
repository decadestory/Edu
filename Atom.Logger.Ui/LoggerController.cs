using Atom.Logger.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Logger.Ui
{

    public class LoggerController : ControllerBase
    {
        public IALogger logger { get; set; }

        [HttpGet]
        [Route("Atom/Logger")]
        public async Task Logger()
        {
            var html = logger.Html();
            Response.ContentType = "text/html";
            var data = Encoding.UTF8.GetBytes(html);
            await Response.Body.WriteAsync(data, 0, data.Length);
        }

        [HttpPost]
        [Route("Atom/Logs")]
        public Br<List<AtomLoggerModel>> Logs([FromBody]AtomLoggerModel model)
        {
            var result = logger.GetLogList(model);
            return new Br<List<AtomLoggerModel>>(result.Item1, extData: result.Item2);
        }

    }
}
