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
        protected override string SaveProtoClass => Glob.codeOutSetting.ServerProto.OutClassDir.ToReality();


        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList"></param>
        protected override void CreateProtocolType(List<ProtoConifg> configList)
        {
            string savePath = Glob.codeOutSetting.ServerProto.ProtocolTypeFile.ToReality();
            StringBuilder rspProtocol = new StringBuilder();
            StringBuilder reqProtocol = new StringBuilder();
            string msgName = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                msgName = configList[i].MsgName.Split('.')[1].ToLower();
                if (msgName.StartsWith("sc_"))
                    rspProtocol.AppendLine($"        typeToProtocolDic.Add(typeof({configList[i].MsgName}), {configList[i].MsgId}); //{ configList[i].Comment}");
                else if (msgName.StartsWith("cs_"))
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
        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateNetActionFile(List<ProtoConifg> configList)
        {
            string savePath = Glob.codeOutSetting.ServerProto.NetActionFile.ToReality();
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
        /// <summary>
        /// NetMessage
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateNetMessage(List<ProtoConifg> configList)
        {
            string savePath = Glob.codeOutSetting.ServerProto.NetDir.ToReality();
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
namespace GameServer
{{
    public partial class Net
    {{
        //{ configList[i].Comment}
        void {fileName}(NetEventArgs e)
        {{
            //收到的数据
            {info[1]} msg = e.Msg.GetData<{info[1]}>(); 

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
