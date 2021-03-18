using System;

namespace Tools
{
    /// <summary>
    /// 数据库导出配置
    /// </summary>
    public class DBOut : BaseCodeOut
    {

        /// <summary>数据库文件</summary>
        public string DBFile = String.Empty;

        /// <summary>导出数据库结构类文件夹</summary>
        public string OutDBTableClassDir = String.Empty;

        /// <summary>导出DBWriteTable文件</summary>
        public string OutDBWriteTableFile = String.Empty;

        /// <summary>导出TableName文件</summary>
        public string OutDBTableNameFile = String.Empty;

        /// <summary>导出TableDBIndex文件</summary>
        public string OutDBTableDBIndexFile = String.Empty;

        /// <summary>导出GameTable文件夹(GM用)</summary>
        public string OutDBGameTableClassDir = String.Empty;

    }
}
