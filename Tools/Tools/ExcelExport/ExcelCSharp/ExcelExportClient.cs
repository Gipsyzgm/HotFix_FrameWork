using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.ExcelExport.ConfigCSharp
{
    public class ExcelExportClient : ExcelExportBase
    {
        protected bool IsCSV => Config.IsCSV;

        protected bool IsSqlite => Config.IsSqlite;
        public ExcelExportClient(ConfigCodeOut config):base(config)
        {
        }

        /// <summary>
        /// 导出版本检测文字
        /// </summary>
        /// <param name="file"></param>
        /// <param name="outFile"></param>
        public void ExportVerLang(string file, string outFile)
        {
            FileInfo[] fils = new FileInfo[] { new FileInfo(file) };
            Dictionary<string, ExcelSheet> dicSheet = ExcelExportParse.GetExcleSheetForFileInfos(fils);
            foreach (ExcelSheet excel in dicSheet.Values)
            {
                if (excel.Export.IndexOf('V') != -1)
                {
                    string savePath = Path.Combine(outFile);
                    string jsondata = JsonConvert.SerializeObject(excel.Table);
                    Utils.SaveFile(savePath, jsondata);
                }
            }
        }
       
        protected override void CreateMapConfig()
        {
            if (!IsSqlite) return;
            string mapSourceDir = Glob.projectSetting.RealityConfigDir + "/Maps/";
            if (!Directory.Exists(mapSourceDir)) return;
            FileInfo info = new FileInfo(mapSourceDir + "/MapConfig.txt");
            if (!info.Exists) return;

            string json = File.ReadAllText(info.FullName);
            List<JObject> obj = JsonConvert.DeserializeObject<List<JObject>>(json);
            int a = 1;

            DataTable table = new DataTable();
            table.TableName = "MapConfig";
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("type", typeof(int));
            table.Columns.Add("monster", typeof(string));
            table.Columns.Add("symbols", typeof(string));

            DataRow dr;
            foreach (var o in obj)
            {
                dr = table.NewRow();
                dr.SetField("id", o.GetValue("id").ToObject<int>());
                dr.SetField("type", o.GetValue("type").ToObject<int>());
                dr.SetField("monster", o.GetValue("monster").ToString());
                dr.SetField("symbols", o.GetValue("symbols").ToString());
                table.Rows.Add(dr);
            }
            sqlit.InsertTable(table, false, "type");
        }


        /// <summary>
        /// 生成配置表Data文件
        /// </summary>
        /// <param name="sheet"></param>
        protected override void CreateConfigData(ExcelSheet sheet)
        {
            string savePath = Path.Combine(SaveConfigData, sheet.ConfigName + ".txt");

            if (IsCSV)
            {
                string csvdata = CSVUtils.SerializeCSV(sheet);
                if (sheet.IsEncrypt)
                    csvdata = Utils.EncryptDES(csvdata);
                Utils.SaveFile(savePath, csvdata);
            }
            else
            {
                if (IsSqlite)
                {
                    sqlit.InsertTable(sheet);
                }
                else
                {
                    string jsondata = JsonConvert.SerializeObject(sheet.Table);
                    if (sheet.IsEncrypt)
                        jsondata = Utils.EncryptDES(jsondata);
                    Utils.SaveFile(savePath, jsondata);
                }
            }
        }

        /// <summary>
        /// 生成配置表Calss类
        /// </summary>
        /// <param name="sheet"></param>
        protected override void CreateConfigClass(ExcelSheet sheet)
        {
            if (Config.IsSqlite)
            {
                Sqlite_CreateConfigClass(sheet);
                return;
            }
            string savePath = Path.Combine(SaveConfigClass, sheet.ConfigName + ".cs");
            StringBuilder fieldStrs = new StringBuilder();
            string firstField = null; //第一个字段做为Key
            string inter = string.IsNullOrEmpty(sheet.Interface) ? "" : ("," + sheet.Interface);

            //StringBuilder setValueFiled = new StringBuilder();

            string filedProp;
            //int index = 0;
            foreach (ExcelSheetTableField filed in sheet.Fields)
            {
                if (!IsCSV && filed.IsInterface)
                    filedProp = "{ get; set; }";
                else
                    filedProp = ";";

                if (firstField == null)
                    firstField = filed.Name;
                //生成属性字段
                string field = $@"        /// <summary>
        /// {filed.Des.Replace("\n", "\r\n        /// ")}
        /// </summary>
        public {ExcelUtil.GetCSStringType(filed.Type, false)} {filed.Name}{filedProp}";
                fieldStrs.AppendLine(field);
            }
            string str = $@"using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Config.RealityNameSpace}
{{
    /// <summary>{sheet.NameDes}</summary>
    public class {sheet.ConfigName} : BaseConfig{inter}
    {{
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => {firstField};
{fieldStrs}       
    }}        
}}";
            Utils.SaveFile(savePath, str);
        }

        /// <summary>
        /// 生成ConfigMgr.cs文件
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateConfigMgr(List<ExcelSheet> configList)
        {
            if (Config.IsSqlite)
            {
                Sqlite_CreateConfigMgr(configList);
                return;
            }
            string savePath = Config.ConfigMgrFile.ToReality();
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbFun = new StringBuilder();

            StringBuilder sbFieldV = new StringBuilder();
            StringBuilder sbFunV = new StringBuilder();
            string fieldName;
            foreach (ExcelSheet sheet in configList)
            {
                fieldName = sheet.Name;
                if (sheet.IsVert)
                {
                    //fieldName = Utils.ToFirstLower(sheet.ConfigName);
                    sbFieldV.AppendLine($"        /// <summary> {sheet.NameDes}</summary>");
                    sbFieldV.AppendLine($"        public {sheet.ConfigName} {fieldName};");
                    if(Config.IsTaskLoad)
                        sbFunV.AppendLine($"            {fieldName} = await readConfigV<{sheet.ConfigName}>({sheet.IsEncrypt.ToString().ToLower()});");
                    else
                        sbFunV.AppendLine($"            {fieldName} = readConfigV<{sheet.ConfigName}>({sheet.IsEncrypt.ToString().ToLower()});");
                }
                else
                {
                    //fieldName = "dic" + sheet.Name;
                    sbField.AppendLine($"        /// <summary> {sheet.NameDes}</summary>");
                    sbField.AppendLine($"        public readonly Dictionary<object, {sheet.ConfigName}> {fieldName} = new Dictionary<object, {sheet.ConfigName}>();");
                    if (Config.IsTaskLoad)
                        sbFun.AppendLine($"            readConfig({fieldName},{sheet.IsEncrypt.ToString().ToLower()}).Run();");
                    else
                        sbFun.AppendLine($"            readConfig({fieldName},{sheet.IsEncrypt.ToString().ToLower()});");
                }
            }
            string isCSVStr = string.Empty;
            if (Config.IsTaskLoad)
            {
                isCSVStr = $"        private bool IsCSV = {IsCSV.ToString().ToLower()};";
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
using System.Collections.Generic;
{(Config.IsTaskLoad?"using CSF.Tasks;":"")}
namespace {Config.RealityNameSpace}
{{
    public partial class ConfigMgr
    {{
{isCSVStr}
{sbField}
{sbFieldV}
        public {(Config.IsTaskLoad ? "async CTask" : "void")} Initialize()
        {{
{sbFun}
            //读取竖表配置
{sbFunV}
            //等待全部加载完再执行自定义解析
            {(Config.IsTaskLoad ? "await waitLoadComplate();" : "")}            
            customRead();
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }


        //========================================
        //==================SQLitle==================
        //========================================
        /// <summary>
        /// 生成配置表Calss类
        /// </summary>
        /// <param name="sheet"></param>
        protected void Sqlite_CreateConfigClass(ExcelSheet sheet)
        {
            string savePath = Path.Combine(SaveConfigClass, sheet.ConfigName + ".cs");
            StringBuilder fieldStrs = new StringBuilder();

            string inter = string.IsNullOrEmpty(sheet.Interface) ? "" : ("," + sheet.Interface);

            //StringBuilder setValueFiled = new StringBuilder();

            int index = 0;
            foreach (ExcelSheetTableField filed in sheet.Fields)
            {
                string field = $@"        /// <summary>
        /// {filed.Des.Replace("\n", "\r\n        /// ")}
        /// </summary>;";
                if (index++ == 0 && !sheet.IsVert)
                    field += "\n        [PrimaryKey]";

                field += $"\n        public {ExcelUtil.GetCSStringType(filed.Type, false)} {filed.Name} {{ get; set;}}"; //IsInterface
                    fieldStrs.AppendLine(field);
            }
            string str = $@"using System;
using SQLite4Unity3d;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Config.RealityNameSpace}
{{
    /// <summary>{sheet.NameDes}</summary>
    public class {sheet.ConfigName} : IConfig{inter}
    {{
{fieldStrs}       
    }}        
}}";
            Utils.SaveFile(savePath, str);
        }
        private void Sqlite_CreateConfigMgr(List<ExcelSheet> configList)
        {
            string savePath = Config.ConfigMgrFile.ToReality();
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbFun = new StringBuilder();

            StringBuilder sbFieldV = new StringBuilder();
            StringBuilder sbFunV = new StringBuilder();
            string fieldName;
            foreach (ExcelSheet sheet in configList)
            {
                fieldName = sheet.Name;
                if (sheet.IsVert)
                {
                    //fieldName = Utils.ToFirstLower(sheet.ConfigName);
                    sbFieldV.AppendLine($"        /// <summary> {sheet.NameDes}</summary>");
                    sbFieldV.AppendLine($"        public {sheet.ConfigName} {fieldName} => m{fieldName} ?? (m{fieldName} = new ConfigTable<{sheet.ConfigName}>().Initialize());private {sheet.ConfigName} m{fieldName};");
                    //sbFunV.AppendLine($"            {fieldName} = new ConfigTable<{sheet.ConfigName}>().Initialize();");
                }
                else
                {
                    //fieldName = "dic" + sheet.Name;
                    sbField.AppendLine($"        /// <summary> {sheet.NameDes}</summary>");
                    sbField.AppendLine($"        public ConfigTable<{sheet.Fields[0].Type},{sheet.ConfigName}> {fieldName};");
                    sbFun.AppendLine($"            {fieldName} = new ConfigTable<{sheet.Fields[0].Type},{sheet.ConfigName}>();");
                }
            }

            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Config.RealityNameSpace}
{{
    public partial class ConfigMgr
    {{
{sbField}
{sbFieldV}
        public void Initialize()
        {{
{sbFun}
            customRead();
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
    }
}
