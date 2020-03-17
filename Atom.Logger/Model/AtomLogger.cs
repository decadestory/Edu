using Orm.Son.Mapper;
using System;

namespace Atom.Logger.Model
{
    internal class AtomLogger
    {
        [Description("日志ID")]
        public int Id { get; set; }
        [Description("日志类型")]
        public string LogType { get; set; }
        [Description("日志级别")]
        public int LogLevel { get; set; }
        [Description("操作人")]
        public string LogUser { get; set; }
        [Description("调用地址")]
        public string LogUrl { get; set; }
        [Description("日志内容")]
        public string LogTxt { get; set; }
        [Description("请求大小")]
        public long? ContentLength { get; set; }
        [Description("执行时间(ms)")]
        public long Duration { get; set; }
        [Description("添加时间")]
        public DateTime AddTime { get; set; }
        [Description("源服务器")]
        public string ServerSrc { get; set; }
    }
}
