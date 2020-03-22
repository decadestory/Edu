using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter.Model
{
    internal class AtomConfig
    {
        [Description("全局配置ID")]
        [Key]
        public int ConfigId { get; set; }

        [Description("配置类型")]
        public string ConfigType { get; set; }

        [Description("配置编码")]
        public string ConfigCode { get; set; }

        [Description("配置值")]
        public string ConfigValue { get; set; }

        [Description("配置说明")]
        public string ConfigDesc { get; set; }

        [Description("配置上级编码")]
        public string ParentCode { get; set; }

        [Description("平台Id")]
        public string DomainId { get; set; }

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
