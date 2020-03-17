using Orm.Son.Mapper;
using System;

namespace Atom.Logger.Model
{
    internal class AtomOpreateLogger
    {
        [Description("日志ID")]
        public int Id { get; set; }
        [Description("日志类型")]
        public string LogType { get; set; }
        public string LogTypeName { get; set; }
        public int? RelId { get; set; }
        [Description("操作人")]
        public string LogUser { get; set; }
        [Description("日志内容")]
        public string LogTxt { get; set; }
        [Description("添加时间")]
        public DateTime AddTime { get; set; }
    }
}
