using Microsoft.EntityFrameworkCore;
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

namespace Atom.EF.Core
{
    public class AContext : DbContext, IAContext
    {
        public DbContextOption Option { get; }

        public AContext(DbContextOption option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));
            if (string.IsNullOrEmpty(option.ConnectionString))
                throw new ArgumentNullException(nameof(option.ConnectionString));
            if (string.IsNullOrEmpty(option.ModelAssemblyName))
                throw new ArgumentNullException(nameof(option.ModelAssemblyName));
            Option = option;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Option.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void AddModels(ModelBuilder modelBuilder)
        {
            if (string.IsNullOrEmpty(Option.ModelAssemblyName)) throw new Exception("没有实体程序集配置");

            var assembly = Assembly.Load(Option.ModelAssemblyName);
            var types = assembly?.GetTypes(); ;
            var list = types?.Where(t => t.IsSubclassOf(typeof(BaseEntity))).ToList();
            if (list == null || !list.Any()) throw new Exception("实体程序集没有任何实体");

            list.ForEach(t =>
            {
                var entityType = modelBuilder.Model.FindEntityType(t);
                if (entityType == null) modelBuilder.Model.AddEntityType(t);
            });
        }

        //public DatabaseFacade GetDatabase() => Database;

        public new DatabaseFacade Database { get { return base.Database; } }


        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public virtual bool EnsureCreated()
        {
            return Database.EnsureCreated();
        }

        public void IsEntityValid<T>(T entity) where T : BaseEntity
        {
            var vEntity = new ValidationContext(entity);
            var vResults = new List<ValidationResult>();

            var succcess = Validator.TryValidateObject(entity, vEntity, vResults, true);
            if (!succcess) throw new Exception(string.Join("\n", vResults.Select(r => r.ErrorMessage)));
        }

        public  List<T> SqlQuery<T>(string sql, params object[] parameters) where T : class
        {
            return base.Set<T>().FromSqlRaw(sql, parameters).ToList();
        }

        public virtual void BulkInsert<T>(IList<T> entities, string destinationTableName = null) where T : class
        {
            if (entities == null || !entities.Any()) return;
            if (string.IsNullOrEmpty(destinationTableName))
            {
                var mappingTableName = typeof(T).GetCustomAttribute<TableAttribute>()?.Name;
                destinationTableName = string.IsNullOrEmpty(mappingTableName) ? typeof(T).Name : mappingTableName;
            }
            SqlBulkInsert<T>(entities, destinationTableName);
        }

        private void SqlBulkInsert<T>(IList<T> entities, string destinationTableName = null) where T : class
        {
            using (var dt = entities.ToDataTable())
            {
                dt.TableName = destinationTableName;
                var conn = (SqlConnection)Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran)
                        {
                            BatchSize = entities.Count,
                            DestinationTableName = dt.TableName,
                        };
                        GenerateColumnMappings<T>(bulk.ColumnMappings);
                        bulk.WriteToServerAsync(dt);
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
                conn.Close();
            }
        }

        private void GenerateColumnMappings<T>(SqlBulkCopyColumnMappingCollection mappings)
            where T : class
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetCustomAttributes<KeyAttribute>().Any())
                {
                    mappings.Add(new SqlBulkCopyColumnMapping(property.Name, typeof(T).Name + property.Name));
                }
                else
                {
                    mappings.Add(new SqlBulkCopyColumnMapping(property.Name, property.Name));
                }
            }
        }

        public virtual DataTable GetDataTable(string sql, params DbParameter[] parameters)
        {
            return GetDataTables(sql, parameters).FirstOrDefault();
        }

        public virtual List<DataTable> GetDataTables(string sql, params DbParameter[] parameters)
        {
            var dts = new List<DataTable>();
            //TODO： connection 不能dispose 或者 用using，否则下次获取connection会报错提示“the connectionstring property has not been initialized。”
            var connection = Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (var cmd = new SqlCommand(sql, (SqlConnection)connection))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                using (var da = new SqlDataAdapter(cmd))
                {
                    using (var ds = new DataSet())
                    {
                        da.Fill(ds);
                        foreach (DataTable table in ds.Tables)
                        {
                            dts.Add(table);
                        }
                    }
                }
            }
            connection.Close();

            return dts;
        }


    }
}
