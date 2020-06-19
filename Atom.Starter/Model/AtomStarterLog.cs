using System;
using System.Collections.Generic;
using Orm.Son.Mapper;
using System.Text;

namespace Atom.Starter.Model
{
    internal class AtomStarterLog
    {
        [Description("日志Id")]
        public int Id { get; set; }

        [Description("项目Id")]
        public int ProjectId { get; set; }

        [Description("日志类型")]
        public int LogType { get; set; }

        [Description("列名称")]
        public string Name { get; set; }

        [Description("添加时间")]
        public DateTime AddTime { get; set; }

        [Description("添加人")]
        public int AddUserId { get; set; }

        [Description("修改时间")]
        public DateTime EditTime { get; set; }

        [Description("修改人")]
        public int EditUserId { get; set; }

        [Description("是否可用")]
        public bool IsValid { get; set; }
    }
}
