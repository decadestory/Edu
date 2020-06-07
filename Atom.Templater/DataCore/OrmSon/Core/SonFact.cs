using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orm.Son.Core
{
    public static class SonFact
    {
        public static string connString = "conn";
        public static void init(string connStr)
        {
            connString = connStr;
        }

        public static SonConnection Cur
        {
            get
            {
                var cache = CallContext.GetData("SonConnectionCache") as SonConnection;
                if (cache != null) return cache;
                var sonConn = new SonConnection(connString);
                CallContext.SetData("SonConnectionCache", sonConn);
                return sonConn;
            }
        }

    }


    public static class CallContext
    {
        static ConcurrentDictionary<string, AsyncLocal<object>> state = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public static void SetData(string name, object data) =>
            state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

        public static object GetData(string name) =>
            state.TryGetValue(name, out AsyncLocal<object> data) ? data.Value : null;
    }


}
