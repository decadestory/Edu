using Atom.EF.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atom.EF.Core
{
    public interface IAContext
    {
        DbContextOption Option { get; }
        DatabaseFacade GetDatabase();
        DbSet<T> Set<T>() where T : BaseEntity;
        int SaveChanges();
        bool EnsureCreated();

        void IsEntityValid<T>(T entity) where T : BaseEntity;

        void BulkInsert<T>(IList<T> entities, string destinationTableName = null) where T : class;
        DataTable GetDataTable(string sql, params DbParameter[] parameters);
    }
}
