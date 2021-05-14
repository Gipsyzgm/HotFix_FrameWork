using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.ProtoExport.ProtoCSharp
{
    public class ProtoExportClient : ProtoExportBase
    {
        public ProtoExportClient(ProtoCodeOut config) : base(config)
        {

        }

        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList"></param>
        protected override void CreateProtocolType(List<ProtoConifg> configList, int addIndex = -1)
        {
            string savePath = Config.RealityProtocolTypeFile;
            //请求协议,客户端——>服务器
            StringBuilder reqProtocol = new StringBuilder();
            //收到消息,服务器——>客户端
            StringBuilder rspProtocol = new StringBuilder();
            //请求消息/收到消息对应关系
            StringBuilder csListProtocol = new StringBuilder();
            //收到消息/请求消息对应关系
            StringBuilder scListProtocol = new StringBuilder();
            //需要加密的消息
            StringBuilder sbEncrypt = new StringBuilder();
            string msgName = string.Empty;
            Dictionary<int, string> sameSCProto = new Dictionary<int, string>();
            for (int i = 0; i < configList.Count; i++)
            {

                msgName = configList[i].MsgName.Split('.')[1].ToLower();
                //请求的消息ID加入字典
                if (msgName.StartsWith("cs_"))
                    reqProtocol.AppendLine($"            typeToProtocolDic.Add(typeof({configList[i].MsgName}), {configList[i].MsgId}); //{ configList[i].Comment}");
                else if (msgName.StartsWith("sc_"))
                {
                    bool isHaveSameSCMsg = false;
                    if (sameSCProto.TryGetValue(configList[i].MsgId,out var msgname))
                    {
                        if (msgname == configList[i].MsgName) //已经有相相的，不再加
                        {
                            isHaveSameSCMsg = true;
                        }          
                    }
                    if (!isHaveSameSCMsg)
                    {
                        //收到的消息ID加入字典Switch语句
                        rspProtocol.AppendLine($"                case {configList[i].MsgId}:msg = new {configList[i].MsgName}(); break;//{ configList[i].Comment}");
                        if (!sameSCProto.ContainsKey(configList[i].MsgId))
                            sameSCProto.Add(configList[i].MsgId, configList[i].MsgName);
                        else
                            Logger.LogError($"{Config.ProtoType}ClientProtocol里有多个Protocol:{configList[i].MsgId} 相同，消息结构不同,请检查!!  {sameSCProto[configList[i].MsgId]}  {configList[i].MsgName}");
                    }
                }
                if (configList[i].RtnMsgId != 0)
                {
                    //请求消息/收到消息对应关系
                    csListProtocol.AppendLine($"            {{{configList[i].MsgId},{configList[i].RtnMsgId}}},//{ configList[i].Comment}");
                    //收到消息/请求消息对应关系
                    scListProtocol.AppendLine($"            {{{configList[i].RtnMsgId},{configList[i].MsgId}}},");
                }

                if (addIndex == i)
                {
                    reqProtocol.AppendLine($"            //======中转消息======");
                    rspProtocol.AppendLine($"            //======中转消息======");
                    csListProtocol.AppendLine($"            //======中转消息======");
                    scListProtocol.AppendLine($"            //======中转消息======");
                }

                if (configList[i].IsEncrypt)
                    sbEncrypt.AppendLine($"            _encryptList.Add({configList[i].MsgId});");
            }
            string csandscList = string.Empty;
            if (Config.IsProtobuffForILR)
            {
                csandscList = $@"/// <summary>请求消息/收到消息对应关系(禁止连续发送)</summary>
        public Dictionary<ushort, ushort> CSList = new Dictionary<ushort, ushort>()
        {{
{csListProtocol.ToString().TrimEnd(',')}        }};
        /// <summary>收到消息/请求消息对应关系(禁止连续发送)</summary>
        public Dictionary<ushort, ushort> SCList = new Dictionary<ushort, ushort>()
        {{
{scListProtocol.ToString().TrimEnd(',')}        }};";

            }

            string str = $@"using System;
using System.Collections.Generic;
using Google.Protobuf;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Config.RealityNameSpace}.Net
{{
     public class {Config.ProtoType}ClientProtocol
     {{
        private Dictionary<Type, ushort> typeToProtocolDic = new Dictionary<Type, ushort>();

        private static readonly {Config.ProtoType}ClientProtocol instance = new {Config.ProtoType}ClientProtocol();
        public static {Config.ProtoType}ClientProtocol Instance => instance;
        private HashSet<int> _encryptList = new HashSet<int>();

        private {Config.ProtoType}ClientProtocol()
        {{
{ reqProtocol}
{sbEncrypt}        }}
{csandscList}
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
{ rspProtocol}            }}
            return msg;
        }}
        /// <summary>是否加密协议</summary>
        public bool IsEncryptProtocol(ushort protocl)
        {{
            return _encryptList.Contains(protocl);
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        /// <summary>
        /// 创建NetAction
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateNetActionFile(List<ProtoConifg> configList, int addIndex = -1)
        {
            string savePath = Config.RealityNetActionFile;
            //协议
            StringBuilder sbProtocol = new StringBuilder();
            //线程ID
            StringBuilder sbThread = new StringBuilder();
           
            Dictionary<int, string> sameSCProto = new Dictionary<int, string>();
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');
                if (info[1].ToLower().StartsWith("sc_")) //服务器发的消息
                {
                    bool isHaveSameSCMsg = false;
                    if (sameSCProto.TryGetValue(configList[i].MsgId, out var msgname))
                    {
                        if (msgname == configList[i].MsgName) //已经有相相的，不再加
                            isHaveSameSCMsg = true;
                    }
                    if (!isHaveSameSCMsg)                   
                    {
                        string fileName = info[1].Substring(3);
                        fileName = Utils.ToFirstUpper(fileName);
                        //comment描述信息是否为空
                        string comment = configList[i].Comment != string.Empty ? ("      //" + configList[i].Comment) : string.Empty;
                        sbProtocol.AppendLine($"            _actionList.Add({configList[i].MsgId}, {fileName});{comment}");
                        if (configList[i].ThreadId > 0 && SCThreadNum > 0)
                            sbThread.AppendLine($"            _actionThreadList.Add({configList[i].MsgId}, {configList[i].ThreadId});{comment}");

                        if (!sameSCProto.ContainsKey(configList[i].MsgId))
                            sameSCProto.Add(configList[i].MsgId, configList[i].MsgName);
                        else
                            Logger.LogError($"{Config.ProtoType}ClientAction 里有多个Protocol:{configList[i].MsgId} 相同，消息结构不同,请检查!!  {sameSCProto[configList[i].MsgId]}  {configList[i].MsgName}");
                    }                   
                }                
            }
            string instance = string.Empty;
            if (Config.IsMultClinet) //多客户端
            {
                instance = $@"        public {Config.ProtoType}ClientAction()";
            }
            else
            {
                instance = $@"        private static readonly {Config.ProtoType}ClientAction instance = new {Config.ProtoType}ClientAction();
        public static {Config.ProtoType}ClientAction Instance => instance;
        private {Config.ProtoType}ClientAction()";
            }

            #region 线程相关
            string threadDict = string.Empty;
            string initThread = string.Empty;
            string dispathThread = string.Empty;
            if (SCThreadNum > 0) //处理客户端的消息线程数
            {
                threadDict = $@"private Dictionary<ushort, int> _actionThreadList = new Dictionary<ushort, int>();
        private CClientActionThread<{Config.ProtoType}ClientMessage> _actionThread;";

                initThread = $@"
            _actionThread = new CClientActionThread<{Config.ProtoType}ClientMessage>(_actionList,{CSThreadNum});";
                dispathThread = $@"        public void Dispatch({Config.ProtoType}ClientMessage e)
        {{
            int threadId = 0;
            _actionThreadList.TryGetValue(e.Protocol, out threadId);
            _actionThread.WorkToExeThread(e, threadId);
        }}";
            }
            else //使用主线程
            {
                dispathThread = $@"        protected void onDispatchMainThread({Config.ProtoType}ClientMessage e)
        {{
            try
            {{
                 ushort protocol = e.Protocol;
                _actionList[protocol].Invoke(e); 
            }}
            catch (Exception ex)
            {{   
                {(Config.IsProtobuffForILR ? "UnityEngine.Debug.Log" : "Logger.LogError")}(ex.Message+""\n""+ex.StackTrace);
            }}
        }}
        public void Dispatch({Config.ProtoType}ClientMessage e)
        {{
            {(Config.IsMultClinet||Config.IsProtobuffForILR ? "onDispatchMainThread(e);" : "MainThreadContext.Instance.Post(o => {{ onDispatchMainThread(e); }});")}
        }}";
            }
            #endregion

            string str = $@"using System;
using System.Collections.Generic;
using CSocket;
{(!Config.IsProtobuffForILR? "using GameLib;":"")}
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Config.RealityNameSpace}.Net
{{
    public partial class {Config.ProtoType}ClientAction
    {{
        private Dictionary<ushort, Action<{Config.ProtoType}ClientMessage>> _actionList = new Dictionary<ushort, Action<{Config.ProtoType}ClientMessage>>();
        {threadDict}        
 {instance}
        {{
{ sbProtocol }{initThread}
{sbThread}
        }}
{dispathThread}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        /// <summary>
        /// 收到消息NetMessage，拆解成单个脚本
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateNetMessage(List<ProtoConifg> configList, int addIndex = -1)
        {          
            string strcommon = $@"using CSocket;
using Google.Protobuf;
namespace  {Config.RealityNameSpace}.Net
{{
    public class {Config.ProtoType}ClientMessage:CClientMessage
    {{
        public {Config.ProtoType}ClientMessage(ushort protocol, IMessage data) : base(protocol, data)
        {{
           
           
        }}
    }}
}}";
            Utils.SaveFile(Config.RealityProtocolTypeFile.Replace("ClientProtocol.cs", "ClientMessage.cs"), strcommon);

            string savePath = Config.RealityNetReceiveDir;
            string saveFilePath = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');    //login.cs_login_verify
                if (info[1].ToLower().StartsWith("sc_")) //收到服务端回应
                {
                    string fileName = Utils.ToFirstUpper(info[1].Substring(3)); //Login_verify
                    saveFilePath = Path.Combine(savePath, Utils.ToFirstUpper(info[0]), fileName + ".cs");
                    string str = $@"using {info[0]};
namespace  {Config.RealityNameSpace}.Net
{{
    public partial class {Config.ProtoType}ClientAction
    {{
        //{ configList[i].Comment}
        void {fileName}({Config.ProtoType}ClientMessage e)
        {{
            //收到的数据
            {info[1]} msg = e.Msg as {info[1]}; 
           
        }}
    }}
}}";
                    Utils.SaveFile(saveFilePath, str, false);
                }
            }
        }
        //=====
    }
}
