using Atom.Permissioner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edu.Model
{
    public class UserTokenModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LoginId { get; set; }
        public string MobilePhone { get; set; }
        public List<AtomPermissionMiniModel> Permissions { get; set; }

    }
}
