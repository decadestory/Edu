using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.ConfigCenter;
using Atom.ConfigCenter.Model;
using Atom.Lib;
using Atom.Permissioner;
using Atom.Permissioner.Model;
using Edu.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edu.Api.Controllers
{
    public class AtomController : BaseController
    {
        public IAtomConfigCenter config{ get; set; }
        public IPermissioner per { get; set; }

        [HttpPost, Auth]
        public Br<bool> AddAtomConfig(AtomConfigModel model)
        {
            var result = config.Set(model, true);
            return new Br<bool>(result > 0);
        }

        [HttpPost, Auth]
        public Br<bool> EditAtomConfig(AtomConfigModel model)
        {
            var result = config.Set(model, false);
            return new Br<bool>(result > 0);
        }

        [HttpPost, Auth]
        public Br<int> DelAtomConfig(int configId)
        {
            var result = config.Del(configId);
            return new Br<int>(result);
        }

        [HttpPost, Auth]
        public Br<List<NAtomConfigModel>> SearchAtomConfig(NAtomConfigModel request)
        {
            var result = config.SearchAtomConfig(request);
            return new Br<List<NAtomConfigModel>> { Data = result.Item1, ExtData = result.Item2 };
        }


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


        [HttpPost, Auth]
        public Br<List<NAtomCateConfigModel>> SearchDict(NAtomCateConfigModel request)
        {
            var result = config.SearchDict(request);
            return new Br<List<NAtomCateConfigModel>> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> AddDict(NAtomCateConfigModel request)
        {
            var result = config.AddDict(request,0);
            return new Br<bool> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> EditDict(NAtomCateConfigModel request)  
        {
            var result = config.EditDict(request);
            return new Br<bool> { Data = result };
        }

        [HttpPost, Auth]
        public Br<bool> DelDict(int cid)
        {
            var result = config.DelDict(cid);
            return new Br<bool> { Data = result };
        }

        [HttpPost, Auth]
        public Br<List<AtomCateConfigModel>> GetDictsByParentCode(string dictCode)
        {
            var result =config.GetCates(dictCode);
            return new Br<List<AtomCateConfigModel>>(result);
        }


    }
}