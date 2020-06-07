using Atom.ConfigCenter.Model;
using Orm.Son.Core;
using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter.DataCore
{
    internal static class AtomConfigCenterDataCore
    {
        private static readonly object locker = new object();

        public  static bool CheckOrCreateDb()
        {
            var resA = SonFact.Cur.CreateTable<AtomCateConfig>();
            var resB = SonFact.Cur.CreateTable<AtomConfigValue>();
            return resA && resB;
        }

        //配置管理
        public static List<NAtomCateConfigModel> SearchDict(NAtomCateConfigModel request, int domid=0)
        {
            var list = new List<NAtomCateConfigModel>();
            //var doms =SonFact.Cur.FindMany<Domain>(t => t.IsValid==true);
            var oldlisttmp =SonFact.Cur.FindMany<AtomCateConfig>(t => t.IsValid == true && t.MainConfigCode == "dict").OrderBy(t => t.Sort).ToList();
            if (domid != 0)
                oldlisttmp = oldlisttmp.Where(t => t.DomainId == 0 || t.DomainId == domid).ToList();

            var firsts = oldlisttmp.Where(t => t.ParentCateCode == null || t.ParentCateCode=="").ToList();
            if (!firsts.Any()) return list;

            //if (request.ExpceptCustom == true)
            //    firsts = firsts.Where(t => t.CateCode != "custom_dict").ToList();

            foreach (var first in firsts)
            {
                var firstModel = EntityMapper.Mapper<AtomCateConfig, NAtomCateConfigModel>(first);
                firstModel.DomainName = firstModel.DomainId == 0 ? "全平台" : domid+"";
                list.Add(firstModel);
                SearchDictChildren(firstModel, 0, oldlisttmp, domid);
            }

            return list;
        }

        private static void SearchDictChildren(NAtomCateConfigModel model, int level, List<AtomCateConfig> oldlist, int domid)
        {
            model.Level = level;
            var childrenlist = oldlist.Where(o => o.ParentCateCode == model.CateCode).OrderBy(t => t.Sort).ToList();
            var newlist = new List<NAtomCateConfigModel>();
            childrenlist.ForEach(o =>
            {
                var cd = EntityMapper.Mapper<AtomCateConfig, NAtomCateConfigModel>(o);
                cd.DomainName = cd.DomainId == 0 ? "全平台" : domid+"";
                newlist.Add(cd);
            });
            model.Children = newlist;
            level += 1;
            foreach (var item in newlist)
                SearchDictChildren(item, level, oldlist, domid);
        }

        public static bool AddDict(NAtomCateConfigModel model,int domid=0)
        {
            var exist = SonFact.Cur.Top<AtomCateConfig>(t => t.CateCode == model.CateCode);
            if (exist != null) throw new Exception("呵呵！配置代码重复,请修改");

            var newPer = EntityMapper.Mapper<NAtomCateConfigModel, AtomCateConfig>(model);
            newPer.AddTime = DateTime.Now;
            newPer.EditTime = DateTime.Now;
            newPer.AddUserId = 0;
            newPer.EditUserId = 0;
            newPer.MainConfigCode = "dict";
            newPer.DomainId = domid == 0 ? model.DomainId : domid;

            var result =SonFact.Cur.Insert(newPer);
            return result > 0;
        }

        public static bool EditDict(NAtomCateConfigModel model)
        {
            var existCode =SonFact.Cur.Top<AtomCateConfig>(t => t.CateCode == model.CateCode && t.ConfigCateId != model.ConfigCateId);
            if (existCode != null) throw new Exception("呵呵！字典代码重复,请修改");

            if (!model.IsValid.HasValue) return DelDict(model.ConfigCateId);

            var exist =SonFact.Cur.Top<AtomCateConfig>(t => t.ConfigCateId == model.ConfigCateId);
            exist.IsValid = model.IsValid.Value;
            exist.CateName = model.CateName;
            exist.CateCode = model.CateCode;
            exist.ExtCateCode = model.ExtCateCode;
            exist.MainConfigCode = "dict";
            exist.ParentCateCode = model.ParentCateCode;
            exist.Level = model.Level;
            exist.Value = model.Value;
            exist.ScdValue = model.ScdValue;
            exist.ThdValue = model.ThdValue;
            exist.Sort = model.Sort;
            exist.DomainId = model.DomainId;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = 0;
            exist.EditTime = DateTime.Now;
            exist.EditUserId = 0;

            var result = SonFact.Cur.Update(exist);
            return result > 0;
        }

        public static bool DelDict(int cid)
        {
            var exist = SonFact.Cur.Top<AtomCateConfig>(t => t.ConfigCateId == cid);
            if (exist == null) throw new Exception("要删除的字典不存在");
            var existchildren = SonFact.Cur.FindMany<AtomCateConfig>(t => t.ParentCateCode == exist.CateCode);
            if (existchildren.Any()) throw new Exception("下级字典为空时才能删除");
            var result = SonFact.Cur.Delete<AtomCateConfig>(cid);
            return result > 0;
        }

        public static List<AtomCateConfigModel> GetCates(string parentCode, bool hasDisable = false)
        {
            var result = hasDisable ? SonFact.Cur.ExecuteQuery<AtomCateConfigModel>($"select * from AtomCateConfig  where  isnull(ParentCateCode,'')='{parentCode}' order by sort")
                : SonFact.Cur.ExecuteQuery<AtomCateConfigModel>($"select * from AtomCateConfig  where isvalid=1 and isnull(ParentCateCode,'')='{parentCode}' order by sort");
            //var result = SonFact.Cur.FindMany<AtomCateConfig, AtomCateConfigModel>(t => t.ParentCateCode == parentCode && t.IsValid == true);
            if (!result.Any()) return new List<AtomCateConfigModel>();
            return result;
        }

        public static List<AtomCateConfigModel> GetCatesByDomain(string parentCode, int domainId)
        {
            if (domainId == 0) return GetCates(parentCode);
            var result = SonFact.Cur.FindMany<AtomCateConfig, AtomCateConfigModel>(t => t.ParentCateCode == parentCode && t.IsValid == true && (t.DomainId == domainId || t.DomainId == 0));
            result = result.OrderBy(t => t.Sort).ToList();
            if (!result.Any()) return new List<AtomCateConfigModel>();
            return result;
        }

        public static List<AtomCateConfigModel> Get(string code)
        {
            var result = SonFact.Cur.ExecuteQuery<AtomCateConfigModel>($"select * from AtomCateConfig  where CateCode='{code}'");
            if (!result.Any()) return new List<AtomCateConfigModel>();
            return result;
        }


        //明细配置管理
        public static long SetVal(AtomConfigValue acv, bool isAdd)
        {
            if (string.IsNullOrWhiteSpace(acv.CateCode))
                throw new Exception("编码不可为空");
            if (string.IsNullOrWhiteSpace(acv.CateValue))
                throw new Exception("配置值不可为空");

            lock (locker)
            {
                var existCate = SonFact.Cur.Top<AtomCateConfig>(t => t.CateCode == acv.CateCode);
                if (existCate == null) throw new Exception("配置Cate不存在");

                var exist = SonFact.Cur.Top<AtomConfigValue>(t => t.CateCode == acv.CateCode && t.RelId == acv.RelId && t.ValueType == acv.ValueType);
                if (exist != null && isAdd) throw new Exception("配置已经存在");
                if (exist != null && !isAdd)
                {
                    acv.ConfigValueId = exist.ConfigValueId;
                    var result = SonFact.Cur.Update(acv);
                    return Convert.ToInt64(result);
                }

                return SonFact.Cur.Insert(acv);
            }
        }

        public static AtomCateConfigModel Get(int relId, string cateCode)
        {
            var result = new AtomCateConfigModel();
            result.Values = new List<AtomConfigValueModel>();
            var now = DateTime.Now;
            var list = SonFact.Cur.FindMany<AtomConfigValue, AtomConfigValueModel>(t => t.IsValid == true && t.CateCode == cateCode && (t.RelId == relId || t.ValueType == "default"));

            foreach (var item in list)
            {
                if (item.StartTime != null && item.StartTime.Value > now) continue;
                if (item.EndTime != null && item.EndTime.Value < now) continue;
                result.Values.Add(item);
            }
            return result;
        }

        public static AtomCateConfigModel GetAllById(int relId)
        {
            var result = new AtomCateConfigModel();
            result.Children = new List<AtomCateConfigModel>();
            var now = DateTime.Now;

            var cates = SonFact.Cur.FindMany<AtomConfigValue, AtomConfigValueModel>(t => t.IsValid == true && (t.RelId == relId || t.ValueType == "default"));
            foreach (var item in cates)
            {
                if (item.StartTime != null && item.StartTime.Value > now) continue;
                if (item.EndTime != null && item.EndTime.Value < now) continue;
                result.Values.Add(item);
            }

            var cateCodes = cates.Select(t => t.CateCode).Distinct();

            foreach (var c in cateCodes)
            {
                var vs = Get(relId, c);
                result.Children.Add(vs);
            }
            return result;
        }


    }
}
