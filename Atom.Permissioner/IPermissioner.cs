using Atom.Permissioner.Model;
using System;
using System.Collections.Generic;

namespace Atom.Permissioner
{
    public interface IPermissioner
    {
        List<AtomPermissionMiniModel> GetPermission(int userId);
        List<AtomPermissionModel> GetAppPermission(int userId);
        List<AtomPermissionModel> GetPermission(string roleCode);
        List<NPermissionModel> SearchPermission(NPermissionModel request);
        List<NRolePermissionModel> GetRolePermissions(string code);
        bool SetRolePermissions(NPermissionModel request);
        bool AddPermission(NPermissionModel model);
        bool EditPermission(NPermissionModel model);
        bool DelPermission(int pid);

        List<NWorkRoleModel> SearchWorkRole(NWorkRoleModel request);
        bool AddWorkRole(NWorkRoleModel model);
        bool EditWorkRole(NWorkRoleModel model);
        bool DelWorkRole(int pid);


    }
}
