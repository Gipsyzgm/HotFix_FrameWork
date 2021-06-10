using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.ProtoExport.ProtoCSharp
{
    public class ProtoExportServer: ProtoExportBase
    {
        public ProtoExportServer(ProtoCodeOut config) : base(config)
        { 
        }

        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList"></param>
        protected override void CreateProtocolType(List<ProtoConifg> configList, int addIndex = -1)
        {
            string savePath = Config.RealityProtocolTypeFile;
            StringBuilder rspProtocol = new StringBuilder();
            StringBuilder reqProtocol = new StringBuilder();
            StringBuilder sbEncrypt = new StringBuilder();
            string msgName = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                msgName = configList[i].MsgName.Split('.')[1].ToLower();
                if (msgName.StartsWith("sc_"))
                    rspProtocol.AppendLine($"            typeToProtocolDic.Add(typeof({configList[i].MsgName}), {configList[i].MsgId}); //{ configList[i].Comment}");
                else if (msgName.StartsWith("cs_"))
                    reqProtocol.AppendLine($"                case {configList[i].MsgId}:msg = new {configList[i].MsgName}(); break;//{ configList[i].Comment}");

                if (addIndex == i)
                {
                    reqProtocol.AppendLine($"            //======中转消息======");
                    rspProtocol.AppendLine($"                //======中转消息======");
                }

                if (configList[i].IsEncrypt)
                    sbEncrypt.AppendLine($"            _encryptList.Add({configList[i].MsgId});");
            }
            string str = $@"using System;
using System.Collections.Generic;
using Google.Protobuf;
namespace {Config.RealityNameSpace}.Net
{{
    /// <summary>
    /// 工具生成，不要修改
    /// </summary>
    public class {Config.ProtoType}ServerProtocol
    {{
        private Dictionary<Type, ushort> typeToProtocolDic = new Dictionary<Type, ushort>();

        private static readonly {Config.ProtoType}ServerProtocol instance = new {Config.ProtoType}ServerProtocol();
        public static {Config.ProtoType}ServerProtocol Instance => instance;
        private HashSet<int> _encryptList = new HashSet<int>();

        private {Config.ProtoType}ServerProtocol()
        {{
{ rspProtocol}
{sbEncrypt}        }}

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
{reqProtocol}            }}
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
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateNetActionFile(List<ProtoConifg> configList, int addIndex = -1)
        {
            string savePath = Config.RealityNetActionFile;
            StringBuilder sbProtocol = new StringBuilder();
            StringBuilder sbThread = new StringBuilder();
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');
                if (info[1].ToLower().StartsWith("cs_")) //客户端发的请求消息
                {
                    string fileName = info[1].Substring(3);
                    fileName = Utils.ToFirstUpper(fileName);
                    string comment = configList[i].Comment != string.Empty ? ("      //" + configList[i].Comment) : string.Empty;
                    sbProtocol.AppendLine($"            _actionList.Add({configList[i].MsgId}, {fileName});{comment}");
                    if(configList[i].ThreadId>0 && CSThreadNum > 0)
                        sbThread.AppendLine($"            _actionThreadList.Add({configList[i].MsgId}, {configList[i].ThreadId});{comment}");
                }
            }
            string threadDict = string.Empty;
            string initThread = string.Empty;
            string dispathThread = string.Empty;
            if (CSThreadNum > 0) //处理客户端的消息线程数
            {
                threadDict = $@"private Dictionary<ushort, int> _actionThreadList = new Dictionary<ushort, int>();
        private CServerActionThread<{Config.ProtoType}ServerMessage, {Config.ProtoType}ServerSession> _actionThread;";

                initThread = $@"
            _actionThread = new CServerActionThread<{Config.ProtoType}ServerMessage,{Config.ProtoType}ServerSession>(_actionList,{CSThreadNum});";
                dispathThread = $@"        public void Dispatch({Config.ProtoType}ServerMessage e)
        {{
            int threadId = 0;
            _actionThreadList.TryGetValue(e.Protocol, out threadId);
            _actionThread.WorkToExeThread(e, threadId);
        }}";
            }
            else //使用主线程
            {
                dispathThread = $@"        protected void onDispatchMainThread({Config.ProtoType}ServerMessage e)
        {{
            try
            {{               
                 ushort protocol = e.Protocol;
                _actionList[protocol].Invoke(e); 
            }}
            catch (Exception ex)
            {{   
                Logger.LogError(ex.Message, ex.StackTrace);
            }}
        }}
        public void Dispatch({Config.ProtoType}ServerMessage e)
        {{
            MainThreadContext.Instance.Post(o => {{ onDispatchMainThread(e); }});
        }}";
            }
            string str = $@"using System;
using System.Collections.Generic;
using CommonLib;
using CSocket;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Config.RealityNameSpace}.Net
{{
    public partial class {Config.ProtoType}ServerAction
    {{
        private Dictionary<ushort, Action<{Config.ProtoType}ServerMessage>> _actionList = new Dictionary<ushort, Action<{Config.ProtoType}ServerMessage>>();
        {threadDict}
        private static readonly {Config.ProtoType}ServerAction instance = new {Config.ProtoType}ServerAction();
        public static {Config.ProtoType}ServerAction Instance => instance;
        private {Config.ProtoType}ServerAction()
        {{
{ sbProtocol}{initThread}
{sbThread }        }}
{dispathThread}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        /// <summary>
        /// NetMessage
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateNetMessage(List<ProtoConifg> configList, int addIndex = -1)
        {
           
            string strcommon = $@"using CSocket;
using Google.Protobuf;
using CommonLib;
using Telepathy;
namespace  {Config.RealityNameSpace}.Net
{{
    public class {Config.ProtoType}ServerMessage:CServerMessage
    {{
        public {Config.ProtoType}ServerMessage(Server server, int clientid) : base(server,clientid)
        {{
                
        }}
        public {Config.ProtoType}ServerMessage(Server server,int clientid, IMessage msg, ushort protocol) : base(server,clientid,msg,protocol)
        {{
                     
        }}

        public override void Send<M>(M data)
        {{
            ushort protocol = {Config.ProtoType}ServerProtocol.Instance.GetProtocolByType(data.GetType());
            if (protocol == 0)
            {{
                Logger.MessageError(data.GetType());
                return;
            }}
            byte[] body = data.ToByteArray();
            Logger.LogMsg(true, protocol, body.Length, data);
            SendMessage(body, protocol);                     
        }}
    }}    
}}";
         
            Utils.SaveFile(Config.RealityProtocolTypeFile.Replace("ServerProtocol.cs", "ServerMessage.cs"), strcommon);
            
            string savePath = Config.RealityNetReceiveDir;
            StringBuilder sbProtocol = new StringBuilder();
            string saveFilePath = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');    //login.cs_login_verify
                if (info[1].ToLower().StartsWith("cs_")) //客户端发的请求消息
                {
                    string fileName = Utils.ToFirstUpper(info[1].Substring(3)); //Login_verify
                    saveFilePath = Path.Combine(savePath, Utils.ToFirstUpper(info[0]), fileName + ".cs");
                    string str = $@"using {info[0]};
namespace {Config.RealityNameSpace}.Net
{{
    public partial class {Config.ProtoType}ServerAction
    {{
        //{ configList[i].Comment}
        void {fileName}({Config.ProtoType}ServerMessage e)
        {{
            //收到的数据
            {info[1]} msg = e.Msg as {info[1]}; 

            //发送数据
            //SC_{info[1].Substring(3)} sendMsg = new SC_{info[1].Substring(3)}();            
            //e.Send(sendMsg);
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
