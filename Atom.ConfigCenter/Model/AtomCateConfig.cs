using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter.Model
{
    internal class AtomCateConfig
    {
        [Description("分类配置ID")]
        [Key]
        public int ConfigCateId { get; set; }

        [Description("分类名称")]
        public string CateName { get; set; }

        [Description("分类编码")]
        public string CateCode { get; set; }

        [Description("上级分类编码")]
        public string ParentCateCode { get; set; }

        [Description("扩展分类编码")]
        public string ExtCateCode { get; set; }

        [Description("主配置编码")]
        public string MainConfigCode { get; set; }

        [Description("是否有值")]
        public bool HasValue { get; set; }

        [Description("层级")]
        public int? Level { get; set; }

        [Description("值")]
        public string Value { get; set; }

        [Description("值二")]
        public string ScdValue { get; set; }

        [Description("值三")]
        public string ThdValue { get; set; }

        [Description("排序")]
        public int Sort { get; set; }

        [Description("平台Id")]
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

    }
}
