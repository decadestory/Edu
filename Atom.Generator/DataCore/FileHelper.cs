using Atom.Generator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Atom.Generator.DataCore
{
    class FileHelper
    {
        static string path = "D:/GitHub/Edu/Edu/";
        static string nspace = "Edu";
        static string author = "Jerry";

        static string pathEntity = path + nspace + ".Entity/";
        static string pathModel = path + nspace + ".Model/";
        static string pathRepo = path + nspace + ".Repo/";
        static string pathSvc = path + nspace + ".Svc/";
        static string pathCtrl = path + nspace + ".Api/Controllers/";
        static string pathIRepo = path + nspace + ".Repo/Interface/";
        static string pathISvc = path + nspace + ".Svc/Interface/";
        static string pathEnMapper = path + nspace + ".Entity/Mapper/";



        public static void CreateEntityFile(string tableName, List<Column> cols)
        {
            if (!Directory.Exists(pathEntity)) Directory.CreateDirectory(pathEntity);
            var fileName = pathEntity + tableName + ".cs";
            if (File.Exists(fileName)) return;
            File.Delete(fileName);
            var fileString = GetEntityString(tableName, cols);
            WriteFile(fileName, fileString);
        }

        public static void CreateEntityMapperFile(string tableName, List<Column> cols)
        {
            if (!Directory.Exists(pathEnMapper)) Directory.CreateDirectory(pathEnMapper);
            var fileName = pathEnMapper + tableName + "Mapper.cs";
            if (File.Exists(fileName)) return;
            File.Delete(fileName);
            var fileString = GetEntityMapperString(tableName, cols);
            WriteFile(fileName, fileString);
        }

        public static void CreateModelFile(string tableName, List<Column> cols)
        {
            if (!Directory.Exists(pathModel)) Directory.CreateDirectory(pathModel);
            var fileName = pathModel + tableName + "Model.cs";
            if (File.Exists(fileName)) return;
            var fileString = GetModelString(tableName, cols);
            WriteFile(fileName, fileString);
        }

        public static void CreateRepolFile(string tableName)
        {
            if (!Directory.Exists(pathRepo)) Directory.CreateDirectory(pathRepo);
            if (!Directory.Exists(pathIRepo)) Directory.CreateDirectory(pathIRepo);

            var fileName = pathRepo + tableName + "Repo.cs";
            if (File.Exists(fileName)) return;
            var fileString = GetRepoString(tableName, false);
            WriteFile(fileName, fileString);

            fileName = pathIRepo + "I" + tableName + "Repo.cs";
            if (File.Exists(fileName)) return;
            fileString = GetRepoString(tableName, true);
            WriteFile(fileName, fileString);
        }

        public static void CreateSvclFile(string tableName)
        {
            if (!Directory.Exists(pathSvc)) Directory.CreateDirectory(pathSvc);
            if (!Directory.Exists(pathISvc)) Directory.CreateDirectory(pathISvc);

            var fileName = pathSvc + tableName + "Svc.cs";
            if (File.Exists(fileName)) return;
            var fileString = GetSvcString(tableName, false);
            WriteFile(fileName, fileString);

            fileName = pathISvc + "I" + tableName + "Svc.cs";
            if (File.Exists(fileName)) return;
            fileString = GetSvcString(tableName, true);
            WriteFile(fileName, fileString);
        }

        public static void CreateCtrllFile(string tableName)
        {
            if (!Directory.Exists(pathCtrl)) Directory.CreateDirectory(pathCtrl);

            var fileName = pathCtrl + tableName + "Controller.cs";
            if (File.Exists(fileName)) return;
            var fileString = GetCtrlString(tableName);
            WriteFile(fileName, fileString);
        }

        public static string GetEntityString(string tableName, List<Column> cols)
        {
            var sb = new StringBuilder();

            sb.AppendLine("/*******************************************************");
            sb.AppendLine("*创建作者： " + author);
            sb.AppendLine("*类的名称： " + tableName);
            sb.AppendLine("*命名空间： " + nspace + ".Entity");
            sb.AppendLine("*创建时间： " + DateTime.Now.ToShortDateString());
            sb.AppendLine("********************************************************/");

            sb.AppendLine("using Atom.EF.Base;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");

            sb.AppendLine("namespace " + nspace + ".Entity");
            sb.AppendLine("{");
            sb.AppendLine("\t public class " + tableName + " : BaseEntity");
            sb.AppendLine("\t {");

            foreach (var item in cols)
            {
                var listFilter = new List<string> { "AddTime", "EditTime", "AddUserId", "EditUserId", "IsValid" };
                if (listFilter.Contains(item.Name)) continue;

                if (!string.IsNullOrEmpty(item.Description))
                {
                    sb.AppendLine("\t\t /// <summary>");
                    sb.AppendLine("\t\t /// " + item.Description);
                    sb.AppendLine("\t\t /// <summary>");
                }

                sb.AppendLine("\t\t public " + item.TypeName + item.NullString + " " + item.Name + " " + item.EndString);
            }

            sb.AppendLine("\t }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static string GetEntityMapperString(string tableName, List<Column> cols)
        {
            var cf = cols.FirstOrDefault();

            var sb = new StringBuilder();

            sb.AppendLine("/*******************************************************");
            sb.AppendLine("*创建作者： " + author);
            sb.AppendLine("*类的名称： " + tableName);
            sb.AppendLine("*命名空间： " + nspace + ".Entity.Mapper");
            sb.AppendLine("*创建时间： " + DateTime.Now.ToShortDateString());
            sb.AppendLine("********************************************************/");

            sb.AppendLine("using Atom.EF.Base;");
            sb.AppendLine("using System;");

            sb.AppendLine("namespace " + nspace + ".Entity.Mapper");
            sb.AppendLine("{");
            sb.AppendLine("\t public class " + tableName + "Mapper : BaseMap<" + tableName + ">");
            sb.AppendLine("\t {");

            sb.AppendLine("\t\tpublic override void Init()");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tToTable(\"" + tableName + "\");");
            sb.AppendLine("\t\t\tHasKey(m => m." + cf.Name + ");");
            sb.AppendLine("\t\t}");

            sb.AppendLine("\t }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static string GetModelString(string tableName, List<Column> cols)
        {
            var sb = new StringBuilder();

            sb.AppendLine("/*******************************************************");
            sb.AppendLine("*创建作者： " + author);
            sb.AppendLine("*类的名称： " + tableName + "Model");
            sb.AppendLine("*命名空间： " + nspace + ".Model");
            sb.AppendLine("*创建时间： " + DateTime.Now.ToShortDateString());
            sb.AppendLine("********************************************************/");

            sb.AppendLine("");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            sb.AppendLine("");

            sb.AppendLine("namespace " + nspace + ".Model");
            sb.AppendLine("{");
            sb.AppendLine("\t public class " + tableName + "Model");
            sb.AppendLine("\t {");

            foreach (var item in cols) sb.AppendLine("\t\t public " + item.TypeName + item.NullString + " " + item.Name + " " + item.EndString);

            sb.AppendLine("\t }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static string GetRepoString(string tableName, bool isInterface = false)
        {
            var sb = new StringBuilder();

            if (isInterface == true) goto repoInter;

            sb.AppendLine("/*******************************************************");
            sb.AppendLine("*创建作者： " + author);
            sb.AppendLine("*类的名称： " + tableName + "Repo");
            sb.AppendLine("*命名空间： " + nspace + ".Repo");
            sb.AppendLine("*创建时间： " + DateTime.Now.ToShortDateString());
            sb.AppendLine("********************************************************/");

            sb.AppendLine("");
            sb.AppendLine("using Atom.EF.Base;");
            sb.AppendLine("using Edu.Entity;");
            sb.AppendLine("using Edu.Model;");
            sb.AppendLine("using Edu.Repo.Interface;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using Atom.EF.Core;");
            sb.AppendLine("using System;");
            sb.AppendLine("");

            sb.AppendLine("namespace " + nspace + ".Repo");
            sb.AppendLine("{");
            sb.AppendLine($"\t public class {tableName}Repo : BaseRepo<{tableName}>, I{tableName}Repo");
            sb.AppendLine("\t {");
            sb.AppendLine("\t\t public IAContextEF6 db { get; set; }");

            sb.AppendLine("\t }");
            sb.AppendLine("}");
            return sb.ToString();


            repoInter:

            sb.AppendLine("/*******************************************************");
            sb.AppendLine("*创建作者： " + author);
            sb.AppendLine("*类的名称： I" + tableName + "Repo");
            sb.AppendLine("*命名空间： " + nspace + ".Repo.Interface");
            sb.AppendLine("*创建时间： " + DateTime.Now.ToShortDateString());
            sb.AppendLine("********************************************************/");

            sb.AppendLine("");

            sb.AppendLine("using Atom.EF.Base.Interface;");
            sb.AppendLine("using Edu.Entity;");
            sb.AppendLine("using Edu.Model;");
            sb.AppendLine("using System;");
            sb.AppendLine("");

            sb.AppendLine("namespace " + nspace + ".Repo.Interface");
            sb.AppendLine("{");
            sb.AppendLine($"\t public interface I{tableName}Repo :  IBaseRepo<{tableName}>");
            sb.AppendLine("\t {");

            sb.AppendLine("\t }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static string GetSvcString(string tableName, bool isInterface = false)
        {
            var sb = new StringBuilder();

            if (isInterface == true) goto svcInter;

            sb.AppendLine("/*******************************************************");
            sb.AppendLine("*创建作者： " + author);
            sb.AppendLine("*类的名称： " + tableName + "Svc");
            sb.AppendLine("*命名空间： " + nspace + ".Svc");
            sb.AppendLine("*创建时间： " + DateTime.Now.ToShortDateString());
            sb.AppendLine("********************************************************/");

            sb.AppendLine("");
            sb.AppendLine("using Atom.EF.Base;");
            sb.AppendLine("using Edu.Entity;");
            sb.AppendLine("using Edu.Model;");
            sb.AppendLine("using Edu.Repo.Interface;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using Edu.Svc.Interface;");
            sb.AppendLine("using System;");
            sb.AppendLine("");

            sb.AppendLine("namespace " + nspace + ".Svc");
            sb.AppendLine("{");
            sb.AppendLine($"\t public class {tableName}Svc : BaseSvc, I{tableName}Svc");
            sb.AppendLine("\t {");
            sb.AppendLine("\t\t public  I"+tableName+"Repo rep { get; set; }");

            sb.AppendLine("\t }");
            sb.AppendLine("}");
            return sb.ToString();


            svcInter:

            sb.AppendLine("/*******************************************************");
            sb.AppendLine("*创建作者： " + author);
            sb.AppendLine("*类的名称： I" + tableName + "Svc");
            sb.AppendLine("*命名空间： " + nspace + ".Svc.Interface");
            sb.AppendLine("*创建时间： " + DateTime.Now.ToShortDateString());
            sb.AppendLine("********************************************************/");

            sb.AppendLine("");

            sb.AppendLine("using Atom.EF.Base.Interface;");
            sb.AppendLine("using Edu.Entity;");
            sb.AppendLine("using Edu.Model;");
            sb.AppendLine("using System;");
            sb.AppendLine("");

            sb.AppendLine("namespace " + nspace + ".Svc.Interface");
            sb.AppendLine("{");
            sb.AppendLine($"\t public interface I{tableName}Svc :  IBaseSvc");
            sb.AppendLine("\t {");

            sb.AppendLine("\t }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static string GetCtrlString(string tableName)
        {
            var sb = new StringBuilder();

            sb.AppendLine("/*******************************************************");
            sb.AppendLine("*创建作者： " + author);
            sb.AppendLine("*类的名称： " + tableName + "Controller");
            sb.AppendLine("*命名空间： " + nspace + ".Api.Controllers");
            sb.AppendLine("*创建时间： " + DateTime.Now.ToShortDateString());
            sb.AppendLine("********************************************************/");

            sb.AppendLine("");
            sb.AppendLine("using Atom.Lib;");
            sb.AppendLine(" using Atom.Logger;");
            sb.AppendLine("using Edu.Model;");
            sb.AppendLine("using Edu.Api.Infrastructure.Filters;");
            sb.AppendLine(" using Edu.Svc.Interface;");
            sb.AppendLine("using Microsoft.AspNetCore.Http;");
            sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
            sb.AppendLine("using System;");
            sb.AppendLine("");


            sb.AppendLine("namespace " + nspace + ".Api.Controllers");
            sb.AppendLine("{");
            sb.AppendLine($"\t   public class {tableName}Controller : BaseController");
            sb.AppendLine("\t {");
            sb.AppendLine("\t\t public I"+tableName+"Svc svc { get; set; }");
            sb.AppendLine("\t\t public IALogger logger { get; set; }");

            sb.AppendLine("\t }");
            sb.AppendLine("}");
            return sb.ToString();

        }





        public static void WriteFile(string fileName, string fileString)
        {
            using (var sw = new StreamWriter(fileName, true))
            {
                sw.Write(fileString);
                sw.Flush();
            }
        }


    }
}
