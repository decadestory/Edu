using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Atom.EF.Extention
{
    public static class AtomExtention
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            DataTable dtReturn = new DataTable();


            if (source == null) return dtReturn;
            // column names 
            PropertyInfo[] oProps = null;

            foreach (var rec in source)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (var pi in oProps)
                    {
                        var colType = pi.PropertyType;

                        if (colType.IsNullable())
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        if (colType == typeof(Boolean))
                        {
                            colType = typeof(int);
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                var dr = dtReturn.NewRow();

                foreach (var pi in oProps)
                {
                    var value = pi.GetValue(rec, null) ?? DBNull.Value;
                    if (value is bool)
                    {
                        dr[pi.Name] = (bool)value ? 1 : 0;
                    }
                    else
                    {
                        dr[pi.Name] = value;
                    }
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static bool IsNullable(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

    }
}
