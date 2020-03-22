using Atom.Permissioner.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Atom.Permissioner.Cacher
{
    internal class PermissionCacher
    {
        static IMemoryCache cache = new MemoryCache(Options.Create(new MemoryCacheOptions()));

        public static List<AtomPermissionMiniModel> GetIfExist(int userId)
        {
            var key = "core_permission_key_uid_" + userId;
            if (cache.Get(key) == null) return new List<AtomPermissionMiniModel>();
            return cache.Get(key) as List<AtomPermissionMiniModel>;
        }

        public static void AddCache(int userId, List<AtomPermissionMiniModel> cps)
        {
            var key = "core_permission_key_uid_" + userId;
            var timeSpan = new TimeSpan(0, 5, 0);
            cache.Set(key, cps, timeSpan);
        }



        public static List<AtomPermissionModel> GetAppIfExist(int userId)
        {
            var key = "app_permission_key_uid_" + userId;
            if (cache.Get(key) == null) return new List<AtomPermissionModel>();
            return cache.Get(key) as List<AtomPermissionModel>;
        }

        public static void AddAppCache(int userId, List<AtomPermissionModel> cps)
        {
            var key = "app_permission_key_uid_" + userId;
            var timeSpan = new TimeSpan(0, 5, 0);
            cache.Set(key, cps,timeSpan);
        }



        public static List<AtomPermissionModel> GetIfExist(string roleCode)
        {
            var key = "core_permission_key_rcode_" + roleCode;
            if (cache.Get(key) == null) return new List<AtomPermissionModel>();
            return cache.Get(key) as List<AtomPermissionModel>;
        }

        public static void AddCache(string roleCode, List<AtomPermissionModel> cps)
        {
            var key = "core_permission_key_rcode_" + roleCode;
            var timeSpan = new TimeSpan(0, 5, 0);
            cache.Set(key, cps,timeSpan);
        }
    }
}
