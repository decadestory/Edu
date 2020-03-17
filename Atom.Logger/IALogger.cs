using Atom.Logger.DataCore;
using Atom.Logger.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using static Atom.Logger.DataCore.ALoggerDataCore;

namespace Atom.Logger
{
    public interface IALogger
    {
          bool BInfo(string txt, string logType = "std", string logTypeName = "", int relId = 0, string user = null);

          List<AtomOpreateLoggerModel> GetBLogs(int relId);

          bool Debug(string txt, string logType = "std", string user = null);

          bool Info(string txt, string logType = "std", string user = null);

         bool Warn(string txt, string logType = "std", string user = null);

          bool Error(string txt, string logType = "std", string user = null);

          bool Fatal(string txt, string logType = "std", string user = null);

          bool Monitor(string logUrl = "", string user = null, long? contentLength = 0, long duration = 0, string logType = "monitor", string param = "");

          bool Exception(Exception ex, string logType = "exception", string user = null, string txt = "");

          void LogToFile(string txt, int level = 1);

          Tuple<List<AtomLoggerModel>, long> GetLogList(AtomLoggerModel request);

        }
}
