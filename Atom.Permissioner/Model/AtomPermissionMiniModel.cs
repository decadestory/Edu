using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orm.Son.Mapper;

namespace Atom.Permissioner.Model
{
    public class AtomPermissionMiniModel
    {
        public int Id { get; set; }
        public int Ptype { get; set; }
        public string ParentCode { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string DefaultUrl { get; set; }
        public int Sort { get; set; }
        public string VUrl { get; set; }
        public string CPath { get; set; }
        public int ServerId { get; set; }
    }
}
