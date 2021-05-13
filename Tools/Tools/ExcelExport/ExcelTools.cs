using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Tools
{
    public class ExcelTools
    {
        #region 解析Excel单个Sheet数据,并生成相关文件
        /// <summary>
        /// 解析并导出文件
        /// </summary>
        /// <param name="saveJsonPath">保存Json格式数据路径</param>
        /// <param name="saveClassPath">生成对应的Class路径</param>
        /// <param name="sheetName">sheet表名</param>
        /// <param name="excelData">sheet中excel表数据</param>
        /// <param name="type">1服务端 2客户端 3C#测试客户端</param>
        /// <param name="isExportClass">是否导出对应的Class文件</param>
        public static void ParseExcelSheet(string saveJsonPath, string saveClassPath,DataTable dtData,int type,bool isExportClass)
        {
            //bool isServer = type == 1;
            //List<string> fieldDescribes = new List<string>();       //字段注释
            //List<string> fieldTypes = new List<string>();            //字段类型
            //List<string> fieldNames = new List<string>();           //字段名    
            //List<bool> columnFilters = new List<bool>();
            //if (dtData.Rows.Count < 3)
            //{
            //    Logger.LogError($"{dtData.TableName}表中格式有误,至少需要三行,显示字段注释，字段类型，字段名!!");
            //    return;
            //}
            //if (dtData.Rows.Count > 10000 || dtData.Columns.Count > 500)
            //{
            //    Logger.LogWarning($"{dtData.TableName}表中有{dtData.Rows.Count}行，{dtData.Columns.Count }列,请检查表中是否大范围内设置了单元格样式！！");
            //}

            //DataTable expDt = new DataTable();
            //for (int i = 0; i < dtData.Columns.Count; i++)
            //{
            //    string name = dtData.Rows[2][i].ToString();
            //    if (string.IsNullOrEmpty(name))
            //        break;
            //    fieldDescribes.Add(string.IsNullOrEmpty(dtData.Rows[0][i].ToString()) ? string.Empty : dtData.Rows[0][i].ToString());
            //    fieldTypes.Add(string.IsNullOrEmpty(dtData.Rows[1][i].ToString()) ? "string" : dtData.Rows[1][i].ToString());                
            //    columnFilters.Add(!ExcelUtil.isExport(name, isServer)); //记录哪些列需要过滤掉
            //    name = name.Replace("s_", "").Replace("c_", "");
            //    fieldNames.Add(name);
            //    if (!columnFilters[i])
            //    {
            //        if (expDt.Columns.Contains(name))
            //        {
            //            Logger.LogError($"{dtData.TableName}表中存在同名字段{name},跳过此表导出!!");
            //            return;
            //        }
            //        expDt.Columns.Add(name, ExcelUtil.GetStringType(fieldTypes[i]));
            //    }
            //}
                       
            //for (int r = 3; r < dtData.Rows.Count; r++) //遍历数据行
            //{
            //    DataRow expRow = expDt.NewRow();
            //    if (string.IsNullOrEmpty(dtData.Rows[r][0].ToString()))
            //        continue;
            //    for (int i = 0; i < fieldNames.Count; i++) //遍历数据列
            //    {
            //        if (columnFilters[i])
            //            continue;
            //        string fileValue = string.IsNullOrEmpty(dtData.Rows[r][i].ToString()) ? string.Empty : dtData.Rows[r][i].ToString();
            //        object val = ExcelUtil.GetObjectValue(fieldTypes[i], fileValue, dtData.TableName + "  " + fieldNames[i]);
            //        expRow[fieldNames[i]] = val;
            //    }
            //    expDt.Rows.Add(expRow);
            //}

            //string jsondata = JsonConvert.SerializeObject(expDt);
            //string[] classInfo = dtData.TableName.Split('#');
            //string className = classInfo[0].Replace("s_", "").Replace("c_", "")+"Config";
            //string classDes = classInfo.Length > 1 ? classInfo[1] : string.Empty;
            //className = Utils.ToFirstUpper(className);
            ////生成服务器配置表
            //if (type==1)
            //{
            //    //生成Json文件
            //    saveJsonPath = Path.Combine(saveJsonPath, className + ".txt");
            //    Utils.SaveFile(saveJsonPath, jsondata);
            //    string serverClassLog = string.Empty;
            //    //生成CS类文件
            //    if (isExportClass)
            //    {
            //        string classStr = getServerConfigCS(className, classDes, fieldNames, fieldTypes, fieldDescribes, columnFilters);
            //        saveClassPath = Path.Combine(saveClassPath, className + ".cs");
            //        Utils.SaveFile(saveClassPath, classStr);
            //    }
            //}
            //else if (type == 2) //生成客户端配置表
            //{
            //    //生成Json文件
            //    saveJsonPath = Path.Combine(saveJsonPath, className + ".dat");
            //    //文件进行压缩
            //    //MemoryStream data = Utils.Compress(System.Text.Encoding.UTF8.GetBytes(jsondata));
            //    //MemoryStream data = new MemoryStream(Utils.CompressBytes(System.Text.Encoding.UTF8.GetBytes(jsondata)));
            //    byte[] data = Utils.CompressBytes(jsondata);
            //    //string data = Utils.CompressString(jsondata);
            //    Utils.SaveFile(saveJsonPath, data);
            //    string serverClassLog = string.Empty;
            //    //生成as类文件
            //    if (isExportClass)
            //    {
            //        string classStr = getClientConfigAS(className, classDes, fieldNames, fieldTypes, fieldDescribes, columnFilters);
            //        saveClassPath = Path.Combine(saveClassPath, className + ".as");
            //        Utils.SaveFile(saveClassPath, classStr);
            //    }
            //}
            //else if (type == 3)
            //{
            //    //生成Json文件
            //    saveJsonPath = Path.Combine(saveJsonPath, className + ".txt");
            //    Utils.SaveFile(saveJsonPath, jsondata);
            //    string serverClassLog = string.Empty;
            //    //生成CS类文件
            //    if (isExportClass)
            //    {
            //        string classStr = getCSClientConfigCS(className, classDes, fieldNames, fieldTypes, fieldDescribes, columnFilters);
            //        saveClassPath = Path.Combine(saveClassPath, className + ".cs");
            //        Utils.SaveFile(saveClassPath, classStr);
            //    }
            //}
            //else if (type == 4)
            //{
            //    //生成Json文件
            //    saveJsonPath = Path.Combine(saveJsonPath, className + ".txt");
            //    Utils.SaveFile(saveJsonPath, jsondata);
            //    string serverClassLog = string.Empty;
            //    //生成CS类文件
            //    if (isExportClass)
            //    {
            //        string classStr = getCSGMConfigCS(className, classDes, fieldNames, fieldTypes, fieldDescribes, columnFilters);
            //        saveClassPath = Path.Combine(saveClassPath, className + ".cs");
            //        Utils.SaveFile(saveClassPath, classStr);
            //    }
            //}
        }
        #endregion

        #region 生成服务端配置文件模板类
        /// <summary>
        /// 获取生成服务器Config配置表CS文件
        /// </summary>
        /// <param name="configName">配置文件名</param>
        /// <param name="configDes">配置描述</param>
        /// <param name="fieldNames">所有字段</param>
        /// <param name="fieldTypes">所有类型</param>
        /// <param name="describes">字段描述</param>
        /// <param name="columnFilters">字段是否过滤掉</param>
        public static string getServerConfigCS(string configName, string configDes, List<string> fieldNames, List<string> fieldTypes, List<string> describes,List<bool> columnFilters)
        {
            StringBuilder fieldStrs = new StringBuilder();
            string firstField = null; //第一个字段做为Key
            for (int i = 0; i < fieldTypes.Count; i++)
            {
                if (columnFilters[i])
                    continue;
                if (firstField == null)
                    firstField = fieldNames[i];
                //生成属性字段
                string field = $@"        /// <summary>
        /// {describes[i].Replace("\n", "\r\n        /// ")}
        /// </summary>
        public {ExcelUtil.GetCSStringType(fieldTypes[i],false)} {fieldNames[i]} {{ get; set; }}";
                fieldStrs.AppendLine(field);
            }
                        
            string str = $@"using System;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer.XGame.ConfigMgr
{{
    /// <summary>{configDes}</summary>
    public class {configName} : BaseConfig
    {{
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override int UniqueID => {firstField};
{fieldStrs}    }}
}}";
            return str;
        }
        #endregion
     
        
        #region 生成C#客户端配置文件模板类
        /// <summary>
        /// 获取生成服务器Config配置表CS文件
        /// </summary>
        /// <param name="configName">配置文件名</param>
        /// <param name="configDes">配置描述</param>
        /// <param name="fieldNames">所有字段</param>
        /// <param name="fieldTypes">所有类型</param>
        /// <param name="describes">字段描述</param>
        /// <param name="columnFilters">字段是否过滤掉</param>
        public static string getCSClientConfigCS(string configName, string configDes, List<string> fieldNames, List<string> fieldTypes, List<string> describes, List<bool> columnFilters)
        {
            StringBuilder fieldStrs = new StringBuilder();
            string firstField = null; //第一个字段做为Key
            for (int i = 0; i < fieldTypes.Count; i++)
            {
                if (columnFilters[i])
                    continue;
                if (firstField == null)
                    firstField = fieldNames[i];
                //生成属性字段
                string field = $@"        /// <summary>
        /// {describes[i].Replace("\n", "\r\n        /// ")}
        /// </summary>
        public {ExcelUtil.GetCSStringType(fieldTypes[i], false)} {fieldNames[i]} {{ get; set; }}";
                fieldStrs.AppendLine(field);
            }

            string str = $@"using System;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameClient.ConfigMgr
{{
    /// <summary>{configDes}</summary>
    public class {configName} : BaseConfig
    {{
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override int UniqueID => {firstField};
{fieldStrs}    }}
}}";
            return str;
        }
        #endregion


        #region 生成GM配置文件模板类
        /// <summary>
        /// 获取生成服务器Config配置表CS文件
        /// </summary>
        /// <param name="configName">配置文件名</param>
        /// <param name="configDes">配置描述</param>
        /// <param name="fieldNames">所有字段</param>
        /// <param name="fieldTypes">所有类型</param>
        /// <param name="describes">字段描述</param>
        /// <param name="columnFilters">字段是否过滤掉</param>
        public static string getCSGMConfigCS(string configName, string configDes, List<string> fieldNames, List<string> fieldTypes, List<string> describes, List<bool> columnFilters)
        {
            StringBuilder fieldStrs = new StringBuilder();
            string firstField = null; //第一个字段做为Key
            for (int i = 0; i < fieldTypes.Count; i++)
            {
                if (columnFilters[i])
                    continue;
                if (firstField == null)
                    firstField = fieldNames[i];
                //生成属性字段
                string field = $@"        /// <summary>
        /// {describes[i].Replace("\n", "\r\n        /// ")}
        /// </summary>
        public {ExcelUtil.GetCSStringType(fieldTypes[i], false)} {fieldNames[i]} {{ get; set; }}";
                fieldStrs.AppendLine(field);
            }

            string str = $@"using System;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer.XGame.ConfigMgr
{{
    /// <summary>{configDes}</summary>
    public class {configName} : BaseConfig
    {{
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override int UniqueID => {firstField};
{fieldStrs}    }}
}}";
            return str;
        }
        #endregion
    }

}
