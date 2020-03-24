using Atom.EF.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atom.EF.Core
{
    public interface IAContextEF6
    {
        DbContextOption Option { get; }

        void IsEntityValid<T>(T entity) where T : BaseEntity;

        DbEntityEntry Entry<T>(T entity) where T : BaseEntity;
       DbSet<T> Set<T>() where T : class;
        int SaveChanges();

        public Database Database { get; }

    }
}
