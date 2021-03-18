using System;

namespace Tools
{
    /// <summary>
    /// Config导出配置
    /// </summary>
    public class ProtoCodeOut : BaseCodeOut
    {
        //命名空间
        public string NameSpace = string.Empty;

        //生成Proto的配置目录
        public string ProtoType = string.Empty;

        /// <summary>Net文件夹</summary>
        public string NetReceiveDir = String.Empty;

        /// <summary>导出ProtocolType文件</summary>
        public string ProtocolTypeFile = String.Empty;

        /// <summary>导出NetActionFile文件</summary>
        public string NetActionFile = String.Empty;

        /// <summary>共用Proto导出目录</summary>
        public string CommonProtoDir = String.Empty;

        //一个进程里是否同时存在多个客户端
        public bool IsMultClinet = false;

        /// <summary>是否ILR热更的改Protobuff</summary>
        public bool IsProtobuffForILR = false;

        /// <summary>是否专属于客户端开发导出</summary>
        public bool IsClientDev = false;



        private string _RealityNameSpace;
        public string RealityNameSpace
        {
            get
            {
                if (_RealityNameSpace == null)
                    _RealityNameSpace = NameSpace.Replace("$HotName$", Glob.projectSetting.HotName);
                return _RealityNameSpace;
            }
        }

        private string _RealityNetReceiveDir;
        public string RealityNetReceiveDir
        {
            get
            {
                if (_RealityNetReceiveDir == null)
                    _RealityNetReceiveDir = NetReceiveDir.ToReality().Replace("$NameSpace$", RealityNameSpace).Replace("$ProtoType$", ProtoType);
                return _RealityNetReceiveDir;
            }
        }

        private string _RealityProtocolTypeFile;
        public string RealityProtocolTypeFile
        {
            get
            {
                if (_RealityProtocolTypeFile == null)
                    _RealityProtocolTypeFile = ProtocolTypeFile.ToReality().Replace("$NameSpace$", RealityNameSpace).Replace("$ProtoType$", ProtoType);
                return _RealityProtocolTypeFile;
            }
        }

        private string _RealityNetActionFile;
        public string RealityNetActionFile
        {
            get
            {
                if (_RealityNetActionFile == null)
                    _RealityNetActionFile = NetActionFile.ToReality().Replace("$NameSpace$", RealityNameSpace).Replace("$ProtoType$", ProtoType);
                return _RealityNetActionFile;
            }
        }

        private string _RealityOutClassDir;
        public string RealityOutClassDir
        {
            get
            {
                if (_RealityOutClassDir == null)
                    _RealityOutClassDir = OutClassDir.ToReality().Replace("$NameSpace$", RealityNameSpace).Replace("$ProtoType$", ProtoType);
                return _RealityOutClassDir;
            }
        }

        private string _RealityCommonProtoDir;
        public string RealityCommonProtoDir
        {
            get
            {
                if (_RealityCommonProtoDir == null)
                    _RealityCommonProtoDir = CommonProtoDir.ToReality().Replace("$NameSpace$", RealityNameSpace).Replace("$ProtoType$", ProtoType);
                return _RealityCommonProtoDir;
            }
        }


    }
}
