using System;
using System.Collections.Generic;
using Orm.Son.Mapper;
using System.Text;

namespace Atom.Starter.Model
{
    internal class AtomDbTable
    {
        [Description("表Id")]
        public int Id { get; set; }

        [Description("表名称")]
        public string Name { get; set; }

        [Description("数据库表名")]
        public string DbTableName { get; set; }

        [Description("项目Id")]
        public int ProjectId { get; set; }

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
