using Atom.Logger.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Atom.Logger.DataCore
{
    internal class ALoggerDataCore
    {
        public bool CheckOrCreateDb()
        {
            var ta = SonFact.Cur.CreateTable<AtomOpreateLogger>();
            var tb = SonFact.Cur.CreateTable<AtomLogger>();
            var tbc = SonFact.Cur.CreateTable<AtomLoggerConfig>();
            
            return ta && tb && tbc;
        }

        public long Add(AtomLogger log)
        {
            try
            {
                var result = SonFact.Cur.Insert(log);
                return result;
            }
            catch (Exception e)
            {
                var errMsg = e.Message;
                var innerErr = e.InnerException != null ? e.InnerException.Message + " StackTrace: " + e.InnerException.StackTrace : "";
                SaveToFile("Error:" + errMsg + " InnerError:" + innerErr, 4);
                return 1;
            }
        }

        public AtomLoggerConfig GetLogConfig()
        {
            try
            {
                var result = SonFact.Cur.Top<AtomLoggerConfig>(t=>t.Id>0);
                return result;
            }
            catch (Exception e)
            {
                var errMsg = e.Message;
                var innerErr = e.InnerException != null ? e.InnerException.Message + " StackTrace: " + e.InnerException.StackTrace : "";
                SaveToFile("Error:" + errMsg + " InnerError:" + innerErr, 4);
                return null;
            }
        }



        public long Add(AtomOpreateLogger log)
        {
            try
            {
                var result = SonFact.Cur.Insert(log);
                return result;
            }
            catch (Exception e)
            {
                var errMsg = e.Message;
                var innerErr = e.InnerException != null ? e.InnerException.Message + " StackTrace: " + e.InnerException.StackTrace : "";
                SaveToFile("Error:" + errMsg + " InnerError:" + innerErr, 4);
                return 1;
            }
        }

        public List<AtomOpreateLoggerModel> GetBLogs(int relId)
        {
            var sql = $@"select top 500 * from AtomOpreateLogger where LogType in ('delete','import') and relid={relId} order by AddTime desc";
            var result = SonFact.Cur.ExecuteQuery<AtomOpreateLoggerModel>(sql);
            return result;
        }



        private bool Is2012SqlServer()
        {
            var sql = "SELECT  SERVERPROPERTY('productversion') as version";
            var result = SonFact.Cur.ExecuteQuery<string>(sql);

            var is2012 = result.First().CompareTo("11.0.3487.0");
            return is2012 > 0;
        }

        public  Tuple<List<AtomLoggerModel>, long> GetLogList(AtomLoggerModel request)
        {
            var where = new StringBuilder(" where 1=1 ");

            if (!string.IsNullOrWhiteSpace(request.LogType))
                where.AppendFormat(" and LogType='{0}'", request.LogType);

            if (request.LogLevel != 0)
                where.AppendFormat(" and LogLevel={0}",request.LogLevel);

            if (request.StartTime.HasValue)
                where.AppendFormat(" and AddTime>='{0}'", request.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));

            if (request.EndTime.HasValue)
                where.AppendFormat(" and AddTime<='{0}'", request.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));

            if (!string.IsNullOrWhiteSpace(request.LogUser))
                where.AppendFormat(" and LogUser='{0}'", request.LogUser);

            if (!string.IsNullOrWhiteSpace(request.LogUrl))
                where.AppendFormat(" and LogUrl like '%{0}%'", request.LogUrl);

            if (!string.IsNullOrWhiteSpace(request.LogTxt))
                where.AppendFormat(" and LogTxt like '%{0}%'", request.LogTxt);

            var sql = $@"   with cte as (select ROW_NUMBER() over (order by AddTime desc) NumId,* from  AtomLogger {where} )";
            var sqlCnt = $"{sql} select count(1) NumId from cte ";
            var sqlData = $"{sql}  select top {request.PageSize} * from cte where NumId>{(request.PageIndex-1)*request.PageSize}";
            var cnt = SonFact.Cur.ExecuteQuery<AtomLoggerModel>(sqlCnt).First();
            var data = SonFact.Cur.ExecuteQuery<AtomLoggerModel>(sqlData);
            return new Tuple<List<AtomLoggerModel>, long>(data,cnt.NumId);
        }

        public void SaveToFile(string content, int level)
        {
            try
            {
                var path = GetFilePath(level);
                var method = new StackTrace(true).GetFrame(2).GetMethod();
                var declaringType = method.DeclaringType;
                var nspace = declaringType != null ? declaringType.Namespace : string.Empty;
                var md = "<" + nspace + "." + method.Name + "> ";

                using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 1024, false))
                {
                    var time = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" + "]");
                    var bty = Encoding.UTF8.GetBytes(time + md + content + "\r\n");
                    fs.Write(bty, 0, bty.Length);
                    fs.Close();
                }
            }
            catch
            {

            }
        }

        private static string GetFilePath(int level)
        {
            var logPath = AppDomain.CurrentDomain.BaseDirectory + "/Logs/";
            switch (level)
            {
                case 1:
                    logPath += "Info/"; break;
                case 2:
                    logPath += "Warn/"; break;
                case 3:
                    logPath += "Error/"; break;
                case 4:
                    logPath += "Fatal/"; break;
                case 5:
                    logPath += "Monitor/"; break;
                case 6:
                    logPath += "Exception/"; break;
            }
            if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
            logPath += DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            return logPath;
        }

        public enum LogLevel
        {
            Info = 1,
            Warn = 2,
            Error = 3,
            Fatal = 4,
            Monitor = 5,
            Exception = 6,
            Debug = 7,
        }



    }
}
