using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Atom.ConfigCenter;
using Atom.ConfigCenter.Model;
using Atom.Lib;
using Atom.Logger;
using Atom.Logger.Model;
using Atom.Permissioner;
using Atom.Permissioner.Model;
using Edu.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edu.Api.Controllers
{
    public class AtomController : BaseController
    {
        public IAtomConfigCenter config { get; set; }
        public IPermissioner per { get; set; }
        public IALogger logger { get; set; }

        [HttpPost, Auth]
        public Br<NPermissionModel> SearchPermission(NPermissionModel request)
        {
            var result = new NPermissionModel();
            var res = per.SearchPermission(request);
            result.Children = res;

            if (request.IsSet == true)
            {
                var resultSelected = per.GetRolePermissions(request.UserRoleCode);
                result.Selected = resultSelected;
            }
            return new Br<NPermissionModel> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> AddPermission(NPermissionModel request)
        {
            var result = per.AddPermission(request);
            return new Br<bool> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> EditPermission(NPermissionModel request)
        {
            var result = per.EditPermission(request);
            return new Br<bool> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> DelPermission(int pid)
        {
            var result = per.DelPermission(pid);
            return new Br<bool> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> SetRolePermissions(NPermissionModel request)
        {
            var result = per.SetRolePermissions(request);
            return new Br<bool>(result);
        }


        [HttpPost, Auth]
        public Br<List<NWorkRoleModel>> SearchWorkRole(NWorkRoleModel request)
        {
            var result = per.SearchWorkRole(request);
            return new Br<List<NWorkRoleModel>> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> AddWorkRole(NWorkRoleModel request)
        {
            var result = per.AddWorkRole(request);
            return new Br<bool> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> EditWorkRole(NWorkRoleModel request)
        {
            var result = per.EditWorkRole(request);
            return new Br<bool> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> DelWorkRole(int pid)
        {
            var result = per.DelWorkRole(pid);
            return new Br<bool> { Data = result };
        }

    }
}