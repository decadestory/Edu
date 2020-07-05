using System;
using System.Collections.Generic;
using Orm.Son.Mapper;
using System.Text;

namespace Atom.Starter.Model
{
    public class AtomDbColumnModel
    {
        [Description("列Id")]
        public int Id { get; set; }

        [Description("表Id")]
        public int DbTableId { get; set; }

        [Description("列名称")]
        public string Name { get; set; }

        [Description("数据库列名")]
        public string DbColumnName { get; set; }

        [Description("数据类型")]
        public string DbType { get; set; }

        [Description("允许NULL")]
        public bool IsNull { get; set; }

        [Description("是否主键")]
        public bool IsPrimary { get; set; }

        [Description("是否自增")]
        public bool IsIdentity { get; set; }

        [Description("列描述")]
        public string Desc { get; set; }

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
