using Atom.ConfigCenter.Model;
using System;
using System.Collections.Generic;

namespace Atom.ConfigCenter
{
    public interface IAtomConfigCenter
    {
        long Set(AtomConfigModel model, bool isAdd);

        int Del(int id);

        long Set(string code, string value, string pCode = null, string desc = null, string cType = null, DateTime? st = null, DateTime? et = null, string extVal = null, bool isAdd = false);

        long SetCate(string name, string code, string pCode = null, string extCode = null, string mainCode = null, int sort = 0, bool hasVal = false, bool isAdd = false);

        List<AtomCateConfigModel> GetCates(string parentCode, bool hasDisable = false);

        List<AtomCateConfigModel> GetCatesByDomain(string parentCode, int domainId);

        long SetVal(string cateCode, string value, int relId = 0, string extVal = null, string valueType = null, DateTime? st = null, DateTime? et = null, bool isAdd = false);

        AtomConfigModel Get(string code);

        AtomConfigModel Gets(string code);

        AtomCateConfigModel Get(int relId, string cateCode);

        AtomCateConfigModel GetAllById(int relId);

        Tuple<List<NAtomConfigModel>, int> SearchAtomConfig(NAtomConfigModel model);

        List<NAtomCateConfigModel> SearchDict(NAtomCateConfigModel request, int domid = 0);

        bool AddDict(NAtomCateConfigModel model, int domid = 0);

        bool EditDict(NAtomCateConfigModel model);

        bool DelDict(int cid);

        string Html();


    }
}
