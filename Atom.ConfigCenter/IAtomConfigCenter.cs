using Atom.ConfigCenter.Model;
using System;
using System.Collections.Generic;

namespace Atom.ConfigCenter
{
    public interface IAtomConfigCenter
    {
        List<NAtomCateConfigModel> SearchDict(NAtomCateConfigModel request, int domid = 0);

        bool AddDict(NAtomCateConfigModel model, int domid = 0);

        bool EditDict(NAtomCateConfigModel model);

        bool DelDict(int cid);

        List<AtomCateConfigModel> GetCates(string parentCode, bool hasDisable = false);

        List<AtomCateConfigModel> GetCatesByDomain(string parentCode, int domainId);

        List<AtomCateConfigModel> Get(string code);

        long SetVal(string cateCode, string value, int relId = 0, string extVal = null, string valueType = null, DateTime? st = null, DateTime? et = null, bool isAdd = false);

        AtomCateConfigModel Get(int relId, string cateCode);

        AtomCateConfigModel GetAllById(int relId);

        string Html();


    }
}
