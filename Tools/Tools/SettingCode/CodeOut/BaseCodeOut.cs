using System;

namespace Tools
{
    public class CodeType
    {
        public const string CShap = "C#";
        public const string AS3 = "AS3";
        public const string Lua = "Lua";
        public const string CPP = "C++";
    }

    public class BaseCodeOut
    {
        /// <summary>
        /// 代码语言类型 CodeType
        /// C#,AS3,Lua,Java,C++
        /// </summary>
        public string CodeType = String.Empty;

        /// <summary>导出配置文件数据目录</summary>
        public string OutDataDir = String.Empty;

        /// <summary>导出配置文件类目录</summary>
        public string OutClassDir = String.Empty;
    }
}
