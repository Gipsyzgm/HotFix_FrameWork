namespace Tools
{
    public class ProjectSetting : BaseSetting
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override int UniqueID => Id;

        public int Id;

        public string Name;

        public string HotName;

        public string ProjectDir;

        public string ClientDir;

        public string ClientHotDir;

        public string ServerDir;

        public string ConfigDir;

        public string ProtoDir;

        public string GMServerDir;

        public int CodeOutId;


        private string _RealityClientDir;
        /// <summary>客户端目录</summary>
        public string RealityClientDir
        {
            get
            {
                if (_RealityClientDir == null)
                    _RealityClientDir = ClientDir.Replace("$ProjectDir$", ProjectDir);
                return _RealityClientDir;
            }
        }

        private string _RealityClientHotDir;
        /// <summary>客户端热更目录</summary>
        public string RealityClientHotDir
        {
            get
            {
                if (_RealityClientHotDir == null)
                    _RealityClientHotDir = ClientHotDir.Replace("$ProjectDir$", ProjectDir).Replace("$HotName$", HotName);
                return _RealityClientHotDir;
            }
        }

        private string _RealityServerDir;
        /// <summary>服务端目录</summary>
        public string RealityServerDir
        {
            get
            {
                if (_RealityServerDir == null)
                    _RealityServerDir = ServerDir.Replace("$ProjectDir$", ProjectDir);
                return _RealityServerDir;
            }
        }

        private string _RealityGMServerDir;
        /// <summary>GM服务端目录</summary>
        public string RealityGMServerDir
        {
            get
            {
                if (_RealityGMServerDir == null)
                    _RealityGMServerDir = GMServerDir.Replace("$ProjectDir$", ProjectDir);
                return _RealityGMServerDir;
            }
        }

        private string _RealityConfigDir;
        /// <summary>配置文件目录</summary>
        public string RealityConfigDir
        {
            get
            {
                if (_RealityConfigDir == null)
                    _RealityConfigDir = ConfigDir.Replace("$ProjectDir$", ProjectDir);
                return _RealityConfigDir;
            }
        }


        private string _RealityProtoDir;
        /// <summary>Protobuf文件目录</summary>
        public string RealityProtoDir
        {
            get
            {
                if (_RealityProtoDir == null)
                    _RealityProtoDir = ProtoDir.Replace("$ProjectDir$", ProjectDir);
                return _RealityProtoDir;
            }
        }
    }
}
