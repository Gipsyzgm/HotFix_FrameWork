using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.ProtoExport.ProtoCSharp
{
    public class ProtoExportTransit: ProtoBase
    {
        TransitProtoOut Config;
        public List<ProtoConifg> ConfigList;
        public ProtoExportTransit(TransitProtoOut config)
        {
            Config = config;
            ConfigList = getConfigList(Config.ProtoType);
        }
        /// <summary>
        /// 工具生成Protobuf类
        /// </summary>
        /// <param name="callback"></param>
        public virtual void Generate(Action callback = null, Action error = null)
        {
            //生成客户端Proto
            if (Config.ClientIndex != -1 && ToolsCookieHelper.Config.IsClientDev)
            {
                if (Config.ClientIndex > Glob.codeOutSetting.ClientProtos.Length)
                {
                    Logger.LogError("TransitProtos ClientIndex配置错误!!");
                    return;
                }
                ProtoCodeOut config = Glob.codeOutSetting.ClientProtos[Config.ClientIndex];
                generateProto(config, Config.ProtoType,false);
            }
            //生成服务端Proto
            if (Config.ServerIndex != -1 && ToolsCookieHelper.Config.IsServerDev)
            {
                if (Config.ServerIndex > Glob.codeOutSetting.ServerProtos.Length)
                {
                    Logger.LogError("TransitProtos ServerIndex配置错误!!");
                    return;
                }
                ProtoCodeOut config = Glob.codeOutSetting.ServerProtos[Config.ServerIndex];
                generateProto(config, Config.ProtoType,false);               
            }

            if (Config.TransitServerIndex != -1 && ToolsCookieHelper.Config.IsServerDev)
            {
                if (Config.TransitServerIndex > Glob.codeOutSetting.ServerProtos.Length)
                {
                    Logger.LogError("TransitProtos ServerIndex配置错误!!");
                    return;
                }
                ProtoCodeOut config = Glob.codeOutSetting.ServerProtos[Config.TransitServerIndex];
                createTransitFile(ConfigList, config);
            }
           
        }

        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList"></param>
        protected void createTransitFile(List<ProtoConifg> configList, ProtoCodeOut transitProtoConfig)
        {
            string savePath = Config.RealityTransitFile;
            StringBuilder rspProtocol = new StringBuilder();
            StringBuilder reqProtocol = new StringBuilder();
            StringBuilder sbEncrypt = new StringBuilder();
            string msgName = string.Empty;
            for (int i = 0; i < configList.Count; i++)
            {
                msgName = configList[i].MsgName.Split('.')[1].ToLower();
                if (msgName.StartsWith("sc_"))
                    rspProtocol.AppendLine($"            returnProtocols.Add({configList[i].MsgId}); //{ configList[i].Comment}");
                else if (msgName.StartsWith("cs_"))
                    reqProtocol.AppendLine($"            requestProtocols.Add({configList[i].MsgId}); //{ configList[i].Comment}");
                
                if (configList[i].IsEncrypt)
                    sbEncrypt.AppendLine($"            _encryptList.Add({configList[i].MsgId});");
            }
            string str = $@"using System;
using System.Collections.Generic;
namespace {transitProtoConfig.RealityNameSpace}.Net
{{
    /// <summary>
    /// 工具生成，不要修改
    /// </summary>
    public class {Config.ProtoType}Transit
    {{
        public HashSet<ushort> requestProtocols = new HashSet<ushort>();
        public HashSet<ushort> returnProtocols = new HashSet<ushort>();
        private HashSet<int> _encryptList = new HashSet<int>();
        private static readonly {Config.ProtoType}Transit instance = new {Config.ProtoType}Transit();
        public static {Config.ProtoType}Transit Instance => instance;
        
        private {Config.ProtoType}Transit()
        {{
            //请求中转的消息
{reqProtocol}

            //返回中转的消息
{rspProtocol}
{sbEncrypt} 
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
    }
}
