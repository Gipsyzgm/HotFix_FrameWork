using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Tools.ProtoExport;
namespace Tools
{
   
    public class ProtoExportHelper
    {
        private const string Config_FilePath = @"_config.txt";                        //proto配置文件     相对protoDir文件夹
        private const string ProtoC_FileName = @"protoc.exe";                       //服务端导出Proto工具路径
        private const string Server_Proto_FilePath = @"GameServer\Socket\ProtoBuf\Proto";                           //Proto生成保存位置,相对serverPath文件夹 全部生成时清空下面所有文件
        private const string Server_ProtocolType_FilePath = @"GameServer\Socket\ProtoBuf\ProtocolType.cs";  //相对serverPath文件夹
        private const string Server_NetReqProtocol_FilePath = @"GameServer\Net";                                      //相对serverPath文件夹
        private const string Server_NetAction_FilePath = @"GameServer\Net\NetAction.cs";                           //相对serverPath文件夹

        private const string Clinet_Proto_FilePath = @"src\CNet\Protos";               //Proto生成保存位置,相对clinetPath文件夹 全部生成时清空下面所有文件
        private const string Clinet_Proto_ResPath = @"bin\h5\res\proto";              //客户端Proto资源文件目录,从公用目录中复制一个过去 全部生成时清空下面所有文件
        private const string Client_ProtocolType_FilePath = @"src\CNet\ProtocolType.as";     //相对clientPath文件夹
        private const string Client_NetRspProtocol_FilePath = @"src\CNet\Respond";           //相对clientPath文件夹
        private const string Client_NetAction_FilePath = @"src\CNet\Respond\NetAction.as";//相对clientPath文件夹
        private const string Client_CProto_FilePath = @"src\CNet\CProto.as";                    //相对clientPath文件夹


        private const string CSClient_Proto_FilePath = @"GameClient\Socket\ProtoBuf\Proto";
        private const string CSClient_ProtocolType_FilePath = @"GameClient\Socket\ProtoBuf\ProtocolType.cs";  //相对CSClient文件夹
        private const string CSClient_NetReqProtocol_FilePath = @"GameClient\Net";                                      //相对CSClient文件夹
        private const string CSClient_NetAction_FilePath = @"GameClient\Net\NetAction.cs";                           //相对CSClient文件夹

        #region 获取解析好的Config配置文件
        /// <summary>
        /// 创建服务端Net相关文件
        /// </summary>
        /// <param name="protoDir">proto目录</param>
        /// <param name="callback">执行完回调</param>
        private static List<ProtoConifg> getConfigList(string configPath)
        {
            List<ProtoConifg> configList = new List<ProtoConifg>();
            StreamReader sr = File.OpenText(configPath);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                line = Regex.Replace(line, "[\t ]", "");  //去tab,去空格
                if (line != string.Empty && !line.StartsWith("#"))
                {
                    ProtoConifg config = new ProtoConifg();
                    string[] array_info = line.Split('=');
                    string[] array_infoId = array_info[0].Split(',');
                    config.ThreadId = Convert.ToInt32(array_infoId[0]);    //工作线程ID
                    config.MsgId = Convert.ToUInt16(array_infoId[1]);   //protocolId

                    array_info = array_info[1].Split('#');
                    config.MsgName = array_info[0];
                    config.Comment = string.Empty;
                    if (array_info.Length > 1)
                        config.Comment = array_info[1];
                    configList.Add(config);
                }
            }
            sr.Close();
            return configList;
        }
        #endregion

        #region 使用ProtoGen工具导出服务端Proto文件        
        /// <summary>
        /// 使用ProtoGen工具生成protoC#文件
        /// </summary>       
        /// <param name="protoGenPath">工具目录</param>
        /// <param name="protoPath">proto目录</param>
        /// <param name="serverPath">服务器根目录</param> 
        /// <param name="SelectFiles">选中的导出文件,null全部导出</param>   
        /// <param name="callback">执行完回调</param>   
        public static void GenerateSelectProtoServerCS(string protoGenPath, string protoDir, string serverPath, List<string> SelectFiles, Action callback = null)
        {
            string savePath = Path.Combine(serverPath, Server_Proto_FilePath); //proto生成后保存的路径
            if (SelectFiles == null) //全选了
                Utils.DeleteDirectory(savePath); //清空下面所有文件
            Utils.CheckCreateDirectory(savePath);
            string protogen = Path.Combine(protoGenPath, ProtoC_FileName);

            DirectoryInfo folder = new DirectoryInfo(protoDir);
            List<string> cmds = new List<string>();
            foreach (FileInfo file in folder.GetFiles("*.proto"))
            {
                if (SelectFiles != null && !SelectFiles.Contains(file.Name))
                    continue;
                // string cmd = $"{protogen} -w:{protoDir} -i:{file.Name} -o:{savePath}\\{file.Name.Split('.')[0]}.cs";
                string cmd = $"{protogen} --csharp_out={savePath} -I {protoDir} {file.FullName}";
                cmds.Add(cmd);
            }
            Utils.Cmd(cmds);
            if (callback != null)
                callback();
        }
        #endregion

        #region 创建服务端Net相关文件
        /// <summary>
        /// 创建服务端Net相关文件
        /// </summary>
        /// <param name="protoDir">proto目录</param>
        /// <param name="serverPath">服务器根目录</param>
        /// <param name="callback">执行完回调</param>
        public static void CreateServerNetFile(string protoDir, string serverPath, Action callback = null)
        {
            string configPath = Path.Combine(protoDir, Config_FilePath);
            List<ProtoConifg> configList = getConfigList(configPath);
            createServerProtocolTypeFile(configList, serverPath);
            createServerNetActionFile(configList, serverPath);
            createServerNetReqProtocolFile(configList, serverPath);
            if (callback != null)
                callback();
        }
        #endregion

        #region 导出服务端ProtocolType.cs文件      
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createServerProtocolTypeFile(List<ProtoConifg> configList, string serverPath)
        {
            string savePath = Path.Combine(serverPath, Server_ProtocolType_FilePath);
            StringBuilder rspProtocol = new StringBuilder();
            StringBuilder reqProtocol = new StringBuilder();
            string msgName = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                msgName = configList[i].MsgName.Split('.')[1].ToLower();
                if (msgName.StartsWith("rsp_"))
                    rspProtocol.AppendLine($"        typeToProtocolDic.Add(typeof({configList[i].MsgName}), {configList[i].MsgId}); //{ configList[i].Comment}");
                else if (msgName.StartsWith("req_"))
                    reqProtocol.AppendLine($"            case {configList[i].MsgId}:msg = new {configList[i].MsgName}(); break;//{ configList[i].Comment}");
            }
            string str = $@"using System;
using System.Collections.Generic;
using Google.Protobuf;
/// <summary>
/// 工具生成，不要修改
/// </summary>
public class ProtocolType
{{
    private Dictionary<Type, ushort> typeToProtocolDic = new Dictionary<Type, ushort>();

    private static readonly ProtocolType instance = new ProtocolType();
    public static ProtocolType Instance => instance;
   
    private ProtocolType()
    {{
{ rspProtocol}    }}

    /// <summary>
    /// 跟据发送消息类型获取消息协议号
    /// </summary>
    public ushort GetProtocolByType(Type type)
    {{
        ushort protocol = 0;
        typeToProtocolDic.TryGetValue(type, out protocol);
        return protocol;
    }}
    /// <summary>
    /// 跟据收到的请求协议号创建消息数据结构类
    /// </summary>
    public IMessage CreateMsgByProtocol(ushort protocl)
    {{
        IMessage msg = null;
        switch (protocl)
        {{
{ reqProtocol}        }}
        return msg;
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出服务端NetAction.cs文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createServerNetActionFile(List<ProtoConifg> configList, string serverPath)
        {
            string savePath = Path.Combine(serverPath, Server_NetAction_FilePath);
            StringBuilder sbProtocol = new StringBuilder();
            StringBuilder sbThread = new StringBuilder();
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');
                if (info[1].ToLower().StartsWith("req_")) //客户端发的请求消息
                {
                    string fileName = info[1].Substring(4);
                    fileName = Utils.ToFirstUpper(fileName);
                    string comment = configList[i].Comment != string.Empty ? ("      //" + configList[i].Comment) : string.Empty;
                    sbProtocol.AppendLine($"            _actionList.Add({configList[i].MsgId}, {fileName});{comment}");
                    sbThread.AppendLine($"            _actionThreadList.Add({configList[i].MsgId}, {configList[i].ThreadId});{comment}");
                }
            }
            string str = $@"using System;
using System.Collections.Generic;
using GameLib;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer
{{
    public partial class Net
    {{
        private Dictionary<ushort, Action<NetEventArgs>> _actionList = new Dictionary<ushort, Action<NetEventArgs>>();
        private Dictionary<ushort, int> _actionThreadList = new Dictionary<ushort, int>();
        void InitAction()
        {{
{ sbProtocol.ToString() }

{ sbThread.ToString() }
        }}
        void dispatchAction(NetEventArgs e)
        {{
            try
            {{
                ushort protocol = e.Msg.Protocol;
                if (_actionList.ContainsKey(protocol))
                    WorkToExeThread(e, _actionThreadList[protocol]);     //_actionList[protocol](e);              
                else
                    Logger.LogError(""[NetAction]消息"", protocol, ""没加到行为列表中!"");
            }}
            catch (Exception ex)
            {{
                Logger.LogError(ex.Message, ex.StackTrace);
            }}
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出服务端收到的Net请求协议文件
        /// <summary>
        /// 创建户端请求协议文件
        /// </summary>
        private static void createServerNetReqProtocolFile(List<ProtoConifg> configList, string serverPath)
        {
            string savePath = Path.Combine(serverPath, Server_NetReqProtocol_FilePath);
            StringBuilder sbProtocol = new StringBuilder();
            string saveFilePath = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');    //login.req_login_verify
                if (info[1].ToLower().StartsWith("req_")) //客户端发的请求消息
                {
                    string fileName = Utils.ToFirstUpper(info[1].Substring(4)); //Login_verify
                    saveFilePath = Path.Combine(savePath, Utils.ToFirstUpper(info[0]), fileName + ".cs");
                    string str = $@"using {info[0]};
namespace GameServer
{{
    public partial class Net
    {{
        //{ configList[i].Comment}
        void {fileName}(NetEventArgs e)
        {{
            //收到的数据
            {info[1]} data = e.Msg.GetData<{info[1]}>(); 

            //发送数据
            //Rsp_{info[1].Substring(4)} sendData = new Rsp_{info[1].Substring(4)}();            
            //e.Send(sendData);
        }}
    }}
}}";
                    Utils.SaveFile(saveFilePath, str, false);
                }
            }
        }
        #endregion
        //===========================================
        #region 导出客户端Proto文件
        /// <summary>
        /// 生成客户端Proto文件
        /// </summary>       
        /// <param name="protoGenPath">工具目录</param>
        /// <param name="protoPath">proto目录</param>
        /// <param name="clientPath">客户端根目录</param> 
        /// <param name="SelectFiles">选中的导出文件,null全部导出</param>   
        /// <param name="callback">执行完回调</param>   
        public static void GenerateSelectProtoClientAS(string protoDir, string clientPath, List<string> SelectFiles, Action callback = null)
        {
            string savePath = Path.Combine(clientPath, Clinet_Proto_FilePath); //proto生成后保存的路径
            string copyProtoPath = Path.Combine(clientPath, Clinet_Proto_ResPath);
            if (SelectFiles == null) //全选了
            {
                Utils.DeleteDirectory(savePath); //清空下面所有文件
                Utils.DeleteDirectory(copyProtoPath);
            }
            Utils.CheckCreateDirectory(savePath);
            Utils.CheckCreateDirectory(copyProtoPath);

            DirectoryInfo folder = new DirectoryInfo(protoDir);
            foreach (FileInfo file in folder.GetFiles("*.proto"))
            {
                if (SelectFiles != null && !SelectFiles.Contains(file.Name))
                    continue;
                //生成Proto文件
                string targetPath = Path.Combine(copyProtoPath, file.Name.Replace(".proto", ".dat"));
                //File.Copy(file.FullName, targetPath, true);
                //Logger.Log("复制Proto文件到:" + targetPath);
                string txt = File.ReadAllText(file.FullName);
                txt = Regex.Replace(txt, "//.*\n", "");//去注释
                byte[] data = Utils.CompressBytes(txt);
                Utils.SaveFile(targetPath, data);

                //生成类文件
                ProtoParseClientHelper.GenerateProtoClinetAs(file, savePath);
               

            }

            if (callback != null)
                callback();
        }
        #endregion

        #region 创建客户端Net相关文件
        /// <summary>
        /// 创建服务端Net相关文件
        /// </summary>
        /// <param name="protoDir">proto目录</param>
        /// <param name="clientPath">服务器根目录</param>
        /// <param name="callback">执行完回调</param>
        public static void CreateClientNetFile(string protoDir, string clientPath, Action callback = null)
        {
            string configPath = Path.Combine(protoDir, Config_FilePath);
            List<ProtoConifg> configList = getConfigList(configPath);
            createClientProtocolTypeFile(configList, clientPath);
            createClientNetActionFile(configList, clientPath);
            createClientNetRspProtocolFile(configList, clientPath);
            createClientCProtoFile(protoDir, clientPath);
            if (callback != null)
                callback();
        }
        #endregion

        #region 导出客户端ProtocolType.as文件      
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createClientProtocolTypeFile(List<ProtoConifg> configList, string clientPath)
        {
            //configList [10000,"Login.Req_login_verify","登录验证"]
            string savePath = Path.Combine(clientPath, Client_ProtocolType_FilePath);
            StringBuilder sbProtocol = new StringBuilder();
            for (int i = 0; i < configList.Count; i++)
            {
                if (configList[i].Comment != string.Empty)
                    sbProtocol.AppendLine($"			//{ configList[i].Comment}");
                string pbName = configList[i].MsgName.Split('.')[0] + "PB";
                sbProtocol.AppendLine($@"			protocolToTypeDic.put({configList[i].MsgId}, Net.proto.{pbName}.lookup(""{configList[i].MsgName}""));"); //Login.Req_login_verify
                sbProtocol.AppendLine($@"			typeToProtocolDic.put(""CNet.Protos.{configList[i].MsgName}"", {configList[i].MsgId});");
            }
            string str = $@"/**工具生成不要修改*/
package CNet
{{
    import Mgr.Hashtable;
	/**协议号和消息类型对应关系*/
	public class ProtocolType
	{{		
		private static var protocolToTypeDic:Hashtable = new Hashtable();
		private static var typeToProtocolDic:Hashtable = new Hashtable();		
		private static var instance:ProtocolType = new ProtocolType();
		public static function get Instance():ProtocolType{{return instance;}}
		
		public function ProtocolType()
		{{
{ sbProtocol.ToString() }		}}
			
		/**
		 * 跟据协议号获了协议类型
		 */
		public function GetTypeByProtocol(protocol:int):*
		{{
			return protocolToTypeDic.get(protocol)
		}}
		/**
		 * 跟据协议类型获取协议号
		 */
		public function GetProtocolByType(type:String):int
		{{		
			return typeToProtocolDic.get(type)
		}}
	}}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出客户端NetAction.as文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createClientNetActionFile(List<ProtoConifg> configList, string serverPath)
        {
            string savePath = Path.Combine(serverPath, Client_NetAction_FilePath);
            StringBuilder sbImport = new StringBuilder();
            StringBuilder sbProtocol = new StringBuilder();
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');
                if (info[1].ToLower().StartsWith("rsp_")) //服务端回应的消息
                {
                    string fileName = info[1].Substring(4);
                    fileName = Utils.ToFirstUpper(fileName);
                    string comment = configList[i].Comment != string.Empty ? ("      //" + configList[i].Comment) : string.Empty;
                    sbProtocol.AppendLine($"			_actionList.put({configList[i].MsgId}, {fileName}.ReceivedMsg);{comment}");

                    sbImport.AppendLine($"	import CNet.Respond.{Utils.ToFirstUpper(info[0])}.{fileName};");
                }
            }
            string str = $@"/** 工具生成不要修改 */
package CNet.Respond
{{    
	import Mgr.Hashtable;
 {sbImport}   
	/**收到消息进行派发*/
	public class NetAction
	{{		
		private static var _actionList:Hashtable = new Hashtable();		
		private static var instance:NetAction = new NetAction();
		public static function get Instance():NetAction	{{return instance;}}
		
		public function NetAction()
		{{
{sbProtocol}
		}}
		/**派发回应消息*/
		public function dispatch(protocol:int,data:*):void
		{{
			if(_actionList.containsKey(protocol))
				_actionList.get(protocol)(data);
			else
				trace(""[NetAction]消息""+protocol+""没加到行为列表中!"");
        }}
	}}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出客户端收到的Net回应协议文件
        /// <summary>
        /// 创建服务端回应协议文件
        /// </summary>
        private static void createClientNetRspProtocolFile(List<ProtoConifg> configList, string clientPath)
        {
            string savePath = Path.Combine(clientPath, Client_NetRspProtocol_FilePath);
            StringBuilder sbProtocol = new StringBuilder();
            string saveFilePath = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');    //login.req_login_verify
                if (info[1].ToLower().StartsWith("rsp_")) //服务端回应消息
                {
                    string fileName = Utils.ToFirstUpper(info[1].Substring(4)); //Login_verify
                    saveFilePath = Path.Combine(savePath, Utils.ToFirstUpper(info[0]), fileName + ".as");
                    string str = $@"package CNet.Respond.{Utils.ToFirstUpper(info[0])}
{{
    import CNet.Net;
    import CNet.Protos.{info[0]}.{info[1]}
	/**{ configList[i].Comment}*/	
	public class {fileName}
	{{		
		public static function ReceivedMsg(msg:{info[1]}):void
		{{

		}}
	}}	
}}";
                    Utils.SaveFile(saveFilePath, str, false);
                }
            }
        }
        #endregion

        #region 导出客户端CProto.as文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createClientCProtoFile(string protoDir, string clientPath)
        {

            string savePath = Path.Combine(clientPath, Client_CProto_FilePath);

            DirectoryInfo folder = new DirectoryInfo(protoDir);
            StringBuilder sbFiled = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            StreamReader sr;
            foreach (FileInfo file in folder.GetFiles("*.proto"))
            {
                string fileName = file.Name.Split('.')[0];
                string fuFileName = Utils.ToFirstUpper(file.Name.Split('.')[0]);

                sr = File.OpenText(file.FullName);
                string comment = sr.ReadLine().TrimStart('/'); //读取第一行做为注释..
                sr.Close();
                sbFiled.AppendLine($"		/**{comment}*/");
                sbFiled.AppendLine($"		public var {fileName}PB:*;");
                //sbValue.AppendLine($@"			pb.load(""res/proto/{fileName}.proto"",function(err:*,root:*):void{{if(err){{trace(err);}};{fileName}PB = root;}});");
                sbValue.AppendLine($@"			resArr.push({{url:""res/proto/{fileName}.dat"", type:Loader.BUFFER}});");
            }

            string str = $@"/**工具生成不要修改 */
package CNet
{{
    import Mgr.ByteArray;	
    import Mgr.version.VerMgr;
    import Mgr.version.VerType;
	import laya.net.Loader;
	import laya.utils.Handler;

	/**所有Proto消息结构*/
	public class CProto
	{{		
{sbFiled}
        public var resArr:Array = [];  //存放资源数组	
		public function CProto()
		{{
			//var pb:*;
			//__JS__(""pb = protobuf"");
            //pb.load(""res/proto/PbBag.proto"",function(err:*,root:*):void{{if(err){{trace(err);}};PbBagPB = root;}});
{ sbValue}
            VerMgr.SetUrlVer(resArr,VerType.Protobuf);
			Laya.loader.load(resArr, Handler.create(this, onComplete));
		}}

		private function onComplete():void 
		{{
            var pb:*;
			__JS__(""pb = protobuf"");	

            for (var k:int = 0; k < resArr.length; k++) 
			{{
                var url:String = resArr[k].url;
                var arr:ArrayBuffer = Loader.getRes(url);
                if (arr == null)
                {{
                    trace(""PB文件加载失败,URL: "" + url);
                }}
                else
                {{
                    var bytes:ByteArray = new ByteArray();
                    bytes.writeArrayBuffer(arr);
                    bytes.pos = 0;
                    bytes.uncompress();
                    var str:String = bytes.toString();
                    var name:String = url.substring(url.lastIndexOf(""/"") + 1, url.lastIndexOf("".""));
                    this[name + ""PB""] = pb.parse(str).root;
                }}
            }}
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);         

        }
        #endregion

        //===========================================
        #region 使用ProtoGen工具导出CS客户端Proto文件        
        /// <summary>
        /// 使用ProtoGen工具生成protoC#文件
        /// </summary>       
        /// <param name="protoGenPath">工具目录</param>
        /// <param name="protoPath">proto目录</param>
        /// <param name="csClientPath">服务器根目录</param> 
        /// <param name="SelectFiles">选中的导出文件,null全部导出</param>   
        /// <param name="callback">执行完回调</param>   
        public static void GenerateSelectProtoCSClientCS(string protoGenPath, string protoDir, string csClientPath, List<string> SelectFiles, Action callback = null)
        {
            string savePath = Path.Combine(csClientPath, CSClient_Proto_FilePath); //proto生成后保存的路径
            if (SelectFiles == null) //全选了
                Utils.DeleteDirectory(savePath); //清空下面所有文件
            Utils.CheckCreateDirectory(savePath);
            string protogen = Path.Combine(protoGenPath, ProtoC_FileName);

            DirectoryInfo folder = new DirectoryInfo(protoDir);
            List<string> cmds = new List<string>();
            foreach (FileInfo file in folder.GetFiles("*.proto"))
            {
                if (SelectFiles != null && !SelectFiles.Contains(file.Name))
                    continue;
                // string cmd = $"{protogen} -w:{protoDir} -i:{file.Name} -o:{savePath}\\{file.Name.Split('.')[0]}.cs";
                string cmd = $"{protogen} --csharp_out={savePath} -I {protoDir} {file.FullName}";
                cmds.Add(cmd);
            }
            Utils.Cmd(cmds);
            if (callback != null)
                callback();
        }
        #endregion

        #region 创建CS客户端Net相关文件
        /// <summary>
        /// 创建服务端Net相关文件
        /// </summary>
        /// <param name="protoDir">proto目录</param>
        /// <param name="csClientPath">服务器根目录</param>
        /// <param name="callback">执行完回调</param>
        public static void CreateCSClientNetFile(string protoDir, string csClientPath, Action callback = null)
        {
            string configPath = Path.Combine(protoDir, Config_FilePath);
            List<ProtoConifg> configList = getConfigList(configPath);
            createCSClientProtocolTypeFile(configList, csClientPath);
            createCSClientNetActionFile(configList, csClientPath);
            createCSClientNetRspProtocolFile(configList, csClientPath);
            if (callback != null)
                callback();
        }
        #endregion

        #region 导出服务端ProtocolType.cs文件      
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createCSClientProtocolTypeFile(List<ProtoConifg> configList, string csClientPath)
        {
            string savePath = Path.Combine(csClientPath, CSClient_ProtocolType_FilePath);
            StringBuilder reqProtocol = new StringBuilder();
            StringBuilder rspProtocol = new StringBuilder();
            string msgName = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                msgName = configList[i].MsgName.Split('.')[1].ToLower();
                if (msgName.StartsWith("req_"))
                    reqProtocol.AppendLine($"        typeToProtocolDic.Add(typeof({configList[i].MsgName}), {configList[i].MsgId}); //{ configList[i].Comment}");
                else if (msgName.StartsWith("rsp_"))
                    rspProtocol.AppendLine($"            case {configList[i].MsgId}:msg = new {configList[i].MsgName}(); break;//{ configList[i].Comment}");
            }
            string str = $@"using System;
using System.Collections.Generic;
using Google.Protobuf;
/// <summary>
/// 工具生成，不要修改
/// </summary>
public class ProtocolType
{{
    private Dictionary<Type, ushort> typeToProtocolDic = new Dictionary<Type, ushort>();

    private static readonly ProtocolType instance = new ProtocolType();
    public static ProtocolType Instance => instance;
   
    private ProtocolType()
    {{
{ reqProtocol}    }}

    /// <summary>
    /// 跟据发送消息类型获取消息协议号
    /// </summary>
    public ushort GetProtocolByType(Type type)
    {{
        ushort protocol = 0;
        typeToProtocolDic.TryGetValue(type, out protocol);
        return protocol;
    }}
    /// <summary>
    /// 跟据收到的请求协议号创建消息数据结构类
    /// </summary>
    public IMessage CreateMsgByProtocol(ushort protocl)
    {{
        IMessage msg = null;
        switch (protocl)
        {{
{ rspProtocol}        }}
        return msg;
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出服务端NetAction.cs文件
        /// <summary>
        /// 创建 ProtocolType.cs文件
        /// </summary>
        private static void createCSClientNetActionFile(List<ProtoConifg> configList, string csClientPath)
        {
            string savePath = Path.Combine(csClientPath, CSClient_NetAction_FilePath);
            StringBuilder sbProtocol = new StringBuilder();
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');
                if (info[1].ToLower().StartsWith("rsp_")) //客户端发的请求消息
                {
                    string fileName = info[1].Substring(4);
                    fileName = Utils.ToFirstUpper(fileName);
                    string comment = configList[i].Comment != string.Empty ? ("      //" + configList[i].Comment) : string.Empty;
                    sbProtocol.AppendLine($"            _actionList.Add({configList[i].MsgId}, {fileName});{comment}");
                }
            }
            string str = $@"using System;
using System.Collections.Generic;
using GameLib;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameClient
{{
    public partial class Net
    {{
        private Dictionary<ushort, Action<Message>> _actionList = new Dictionary<ushort, Action<Message>>();
        void InitAction()
        {{
{ sbProtocol.ToString() }
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        #endregion

        #region 导出服务端收到的Net请求协议文件
        /// <summary>
        /// 创建户端收到协议文件
        /// </summary>
        private static void createCSClientNetRspProtocolFile(List<ProtoConifg> configList, string csClientPath)
        {
            string savePath = Path.Combine(csClientPath, CSClient_NetReqProtocol_FilePath);
            StringBuilder sbProtocol = new StringBuilder();
            string saveFilePath = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');    //login.req_login_verify
                if (info[1].ToLower().StartsWith("rsp_")) //收到服务端回应
                {
                    string fileName = Utils.ToFirstUpper(info[1].Substring(4)); //Login_verify
                    saveFilePath = Path.Combine(savePath, Utils.ToFirstUpper(info[0]), fileName + ".cs");
                    string str = $@"using {info[0]};
namespace GameClient
{{
    public partial class Net
    {{
        //{ configList[i].Comment}
        void {fileName}(Message msg)
        {{
            //收到的数据
            {info[1]} data = msg.GetData<{info[1]}>(); 
           
        }}
    }}
}}";
                    Utils.SaveFile(saveFilePath, str, false);
                }
            }
        }
        #endregion
    }
}