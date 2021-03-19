using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    public class LogExportHelper
    {

        protected static HashSet<string> classNames = new HashSet<string>();
        /// <summary>
        /// 创建服务端Net相关文件
        /// </summary>
        /// <param name="DBPath">DB配置文件目录</param>
        /// <param name="serverPath">服务器根目录</param>
        /// <param name="callback">执行完回调</param>
        public static void CreateServerLogClassFile(string DBPath, string serverPath, Action callback = null)
        {
            //Utils.DeleteDirectory(Path.Combine(serverPath, Server_DBTableClass_FilePath));
            Utils.DeleteDirectory(Glob.codeOutSetting.ServerLog.OutLogClassDir.ToReality());

            DataSet ds = ExcelUtil.ReadExcelSheetData(DBPath);
            List<string[]> tabNames = new List<string[]>();
            classNames.Clear();
            foreach (DataTable dt in ds.Tables)
            {
                string[] tableInfo = null;
                List<string[]> fieldInfo = null; //[字段名，字段类型，字段描述]
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string cellFirst = dt.Rows[i][0].ToString(); //每行的第一格
                    if (cellFirst == string.Empty|| cellFirst.StartsWith("#"))
                        continue;
                    if (cellFirst.StartsWith("$")) //一个新的表
                    {
                        if (tableInfo != null) //解析上一个表
                        {
                            parseTabelInfo(tableInfo, fieldInfo, serverPath);
                            tabNames.Add(tableInfo);
                        }

                        tableInfo = new string[2];
                        tableInfo[0] = cellFirst.Replace("$", "");    //表名
                        tableInfo[1] = dt.Rows[i][1].ToString();       //表描述 
                        fieldInfo = new List<string[]>();
                        continue;
                    }
                    if (tableInfo != null)
                    {
                        fieldInfo.Add(new string[] { dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString() });
                    } 
                }
                //单个Sheet读完，解析最后一个表
                if (tableInfo != null)
                {
                    parseTabelInfo(tableInfo, fieldInfo, serverPath);
                    tabNames.Add(tableInfo);
                }          
            }
            if (callback != null)
                callback();
        }      

        /// <summary>
        /// 解析Table信息并生成cs结构文件
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="serverPath"></param>
        private static void parseTabelInfo(string[] tableInfo, List<string[]> fieldInfo, string serverPath)
        {
            classNames.Add(tableInfo[0]);
            //string saveFilePath = Path.Combine(serverPath, Server_DBTableClass_FilePath, tableInfo[0] + ".cs");
            string saveFilePath = Path.Combine(Glob.codeOutSetting.ServerLog.OutLogClassDir.ToReality(), tableInfo[0] + ".cs");

            StringBuilder fieldStrs = new StringBuilder();
            StringBuilder sbToString = new StringBuilder();
            string fieldStr = string.Empty;
            for (int i = 0; i < fieldInfo.Count; i++)
            {
                fieldStr = $"{ExcelUtil.GetCSStringType(fieldInfo[i][1])} {fieldInfo[i][0]};";

                if (classNames.Contains(fieldInfo[i][1]))  //字段为别一个日志的结构体
                {
                    sbToString.AppendLine($@"            sb.Append({fieldInfo[i][0]}.ToString());");
                }
                else
                {
                    switch (fieldInfo[i][1].ToLower())
                    {
                        case "string":
                            sbToString.AppendLine($@"            sb.Append(FLogUtils.GetString({fieldInfo[i][0]}));");
                            break;
                        default:
                            sbToString.AppendLine($@"            sb.Append({fieldInfo[i][0]});");
                            break;
                    }
                }
                if(i< fieldInfo.Count-1)
                    sbToString = sbToString.AppendLine($"            FLogUtils.AppendSplit(sb);");

                //生成属性字段
                string field = $@"        /// <summary>
        /// {fieldInfo[i][2].Replace("\n", "\r\n        /// ")}
        /// </summary>
        public {fieldStr}";
                fieldStrs.AppendLine(field);
            }
            
            string str = $@"using System.Text;
using System;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer.XGame.FLogMgr
{{    
    public class {tableInfo[0]} : CyLogBase
    {{        
{fieldStrs.ToString()}
       
        /// <summary>
        /// 转成日志字符串
        /// </summary>     
        /// <returns></returns>
        public override string ToString()
        {{
            StringBuilder sb = new StringBuilder();
{sbToString.ToString()}
            return sb.ToString();
        }}
    }}
}}";
            Utils.SaveFile(saveFilePath, str, true);
        }       
    }
}
