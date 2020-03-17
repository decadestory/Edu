using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Logger.Model
{
    internal class AtomLoggerConfig
    {
        [Description("日志配置ID")]
        public int Id { get; set; }

        [Description("是否开启调试日志")]
        public bool IsDebug  { get; set; }

        [Description("是否开启日志报警")]
        public bool IsAlert { get; set; }

        [Description("日志报警间隔(分钟)")]
        public int AlertInterval { get; set; }

    }
}
