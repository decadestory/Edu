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
        public int? Level { get; set; }
        public string Value { get; set; }
        public string ScdValue { get; set; }
        public string ThdValue { get; set; }
        public int Sort { get; set; }
        public int DomainId { get; set; }
        public DateTime? AddTime { get; set; }
        public int AddUserId { get; set; }
        public DateTime? EditTime { get; set; }
        public int EditUserId { get; set; }
        public bool IsValid { get; set; }

        public List<AtomCateConfigModel>  Children { get; set; }
        public List<AtomConfigValueModel>  Values { get; set; }

    }
}
