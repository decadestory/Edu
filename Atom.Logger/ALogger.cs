using Atom.Logger.DataCore;
using Atom.Logger.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using static Atom.Logger.DataCore.ALoggerDataCore;

namespace Atom.Logger
{
    public class ALogger : IALogger
    {
        internal  ALoggerDataCore Logmgt = new ALoggerDataCore();
        internal  string ServerSrc = null;
        public   ALogger(string dbConnStr,string serverSrc=null)
        {
            SonFact.init(dbConnStr);
            ServerSrc = serverSrc;
            Logmgt.CheckOrCreateDb();
        }

        public  bool BInfo(string txt, string logType = "std", string logTypeName="", int relId=0, string user = null)
        {
            var log = new AtomOpreateLogger()
            {
                LogType = logType,
                LogTypeName = logTypeName,
                RelId = relId,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
            };

            return Logmgt.Add(log) > 0;
        }

        public  List<AtomOpreateLoggerModel> GetBLogs(int relId)
        {
            return Logmgt.GetBLogs(relId);
        }


            public  bool Debug(string txt, string logType = "std", string user = null)
        {
            var conf = Logmgt.GetLogConfig();
            if (conf==null || conf.IsDebug != true) return true;
           
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Debug,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public  bool Info(string txt, string logType = "std", string user = null)
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Info,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration =0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public  bool Warn(string txt, string logType = "std", string user = null)
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Warn,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public  bool Error(string txt, string logType = "std", string user = null)
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Error,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public  bool Fatal(string txt, string logType = "std", string user = null)
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Fatal,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public  bool Monitor(string logUrl = "", string user = null, long? contentLength=0 , long duration = 0, string logType = "monitor", string param = "")
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Monitor,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = param,
                LogUrl = logUrl,
                ContentLength = contentLength,
                Duration = duration,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public  bool Exception(Exception ex, string logType = "exception", string user = null, string txt = "")
        {
            var innerEx = ex.InnerException != null ? ex.InnerException.Message : "";

            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Exception,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = ex.Message+" | "+ innerEx,
                LogUrl = ex.StackTrace,
                ContentLength = 0,
                Duration = 0,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public  void LogToFile(string txt , int level=1)
        {
            Logmgt.SaveToFile(txt,level);
        }

        public  Tuple<List<AtomLoggerModel>, long> GetLogList(AtomLoggerModel request)
        {
            var result = Logmgt.GetLogList(request);
            return result;
        }

        }
}
