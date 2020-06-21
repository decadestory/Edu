using Atom.ConfigCenter.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter.Ui
{

    public class ConfigCenterController : ControllerBase
    {
        public IAtomConfigCenter config { get; set; }

        [HttpGet("Atom/Configer")]
        public async Task ConfigerView()
        {
            var html = config.Html();
            Response.ContentType = "text/html";
            var data = Encoding.UTF8.GetBytes(html);
            await Response.Body.WriteAsync(data, 0, data.Length);
        }

        [HttpPost("Atom/AddDict")]
        public Br<string> AddDict([FromBody] NAtomCateConfigModel request)
        {
            var result = config.AddDict(request, 0);
            return new Br<string> { Data = result + "" };
        }

        [HttpPost("Atom/EditDict")]
        public Br<string> EditDict([FromBody] NAtomCateConfigModel request)
        {
            var result = config.EditDict(request);
            return new Br<string> { Data = result + "" };
        }


        [HttpPost("Atom/GetDictsByParentCode")]
        public Br<List<AtomCateConfigModel>> GetDictsByParentCode(string dictCode, bool hasDisable = false)
        {
            var result = config.GetCates(dictCode, hasDisable);
            var reBr = new Br<List<AtomCateConfigModel>>(result);
            return reBr;
        }

    }
}
