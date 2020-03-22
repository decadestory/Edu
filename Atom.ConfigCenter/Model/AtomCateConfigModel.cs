using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter.Model
{
    public class AtomCateConfigModel
    {
        public int ConfigCateId { get; set; }

        public string CateName { get; set; }

        public string CateCode { get; set; }

        public string ParentCateCode { get; set; }

        public string ExtCateCode { get; set; }

        public string MainConfigCode { get; set; }

        public bool HasValue { get; set; }

        [Description("层级")]
        public int? Level { get; set; }

        [Description("值")]
        public string Value { get; set; }

        [Description("值二")]
        public string ScdValue { get; set; }

        [Description("值三")]
        public string ThdValue { get; set; }

        public int Sort { get; set; }

        public int DomainId { get; set; }


        [Description("添加时间")]
        public DateTime? AddTime { get; set; }
        [Description("添加人")]
        public int AddUserId { get; set; }
        [Description("修改时间")]
        public DateTime? EditTime { get; set; }
        [Description("修改人")]
        public int EditUserId { get; set; }
        [Description("是否可用")]
        public bool IsValid { get; set; }

        public List<AtomCateConfigModel>  Children { get; set; }
        public List<AtomConfigValueModel>  Values { get; set; }

    }
}
