using Atom.Generator.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Atom.Generator.DataCore
{
    internal class GeneratorCore
    {

        public static void Generate(string tableName) 
        {
            Console.WriteLine("=====================");
            Console.WriteLine("Generate Entity...");
            var cols =GetColumns(tableName);
            FileHelper.CreateEntityFile(tableName, cols);
            Console.WriteLine("Generate Entity Success !!");
            Console.WriteLine("=====================");


            //Console.WriteLine("=====================");
            //Console.WriteLine("Generate Model...");
            //FileHelper.CreateModelFile(tableName, cols);
            //Console.WriteLine("Generate Model Success !!");
            //Console.WriteLine("=====================");

            //Console.WriteLine("=====================");
            //Console.WriteLine("Generate Reposity...");
            //FileHelper.CreateRepolFile(tableName);
            //Console.WriteLine("Generate Reposity Success !!");
            //Console.WriteLine("=====================");


            //Console.WriteLine("=====================");
            //Console.WriteLine("Generate Service...");
            //FileHelper.CreateSvclFile(tableName);
            //Console.WriteLine("Generate Service Success !!");
            //Console.WriteLine("=====================");


            //Console.WriteLine("=====================");
            //Console.WriteLine("Generate Controller...");
            //FileHelper.CreateCtrllFile(tableName);
            //Console.WriteLine("Generate Controller Success !!");
            //Console.WriteLine("=====================");

        }

        public static List<Column> GetColumns(string tableName)
        {
                var sql = @"select 
                cast(ep.value as varchar) [Description],
                (case when  ty.[name] in ('text','ntext' ,'char','nchar', 'varchar', 'nvarchar') then 'string'
                when ty.[name] in ('date' , 'datetime' , 'datetime2','smalldatetime') then 'DateTime'
                when ty.[name] in ('bit') then 'bool'
                when ty.[name] in ('smallint') then 'short'
                when ty.[name] in ('bigint') then 'long'
                when ty.[name] in ('real') then 'float'
                when ty.[name] in ('float') then 'double'
                when ty.[name] in ('money') then 'decimal'
                when ty.[name] in ('uniqueidentifier') then 'Guid'
                else ty.[name] end) as TypeName,
                (case c.[is_nullable] when 1 then case when ty.[name] not in('text','ntext' ,'char','nchar', 'varchar', 'nvarchar') then '?' else '' end
                 else '' end) as NullString,c.name Name ,' {set;get;}' as EndString from sys.tables t
                INNER JOIN sys.columns c ON t.object_id = c.object_id
                LEFT JOIN sys.extended_properties ep ON ep.major_id = c.object_id AND ep.minor_id = c.column_id 
                left JOIN sys.types ty on ty.[system_type_id]=c.[user_type_id] and ty.[name]!='sysname'
                WHERE ep.class =1 AND t.name='" + tableName + "' or c.object_id=Object_Id('" + tableName + "')";
                var data = SonFact.Cur.ExecuteQuery<Column>(sql);
                return data;
        }


    }
}
