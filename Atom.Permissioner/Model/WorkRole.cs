using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orm.Son.Mapper;

namespace Atom.Permissioner.Model
{
    internal class WorkRole
    {
        [Description("角色Id")]
        public int Id { get; set; }

        [Description("角色名称")]
        public string Name { get; set; }
        [Description("角色代码")]
        public string Code { get; set; }
        [Description("角色上级代码")]
        public string ParentCode { get; set; }

        [Description("跨级类型")]
        public int? CrossPermissionType { get; set; }

        [Description("角色类型")]
        public int? RoleType { get; set; }

        [Description("权限类型")]
        public int? PermissionType { get; set; }

        [Description("角色类型（如：acd/cpd）")]
        public int DomainId { get; set; }
        [Description("添加时间")]
        public DateTime AddTime { get; set; }
        [Description("添加人")]
        public int AddUserId { get; set; }
        [Description("编辑时间")]
        public DateTime EditTime { get; set; }
        [Description("编辑人")]
        public int EditUserId { get; set; }
        [Description("是否可用")]
        public bool IsValid { get; set; }
    }
}
