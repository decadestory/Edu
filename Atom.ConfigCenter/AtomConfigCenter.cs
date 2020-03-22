using Atom.ConfigCenter.Cacher;
using Atom.ConfigCenter.DataCore;
using Atom.ConfigCenter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter
{
    public class AtomConfigCenter: IAtomConfigCenter
    {
        public AtomConfigCenter(string dbConnStr)
        {
            SonFact.init(dbConnStr);
            AtomConfigCenterDataCore.CheckOrCreateDb();
        }

        public  long Set(AtomConfigModel model , bool isAdd)
        {
            var ac = new AtomConfig
            {
                ConfigCode = model.ConfigCode,
                ConfigValue = model.ConfigValue,
                ParentCode = model.ParentCode,
                ConfigDesc = model.ConfigDesc,
                ConfigType = string.IsNullOrWhiteSpace(model.ConfigType) ? "default" : model.ConfigType,
                ExtValue = model.ExtValue,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                AddTime = DateTime.Now,
                AddUserId = 0,
                EditTime = DateTime.Now,
                EditUserId =0,
                IsValid = true
            };
            return AtomConfigCenterDataCore.Set(ac, isAdd);
        }

        public  int Del(int id)
        {
            return AtomConfigCenterDataCore.Del(id);
        }

        public  long Set(string code, string value, string pCode = null, string desc = null, string cType = null, DateTime? st = null, DateTime? et = null, string extVal = null, bool isAdd = false)
        {
            var ac = new AtomConfig
            {
                ConfigCode = code,
                ConfigValue = value,
                ParentCode = pCode,
                ConfigDesc = desc,
                ConfigType = string.IsNullOrWhiteSpace(cType) ? "default" : cType,
                ExtValue = extVal,
                StartTime = st,
                EndTime = et,
                AddTime = DateTime.Now,
                IsValid = true
            };
            return AtomConfigCenterDataCore.Set(ac, isAdd);
        }

        public  long SetCate(string name, string code, string pCode = null, string extCode = null, string mainCode=null, int sort=0, bool hasVal=false, bool isAdd = false)
        {
            var acc = new AtomCateConfig
            {
                CateName = name,
                CateCode = code,
                ParentCateCode = pCode,
                AddTime = DateTime.Now,
                ExtCateCode = extCode,
                MainConfigCode = mainCode,
                Sort=sort,
                HasValue=hasVal,
                IsValid = true
            };
            return AtomConfigCenterDataCore.SetCate(acc, isAdd);
        }

        public  List<AtomCateConfigModel> GetCates(string parentCode)
        {
            return AtomConfigCenterDataCore.GetCates(parentCode);
        }

        public  List<AtomCateConfigModel> GetCatesByDomain(string parentCode,int domainId)
        {
            return AtomConfigCenterDataCore.GetCatesByDomain(parentCode, domainId);
        }

        public  long SetVal(string cateCode, string value, int relId = 0, string extVal = null, string valueType=null, DateTime? st = null, DateTime? et = null, bool isAdd = false)
        {
            var acv = new AtomConfigValue
            {
                CateCode = cateCode,
                AddTime = DateTime.Now,
                CateValue = value,
                IsValid = true,
                RelId = relId,
                StartTime = st,
                EndTime = et,
                ExtValue = extVal,
                ValueType=valueType,
            };

            return AtomConfigCenterDataCore.SetVal(acv, isAdd);
        }

        public  AtomConfigModel Get(string code)
        {
            var res = AtomConfigCenterDataCore.Get(code);
            if (res == null) return null;
            return res;
        }

        public  AtomConfigModel Gets(string code)
        {
            var res = AtomConfigCenterDataCore.Gets(code);
            if (res == null) return null;
            return res;
        }

        public  AtomCateConfigModel Get(int relId, string cateCode)
        {
            return AtomConfigCenterDataCore.Get(relId,cateCode);
        }

        public  AtomCateConfigModel GetAllById(int relId)
        {
            return AtomConfigCenterDataCore.GetAllById(relId);
        }

        public Tuple<List<NAtomConfigModel>,int> SearchAtomConfig(NAtomConfigModel request)
        {
            return AtomConfigCenterDataCore.SearchAtomConfig(request);
        }

        public List<NAtomCateConfigModel> SearchDict(NAtomCateConfigModel request, int domid = 0)
        {
            return AtomConfigCenterDataCore.SearchDict(request,domid);
        }

        public bool AddDict(NAtomCateConfigModel model, int domid = 0)
        {
            return AtomConfigCenterDataCore.AddDict(model, domid);
        }

        public bool EditDict(NAtomCateConfigModel model)
        {
            return AtomConfigCenterDataCore.EditDict(model);
        }

        public bool DelDict(int cid)
        {
            return AtomConfigCenterDataCore.DelDict(cid);
        }
    }
}
