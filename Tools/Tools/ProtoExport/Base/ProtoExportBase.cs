using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools.ProtoExport
{
    public class ProtoExportBase: ProtoBase
    {
       //proto配置文件     相对protoDir文件夹
        public ProtoCodeOut Config;


        public List<ProtoConifg> TransitConfigList;
        public ProtoExportBase(ProtoCodeOut config)
        {
            Config = config;
        }

        //保存Proto文件路径
        protected virtual string SaveProtoClass => Config.RealityOutClassDir;

        /// <summary>
        /// 工具生成Protobuf类
        /// </summary>
        /// <param name="callback"></param>
        public virtual void Generate(Action callback = null,Action error=null)
        {
            //不存在的不导出
            string protoDir = Path.Combine(Glob.projectSetting.RealityProtoDir, Config.ProtoType);
            if (!Directory.Exists(protoDir))
                return;

            generateProto(Config, Config.ProtoType);
            GenerateNet();
            CreateCommonProto();
            callback?.Invoke();
        }

        /// <summary>
        /// 生成Net相关文件
        /// </summary>
        protected virtual void GenerateNet()
        {
            
            List<ProtoConifg> configList = getConfigList(Config.ProtoType);
            int transitIndex = -1;
            if (TransitConfigList != null && TransitConfigList.Count > 0)
            {
                transitIndex = configList.Count-1; //从这条后面都是追加的
                configList.AddRange(TransitConfigList);
            }

            CreateProtocolType(configList, transitIndex);
            CreateNetActionFile(configList, transitIndex);
            CreateNetMessage(configList, transitIndex);
        }
        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList"></param>
        protected virtual void CreateProtocolType(List<ProtoConifg> configList,int addIndex = -1)
        {
        }
        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected virtual void CreateNetActionFile(List<ProtoConifg> configList, int addIndex = -1)
        {
        }
        /// <summary>
        /// NetMessage
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected virtual void CreateNetMessage(List<ProtoConifg> configList, int addIndex = -1)
        {
        }

        protected virtual void CreateCommonProto()
        {
            if (Config.CommonProtoDir == string.Empty) return;
            string protogen = ProtoUtils.GetProtoCFile(Config.IsProtobuffForILR);
            string protoDir = Path.Combine(Glob.projectSetting.RealityProtoDir, "Common");
            List<string> cmds = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(protoDir);

            Utils.ResetDirectory(Config.RealityCommonProtoDir);
            if (!folder.Exists)
            {
                Logger.LogError($"{protoDir}文件夹不存在!!");
                return;
            }
            foreach (FileInfo file in folder.GetFiles("*.proto"))
            {
                string cmd = $"{protogen} --csharp_out={Config.RealityCommonProtoDir} -I {protoDir} {file.FullName}";
                cmds.Add(cmd);
            }
            Utils.Cmd(cmds);
        }

    }
}
