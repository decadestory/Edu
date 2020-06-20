using System;
using System.Collections.Generic;
using Orm.Son.Mapper;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Atom.Starter.Model
{
    public class AtomProjectModel
    {
        [Description("项目Id")]
        public int Id { get; set; }

        [Description("项目名称")]
        [Required(ErrorMessage = "项目名称不能为空")]
        public string Name { get; set; }

        [Description("项目描述")]
        [Required(ErrorMessage = "项目描述不能为空")]
        public string Desc { get; set; }

        [Description("项目图标")]
        public string Icon { get; set; }

        [Description("数据库名称")]
        [Required(ErrorMessage = "数据库名称不能为空")]
        public string DbName { get; set; }

        [Description("数据库连接")]
        [Required(ErrorMessage = "数据库连接不能为空")]
        public string DbConnStr { get; set; }

        [Description("添加时间")]
        public DateTime AddTime { get; set; }

        [Description("添加人")]
        public int AddUserId { get; set; }

        [Description("修改时间")]
        public DateTime EditTime { get; set; }

        [Description("修改人")]
        public int EditUserId { get; set; }

        [Description("是否可用")]
        public bool? IsValid { get; set; }
    }
}
