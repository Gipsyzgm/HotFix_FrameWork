using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    public class APIExportHelper
    {
        private const string GM_API_AIPAction_FilePath = @"GMServer\HTTP\";                       //消息处理路径
        private const string GM_API_Class_FilePath = @"GMServer\HTTP\_APIMsg";                       //生成接口类
        private const string GM_API_Action_FilePath = @"GMServer\HTTP\HttpAction.cs";     //生成接口Action

        /// <summary>
        /// 创建服务端Net相关文件
        /// </summary>
        /// <param name="aifFilePath">AIP配置文件目录</param>
        /// <param name="gmPath">GM根目录</param>
        /// <param name="callback">执行完回调</param>
        public static void CreateAPIClassFile(string aifFilePath, string gmPath, Action callback = null)
        {
            Utils.DeleteDirectory(Path.Combine(gmPath, GM_API_Class_FilePath));
            DataSet ds = ExcelUtil.ReadExcelSheetData(aifFilePath);
            List<MClassInfo> tabNames = new List<MClassInfo>();
            foreach (DataTable dt in ds.Tables)
            {
                MClassInfo tableInfo = null;
                List<MFieldInfo> MFieldInfo = null; //[字段名，字段类型，字段描述]
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string cellFirst = dt.Rows[i][0].ToString(); //每行的第一格
                    if (cellFirst == string.Empty || cellFirst.StartsWith("#"))
                        continue;
                    if (cellFirst.StartsWith("Req_") || cellFirst.StartsWith("Rsp_") || cellFirst.StartsWith("One_")) //一个新的AIP
                    {
                        if (tableInfo != null) //解析上一个表
                        {
                            parseTabelInfo(tableInfo, MFieldInfo, gmPath);
                            createActionFile(tableInfo, gmPath);
                            tabNames.Add(tableInfo);
                        }

                        tableInfo = new MClassInfo();
                        tableInfo.name = cellFirst;    //表名
                        tableInfo.module = dt.Rows[i][1].ToString();       //类所属文件夹 
                        tableInfo.comment = dt.Rows[i][2].ToString();       //表描述 
                        MFieldInfo = new List<MFieldInfo>();
                        continue;
                    }
                    if (tableInfo != null)
                    {
                        MFieldInfo oneField = new MFieldInfo();
                        oneField.name = dt.Rows[i][0].ToString();
                        oneField.type = dt.Rows[i][1].ToString();
                        oneField.comment = dt.Rows[i][2].ToString();
                        MFieldInfo.Add(oneField);
                    }
                }
                //单个Sheet读完，解析后一个表
                if (tableInfo != null)
                {
                    parseTabelInfo(tableInfo, MFieldInfo, gmPath);
                    createActionFile(tableInfo, gmPath);
                    tabNames.Add(tableInfo);
                }
            }
            createHttpActionFile(gmPath, tabNames);
            if (callback != null)
                callback();
        }

        /// <summary>
        /// 解析Table信息并生成cs结构文件
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="MFieldInfo"></param>
        /// <param name="serverPath"></param>
        private static void createActionFile(MClassInfo tableInfo,string gmPath)
        {
            if (!tableInfo.name.StartsWith("Req_"))
                return;
            string name = tableInfo.name.Replace("Req_", "");
            string saveFilePath = Path.Combine(gmPath, GM_API_AIPAction_FilePath, tableInfo.module, name + ".cs");

            StringBuilder fieldStrs = new StringBuilder();
            string str = $@"using GameLib;
using GMServer.XGame.Module;
using GMServer.XGame.ConfigMgr;
using GMServer.XGame.DBMgr;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace GMServer
{{
    public partial class HttpServer
    {{
        /// <summary>{tableInfo.comment}</summary>
        void {name}(HttpEventArgs e)
        {{
            if (!e.CheckToken()) //验证token是否有效果
                return;

            //权限判断...
            GameDB db = e.GetDB();//获取DB对象
            if (db == null)
                return;

            Req_{name} data = e.Msg as Req_{name};
            
            //返回消息
            //Rsp_{name} rsp = new Rsp_{name}();    
  
            //e.Send(rsp);
        }}
    }}
}}";
            Utils.SaveFile(saveFilePath, str, false);
        }


        /// <summary>
        /// 解析Table信息并生成cs结构文件
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="MFieldInfo"></param>
        /// <param name="serverPath"></param>
        private static void parseTabelInfo(MClassInfo tableInfo, List<MFieldInfo> MFieldInfo, string gmPath)
        {
            string saveFilePath = Path.Combine(gmPath, GM_API_Class_FilePath, tableInfo.module, tableInfo.name + ".cs");

            StringBuilder fieldStrs = new StringBuilder();
            string fieldStr = string.Empty;
            for (int i = 0; i < MFieldInfo.Count; i++)
            {

                fieldStr = $"{ExcelUtil.GetCSStringType(MFieldInfo[i].type)} {MFieldInfo[i].name} {{ get; set; }}";

                //生成属性字段
                string field = $@"        /// <summary>
        /// {MFieldInfo[i].comment.Replace("\n", "\r\n        /// ")}
        /// </summary>
        public {fieldStr}";
                fieldStrs.AppendLine(field);
            }

            string baseClass = "BaseHttpReqMsg";
            if (tableInfo.name.StartsWith("Rsp_"))
                baseClass = "BaseHttpRspMsg";


            string str = $@"using System;
using System.Collections.Generic;
using GMServer.XGame.DBMgr;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GMServer
{{    
    public class {tableInfo.name} : {baseClass}
    {{
{fieldStrs.ToString()}
    }}
}}";
            Utils.SaveFile(saveFilePath, str, true);
        }



        /// <summary>
        /// 生成生成HttpAction.cs文件
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="tabNames"></param>
        private static void createHttpActionFile(string serverPath, List<MClassInfo> tabNames)
        {
            string saveFilePath = Path.Combine(serverPath, GM_API_Action_FilePath);
            StringBuilder sbTable = new StringBuilder();
            string strtab = string.Empty;
            for (int i = 0; i < tabNames.Count; i++)
            {
                if(tabNames[i].name.StartsWith("Req_"))
                    sbTable.AppendLine($@"            _actionList.Add(""{tabNames[i].name}"", {tabNames[i].name.Replace("Req_","")});   //{tabNames[i].comment}");
            }

            string str = $@"using GameLib;
using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GMServer
{{
    public partial class HttpServer
    {{
       private Dictionary<string, Action<HttpEventArgs>> _actionList = new Dictionary<string, Action<HttpEventArgs>>();

        private void InitAction()
        {{
{sbTable}        }}
        void dispatchAction(HttpEventArgs e)
        {{
            try
            {{                
                if (_actionList.ContainsKey(e.Protocol))
                    _actionList[e.Protocol](e);              
                else
                    SendError(e.Response, $""[NetAction]消息{{ e.Protocol}} 没加到行为列表中!"");
            }}
            catch (Exception ex)
            {{
                Logger.LogError(ex.Message, ex.StackTrace);
            }}
        }}
    }}
}}";
            Utils.SaveFile(saveFilePath, str, true);
        }





    }
}
