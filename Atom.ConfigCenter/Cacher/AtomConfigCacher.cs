using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Atom.ConfigCenter.Cacher
{
    internal class AtomConfigCacher
    {
        static IMemoryCache cache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        public static string GetIfExist(string code)
        {
           
            var key = "acc[" + code + "]";
            if (cache.Get(key) == null) return string.Empty;
            return cache.Get(key) as string;
        }

        public static void AddCache(string code,string value)
        {
            var key = "acc[" + code + "]";
            var timeSpan = new TimeSpan(0, 1, 0);
            cache.Set<string>(key, value,  timeSpan);
        }

    }
}
