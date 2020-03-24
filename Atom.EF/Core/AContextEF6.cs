using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Atom.EF.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;
using System.Data.Common;
using System.Threading;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Atom.EF.Extention;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Atom.EF.Core
{
    public class AContextEF6 : DbContext, IAContextEF6
    {
        public DbContextOption Option { get; }

        public AContextEF6(DbContextOption option) : base(option.ConnectionString)
        {
            Option = option;

            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            var isCreateDb = 0;

            switch (isCreateDb)
            {
                case 0:
                    //从不创建数据库
                    Database.SetInitializer<AContextEF6>(null);
                    break;
                case 1:
                    //数据库不存在时重新创建数据库
                    Database.SetInitializer(new CreateDatabaseIfNotExists<AContextEF6>());
                    break;
                case 2:
                    //每次启动应用程序时创建数据库
                    Database.SetInitializer(new DropCreateDatabaseAlways<AContextEF6>());
                    break;
                case 3:
                    //模型更改时重新创建数据库
                    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AContextEF6>());
                    break;
            }

            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            AddModels(modelBuilder);
            //modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());

            base.OnModelCreating(modelBuilder);
        }


        private void AddModels(DbModelBuilder modelBuilder)
        {
            if (string.IsNullOrEmpty(Option.ModelAssemblyName)) throw new Exception("没有实体程序集配置");

            var assembly = Assembly.Load(Option.ModelAssemblyName);
            var types = assembly?.GetTypes(); ;
            var list = types?.Where(t => t.FullName.EndsWith("Mapper")).ToList();
            if (list == null || !list.Any()) throw new Exception("实体程序集没有任何实体");

            list.ForEach(t =>
            {
                dynamic configurationInstance = Activator.CreateInstance(t);
                modelBuilder.Configurations.Add(configurationInstance);
            });
        }

        public new DbEntityEntry Entry<T>(T entity) where T:BaseEntity
        {
            return base.Entry<T>(entity);
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new Database Database { get { return base.Database; } }


        public void IsEntityValid<T>(T entity) where T : BaseEntity
        {
            var vEntity = new ValidationContext(entity);
            var vResults = new List<ValidationResult>();

            var succcess = Validator.TryValidateObject(entity, vEntity, vResults, true);
            if (!succcess) throw new Exception(string.Join("\n", vResults.Select(r => r.ErrorMessage)));
        }


    }
}
