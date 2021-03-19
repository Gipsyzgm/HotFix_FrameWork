namespace Tools
{
    /// <summary>
    /// Config导出配置
    /// </summary>
    public class ConfigCodeOut : BaseCodeOut
    {
        //禁用
        public bool Disable = false;

        //命名空间
        public string NameSpace = string.Empty;


        /// <summary>配置文件管理器目录</summary>
        public string ConfigMgrDir = string.Empty;


        /// <summary>导出初始化文件</summary>
        public string ConfigInitFile = string.Empty;

        /// <summary>导出管理器文件</summary>
        public string ConfigMgrFile = string.Empty;

        /// <summary>版本检测文字源文件</summary>
        public string VerLangFile = string.Empty;

        /// <summary>版本检测导出文件</summary>
        public string VerLangOutFile = string.Empty;

        /// <summary>是否导出CSV格式</summary>
        public bool IsCSV = false;

        /// <summary>是否使用Task 加载</summary>
        public bool IsTaskLoad = false;

        /// <summary>是否使用Sqlite</summary>
        public bool IsSqlite = false;
        /// <summary>Sqlite导出目录</summary>
        public string OutSqlitePath = string.Empty;

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
    }
}
