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
        protected override string SaveProtoClass => Glob.codeOutSetting.ClientProto.OutClassDir.ToReality();

        protected override string ProtoC_File => "3rdLib/protoc_ilr.exe";

        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList"></param>
        protected override void CreateProtocolType(List<ProtoConifg> configList)
        {
            string savePath = Glob.codeOutSetting.ClientProto.ProtocolTypeFile.ToReality();
            StringBuilder reqProtocol = new StringBuilder();
            StringBuilder rspProtocol = new StringBuilder();

            StringBuilder csListProtocol = new StringBuilder();
            StringBuilder scListProtocol = new StringBuilder();
            string msgName = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                msgName = configList[i].MsgName.Split('.')[1].ToLower();
                if (msgName.StartsWith("cs_"))
                    reqProtocol.AppendLine($"            typeToProtocolDic.Add(typeof({configList[i].MsgName}), {configList[i].MsgId}); //{ configList[i].Comment}");
                else if (msgName.StartsWith("sc_"))
                    rspProtocol.AppendLine($"                case {configList[i].MsgId}:msg = new {configList[i].MsgName}(); break;//{ configList[i].Comment}");

                if (configList[i].RtnMsgId != 0)
                {
                    csListProtocol.AppendLine($"            {{{configList[i].MsgId},{configList[i].RtnMsgId}}},//{ configList[i].Comment}");
                    scListProtocol.AppendLine($"            {{{configList[i].RtnMsgId},{configList[i].MsgId}}},");
                }
            }
            string str = $@"using System;
using System.Collections.Generic;
using Google.Protobuf;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Glob.projectSetting.HoxProjectName}
{{
    public class ProtocolType
    {{
        private Dictionary<Type, ushort> typeToProtocolDic = new Dictionary<Type, ushort>();

        private static readonly ProtocolType instance = new ProtocolType();
        public static ProtocolType Instance => instance;
   
        private ProtocolType()
        {{
{ reqProtocol}        }}

        /// <summary>请求消息/收到消息对应关系(禁止连续发送)</summary>
        public Dictionary<ushort, ushort> CSList = new Dictionary<ushort, ushort>()
        {{
{csListProtocol.ToString().TrimEnd(',')}        }};
        /// <summary>收到消息/请求消息对应关系(禁止连续发送)</summary>
        public Dictionary<ushort, ushort> SCList = new Dictionary<ushort, ushort>()
        {{
{scListProtocol.ToString().TrimEnd(',')}        }};
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
{ rspProtocol}                }}
            return msg;
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        /// <summary>
        /// 创建NetAction
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateNetActionFile(List<ProtoConifg> configList)
        {
            string savePath = Glob.codeOutSetting.ClientProto.NetActionFile.ToReality();
            StringBuilder sbProtocol = new StringBuilder();
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');
                if (info[1].ToLower().StartsWith("sc_")) //客户端发的请求消息
                {
                    string fileName = info[1].Substring(3);
                    fileName = Utils.ToFirstUpper(fileName);
                    string comment = configList[i].Comment != string.Empty ? ("      //" + configList[i].Comment) : string.Empty;
                    sbProtocol.AppendLine($"            actionList.Add({configList[i].MsgId}, {fileName});{comment}");
                }
            }
            string str = $@"using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace {Glob.projectSetting.HoxProjectName}
{{
    public partial class NetAction
    {{
        public Dictionary<ushort, Action<Message>> actionList = new Dictionary<ushort, Action<Message>>();
        public void InitAction()
        {{
{ sbProtocol.ToString() }
        }}
    }}
}}";
            Utils.SaveFile(savePath, str);
        }
        /// <summary>
        /// 收到消息NetMessage
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected override void CreateNetMessage(List<ProtoConifg> configList)
        {
            string savePath = Glob.codeOutSetting.ClientProto.NetDir.ToReality();
            StringBuilder sbProtocol = new StringBuilder();
            string saveFilePath = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                string[] info = configList[i].MsgName.Split('.');    //login.cs_login_verify
                if (info[1].ToLower().StartsWith("sc_")) //收到服务端回应
                {
                    string fileName = Utils.ToFirstUpper(info[1].Substring(3)); //Login_verify
                    saveFilePath = Path.Combine(savePath, Utils.ToFirstUpper(info[0]), fileName + ".cs");
                    string str = $@"using {info[0]};
namespace {Glob.projectSetting.HoxProjectName}
{{
    public partial class NetAction
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

        //=====
    }
}
