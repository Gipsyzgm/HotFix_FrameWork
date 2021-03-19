using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Tools
{

    public class ExcelExportHelper
    {
        //@"bin\h5\res\config";  
        private const string Client_ConfigClass_FilePath = @"src\ConfigData\Configs";                            //Excel生成保存位置,相对clinetPath文件夹
        private const string Client_ConfigName_FilePath = @"src\ConfigData\ConfigName.as";                  //Excel生成保存位置,相对clinetPath文件夹
        private const string Server_ConfigClass_FilePath = @"GameServer\XGame\ConfigMgr\Config";    //Excel生成保存位置,相对serverPath文件夹
        private const string Server_ConfigInit_FilePath = @"GameServer\XGame\ConfigMgr\ConfigInit.cs";  //配置表初始化文件路径
        private const string Server_ConfigMgr_FilePath = @"GameServer\XGame\ConfigMgr\ConfigMgr.cs";  //


        private const string CSClient_ConfigClass_FilePath = @"GameClient\ConfigMgr\Config";    //Excel生成保存位置,相对csClientPath文件夹
        private const string CSClient_ConfigInit_FilePath = @"GameClient\ConfigMgr\ConfigInit.cs";  //配置表初始化文件路径
        private const string CSClient_ConfigMgr_FilePath = @"GameClient\ConfigMgr\ConfigMgr.cs";  //


        private const string GMServer_ConfigClass_FilePath = @"GameServer\XGame\ConfigMgr\Config";    //Excel生成保存位置,相对csClientPath文件夹
        private const string GMServer_ConfigInit_FilePath = @"GameServer\XGame\ConfigMgr\ConfigInit.cs";  //配置表初始化文件路径
        private const string GMServer_ConfigMgr_FilePath = @"GameServer\XGame\ConfigMgr\ConfigMgr.cs";  //

        /// <summary>
        /// 生成客户端和服务端配置文件
        /// </summary>       
        /// <param name="ExcelPath">Excel目录</param>
        /// <param name="clientPath">客户端路径</param> 
        /// <param name="clientSavePath">客户端导出json保存路径</param>   
        /// <param name="serverPath">服务端路径</param> 
        /// <param name="serverSavePath">服务端导出json保存路径</param> 
        /// <param name="callback">执行完回调</param>   
        public static void GenerateSelectExcel(string ExcelDir, string clientPath, string clientSavePath, string serverPath, string serverSavePath, List<string> SelectFiles, Action callback = null)
        {
            DirectoryInfo folder = new DirectoryInfo(ExcelDir);
            List<string> serverConfigName = new List<string>();
            List<string> clientConfigName = new List<string>();

            if (SelectFiles == null)  //全选了，删除相关文件夹下的全部文件
            {
                if (clientPath != null && clientSavePath != null)
                {
                    Utils.DeleteDirectory(Path.Combine(clientPath, Client_ConfigClass_FilePath)); //清空下面所有文件
                }
                if (serverPath != null && serverSavePath != null)
                {
                    Utils.DeleteDirectory(Path.Combine(serverPath, Server_ConfigClass_FilePath)); //清空下面所有文件
                }
            }

            foreach (FileInfo file in folder.GetFiles("*.xls*"))
            {
                if (file.Name.StartsWith("~$") || file.Name.StartsWith("_"))
                    continue;
                if (SelectFiles != null && !SelectFiles.Contains(file.Name))
                    continue;

                //获取配置表中所有表
                DataSet ds = ExcelUtil.ReadExcelSheetData(file.FullName);
                foreach (DataTable dt in ds.Tables)
                {
                    if (clientPath != null)  //导出客户端用表
                    {
                        //判断此表是否需要导出
                        if (ExcelUtil.isExport(dt.TableName, false))
                        {
                            string saveClassPath = Path.Combine(clientPath, Client_ConfigClass_FilePath);
                            ExcelTools.ParseExcelSheet(clientSavePath, saveClassPath, dt, 2, true);
                            clientConfigName.Add(dt.TableName);
                        }
                    }
                    if (serverPath != null)//导出服务端用表
                    {
                        //判断此表是否需要导出
                        if (ExcelUtil.isExport(dt.TableName, true))
                        {
                            string saveClassPath = Path.Combine(serverPath, Server_ConfigClass_FilePath);
                            ExcelTools.ParseExcelSheet(serverSavePath, saveClassPath, dt, 1, true);
                            serverConfigName.Add(dt.TableName);
                        }
                    }
                }
            }
            //生成服务端ConfigInit文件
            if (serverConfigName.Count > 0 && SelectFiles == null)  //全部导出时才生成
            {
                createServerConfigInitFile(serverConfigName, serverPath);
                createServerConfigMgrFile(serverConfigName, serverPath);
            }

            //生成客户端ConfigName文件
            if (clientConfigName.Count > 0 && SelectFiles == null)  //全部导出时才生成
                createClientConfigNameFile(clientConfigName, clientPath);

            if (callback != null)
                callback();
        }

        #region 导出服务端ConfigInit.cs文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createServerConfigInitFile(List<string> sheetNames, string serverPath)
        {
            string savePath = Path.Combine(serverPath, Server_ConfigInit_FilePath);
            StringBuilder sbConfig = new StringBuilder();
            for (int i = 0; i < sheetNames.Count; i++)
            {

                string[] classInfo = sheetNames[i].Split('#');
                string className = classInfo[0].Replace("s_", "").Replace("c_", "") + "Config";
                string classDes = classInfo.Length > 1 ? classInfo[1] : string.Empty;
                className = Utils.ToFirstUpper(className);
                sbConfig.AppendLine($"            readConfig<{className}>(); //{classDes}");
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer.XGame.ConfigMgr
{{
    public partial class ConfigHelper
    {{
        void ConfigInit()
        {{            
{sbConfig}        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出服务端ConfigMgr.cs文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createServerConfigMgrFile(List<string> sheetNames, string serverPath)
        {
            string savePath = Path.Combine(serverPath, Server_ConfigMgr_FilePath);
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbFun = new StringBuilder();
            for (int i = 0; i < sheetNames.Count; i++)
            {

                string[] classInfo = sheetNames[i].Split('#');
                string name = Utils.ToFirstUpper(classInfo[0].Replace("s_", "").Replace("c_", ""));
                string classDes = classInfo.Length > 1 ? classInfo[1] : string.Empty;
                string fieldName = "dic" + name;
                string className = name + "Config";
                sbField.AppendLine($"        /// <summary> {classDes}</summary>");
                sbField.AppendLine($"        public readonly Dictionary<int, {className}> {fieldName} = new Dictionary<int, {className}>();");
                sbFun.AppendLine($"            readConfig({fieldName});");
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
using GameLib;
using System.Collections.Generic;

namespace GameServer.XGame.ConfigMgr
{{
    public partial class ConfigMgr
    {{
{sbField}
        private ConfigHelper configHelper;
        public ConfigMgr()
        {{
            Logger.Sys(""开始读取所有配置表文件..."");
            configHelper = new ConfigHelper();
{sbFun}            customRead();
            Logger.Sys(""读取配置表文件完成"");
        }}

        private void readConfig<T>(Dictionary<int,T> source) where T:BaseConfig
        {{
            List<T> list = configHelper.Select<T>();
            foreach (T _item in list)
                source.Add(_item.UniqueID, _item);

            if (list.Count == 0)
                Logger.LogWarning($""配置表{{ typeof(T).Name}} 没有数据"");
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion



        #region 导出客户端ConfigName.as文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createClientConfigNameFile(List<string> sheetNames, string clientPath)
        {
            string savePath = Path.Combine(clientPath, Client_ConfigName_FilePath);
            StringBuilder sbConfig = new StringBuilder();
            for (int i = 0; i < sheetNames.Count; i++)
            {
                string[] classInfo = sheetNames[i].Split('#');
                string className = classInfo[0].Replace("s_", "").Replace("c_", "") + "Config";
                string classDes = classInfo.Length > 1 ? classInfo[1] : string.Empty;
                className = Utils.ToFirstUpper(className);
                sbConfig.AppendLine($@"		/**{classDes} 表名*/");
                sbConfig.AppendLine($@"		public static const {className}:String = ""{className}"";");
            }
            string str = $@"/**工具生成，不要修改*/
package ConfigData
{{
	public final class ConfigName
	{{
{sbConfig}	}}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion



        #region 生成C#客户端配置文件
        /// <summary>
        /// 生成C#客户端配置文件
        /// </summary>       
        /// <param name="ExcelPath">Excel目录</param>
        /// <param name="clientPath">客户端路径</param> 
        /// <param name="clientSavePath">客户端导出json保存路径</param> 
        /// <param name="callback">执行完回调</param>   
        public static void GenerateCSClientExcel(string ExcelDir, string clientPath, string clientSavePath, Action callback = null)
        {
            DirectoryInfo folder = new DirectoryInfo(ExcelDir);
            List<string> clientConfigName = new List<string>();

            //删除相关文件夹下的全部文件
            Utils.DeleteDirectory(Path.Combine(clientPath, CSClient_ConfigClass_FilePath)); //清空下面所有文件

            foreach (FileInfo file in folder.GetFiles("*.xls*"))
            {
                if (file.Name.StartsWith("~$")|| file.Name.StartsWith("_"))
                    continue;
             
                //获取配置表中所有表
                DataSet ds = ExcelUtil.ReadExcelSheetData(file.FullName);
                foreach (DataTable dt in ds.Tables)
                {
                    //判断此表是否需要导出
                    if (ExcelUtil.isExport(dt.TableName, false))
                    {
                        string saveClassPath = Path.Combine(clientPath, CSClient_ConfigClass_FilePath);
                        ExcelTools.ParseExcelSheet(clientSavePath, saveClassPath, dt, 3, true);
                        clientConfigName.Add(dt.TableName);
                    }
                }
            }
            //生成客户服务端ConfigInit文件
            if (clientConfigName.Count > 0)
            {
                createCSClientConfigInitFile(clientConfigName, clientPath);
                createCSClientConfigMgrFile(clientConfigName, clientPath);
            }

            if (callback != null)
                callback();
        }
        #endregion

        #region 导出C#客户端ConfigInit.cs文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createCSClientConfigInitFile(List<string> sheetNames, string csCleintPath)
        {
            string savePath = Path.Combine(csCleintPath, CSClient_ConfigInit_FilePath);
            StringBuilder sbConfig = new StringBuilder();
            for (int i = 0; i < sheetNames.Count; i++)
            {

                string[] classInfo = sheetNames[i].Split('#');
                string className = classInfo[0].Replace("s_", "").Replace("c_", "") + "Config";
                string classDes = classInfo.Length > 1 ? classInfo[1] : string.Empty;
                className = Utils.ToFirstUpper(className);
                sbConfig.AppendLine($"            readConfig<{className}>(); //{classDes}");
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameClient.ConfigMgr
{{
    public partial class ConfigHelper
    {{
        void ConfigInit()
        {{            
{sbConfig}        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出C#客户端ConfigMgr.cs文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createCSClientConfigMgrFile(List<string> sheetNames, string serverPath)
        {
            string savePath = Path.Combine(serverPath, CSClient_ConfigMgr_FilePath);
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbFun = new StringBuilder();
            for (int i = 0; i < sheetNames.Count; i++)
            {

                string[] classInfo = sheetNames[i].Split('#');
                string name = Utils.ToFirstUpper(classInfo[0].Replace("s_", "").Replace("c_", ""));
                string classDes = classInfo.Length > 1 ? classInfo[1] : string.Empty;
                string fieldName = "dic" + name;
                string className = name + "Config";
                sbField.AppendLine($"        /// <summary> {classDes}</summary>");
                sbField.AppendLine($"        public readonly Dictionary<int, {className}> {fieldName} = new Dictionary<int, {className}>();");
                sbFun.AppendLine($"            readConfig({fieldName});");
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
using GameLib;
using System.Collections.Generic;

namespace GameClient.ConfigMgr
{{
    public partial class ConfigMgr
    {{
{sbField}
        private ConfigHelper configHelper;
        public ConfigMgr()
        {{
            configHelper = new ConfigHelper();
{sbFun}            customRead();
        }}

        private void readConfig<T>(Dictionary<int,T> source) where T:BaseConfig
        {{
            List<T> list = configHelper.Select<T>();
            foreach (T _item in list)
                source.Add(_item.UniqueID, _item);

            if (list.Count == 0)
                Logger.LogWarning($""配置表{{ typeof(T).Name}} 没有数据"");
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 生成GM服务端配置文件
        /// <summary>
        /// 生成GM服务端配置文件
        /// </summary>       
        /// <param name="ExcelPath">Excel目录</param>
        /// <param name="clientPath">客户端路径</param> 
        /// <param name="clientSavePath">客户端导出json保存路径</param> 
        /// <param name="callback">执行完回调</param>   
        public static void GenerateGMServerExcel(string ExcelDir, string clientPath, string clientSavePath, Action callback = null)
        {
            DirectoryInfo folder = new DirectoryInfo(ExcelDir);
            List<string> clientConfigName = new List<string>();

            //删除相关文件夹下的全部文件
            Utils.DeleteDirectory(Path.Combine(clientPath, GMServer_ConfigClass_FilePath)); //清空下面所有文件

            foreach (FileInfo file in folder.GetFiles("*.xls*"))
            {
                if (file.Name.StartsWith("~$") || file.Name.StartsWith("_"))
                    continue;

                //获取配置表中所有表
                DataSet ds = ExcelUtil.ReadExcelSheetData(file.FullName);
                foreach (DataTable dt in ds.Tables)
                {
                    //判断此表是否需要导出
                    if (ExcelUtil.isExport(dt.TableName, false))
                    {
                        string saveClassPath = Path.Combine(clientPath, GMServer_ConfigClass_FilePath);
                        ExcelTools.ParseExcelSheet(clientSavePath, saveClassPath, dt, 4, true);
                        clientConfigName.Add(dt.TableName);
                    }
                }
            }
            //生成客户服务端ConfigInit文件
            if (clientConfigName.Count > 0)
            {
                createGMServerConfigInitFile(clientConfigName, clientPath);
                createGMServerConfigMgrFile(clientConfigName, clientPath);
            }

            if (callback != null)
                callback();
        }
        #endregion

        #region 导出GM服务端ConfigInit.cs文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createGMServerConfigInitFile(List<string> sheetNames, string csCleintPath)
        {
            string savePath = Path.Combine(csCleintPath, GMServer_ConfigInit_FilePath);
            StringBuilder sbConfig = new StringBuilder();
            for (int i = 0; i < sheetNames.Count; i++)
            {

                string[] classInfo = sheetNames[i].Split('#');
                string className = classInfo[0].Replace("s_", "").Replace("c_", "") + "Config";
                string classDes = classInfo.Length > 1 ? classInfo[1] : string.Empty;
                className = Utils.ToFirstUpper(className);
                sbConfig.AppendLine($"            readConfig<{className}>(); //{classDes}");
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer.XGame.ConfigMgr
{{
    public partial class ConfigHelper
    {{
        void ConfigInit()
        {{            
{sbConfig}        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出GMConfigMgr.cs文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createGMServerConfigMgrFile(List<string> sheetNames, string serverPath)
        {
            string savePath = Path.Combine(serverPath, GMServer_ConfigMgr_FilePath);
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbFun = new StringBuilder();
            for (int i = 0; i < sheetNames.Count; i++)
            {

                string[] classInfo = sheetNames[i].Split('#');
                string name = Utils.ToFirstUpper(classInfo[0].Replace("s_", "").Replace("c_", ""));
                string classDes = classInfo.Length > 1 ? classInfo[1] : string.Empty;
                string fieldName = "dic" + name;
                string className = name + "Config";
                sbField.AppendLine($"        /// <summary> {classDes}</summary>");
                sbField.AppendLine($"        public readonly Dictionary<int, {className}> {fieldName} = new Dictionary<int, {className}>();");
                sbFun.AppendLine($"            readConfig({fieldName});");
            }
            string str = $@"/// <summary>
/// 工具生成，不要修改
/// </summary>
using GameLib;
using System.Collections.Generic;

namespace GameServer.XGame.ConfigMgr
{{
    public partial class ConfigMgr
    {{
{sbField}
        private ConfigHelper configHelper;
        public ConfigMgr()
        {{
            configHelper = new ConfigHelper();
{sbFun}            customRead();
        }}

        private void readConfig<T>(Dictionary<int,T> source) where T:BaseConfig
        {{
            List<T> list = configHelper.Select<T>();
            foreach (T _item in list)
                source.Add(_item.UniqueID, _item);

            if (list.Count == 0)
                Logger.LogWarning($""配置表{{ typeof(T).Name}} 没有数据"");
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion
    }
}
