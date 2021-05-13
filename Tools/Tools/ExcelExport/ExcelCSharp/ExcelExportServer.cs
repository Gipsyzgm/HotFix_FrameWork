using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.ExcelExport.ConfigCSharp
{
    public class ExcelExportServer : ExcelExportBase
    {
        protected virtual string ConfigInitFile => Config.ConfigInitFile.ToReality();

        protected virtual string ConfigMgrFile => Config.ConfigMgrFile.ToReality();

        public ExcelExportServer(ConfigCodeOut config) : base(config)
        {

        }
        /// <summary>
        /// 生成配置表Calss类
        /// </summary>
        /// <param name="sheet"></param>
        protected override void CreateConfigData(ExcelSheet sheet)
        {
          
            string savePath = Path.Combine(SaveConfigData, sheet.ConfigName + ".txt");
            string jsondata = JsonConvert.SerializeObject(sheet.Table);
            Utils.SaveFile(savePath, jsondata);
        }

        /// <summary>
        /// 生成配置表Calss类
        /// </summary>
        /// <param name="sheet"></param>
        protected override void CreateConfigClass(ExcelSheet sheet)
        {
            string savePath = Path.Combine(SaveConfigClass, sheet.ConfigName + ".cs");
            StringBuilder fieldStrs = new StringBuilder();
            string firstField = null; //第一个字段做为Key
            string inter = string.IsNullOrEmpty(sheet.Interface) ? "" : (","+ sheet.Interface);
            foreach (ExcelSheetTableField filed in sheet.Fields)
            {
               
                if (firstField == null)
                    firstField = filed.Name;
                //生成属性字段
                string field = $@"        /// <summary>
        /// {filed.Des.Replace("\n", "\r\n        /// ")}
        /// </summary>
        public {ExcelUtil.GetCSStringType(filed.Type, false)} {filed.Name} {{ get; set; }}";
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
{fieldStrs}    }}
}}";
            Utils.SaveFile(savePath, str);
        }

        /// <summary>
        /// 生成ConfigInit.cs文件
        /// </summary>
        /// <param name="configList"></param>
        protected override void CreateConfigInit(List<ExcelSheet> configList)
        {
            string savePath = ConfigInitFile;
            StringBuilder sbConfig = new StringBuilder();
            foreach (ExcelSheet sheet in configList)
            {
                sbConfig.AppendLine($"            loadConfig<{sheet.ConfigName}>(); //{sheet.NameDes}");
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Config.RealityNameSpace}
{{
    public partial class ConfigMgr
    {{
        protected virtual void configInit()
        {{            
{sbConfig}        }}
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
            string savePath = ConfigMgrFile;
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbFun = new StringBuilder();

            StringBuilder sbFieldV = new StringBuilder();
            StringBuilder sbFunV = new StringBuilder();

            StringBuilder sbReload = new StringBuilder();
            StringBuilder sbReloadV = new StringBuilder();
            string fieldName;
            foreach (ExcelSheet sheet in configList)
            {
                if (sheet.IsVert)
                {
                    fieldName = Utils.ToFirstLower(sheet.ConfigName);
                    sbFieldV.AppendLine($"        /// <summary> {sheet.NameDes}</summary>");
                    sbFieldV.AppendLine($"        public readonly {sheet.ConfigName} {fieldName};");
                    sbFunV.AppendLine($"            readConfigV(ref {fieldName});");
                    sbReloadV.AppendLine($"            copyClassValue({fieldName}, reloadConfigV<{sheet.ConfigName}>());");
                }
                else
                {
                    fieldName = "dic" + sheet.Name;
                    sbField.AppendLine($"        /// <summary> {sheet.NameDes}</summary>");
                    sbField.AppendLine($"        public readonly Dictionary<object, {sheet.ConfigName}> {fieldName} = new Dictionary<object, {sheet.ConfigName}>();");
                    sbFun.AppendLine($"            readConfig({fieldName});");

                    sbReload.AppendLine($"            copyDictionary({fieldName}, reloadConfig<{sheet.ConfigName}>());");
                }
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
using System.Collections.Generic;

namespace {Config.RealityNameSpace}
{{
    public partial class ConfigMgr
    {{
        public static ConfigMgr I {{ get; protected set; }}
{sbField}
{sbFieldV}
        public ConfigMgr()
        {{
            I = this;
            Logger.Sys(""开始读取所有配置表文件..."");
            configInit();
{sbFun}
            //读取竖表配置
{sbFunV}
            customRead();
            Logger.Sys(""读取配置表文件完成"");
        }}

        protected virtual void reloadAll()
        {{
{sbReload}
{sbReloadV}
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        //=====
    }
}
