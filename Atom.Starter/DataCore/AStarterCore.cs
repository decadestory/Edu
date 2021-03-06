﻿using Atom.Starter.Model;
using Orm.Son.Core;
using Orm.Son.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
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
            var sql = AStarterSqlGen.GenTableColumnSql();
            SonFact.Cur.ExecuteSql(sql);
        }

        # region 需求管理
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
        #endregion

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

        #region 数据库管理

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

        public long AddTable(AtomDbTableModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new Exception("表名不能为空");
            if (string.IsNullOrWhiteSpace(model.DbTableName)) throw new Exception("数据库表名不能为空");
            if (string.IsNullOrWhiteSpace(model.KeyName)) throw new Exception("数据库主键名不能为空");


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
                var doSql = AStarterSqlGen.CreateTableSql(model.DbTableName, model.KeyName, res, model.Name);
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

        public long AddColumn(AtomDbColumnModel model)
        {
            if (model.DbTableId == default) throw new Exception("请选择表后再进行添加字段");
            if (string.IsNullOrWhiteSpace(model.Name)) throw new Exception("名称不能为空");
            if (string.IsNullOrWhiteSpace(model.DbColumnName)) throw new Exception("数据库列名不能为空");
            if (string.IsNullOrWhiteSpace(model.DbType)) throw new Exception("类型不能为空");

            var en = EntityMapper.Mapper<AtomDbColumnModel, AtomDbColumn>(model);
            en.AddTime = DateTime.Now;
            en.EditTime = DateTime.Now;
            en.AddUserId = 0;
            en.EditUserId = 0;
            en.IsValid = true;

            var tran = SonFact.Cur.BeginTransaction();
            try
            {
                var res = SonFact.Cur.Insert(en);
                var baseTable = SonFact.Cur.Find<AtomDbTable>(model.DbTableId);
                var doSql = AStarterSqlGen.CreateColSql(model, baseTable);
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

        #endregion


        #region 数据库查询
        public AtomSqlExeModel SqlQuery(string sql)
        {
            var ds = SonFact.Cur.ExecuteQuery(sql);
            var cols = (from DataColumn col in ds.Tables[0].Columns select col.ColumnName).ToList();
            return new AtomSqlExeModel { ColumnNames = cols, ResultTable = ds.Tables[0] };
        }

        public AtomSqlExeModel SqlExecute(string sql)
        {
            var cnt = SonFact.Cur.ExecuteSql(sql);
            return new AtomSqlExeModel { RowCnt = Convert.ToInt32(cnt) };
        }

        #endregion


        public List<AtomSearchModel> ASearch(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new Exception("关键字不能为空");
            if (key.Length < 2) throw new Exception("关键字至少2个字符");


            var sql = $@"declare @key nvarchar(100)='{key}'
                                select * from (
                                select '需求管理' TbName,'需求模块' ColName,Id, [Name] from AtomProjectDoc where [Name] like '%'+@key+'%' union all
                                select '需求管理' TbName,'需求描述' ColName,Id, [Desc] from AtomProjectDoc where [Desc] like '%'+@key+'%'union all
                                select '需求管理' TbName,'存在问题' ColName,Id, Questions from AtomProjectDoc where Questions like '%'+@key+'%'union all
                                select '需求管理' TbName,'备注' ColName,Id, Remark from AtomProjectDoc where Remark like '%'+@key+'%' union all

                                select '数据库管理' TbName,'表名称' ColName,Id, [Name] from [AtomDbTable] where [Name] like '%'+@key+'%' union all
                                select '数据库管理' TbName,'数据库表名' ColName,Id, [DbTableName] from [AtomDbTable] where [DbTableName] like '%'+@key+'%' union all

                                select '数据库管理' TbName,'数据库字段名' ColName,Id, DbColumnName from [AtomDbColumn] where DbColumnName like '%'+@key+'%'union all
                                select '数据库管理' TbName,'字段说明' ColName,Id, [Desc] from [AtomDbColumn] where [Desc] like '%'+@key+'%') aa order by TbName";

            var result = SonFact.Cur.ExecuteQuery<AtomSearchModel>(sql).Take(1000).ToList();
            return result;
        }


    }
}
