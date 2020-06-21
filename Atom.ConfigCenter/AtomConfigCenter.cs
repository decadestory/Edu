using Atom.ConfigCenter.Cacher;
using Atom.ConfigCenter.DataCore;
using Atom.ConfigCenter.Model;
using Orm.Son.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.ConfigCenter
{
    public class AtomConfigCenter: IAtomConfigCenter
    {
        public AtomConfigCenter(string dbConnStr)
        {
            SonFact.init(dbConnStr);
            AtomConfigCenterDataCore.CheckOrCreateDb();
        }

        public  List<AtomCateConfigModel> GetCates(string parentCode, bool hasDisable = false)
        {
            return AtomConfigCenterDataCore.GetCates(parentCode,hasDisable);
        }

        public  List<AtomCateConfigModel> GetCatesByDomain(string parentCode,int domainId)
        {
            return AtomConfigCenterDataCore.GetCatesByDomain(parentCode, domainId);
        }

        public  long SetVal(string cateCode, string value, int relId = 0, string extVal = null, string valueType=null, DateTime? st = null, DateTime? et = null, bool isAdd = false)
        {
            var acv = new AtomConfigValue
            {
                CateCode = cateCode,
                AddTime = DateTime.Now,
                CateValue = value,
                IsValid = true,
                RelId = relId,
                StartTime = st,
                EndTime = et,
                ExtValue = extVal,
                ValueType=valueType,
            };

            return AtomConfigCenterDataCore.SetVal(acv, isAdd);
        }

        public  AtomCateConfigModel Get(int relId, string cateCode)
        {
            return AtomConfigCenterDataCore.Get(relId,cateCode);
        }

        public  AtomCateConfigModel GetAllById(int relId)
        {
            return AtomConfigCenterDataCore.GetAllById(relId);
        }

        public List<NAtomCateConfigModel> SearchDict(NAtomCateConfigModel request, int domid = 0)
        {
            return AtomConfigCenterDataCore.SearchDict(request,domid);
        }

        public bool AddDict(NAtomCateConfigModel model, int domid = 0)
        {
            return AtomConfigCenterDataCore.AddDict(model, domid);
        }

        public bool EditDict(NAtomCateConfigModel model)
        {
            return AtomConfigCenterDataCore.EditDict(model);
        }

        public bool DelDict(int cid)
        {
            return AtomConfigCenterDataCore.DelDict(cid);
        }

        public List<AtomCateConfigModel> Get(string code)
        {
            return AtomConfigCenterDataCore.Get(code);
        }

        public string Html()
        {
            var html = "<!DOCTYPE html><html><head>    <meta charset=\"UTF-8\">    <link href=\"https://cdn.bootcdn.net/ajax/libs/element-ui/2.13.1/theme-chalk/index.css\" rel=\"stylesheet\"></head><body>    <div id=\"app\">        <el-container class=\"wrapper\">            <el-header height=\"80px\">                <img src=\"https://dponlinedownload.parrowtech.com/atom.png\" alt=\"element-logo\" class=\"header-logo\"                    width=\"50\">                <div                    style=\"color:white;font-weight: bold; font-size: 24px; display: inline-block; vertical-align: middle;\">                    ATOM CONFIGCENTER                </div>                <ul class=\"header-operations\">                    <li>位置：{{curDirs}}</li>                </ul>            </el-header>            <el-main class=\"content\">                <el-form inline :model=\"query\" label-position=\"right\" label-width=\"100px\" class=\"query-form\">                    <el-form-item label=\" \" label-width=\"0px\">                        <el-button type=\"primary\" icon=\"el-icon-arrow-left\" @click=\"back()\">返回上级</el-button>                        <el-button type=\"primary\" icon=\"el-icon-plus\" @click=\"showAdd()\">添加</el-button>                    </el-form-item>                </el-form>                <el-table :data=\"tableData\" class=\"table\" stripe border v-loading=\"query.loading\" @row-click=\"rowClick\">                    <el-table-column prop=\"CateName\" label=\"配置名称\"></el-table-column>                    <el-table-column prop=\"CateCode\" label=\"配置编码\"></el-table-column>                    <el-table-column prop=\"ParentCateCode\" label=\"上级代码\"></el-table-column>                    <el-table-column prop=\"ExtCateCode\" label=\"分类代码\"></el-table-column>                    <el-table-column prop=\"Level\" label=\"层级\" width=\"50px\"></el-table-column>                    <el-table-column prop=\"Sort\" label=\"排序\" width=\"50px\"></el-table-column>                    <el-table-column prop=\"Value\" label=\"值\"></el-table-column>                    <el-table-column prop=\"ScdValue\" label=\"值二\"></el-table-column>                    <el-table-column prop=\"ThdValue\" label=\"值三\"></el-table-column>                    <el-table-column label=\"状态\" width=\"80px\">                        <template slot-scope=\"scope\">                            <el-tag size=\"mini\" :type=\"scope.row.IsValid==true ? 'success' : 'danger'\"                                disable-transitions>{{scope.row.IsValid==true ? '启用' : '禁用'}}</el-tag>                        </template>                    </el-table-column>                    </el-table-column>                    <el-table-column prop=\"AddTime\" label=\"添加时间\" width=\"160px\"></el-table-column>                    <el-table-column label=\"操作\" width=\"80px\">                        <template slot-scope=\"scope\">                            <el-button type=\"text\" icon=\"el-icon-edit\" @click.stop=\"showEdit(scope.row)\">修改</el-button>                        </template>                    </el-table-column>                </el-table>                <el-dialog :title=\"dialogTitle\" :visible.sync=\"showNewDialog\" :close-on-click-modal=\"false\">                    <el-form ref=\"newModel\" :model=\"newModel\" :rules=\"rules\" label-width=\"120px\">                        <el-form-item label=\"上级代码\" prop=\"ParentCode\">                            <el-input v-model=\"newModel.ParentCateCode\" disabled></el-input>                        </el-form-item>                        <el-form-item label=\"配置名称\" prop=\"CateName\">                            <el-input v-model=\"newModel.CateName\"></el-input>                        </el-form-item>                        <el-form-item label=\"配置代码\" prop=\"CateCode\">                            <el-input v-model=\"newModel.CateCode\"></el-input>                        </el-form-item>                        <el-form-item label=\"层级\" prop=\"Level\">                            <el-input v-model=\"newModel.Level\" disabled></el-input>                        </el-form-item>                        <el-form-item label=\"分类代码\" prop=\"ExtCateCode\">                            <el-input v-model=\"newModel.ExtCateCode\"></el-input>                        </el-form-item>                        <el-form-item label=\"值一\" prop=\"Value\">                            <el-input v-model=\"newModel.Value\"></el-input>                        </el-form-item>                        <el-form-item label=\"值二\" prop=\"ScdValue\">                            <el-input v-model=\"newModel.ScdValue\"></el-input>                        </el-form-item>                        <el-form-item label=\"值三\" prop=\"ThdValue\">                            <el-input v-model=\"newModel.ThdValue\"></el-input>                        </el-form-item>                        <el-form-item label=\"平台\" prop=\"DomainId\">                            <el-select v-model=\"newModel.DomainId\">                                <el-option size=\"mini\" :key=\"0\" label=\"全平台\" :value=\"0\"></el-option>                                <el-option size=\"mini\" v-for=\"r in domains\" :key=\"r.Key\" :label=\"r.Name\" :value=\"r.Key\">                                </el-option>                            </el-select>                        </el-form-item>                        <el-form-item label=\"排序\" prop=\"Level\">                            <el-input-number type=\"number\" v-model=\"newModel.Sort\"></el-input-number>                        </el-form-item>                        <el-form-item label=\"是否启用\" prop=\"IsValid\">                            <el-select v-model=\"newModel.IsValid\">                                <el-option :key=\"1\" label=\"启用\" :value=\"true\"></el-option>                                <el-option :key=\"0\" label=\"禁用\" :value=\"false\"></el-option>                                <el-option :key=\"-1\" label=\"删除\" value=\"\"></el-option>                            </el-select>                        </el-form-item>                        <el-form-item v-if=\"dialogTitle!='修改配置'\">                            <el-button type=\"primary\" :loading=\"editLoading\" @click=\"add\">添加配置</el-button>                        </el-form-item>                        <el-form-item v-if=\"dialogTitle=='修改配置'\">                            <el-button type=\"primary\" :loading=\"editLoading\" @click=\"edit\">修改配置</el-button>                        </el-form-item>                    </el-form>                </el-dialog>            </el-main>        </el-container>    </div></body><script src=\"https://cdn.bootcdn.net/ajax/libs/vue/2.6.11/vue.js\"></script><script src=\"https://cdn.bootcdn.net/ajax/libs/element-ui/2.13.1/index.js\"></script><script src=\"https://cdn.bootcdn.net/ajax/libs/axios/0.19.2/axios.min.js\"></script><script>    new Vue({        el: '#app',        data: function () {            return {                apiUrl: window.location.origin,                query: {                    loading: false,                },                curDirs: \"\",                clickHistory: [{ CateCode: \"\", CateName: \"/\" }],                curParent: { CateCode: \"\" },                tableData: [],                showNewDialog: false,                dialogTitle: '添加配置',                newModel: {},                domains: [{ Key: 1, Name: '平台1' }, { Key: 2, Name: '平台2' }, { Key: 3, Name: '平台3' }],                editLoading: false,                rules: {                    CateName: [                        { required: true, message: \"请输入配置名称\", trigger: \"blur\" }                    ],                    CateCode: [{ required: true, message: \"请输入配置代码\", trigger: \"blur\" }]                }            }        },        mounted() {            this.search();        },        methods: {            search(hold) {                if (hold !== true) this.query.PageIndex = 1;                this.query.loading = true;                axios.post(`${this.apiUrl}/Atom/GetDictsByParentCode?dictCode=${this.curParent.CateCode}&hasDisable=true`, this.query).then(res => {                    this.query.loading = false;                    this.tableData = res.data.Data;                    this.query.total = res.data.ExtData;                })            },            rowClick(row, column, event) {                this.clickHistory.push(row);                this.curParent = row;                this.search();                this.getCurDirs();                console.info('rowClick', this.curParent);            },            back() {                if (this.clickHistory.length <= 1) return;                this.curParent = this.clickHistory.slice(-2)[0];                this.clickHistory.pop();                this.search();                this.getCurDirs();                console.info('back', this.curParent);            },            getCurDirs() {                var t = \"\";                for (var i in this.clickHistory) {                    var e = this.clickHistory[i];                    if (e.CateName == \"/\") e.CateName = '首页';                    t += e.CateName + \"/\";                    console.info('------', e.CateName);                }                console.info('t', t);                this.curDirs = t;            },            showAdd() {                this.showNewDialog = true;                this.dialogTitle = \"添加配置\";                this.newModel = {                    DomainId: 0,                    IsValid: true,                    ParentCateCode: this.curParent.CateCode,                    CateName: '',                    CateCode: '',                    Sort: 0                };                this.newModel.Level = this.curParent.Level || this.curParent.Level == 0 ? parseInt(this.curParent.Level) + 1 : 0;                this.newModel.CateCode = this.curParent.CateCode ? this.curParent.CateCode + \"_\" : '';            },            add() {                this.$refs[\"newModel\"].validate(valid => {                    if (valid) {                        this.editLoading = true;                        axios.post(`${this.apiUrl}/Atom/AddDict`, this.newModel).then(res => {                            this.editLoading = false;                            if (res.data.Code != 1) return this.$message({ message: res.data.Message, type: \"warning\" });;                            this.showNewDialog = false;                            this.$message({ message: \"添加成功\", type: \"success\" });                            this.search();                        })                    } else return false;                });            },            showEdit(row, column, event) {                this.newModel = row;                this.dialogTitle = \"修改配置\";                this.showNewDialog = true;            },            edit() {                this.$refs[\"newModel\"].validate(valid => {                    if (valid) {                        this.editLoading = true;                        axios.post(`${this.apiUrl}/Atom/EditDict`, this.newModel).then(res => {                            this.editLoading = false;                            if (res.data.Code != 1) return this.$message({ message: res.data.Message, type: \"warning\" });;                            this.showNewDialog = false;                            this.$message({ message: \"修改成功\", type: \"success\" });                            this.search();                        })                    } else return false;                });            },            formatIsValid(row, column, cellValue) {                if (cellValue === true) return \"启用\";                else if (cellValue === false) return \"禁用\";                else return \"未知\";            }        },    })</script><style>    html,    body {        height: 100%;    }    * {        padding: 0;        margin: 0;    }    .wrapper {        height: 100%;    }    a {        text-decoration: none;    }    header {        width: 100%;        padding: 0 20px;        z-index: 1;        box-sizing: border-box;        background-color: rgb(64, 158, 255);    }    header::after {        display: inline-block;        content: \"\";        height: 100%;        vertical-align: middle    }    .container {        padding-top: 80px;    }    .menu {        height: 100%;    }    .content {        padding: 0;    }    .header-logo {        display: inline-block;        vertical-align: middle;    }    .header-operations li {        color: #fff;        display: inline-block;        vertical-align: middle;        padding: 0 10px;        margin: 0 10px;        line-height: 80px;        cursor: pointer;    }    .header-operations {        display: inline-block;        float: right;        padding-right: 30px;        height: 100%;    }    .theme-form {        text-align: center;    }    .query-form {        padding-top: 25px;        background-color: #f2f2f2;    }</style></html>";
            return html;
        }

    }
}
