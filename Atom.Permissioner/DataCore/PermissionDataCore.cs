using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atom.Permissioner.Model;
using Orm.Son.Core;
using Orm.Son.Mapper;

namespace Atom.Permissioner.DataCore
{
    internal class PermissionDataCore
    {

        public static void InitTables()
        {
            SonFact.Cur.CreateTable<AtomPermission>();
            SonFact.Cur.CreateTable<WorkRole>();
            SonFact.Cur.CreateTable<AtomRolePermission>();
            SonFact.Cur.CreateTable<UserWorkRole>();
        }

        public static List<AtomPermissionMiniModel> GetPermission(int userId)
        {
            var sql = string.Format(@"select cp.* from AtomPermission cp (nolock)
                                    inner join AtomRolePermission crp (nolock) on cp.Code= crp.PermissionCode
                                    inner join WorkRole cr (nolock) on crp.RoleCode = cr.Code
                                    inner join UserWorkRole cur (nolock) on cur.RoleCode = cr.Code
                                    where cp.IsValid=1 and crp.IsValid=1 and cr.IsValid=1 and cur.IsValid=1 and cp.Ptype not in (100,110) and cur.UserId = {0} order by cp.Sort desc", userId);
            var result = SonFact.Cur.ExecuteQuery<AtomPermissionMiniModel>(sql);

            result.ForEach(t =>
            {
                if (t.ParentCode != null || t.Ptype != 10) return;
                var isExistDefault = result.FirstOrDefault(m => m.VUrl == t.DefaultUrl && m.Ptype == 10);
                if (isExistDefault == null || string.IsNullOrWhiteSpace(t.DefaultUrl))
                {
                    var subms = result.Where(m=>m.ParentCode==t.Code && m.Ptype==10).Select(m=>m.Code).ToList();
                    var leafirst = result.FirstOrDefault(m=> subms.Contains(m.ParentCode) && m.Ptype == 10);
                    if (leafirst != null) t.DefaultUrl = leafirst.VUrl;
                }
            });

            return result;
        }


        public static List<AtomPermissionModel> GetAppPermission(int userId)
        {
            var sql = string.Format(@"select cp.*,cp.Id ServerId from AtomPermission cp (nolock)
                                    inner join AtomRolePermission crp (nolock) on cp.Code= crp.PermissionCode
                                    inner join WorkRole cr (nolock) on crp.RoleCode = cr.Code
                                    inner join UserWorkRole cur (nolock) on cur.RoleCode = cr.Code
                                    where cp.IsValid=1 and crp.IsValid=1 and cr.IsValid=1 and cur.IsValid=1 and cp.Ptype in (100,110) and cur.UserId = {0} order by cp.Sort desc", userId);
            var result = SonFact.Cur.ExecuteQuery<AtomPermissionModel>(sql);
            return result;
        }

        public static List<AtomPermissionModel> GetPermission(string roleCode)
        {
            var sql = string.Format(@"select cp.* from AtomPermission cp (nolock)
                                                            inner join AtomRolePermission crp (nolock) on cp.Code= crp.PermissionCode
                                                            inner join WorkRole cr (nolock) on crp.RoleCode = cr.Code
                                                            where cp.IsValid=1 and crp.IsValid=1 and cr.IsValid=1 and cr.Code='{0}' order by cp.Sort desc", roleCode);
            var result = SonFact.Cur.ExecuteQuery<AtomPermissionModel>(sql);
            return result;
        }


        //权限管理
        public static List<NPermissionModel> SearchPermission(NPermissionModel request)
        {
            var list = new List<NPermissionModel>();
            var oldlisttmp = SonFact.Cur.ExecuteQuery<AtomPermission>("select * from AtomPermission where VUrl is not null");
            if (request.IsSet)
                oldlisttmp = oldlisttmp.Where(t => t.IsValid).ToList();

            var firsts = oldlisttmp.Where(t => t.ParentCode == null).ToList();
            if (!firsts.Any()) return list;

            foreach (var first in firsts)
            {
                var firstModel = EntityMapper.Mapper<AtomPermission, NPermissionModel>(first);
                list.Add(firstModel);
                SearchPermissionChildren(firstModel, 0, oldlisttmp);
            }

            return list;
        }

        private static void SearchPermissionChildren(NPermissionModel model, int level, List<AtomPermission> oldlist)
        {
            model.Level = level;
            var childrenlist = oldlist.Where(o => o.ParentCode == model.Code).ToList();
            var newlist = new List<NPermissionModel>();
            childrenlist.ForEach(o => newlist.Add(EntityMapper.Mapper<AtomPermission, NPermissionModel>(o)));
            model.Children = newlist;
            level += 1;
            foreach (var item in newlist)
                SearchPermissionChildren(item, level, oldlist);
        }

        public static bool AddPermission(NPermissionModel model)
        {
            var exist = SonFact.Cur.Top<AtomPermission>(t => t.Code == model.Code);
            if (exist != null) throw new Exception("呵呵！权限代码重复,请修改");

            var newPer = EntityMapper.Mapper<NPermissionModel, AtomPermission>(model);
            newPer.AddTime = DateTime.Now;
            newPer.EditTime = DateTime.Now;
            newPer.AddUserId = 0;
            newPer.EditUserId = 0;
            SonFact.Cur.Insert(newPer);
            return SonFact.Cur.Insert(newPer) > 0;
        }

        public static bool EditPermission(NPermissionModel model)
        {
            var existCode = SonFact.Cur.Top<AtomPermission>(t => t.Code == model.Code && t.Id != model.Id);
            if (existCode != null) throw new Exception("呵呵！权限代码重复,请修改");

            var exist = SonFact.Cur.Top<AtomPermission>(t => t.Id == model.Id);
            exist.IsValid = model.IsValid;
            exist.Name = model.Name;
            exist.Icon = model.Icon;
            exist.ParentCode = model.ParentCode;
            exist.Ptype = model.Ptype;
            exist.Sort = model.Sort;
            exist.DefaultUrl = model.DefaultUrl;
            exist.VUrl = model.VUrl;
            exist.Code = exist.Code;
            exist.CPath = model.CPath;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = 0;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = 0;

            var result = SonFact.Cur.Update(exist);
            return result > 0;
        }

        public static bool DelPermission(int pid)
        {
            var exist = SonFact.Cur.Top< AtomPermission>(t => t.Id == pid);
            if (exist == null) throw new Exception("要删除的权限不存在");
            var existchildren =SonFact.Cur.FindMany<AtomPermission>(t => t.ParentCode == exist.Code);
            if (existchildren.Any()) throw new Exception("下级权限为空时才能删除");
            var result = SonFact.Cur.Delete<AtomPermission>(exist.Id);
            return result > 0;
        }


        //角色管理
        public static List<NWorkRoleModel> SearchWorkRole(NWorkRoleModel request)
        {
            var list = new List<NWorkRoleModel>();
            var oldlisttmp = SonFact.Cur.FindMany<WorkRole>(t => t.IsValid == true);
            var first = SonFact.Cur.Top<WorkRole>(t => t.Code == "super_admin");
            if (first == null) return list;
            var firstModel = EntityMapper.Mapper<WorkRole, NWorkRoleModel>(first);
            list.Add(firstModel);
            SearchWorkRoleChildren(firstModel, 0, oldlisttmp);
            return list;
        }

        private static void SearchWorkRoleChildren(NWorkRoleModel model, int level, List<WorkRole> oldlist)
        {
            model.Level = level;
            var childrenlist = oldlist.Where(o => o.ParentCode == model.Code).ToList();
            var newlist = new List<NWorkRoleModel>();
            childrenlist.ForEach(o => newlist.Add(EntityMapper.Mapper<WorkRole, NWorkRoleModel>(o)));
            model.Children = newlist;
            level += 1;
            foreach (var item in newlist)
                SearchWorkRoleChildren(item, level, oldlist);
        }

        public static bool AddWorkRole(NWorkRoleModel model)
        {
            var exist = SonFact.Cur.Top<WorkRole>(t => t.Code == model.Code);
            if (exist != null) throw new Exception("呵呵！权限代码重复,请修改");

            var newPer = EntityMapper.Mapper<NWorkRoleModel, WorkRole>(model);
            newPer.AddTime = DateTime.Now;
            newPer.EditTime = DateTime.Now;
            newPer.AddUserId = 0;
            newPer.EditUserId = 0;
            var res = SonFact.Cur.Insert(newPer);

            return res > 0;
        }

        public static bool EditWorkRole(NWorkRoleModel model)
        {
            var existCode = SonFact.Cur.Top<WorkRole>(t => t.Code == model.Code && t.Id != model.Id);
            if (existCode != null) throw new Exception("呵呵！权限代码重复,请修改");

            var exist = SonFact.Cur.Top<WorkRole>(t => t.Id == model.Id);
            exist.Name = model.Name;
            exist.Code = model.Code;
            exist.DomainId = model.DomainId;
            exist.ParentCode = model.ParentCode;
            exist.PermissionType = model.PermissionType;
            exist.RoleType = model.RoleType;
            exist.CrossPermissionType = model.CrossPermissionType;

            exist.IsValid = model.IsValid;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = 0;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = 0;
           
            var result = SonFact.Cur.Update(exist);
            return result > 0;
        }

        public static bool DelWorkRole(int pid)
        {
            var exist = SonFact.Cur.Top<WorkRole>(t => t.Id == pid);
            if (exist == null) throw new Exception("要删除的角色不存在");
            var existchildren =SonFact.Cur.FindMany<WorkRole>(t => t.ParentCode == exist.Code);
            if (existchildren.Any()) throw new Exception("下级角色为空时才能删除");
            var result = SonFact.Cur.Delete<WorkRole>(exist.Id);
            return result > 0;
        }


        public static List<NRolePermissionModel> GetRolePermissions(string code)
        {
            var rolePermission = new List<NRolePermissionModel>();
            var exist =SonFact.Cur.FindMany<AtomRolePermission>(t => t.RoleCode == code && t.IsValid==true);
            if (!exist.Any()) return rolePermission;

            exist.ForEach(t =>
            {
                var rp = EntityMapper.Mapper<AtomRolePermission, NRolePermissionModel>(t);
                rolePermission.Add(rp);
            });

            return rolePermission;
        }


        public static bool SetRolePermissions(NPermissionModel request)
        {
            if (string.IsNullOrWhiteSpace(request.UserRoleCode)) throw new Exception("角色code不能为空");
            var exists = SonFact.Cur.ExecuteQuery($"update AtomRolePermission set isvalid=0 where rolecode='{request.UserRoleCode}' and isvalid=1"); 

            if (request.Children == null || !request.Children.Any()) return true;
            request.Children.ForEach(t =>
            {
                var arp = new AtomRolePermission()
                {
                    AddTime = DateTime.Now,
                    AddUserId = 0,
                    EditTime = DateTime.Now,
                    EditUserId = 0,
                    IsValid = true,
                    PermissionCode = t.Code,
                    RoleCode = request.UserRoleCode
                };
               SonFact.Cur.Insert<AtomRolePermission>(arp);
            });
            return true;
        }




    }
}
