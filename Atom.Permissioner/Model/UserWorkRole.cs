using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orm.Son.Mapper;

namespace Atom.Permissioner.Model
{
    internal class UserWorkRole
    {
        [Description("用户角色Id")]
        public int Id { get; set; }

        [Description("用户Id")]
        public int UserId { get; set; }
        [Description("角色代码")]
        public string RoleCode { get; set; }

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
