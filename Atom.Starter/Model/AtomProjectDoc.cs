using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Starter.Model
{
    internal class AtomProjectDoc
    {
        [Description("需求Id")]
        public int Id { get; set; }

        [Description("项目Id")]
        public int ProjectId { get; set; }

        [Description("需求名称")]
        public string Name { get; set; }

        [Description("需求描述")]
        public string Desc { get; set; }

        [Description("需求状态：0--创建 ，1--确定 ，2--开发中， 3--完成， 4--归档")]
        public string Status { get; set; }

        [Description("存在问题")]
        public string Questions { get; set; }

        [Description("指定人")]
        public string Operator { get; set; }

        [Description("预计测试日期")]
        public DateTime? TestDate { get; set; }

        [Description("预计完成日期")]
        public DateTime? CompleteDate { get; set; }

        [Description("备注")]
        public string Remark { get; set; }

        [Description("添加时间")]
        public DateTime AddTime { get; set; }

        [Description("添加人")]
        public int AddUserId { get; set; }

        [Description("修改时间")]
        public DateTime EditTime { get; set; }

        [Description("修改人")]
        public int EditUserId { get; set; }

        [Description("是否可用")]
        public bool IsValid { get; set; }
    }
}
