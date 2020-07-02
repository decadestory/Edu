using Atom.Starter.Model;
using Orm.Son.Core;
using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Atom.Starter.DataCore
{
    internal class AStarterCore
    {
        public bool CheckOrCreateDb(string projectName, string projectDesc)
        {
            SonFact.Cur.CreateTable<AtomProject>();
            SonFact.Cur.CreateTable<AtomProjectDoc>();
            SonFact.Cur.CreateTable<AtomDbTable>();
            SonFact.Cur.CreateTable<AtomDbColumn>();
            SonFact.Cur.CreateTable<AtomStarterLog>();

            var top1 = SonFact.Cur.Top<AtomProject>(t => t.IsValid == true);
            if (top1 == null)
            {
                var p = new AtomProject();
                p.Name = projectName;
                p.Desc = projectDesc;
                p.AddTime = DateTime.Now;
                p.EditTime = DateTime.Now;
                p.AddUserId = 0;
                p.EditUserId = 0;
                p.IsValid = true;
                SonFact.Cur.Insert(p);
            }

            var top1c = SonFact.Cur.Top<AtomProjectDoc>(t => t.IsValid == true);
            if (top1c == null)
            {
                var pc = new AtomProjectDoc();
                pc.AddTime = DateTime.Now;
                pc.EditTime = DateTime.Now;
                pc.AddUserId = 0;
                pc.EditUserId = 0;
                pc.IsValid = true;
                pc.Name = "需求1:" + projectName;
                pc.Desc = "需求描述:" + projectDesc;
                pc.Operator = "Jerry";
                pc.ProjectId = 1;
                pc.Questions = "测试问题1：字数未限制";
                pc.CompleteDate = DateTime.Now.AddDays(7);
                pc.Status = "开发中";
                pc.TestDate = DateTime.Now.AddDays(5);
                pc.Remark = "大家抓紧时间，还有一周时间了";
                SonFact.Cur.Insert(pc);
            }

            GenTableColumns();

            return true;
        }

        private void GenTableColumns()
        {
            var sql = @"if not exists(select 1 from AtomDbTable)
                                begin
	                                insert into AtomDbTable(Name,DbTableName,ProjectId,AddTime,AddUserId,EditTime,EditUserId,IsValid)
	                                select name,name,object_id,GETDATE(),1,GETDATE(),1,1 from sys.objects where type='U' and name not like '%Atom%'

	                                select * into #temp_adt from AtomDbTable
	                                declare @tid int;
	                                declare @oid int;
	                                while exists (select 1 from #temp_adt)
	                                begin
		                                set @tid=0;
		                                set @oid=0;
		                                select top 1 @tid =id,@oid=ProjectId from #temp_adt;

		                                insert AtomDbColumn(DbTableId,[Name],DbColumnName,DbType,IsNull,IsPrimary,[Desc],AddTime,AddUserId,EditTime,EditUserId,IsValid)
		                                select @tid,Convert(varchar,ep.value),Convert(varchar,c.name),
		                                (case when t.name='varchar' or t.name='char' or t.name='nvarchar' or t.name='nchar' then convert(varchar,t.name)+'('+ case when c.max_length=-1 then 'max' else convert(varchar,c.max_length) end+')' else convert(varchar,t.name) end) DbType,
		                                c.is_nullable,c.is_identity,Convert(varchar,ep.value),getdate(),1,getdate(),1,1
		                                from sys.columns c
		                                LEFT JOIN sys.extended_properties ep ON ep.major_id = c.object_id AND ep.minor_id = c.column_id 
		                                left join sys.types t on c.user_type_id=t.user_type_id
		                                where object_id=@oid;

		                                delete #temp_adt where Id=@tid
	                                end
	                                drop table #temp_adt;
                                end";

            SonFact.Cur.ExecuteQuery(sql);
        }

        //需求管理
        public Tuple<long, bool> AddOrEditDoc(AtomProjectDocModel model)
        {
            if (model.Id > 0) goto edit;
            var en = EntityMapper.Mapper<AtomProjectDocModel, AtomProjectDoc>(model);
            en.AddTime = DateTime.Now;
            en.EditTime = DateTime.Now;
            en.AddUserId = 0;
            en.EditUserId = 0;
            en.IsValid = true;
            var res = SonFact.Cur.Insert(en);
            AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 添加了需求[{res}]");
            return new Tuple<long, bool>(res, true);
            edit:
            if (model.Status == "删除")
            {
                AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 删除了需求[{model.Id}]");
                SonFact.Cur.Delete<AtomProjectDoc>(model.Id);
                return new Tuple<long, bool>(-1, true);
            }
            var old = SonFact.Cur.Find<AtomProjectDoc>(model.Id);
            var isSaved = GetIsSaveOk(old, model);
            if (isSaved == -10) return new Tuple<long, bool>(-2, false);

            var edn = EntityMapper.Mapper<AtomProjectDocModel, AtomProjectDoc>(model);
            edn.EditTime = DateTime.Now;
            edn.EditUserId = 0;
            edn.AddTime = old.AddTime;
            edn.AddUserId = old.AddUserId;
            var rows = SonFact.Cur.Update(edn);
            return new Tuple<long, bool>(-2, true);
        }

        private int GetIsSaveOk(AtomProjectDoc old, AtomProjectDocModel model)
        {
            switch (model.EditColName)
            {
                case "Name":
                    if (old.Name != model.Name)
                    {
                        AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 修改了需求[{model.Id}] 需求模块：{model.Name}");
                        return 10;
                    }
                    else
                        return -10;
                case "Desc":
                    if (old.Desc != model.Desc)
                    {
                        AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 修改了需求[{model.Id}] 需求描述：{model.Desc}");
                        return 10;
                    }
                    else
                        return -10;
                case "Status":
                    if (old.Status != model.Status)
                    {
                        AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 修改了需求[{model.Id}] 需求状态：{model.Status}");
                        return 10;
                    }
                    else
                        return -10;
                case "Questions":
                    if (old.Questions != model.Questions)
                    {
                        AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 修改了需求[{model.Id}] 存在问题：{model.Questions}");
                        return 10;
                    }
                    else
                        return -10;
                case "Operator":
                    if (old.Operator != model.Operator)
                    {
                        AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 修改了需求[{model.Id}] 分配人：{model.Operator}");
                        return 10;
                    }
                    else
                        return -10;
                case "TestDate":
                    if (old.TestDate != model.TestDate)
                    {
                        AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 修改了需求[{model.Id}] 测试日期：{model.TestDate}");
                        return 10;
                    }
                    else
                        return -10;
                case "CompleteDate":
                    if (old.CompleteDate != model.CompleteDate)
                    {
                        AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 修改了需求[{model.Id}] 完成日期：{model.CompleteDate}");
                        return 10;
                    }
                    else
                        return -10;
                case "Remark":
                    if (old.Remark != model.Remark)
                    {
                        AddLog(1, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 修改了需求[{model.Id}] 备注：{model.Remark}");
                        return 10;
                    }
                    else
                        return -10;
            }

            return 10;
        }

        public List<AtomProjectDocModel> Docs(AtomProjectDocModel model)
        {
            var sql = string.Format(@" select * from AtomProjectDoc order by Id OFFSET 0 ROWS FETCH NEXT 100 ROWS ONLY");
            var res = SonFact.Cur.ExecuteQuery<AtomProjectDocModel>(sql);
            return res;
        }

        //1文档操作，2表操作，3字段操作
        private bool AddLog(int logType, string logTxt)
        {
            var log = new AtomStarterLog();
            log.AddTime = DateTime.Now;
            log.EditTime = DateTime.Now;
            log.AddUserId = 0;
            log.EditUserId = 0;
            log.IsValid = true;
            log.LogType = logType;
            log.ProjectId = 1;
            log.LogTxt = logTxt;
            SonFact.Cur.Insert(log);
            return true;
        }

        //数据库管理
        public long AddTable(AtomDbTableModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new Exception("表名不能为空");
            if (string.IsNullOrWhiteSpace(model.DbTableName)) throw new Exception("数据库表名不能为空");
            if (string.IsNullOrWhiteSpace(model.KeyName)) throw new Exception("数据库主键名");


            var en = EntityMapper.Mapper<AtomDbTableModel, AtomDbTable>(model);
            en.AddTime = DateTime.Now;
            en.EditTime = DateTime.Now;
            en.AddUserId = 0;
            en.EditUserId = 0;
            en.IsValid = true;

            var tran = SonFact.Cur.BeginTransaction();
            try
            {
                var res = SonFact.Cur.Insert(en);
                var doSql = AStarterSqlGen.CreateTableSql(model.DbTableName, model.KeyName,res);
                SonFact.Cur.ExecuteSql(doSql);
                AddLog(2, doSql);
                tran.Commit();
                return res;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public List<AtomDbTableModel> Tables(AtomDbTableModel model)
        {
            var sql = string.Format(@" select * from AtomDbTable order by Id OFFSET 0 ROWS FETCH NEXT 100 ROWS ONLY");
            var res = SonFact.Cur.ExecuteQuery<AtomDbTableModel>(sql);
            return res;
        }

        public List<AtomDbColumnModel> Columns(AtomDbColumnModel model)
        {
            var sql = string.Format($@" select * from AtomDbColumn where DbTableId={model.DbTableId} order by Id OFFSET 0 ROWS FETCH NEXT 100 ROWS ONLY");
            var res = SonFact.Cur.ExecuteQuery<AtomDbColumnModel>(sql);
            return res;
        }

    }
}
