﻿using System;
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
        public bool IsEncrypt;  //是否为加密消息
    }

    public class ProtoBase
    {
      
        protected string Config_FilePath = @"_config.txt";
        protected int CSThreadNum = 0; //是CS消息处理线程数 没有使用单线程
        protected int SCThreadNum = 0; //是SC消息处理线程数
        public ProtoBase()
        {
        }

        /// <summary>
        /// 获取Proto配置列表
        /// </summary>
        /// <param name="protoDir">proto目录</param>
        /// <param name="callback">执行完回调</param>
        protected List<ProtoConifg> getConfigList(string protoType)
        {
            string protoDir = Glob.projectSetting.RealityProtoDir;
            string configPath = Path.Combine(protoDir, protoType, Config_FilePath);

            List<ProtoConifg> configList = new List<ProtoConifg>();
            if (!File.Exists(configPath))
                return configList;
            StreamReader sr = File.OpenText(configPath);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                line = Regex.Replace(line, "[\t ]", "");  //去tab,去空格
                if (line == string.Empty) continue;
                if (line.StartsWith("@Thread"))
                {
                    string thInfoStr = line.Replace("@Thread","");
                    string[] thInfo = thInfoStr.Split(',');
                    if(thInfo.Length>0)
                        CSThreadNum = Convert.ToInt32(thInfo[0]);
                    if (thInfo.Length > 1)
                        SCThreadNum = Convert.ToInt32(thInfo[1]);                    ;
                }
                else if (!line.StartsWith("#"))
                {
                    ProtoConifg config = new ProtoConifg();
                    string[] array_info = line.Split('=');
                    if (array_info[0].EndsWith("@"))
                        config.IsEncrypt = true;
                    array_info[0] = array_info[0].TrimEnd('@');

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

        protected void generateProto(ProtoCodeOut config,string ProtoType,bool isResetDir = true)
        {
            string protogen = ProtoUtils.GetProtoCFile(config.IsProtobuffForILR);
            string protoDir = Path.Combine(Glob.projectSetting.RealityProtoDir, ProtoType);
            string protoComm = Path.Combine(Glob.projectSetting.RealityProtoDir, "Common");
            List<string> cmds = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(protoDir);           
            if(isResetDir)
                Utils.ResetDirectory(config.RealityOutClassDir);
            if (!folder.Exists)
            {
                Logger.LogError($"{protoDir}文件夹不存在!!");
                return;
            }
            foreach (FileInfo file in folder.GetFiles("*.proto"))
            {
                string cmd = $"{protogen} --csharp_out={config.RealityOutClassDir} -I {protoDir} -I {protoComm} {file.FullName}";
                cmds.Add(cmd);
            }
            Utils.Cmd(cmds);
        }
    }
}