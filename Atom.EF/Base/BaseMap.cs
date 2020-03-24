using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Atom.EF.Base
{
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T : BaseEntity, new()
    {
        protected BaseMap()
        {
            Init();
        }
        /// <summary>
        /// 初始化代码
        /// </summary>
        public virtual void Init()
        {
            Console.WriteLine("Init");
        }
    }
}
