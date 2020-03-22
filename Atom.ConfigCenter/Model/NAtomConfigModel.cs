using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.ConfigCenter.Model
{
    public class NAtomConfigModel : PagerInBase
    {
        public int ConfigId { get; set; }
        public string ConfigType { get; set; }
        public string ConfigCode { get; set; }
        public string ConfigValue { get; set; }
        public string ConfigDesc { get; set; }
        public string ParentCode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string ExtValue { get; set; }
        public DateTime? AddTime { get; set; }
        public int AddUserId { get; set; }
        public DateTime? EditTime { get; set; }
        public int EditUserId { get; set; }
        public bool IsValid { get; set; }

        public string KeyWord { get; set; }

    }

    public class NAtomCateConfigModel
    {
        public int ConfigCateId { get; set; }
        public string CateName { get; set; }
        public string CateCode { get; set; }
        public string ParentCateCode { get; set; }
        public string ExtCateCode { get; set; }
        public string MainConfigCode { get; set; }
        public bool HasValue { get; set; }
        public int Sort { get; set; }
        public int DomainId { set; get; }
        public string DomainName { set; get; }
        public DateTime? AddTime { get; set; }
        public int AddUserId { get; set; }
        public DateTime? EditTime { get; set; }
        public int EditUserId { get; set; }
        public bool IsValid { get; set; }
        public int? Level { get; set; }
        public string Value { get; set; }
        public string ScdValue { get; set; }
        public string ThdValue { get; set; }
        public List<NAtomCateConfigModel> Children { get; set; }
        public string CateValue { get; set; }
        public string ValueType { get; set; }
        public bool ExpceptCustom { get; set; }

    }



    public class AddTaskDictRequest
    {
        public string DictName { get; set; }
        public string DictCode { get; set; }
        public List<string> DictOptions { get; set; }
    }

    public class AddTaskDictResponse
    {
        public string DictName { get; set; }
        public string DictCode { get; set; }
        public string ShowTexts { get; set; }
        public List<string> DelSubCodes { get; set; }

    }

}
