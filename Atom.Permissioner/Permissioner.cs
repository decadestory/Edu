using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atom.Permissioner.Cacher;
using Atom.Permissioner.DataCore;
using Atom.Permissioner.Model;
using Orm.Son.Core;

namespace Atom.Permissioner
{
    public class Permissioner : IPermissioner
    {
        public Permissioner(string connStr)
        {
            SonFact.init(connStr);
            PermissionDataCore.InitTables();
        }

        public List<AtomPermissionMiniModel> GetPermission(int userId)
        {
            //var list = PermissionCacher.GetIfExist(userId);
            //if (list.Any()) return list;
            var result = PermissionDataCore.GetPermission(userId);
            PermissionCacher.AddCache(userId, result);
            return result;
        }

        public List<AtomPermissionModel> GetAppPermission(int userId)
        {
            var list = PermissionCacher.GetAppIfExist(userId);
            if (list.Any()) return list;
            var result = PermissionDataCore.GetAppPermission(userId);
            PermissionCacher.AddAppCache(userId, result);
            return result;
        }

        public List<AtomPermissionModel> GetPermission(string roleCode)
        {
            var list = PermissionCacher.GetIfExist(roleCode);
            if (list.Any()) return list;
            var result = PermissionDataCore.GetPermission(roleCode);
            PermissionCacher.AddCache(roleCode, result);
            return result;
        }

        public List<NPermissionModel> SearchPermission(NPermissionModel request)
        {
            return PermissionDataCore.SearchPermission(request);
        }

        public List<NRolePermissionModel> GetRolePermissions(string code)
        {
            return PermissionDataCore.GetRolePermissions(code);
        }

        public bool AddPermission(NPermissionModel model)
        {
            return PermissionDataCore.AddPermission(model);
        }

        public bool EditPermission(NPermissionModel model)
        {
            return PermissionDataCore.EditPermission(model);
        }

        public bool DelPermission(int pid)
        {
            return PermissionDataCore.DelPermission(pid);
        }

        public List<NWorkRoleModel> SearchWorkRole(NWorkRoleModel request)
        {
            return PermissionDataCore.SearchWorkRole(request);
        }

        public bool AddWorkRole(NWorkRoleModel model)
        {
            return PermissionDataCore.AddWorkRole(model);
        }

        public bool EditWorkRole(NWorkRoleModel model)
        {
            return PermissionDataCore.EditWorkRole(model);
        }

        public bool DelWorkRole(int pid)
        {
            return PermissionDataCore.DelWorkRole(pid);
        }

        public  bool SetRolePermissions(NPermissionModel request)
        {
            return PermissionDataCore.SetRolePermissions(request);
        }




    }
}
