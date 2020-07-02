using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Starter.DataCore
{
    internal class AStarterSqlGen
    {
        public static string CreateTableSql(string tableName,string keyName,long tableId)
        {
            var sql = $@"CREATE TABLE [dbo].[{tableName}](
                                [{keyName}] [int] IDENTITY(1,1) NOT NULL,
                                [AddTime] [datetime] NULL,
                                [AddUserId] [int] NOT NULL,
                                [EditTime] [datetime] NULL,
                                [EditUserId] [int] NOT NULL,
                                [IsValid] [bit] NOT NULL,
                                CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED([{keyName}] ASC));

                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'{keyName}'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'AddTime'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'AddUserId'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'EditTime'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'EditUserId'
                                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{tableName}', @level2type=N'COLUMN',@level2name=N'IsValid'

                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid]) 
                                VALUES ({tableId}, '主键Id', N'{keyName}', N'int', 0, 1, NULL, GETDATE(), 1, GETDATE(), 1, 1)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid]) 
                                VALUES ( {tableId}, '添加时间', N'AddTime', N'datetime', 0, 0, NULL, GETDATE(), 1, GETDATE(), 1, 1)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid]) 
                                VALUES ({tableId}, '添加人', N'AddUserId', N'int', 0, 0, NULL, GETDATE(), 1, GETDATE(), 1, 1)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid]) 
                                VALUES ({tableId}, '修改时间', N'EditTime', N'datetime', 0, 0, NULL, GETDATE(), 1, GETDATE(), 1, 1)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid]) 
                                VALUES ({tableId}, '修改人', N'EditUserId', N'int', 0, 0, NULL, GETDATE(), 1, GETDATE(), 1, 1)
                                INSERT [dbo].[AtomDbColumn] ([DbTableId], [Name], [DbColumnName], [DbType], [IsNull], [IsPrimary], [Desc], [AddTime], [AddUserId], [EditTime], [EditUserId], [IsValid]) 
                                VALUES ({tableId}, '是否可用', N'IsValid', N'bit', 0, 0, NULL,GETDATE(), 1, GETDATE(), 1, 1)";
            return sql;
        }
    }
}
