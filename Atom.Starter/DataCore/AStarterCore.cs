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
        public bool CheckOrCreateDb()
        {
            SonFact.Cur.CreateTable<AtomProject>();
            SonFact.Cur.CreateTable<AtomProjectDoc>();
            SonFact.Cur.CreateTable<AtomDbTable>();
            SonFact.Cur.CreateTable<AtomDbColumn>();
            SonFact.Cur.CreateTable<AtomStarterLog>();
            return true;
        }

        public long AddOrEditProject(AtomProjectModel model)
        {
            if (model.Id > 0) goto edit;
            var en = EntityMapper.Mapper<AtomProjectModel, AtomProject>(model);
            en.AddTime = DateTime.Now;
            en.EditTime = DateTime.Now;
            en.AddUserId = 0;
            en.EditUserId = 0;
            en.IsValid = true;
            var res = SonFact.Cur.Insert(en);
            return res;
            edit:
            if (!model.IsValid.HasValue) return SonFact.Cur.Delete<AtomProject>(model.Id);
            var edn = EntityMapper.Mapper<AtomProjectModel, AtomProject>(model);
            edn.EditTime = DateTime.Now;
            edn.EditUserId = 0;
            var rows = SonFact.Cur.Update(edn);
            return rows;
        }

        public bool CreateDb(AtomProjectModel model) 
        {
            if (model.Id <= 0) throw new Exception("请选择项目");
            var en = SonFact.Cur.Find<AtomProject>(model.Id);

            var sql = string .Format("if not exists(select top 1 * from sys.databases where name='test') create database '{0}' ", en.DbName);

            return true;
        }
        

    }
}
