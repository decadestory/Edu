using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter.Model
{
    internal class AtomConfigValue
    {
        [Description("分类配置值ID")]
        [Key]
        public int ConfigValueId { get; set; }

        [Description("关系Id")]
        public int RelId { get; set; }

        [Description("分类编码")]
        public string CateCode { get; set; }

        [Description("分类配置值")]
        public string CateValue { get; set; }

        [Description("分类配置值")]
        public string ValueType { get; set; }

        [Description("生效开始时间")]
        public DateTime? StartTime { get; set; }

        [Description("生效结束时间")]
        public DateTime? EndTime { get; set; }

        [Description("扩展值")]
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
