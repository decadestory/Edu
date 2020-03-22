using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter.Model
{
    public class AtomConfigValueModel
    {
        public int ConfigValueId { get; set; }

        public int RelId { get; set; }

        public string CateCode { get; set; }

        public string CateValue { get; set; }

        public string ValueType { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string ExtValue { get; set; }

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
    }
}
