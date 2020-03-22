using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orm.Son.Mapper;

namespace Atom.Permissioner.Model
{
    public class AtomPermissionModel
    {
        [Description("权限Id")]
        public int Id { get; set; }

        [Description("权限类型(10模块,20操作权限,30特殊权限)")]
        public int Ptype { get; set; }
        [Description("上级权限代码")]
        public string ParentCode { get; set; }
        [Description("权限名称")]
        public string Name { get; set; }
        [Description("权限代码(规则：上级_模块名_功能名)")]
        public string Code { get; set; }
        [Description("权限图标")]
        public string Icon { get; set; }
        [Description("权限链接")]
        public string DefaultUrl { get; set; }
        [Description("权限排序")]
        public int Sort { get; set; }
        [Description("添加时间")]
        public DateTime AddTime { get; set; }
        [Description("添加人")]
        public int AddUser { get; set; }
        [Description("编辑时间")]
        public DateTime EditTime { get; set; }
        [Description("编辑人")]
        public int EditUser { get; set; }
        [Description("是否可用")]
        public bool IsValid { get; set; }
        [Description("VueUrl")]
        public string VUrl { get; set; }
        [Description("CPath")]
        public string CPath { get; set; }

        public int ServerId { get; set; }

    }




    public class NPermissionModel
    {
        public int Id { get; set; }
        public int Ptype { get; set; }
        public string ParentCode { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string DefaultUrl { get; set; }
        public int Sort { get; set; }
        public DateTime AddTime { get; set; }
        public int AddUser { get; set; }
        public DateTime EditTime { get; set; }
        public int EditUser { get; set; }
        public bool IsValid { get; set; }
        public string VUrl { get; set; }
        public string CPath { get; set; }

        public int Level { get; set; }
        public List<string> Family { get; set; }
        public string Identity { get; set; }
        public bool IsSet { get; set; }
        public string UserRoleCode { get; set; }

        public List<NRolePermissionModel> Selected { get; set; }
        public List<NPermissionModel> Children { get; set; }
    }

    public class NRolePermissionModel
    {
        public int Id { get; set; }
        public string PermissionCode { get; set; }
        public string RoleCode { get; set; }
        public DateTime AddTime { get; set; }
        public int AddUserId { get; set; }
        public DateTime EditTime { get; set; }
        public int EditUserId { get; set; }
        public bool IsValid { get; set; }
    }



    public class NWorkRoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public int DomainId { get; set; }
        public int Level { get; set; }
        public string Identity { get; set; }
        public DateTime? AddTime { get; set; }
        public int AddUserId { get; set; }
        public DateTime? EditTime { get; set; }
        public int EditUserId { get; set; }
        public bool IsValid { get; set; }
        public int? RoleType { get; set; }
        public int? PermissionType { get; set; }
        public int? CrossPermissionType { get; set; }

        public string KeyWord { get; set; }

        public List<string> Family { get; set; }
        public List<NWorkRoleModel> Children { get; set; }
    }



}
