using Orm.Son.Mapper;
using System;

namespace Atom.Logger.Model
{
    public class AtomLoggerModel
    {
        public long NumId { get; set; }
        public int Id { get; set; }
        public string LogType { get; set; }
        public int LogLevel { get; set; }
        public string LogUser { get; set; }
        public string LogUrl { get; set; }
        public string LogTxt { get; set; }
        public long? ContentLength { get; set; }
        public long Duration { get; set; }
        public DateTime AddTime { get; set; }
        public string ServerSrc { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public DateTime?  StartTime { get; set; }
        public DateTime?  EndTime { get; set; }

    }
}
