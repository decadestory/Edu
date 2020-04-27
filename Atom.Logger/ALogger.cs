using Atom.Logger.DataCore;
using Atom.Logger.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using static Atom.Logger.DataCore.ALoggerDataCore;

namespace Atom.Logger
{
    public class ALogger : IALogger
    {
        internal ALoggerDataCore Logmgt = new ALoggerDataCore();
        internal string ServerSrc = null;
        public ALogger(string dbConnStr, string serverSrc = null)
        {
            SonFact.init(dbConnStr);
            ServerSrc = serverSrc;
            Logmgt.CheckOrCreateDb();
        }

        public bool BInfo(string txt, string logType = "std", string logTypeName = "", int relId = 0, string user = null)
        {
            var log = new AtomOpreateLogger()
            {
                LogType = logType,
                LogTypeName = logTypeName,
                RelId = relId,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
            };

            return Logmgt.Add(log) > 0;
        }

        public List<AtomOpreateLoggerModel> GetBLogs(int relId)
        {
            return Logmgt.GetBLogs(relId);
        }


        public bool Debug(string txt, string logType = "std", string user = null)
        {
            var conf = Logmgt.GetLogConfig();
            if (conf == null || conf.IsDebug != true) return true;

            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Debug,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public bool Info(string txt, string logType = "std", string user = null)
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Info,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public bool Warn(string txt, string logType = "std", string user = null)
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Warn,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public bool Error(string txt, string logType = "std", string user = null)
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Error,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public bool Fatal(string txt, string logType = "std", string user = null)
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Fatal,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = txt,
                ContentLength = 0,
                Duration = 0,
                LogUrl = null,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public bool Monitor(string logUrl = "", string user = null, long? contentLength = 0, long duration = 0, string logType = "monitor", string param = "")
        {
            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Monitor,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = param,
                LogUrl = logUrl,
                ContentLength = contentLength,
                Duration = duration,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public bool Exception(Exception ex, string logType = "exception", string user = null, string txt = "")
        {
            var innerEx = ex.InnerException != null ? ex.InnerException.Message : "";

            var log = new AtomLogger()
            {
                LogType = logType,
                LogLevel = (int)LogLevel.Exception,
                LogUser = user,
                AddTime = DateTime.Now,
                LogTxt = ex.Message + " | " + innerEx,
                LogUrl = ex.StackTrace,
                ContentLength = 0,
                Duration = 0,
                ServerSrc = ServerSrc
            };

            return Logmgt.Add(log) > 0;
        }

        public void LogToFile(string txt, int level = 1)
        {
            Logmgt.SaveToFile(txt, level);
        }

        public Tuple<List<AtomLoggerModel>, long> GetLogList(AtomLoggerModel request)
        {
            var result = Logmgt.GetLogList(request);
            return result;
        }


        public string Html()
        {
            var html = "<!DOCTYPE html><html><head>    <meta charset=\"UTF-8\">    <link href=\"https://cdn.bootcdn.net/ajax/libs/element-ui/2.13.1/theme-chalk/index.css\" rel=\"stylesheet\"></head><body>    <div id=\"app\">        <el-container class=\"wrapper\">            <el-header height=\"80px\">                <img src=\"https://dponlinedownload.parrowtech.com/atom.png\" alt=\"element-logo\" class=\"header-logo\"                     width=\"50\">                <div style=\"color:white;font-weight: bold; font-size: 24px; display: inline-block; vertical-align: middle;\">                    ATOM LOGGER                </div>                <ul class=\"header-operations\">                    <!-- <li>日志查寻</li> -->                    <li>说明</li>                </ul>            </el-header>            <el-main class=\"content\">                <el-form inline :model=\"query\" label-position=\"right\" label-width=\"100px\" class=\"query-form\">                    <el-form-item label=\"日志类型\" label-width=\"80px\">                        <el-input placeholder=\"日志类型\" suffix-icon=\"el-icon-edit\" v-model=\"query.LogType\"></el-input>                    </el-form-item>                    <el-form-item label=\"日志级别\" label-width=\"80px\">                        <el-select v-model=\"query.LogLevel\" placeholder=\"日志级别\">                            <el-option :key=\"0\" label=\"全部级别\" :value=\"0\"></el-option>                            <el-option :key=\"1\" label=\"普通\" :value=\"1\"></el-option>                            <el-option :key=\"2\" label=\"警告\" :value=\"2\"> </el-option>                            <el-option :key=\"3\" label=\"一般错误\" :value=\"3\"> </el-option>                            <el-option :key=\"4\" label=\"毁灭性错误\" :value=\"4\"> </el-option>                            <el-option :key=\"5\" label=\"接口监控\" :value=\"5\"> </el-option>                            <el-option :key=\"6\" label=\"异常\" :value=\"6\"> </el-option>                            <el-option :key=\"7\" label=\"调试\" :value=\"7\"> </el-option>                        </el-select>                    </el-form-item>                    <el-form-item label=\"源地址\" label-width=\"80px\">                        <el-input placeholder=\"源地址\" v-model=\"query.LogUrl\"></el-input>                    </el-form-item>                    <el-form-item label=\"日志内容\" label-width=\"80px\">                        <el-input placeholder=\"日志内容\" v-model=\"query.LogTxt\"></el-input>                    </el-form-item>                    <el-form-item label=\"开始时间\" label-width=\"80px\">                        <el-date-picker v-model=\"query.StartTime\" type=\"datetime\" value-format=\"yyyy-MM-dd HH:mm:ss\" placeholder=\"开始时间\"></el-date-picker>                    </el-form-item>                    <el-form-item label=\"结束时间\" label-width=\"80px\">                        <el-date-picker v-model=\"query.EndTime\" type=\"datetime\" value-format=\"yyyy-MM-dd HH:mm:ss\" placeholder=\"结束时间\"></el-date-picker>                    </el-form-item>                    <el-form-item label=\" \" label-width=\"80px\">                        <el-button type=\"primary\" icon=\"el-icon-search\" @click=\"search()\">筛选</el-button>                    </el-form-item>                </el-form>                <el-table :data=\"tableData\" class=\"table\" stripe border v-loading=\"query.loading\">                    <el-table-column prop=\"LogType\" label=\"日志类型\" width=\"100\"></el-table-column>                    <el-table-column prop=\"LogLevel\" label=\"级别\" width=\"50\"></el-table-column>                    <el-table-column prop=\"LogUrl\" label=\"日志地址\" :show-overflow-tooltip=\"true\"></el-table-column>                    <el-table-column prop=\"LogTxt\" label=\"日志内容\" :show-overflow-tooltip=\"true\"></el-table-column>                    <el-table-column prop=\"Duration\" label=\"请求时间\" width=\"80\"></el-table-column>                    <el-table-column prop=\"ContentLength\" label=\"请求长度\" width=\"80\"></el-table-column>                    <el-table-column prop=\"AddTime\" label=\"日志时间\" width=\"160\"></el-table-column>                    <el-table-column prop=\"ServerSrc\" label=\"服务器\" width=\"80\"></el-table-column>                </el-table>                <el-pagination background layout=\"prev, pager, next,total\" @current-change=\"query.changePage\" :page-size=\"query.PageSize\" :current-page=\"query.PageIndex\" :total=\"query.total\" style=\"margin: 10px 0;\"></el-pagination>            </el-main>        </el-container>    </div></body><script src=\"https://cdn.bootcdn.net/ajax/libs/vue/2.6.11/vue.js\"></script><script src=\"https://cdn.bootcdn.net/ajax/libs/element-ui/2.13.1/index.js\"></script><script src=\"https://cdn.bootcdn.net/ajax/libs/axios/0.19.2/axios.min.js\"></script><script>    new Vue({        el: '#app',        data: function () {            return {                query: {                    PageIndex: 1,                    PageSize: 20,                    total: 0,                    loading: false,                    changePage: (page) => { this.query.PageIndex = page; this.search(true); },                },                tableData: [],            }        },        methods: {            search(hold) {                if (hold !== true) this.query.PageIndex = 1;                this.query.loading = true;                axios.post(window.location.origin+'/api/Atom/Logs', this.query).then(res => {                    this.query.loading = false;                    this.tableData = res.data.Data;                    this.query.total = res.data.ExtData;                })            },        },    })</script><style>    html,    body {        height: 100%;    }    * {        padding: 0;        margin: 0;    }    .wrapper {        height: 100%;    }    a {        text-decoration: none;    }    header {        width: 100%;        padding: 0 20px;        z-index: 1;        box-sizing: border-box;        background-color: rgb(64, 158, 255);    }        header::after {            display: inline-block;            content: \"\";            height: 100%;            vertical-align: middle        }    .container {        padding-top: 80px;    }    .menu {        height: 100%;    }    .content {        padding: 0;    }    .header-logo {        display: inline-block;        vertical-align: middle;    }    .header-operations li {        color: #fff;        display: inline-block;        vertical-align: middle;        padding: 0 10px;        margin: 0 10px;        line-height: 80px;        cursor: pointer;    }    .header-operations {        display: inline-block;        float: right;        padding-right: 30px;        height: 100%;    }    .theme-form {        text-align: center;    }    .query-form {        padding-top: 25px;        background-color: #f2f2f2;    }</style></html>";
            return html;
        }

    }
}
