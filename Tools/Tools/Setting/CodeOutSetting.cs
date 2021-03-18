namespace Tools
{
    public class CodeOutSetting : BaseSetting
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override int UniqueID => Id;
        /// <summary>代码导出配置Id</summary>
        public int Id;
        /// <summary>代码导出配置名称</summary>
        public string Name;

        /// <summary>服务器代码导出配置</summary>
        public ConfigCodeOut ServerConfig;

        /// <summary>客户端代码导出配置</summary>
        public ConfigCodeOut[] ClientConfigs;

        /// <summary> 服务端Proto代码导出配置</summary>
        public ProtoCodeOut[] ServerProtos;

        /// <summary> 客户端Proto代码导出配置</summary>
        public ProtoCodeOut[] ClientProtos;

        /// <summary> 中转消息导出配置</summary>
        public TransitProtoOut[] TransitProtos;

        /// <summary> GM服务器配置</summary>
        public ConfigCodeOut GMServerConfig;

        /// <summary>服务端DB</summary>
        public DBOut ServerDB;

        /// <summary>GM服务端DB</summary>
        public DBOut GMServerDB;

        /// <summary>GM服务端API</summary>
        public APIOut GMServerAPI;

        /// <summary>CDKey</summary>
        public CDKeyOut CDKey;

        /// <summary>服务端日志</summary>
        public LogOut ServerLog;

    }

    public class CodeOut
    {
        /// <summary>
        /// 代码语言类型
        /// C#,AS3,Lua,Java,C++
        /// </summary>
        public string CodeType;

        /// <summary>导出配置文件目录</summary>
        public string ConfigOutDir;

        /// <summary>配置文件管理器目录</summary>
        public string ConfigMgrDir;

        /// <summary>导出配置文件类结构目录</summary>
        public string ConfigClassDir;

        /// <summary>导出初始化文件</summary>
        public string ConfigInitFile;

        /// <summary>导出管理器文件</summary>
        public string ConfigMgrFile;


        private string _RealityConfigOutDir;
        public string RealityConfigOutDir
        {
            get
            {
                if (_RealityConfigOutDir == null)
                    _RealityConfigOutDir = ConfigOutDir.Replace("$ServerDir$", Glob.projectSetting.RealityServerDir).Replace("$ClientDir$", Glob.projectSetting.RealityClientDir);
                return _RealityConfigOutDir;
            }
        }

        private string _RealityConfigMgrDir;
        public string RealityConfigMgrDir
        {
            get
            {
                if (_RealityConfigMgrDir == null)
                    _RealityConfigMgrDir = ConfigMgrDir.Replace("$ServerDir$", Glob.projectSetting.RealityServerDir).Replace("$ClientDir$", Glob.projectSetting.RealityClientDir);
                return _RealityConfigMgrDir;
            }
        }


        private string _RealityConfigClassDir;
        public string RealityConfigClassDir
        {
            get
            {
                if (_RealityConfigClassDir == null)
                    _RealityConfigClassDir = ConfigClassDir.Replace("$ConfigMgrDir$", RealityConfigMgrDir);
                return _RealityConfigClassDir;
            }
        }

        private string _RealityConfigInitFile;
        public string RealityConfigInitFile
        {
            get
            {
                if (_RealityConfigInitFile == null)
                    _RealityConfigInitFile = ConfigInitFile.Replace("$ConfigMgrDir$", RealityConfigMgrDir);
                return _RealityConfigInitFile;
            }
        }

        private string _RealityConfigMgrFile;
        public string RealityConfigMgrFile
        {
            get
            {
                if (_RealityConfigMgrFile == null)
                    _RealityConfigMgrFile = ConfigMgrFile.Replace("$ConfigMgrDir$", RealityConfigMgrDir);
                return _RealityConfigMgrFile;
            }
        }
    }


}
