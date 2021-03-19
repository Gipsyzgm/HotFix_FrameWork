using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools.ProtoExport
{
    public class ProtoConifg
    {
        public ushort MsgId;      //消息协议号
        public string MsgName;  //消息名
        public string Comment;  //消息描述
        public int ThreadId;       //工作线程ID
        public ushort RtnMsgId; //需要返回的消息协议
    }

    public class ProtoExportBase
    {

        protected virtual string ProtoC_File => "3rdLib/protoc.exe";
        protected string Config_FilePath = @"_config.txt";                        //proto配置文件     相对protoDir文件夹
        /// <summary>
        /// 获取Proto配置列表
        /// </summary>
        /// <param name="protoDir">proto目录</param>
        /// <param name="callback">执行完回调</param>
        protected List<ProtoConifg> getConfigList(string configPath)
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
                    if (array_infoId.Length > 2)
                        config.RtnMsgId = Convert.ToUInt16(array_infoId[2]);

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

        //保存Proto文件路径
        protected virtual string SaveProtoClass => string.Empty;

        /// <summary>
        /// 工具生成Protobuf类
        /// </summary>
        /// <param name="callback"></param>
        public virtual void Generate(Action callback = null,Action error=null)
        {
            string savePath = SaveProtoClass;
            string protoDir = Glob.projectSetting.RealityProtoDir;
            string protogen = Path.Combine(Environment.CurrentDirectory, ProtoC_File);
       
            Utils.ResetDirectory(savePath); //清空下面所有文件

            DirectoryInfo folder = new DirectoryInfo(protoDir);
            if (!folder.Exists)
            {
                Logger.LogError($"{protoDir}文件夹不存在!!");
                error?.Invoke();
                return;
            }
            List<string> cmds = new List<string>();
            foreach (FileInfo file in folder.GetFiles("*.proto"))
            {               
                // string cmd = $"{protogen} -w:{protoDir} -i:{file.Name} -o:{savePath}\\{file.Name.Split('.')[0]}.cs";
                string cmd = $"{protogen} --csharp_out={savePath} -I {protoDir} {file.FullName}";
                cmds.Add(cmd);
            }
            Utils.Cmd(cmds);
            GenerateNet();
            callback?.Invoke();
        }

        /// <summary>
        /// 生成Net相关文件
        /// </summary>
        protected virtual void GenerateNet()
        {
            string protoDir = Glob.projectSetting.RealityProtoDir;
            string configPath = Path.Combine(protoDir, Config_FilePath);
            List<ProtoConifg> configList = getConfigList(configPath);

            CreateProtocolType(configList);
            CreateNetActionFile(configList);
            CreateNetMessage(configList);
        }
        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList"></param>
        protected virtual void CreateProtocolType(List<ProtoConifg> configList)
        {
        }
        /// <summary>
        /// 创建ProtocolType
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected virtual void CreateNetActionFile(List<ProtoConifg> configList)
        {
        }
        /// <summary>
        /// NetMessage
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected virtual void CreateNetMessage(List<ProtoConifg> configList)
        {
        }
    }
}
