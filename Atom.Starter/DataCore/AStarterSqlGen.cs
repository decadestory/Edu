using Atom.Starter.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Starter.DataCore
{
    internal class AStarterSqlGen
    {

        public static string GenTableColumnSql()
        {
            var sql = @"if not exists(select 1 from AtomDbTable)
                                    begin
	                                    insert into AtomDbTable(Name,DbTableName,ProjectId,AddTime,AddUserId,EditTime,EditUserId,IsValid)
	                                    select cast(ep.value as nvarchar(max)),o.name,o.object_id,GETDATE(),1,GETDATE(),1,1 from sys.objects o
                                        LEFT JOIN sys.extended_properties ep ON ep.major_id = o.object_id and ep.minor_id=0
                                        where o.type='U' and o.name not like '%Atom%'

	                                    select * into #temp_adt from AtomDbTable
	                                    declare @tid int;
	                                    declare @oid int;
	                                    declare @oname varchar(500);
	                                    while exists (select 1 from #temp_adt)
	                                    begin
		                                    set @tid=0;
		                                    set @oid=0;
		                                    set @oname='';
		                                    select top 1 @tid =id,@oid=ProjectId,@oname=DbTableName from #temp_adt;

		                                    insert AtomDbColumn(DbTableId,[Name],DbColumnName,DbType,IsNull,IsIdentity,[Desc],AddTime,AddUserId,EditTime,EditUserId,IsValid,IsPrimary)
		                                    select @tid,Convert(varchar,ep.value),Convert(varchar,c.name),
		                                    (case when t.name='varchar' or t.name='char' or t.name='nvarchar' or t.name='nchar' then convert(varchar,t.name)+'('+ case when c.max_length=-1 then 'max' else convert(varchar,c.max_length) end+')' else convert(varchar,t.name) end) DbType,
		                                    c.is_nullable,c.is_identity,Convert(varchar,ep.value),getdate(),1,getdate(),1,1,

                                            (select count(1) from sys.indexes ii,sys.all_columns cc,sys.all_objects oo,sys.key_constraints kk
                                            where oo.object_id = @oid and oo.object_id = cc.object_id and oo.object_id = ii.object_id and
                                            kk.parent_object_id = oo.object_id and kk.unique_index_id = ii.index_id and ii.is_primary_key = 1 and cc.name=c.name and
                                            (cc.name = index_col(@oname, ii.index_id,  1) or
                                            cc.name = index_col (@oname, ii.index_id,  2) or
                                            cc.name = index_col (@oname, ii.index_id,  3) or
                                            cc.name = index_col (@oname, ii.index_id,  4) or
                                            cc.name = index_col (@oname, ii.index_id,  5) or
                                            cc.name = index_col (@oname, ii.index_id,  6) or
                                            cc.name = index_col (@oname, ii.index_id,  7) or
                                            cc.name = index_col (@oname, ii.index_id,  8) or
                                            cc.name = index_col (@oname, ii.index_id,  9) or
                                            cc.name = index_col (@oname, ii.index_id, 10) or
                                            cc.name = index_col (@oname, ii.index_id, 11) or
                                            cc.name = index_col (@oname, ii.index_id, 12) or
                                            cc.name = index_col (@oname, ii.index_id, 13) or
                                            cc.name = index_col (@oname, ii.index_id, 14) or
                                            cc.name = index_col (@oname, ii.index_id, 15) or
                                            cc.name = index_col (@oname, ii.index_id, 16)))

		                                    from sys.columns c
		                                    LEFT JOIN sys.extended_properties ep ON ep.major_id = c.object_id AND ep.minor_id = c.column_id 
		                                    left join sys.types t on c.user_type_id=t.user_type_id
		                                    where object_id=@oid;

		                                    delete #temp_adt where Id=@tid
	                                    end
	                                    drop table #temp_adt;
                                    end";
            return sql;
        }

        public static string CreateTableSql(string tableName, string keyName, long tableId, string tableDesc)
        {
            var sql = $@"CREATE TABLE [dbo].[{tableName}](
                                [{keyName}] [int] IDENTITY(1,1) NOT NULL,
                                [AddTime] [datetime] NOT NULL,
                                [AddUserId] [int] NOT NULL,
                                [EditTime] [datetime] NOT NULL,
                                [EditUserId] [int] NOT NULL,
                                [IsValid] [bit] NOT NULL,
                                CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED([{keyName}] ASC));

                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'{tableDesc}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'{keyName}'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'AddTime'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'AddUserId'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'EditTime'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'EditUserId'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'IsValid'

                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid],IsIdentity) 
                                VALUES ({tableId}, '主键Id', N'{keyName}', N'int', 0, 1, NULL, GETDATE(), 1, GETDATE(), 1, 1,1)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid],IsIdentity) 
                                VALUES ( {tableId}, '添加时间', N'AddTime', N'datetime', 0, 0, NULL, GETDATE(), 1, GETDATE(), 1, 1,0)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid],IsIdentity) 
                                VALUES ({tableId}, '添加人', N'AddUserId', N'int', 0, 0, NULL, GETDATE(), 1, GETDATE(), 1, 1,0)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid],IsIdentity) 
                                VALUES ({tableId}, '修改时间', N'EditTime', N'datetime', 0, 0, NULL, GETDATE(), 1, GETDATE(), 1, 1,0)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid],IsIdentity) 
                                VALUES ({tableId}, '修改人', N'EditUserId', N'int', 0, 0, NULL, GETDATE(), 1, GETDATE(), 1, 1,0)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid],IsIdentity) 
                                VALUES ({tableId}, '是否可用', N'IsValid', N'bit', 0, 0, NULL,GETDATE(), 1, GETDATE(), 1, 1,0)";
            return sql;
        }

        public static string CreateColSql(AtomDbColumnModel model, AtomDbTable tb)
        {
            var sql = new StringBuilder();
            var nullSql = model.IsPrimary || !model.IsNull ? "NOT NULL"  : "NULL";
            var idsql = model.IsIdentity && (model.DbType.ToLower() == "int" || model.DbType.ToLower() == "bigint") ? "IDENTITY (1,1)" : "";
            var desc = model.Name + "^" + model.Desc;

            sql.AppendLine($@"ALTER TABLE [{tb.DbTableName}] ADD [{model.DbColumnName}] {model.DbType} {nullSql} {idsql};");
            if (model.IsPrimary) sql.AppendLine($@" ALTER TABLE [{tb.DbTableName}] ADD constraint  [PK_{tb.Name}] PRIMARY KEY({model.DbColumnName}) ;");
            sql.AppendLine($@"EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'{desc}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tb.DbTableName}', @level2type=N'COLUMN',@level2name=N'{model.DbColumnName}'");

            return sql.ToString();
        }

    }
}
