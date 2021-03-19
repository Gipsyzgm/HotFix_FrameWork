using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools.ExcelExport
{
    public class ExcelExportBase
    {
        ////保存ConfigClass文件目录
        protected virtual string SaveConfigClass => Config.OutClassDir.ToReality();

        ////保存ConfigData文件目录
        protected virtual string SaveConfigData => Config.OutDataDir.ToReality();


        protected ConfigCodeOut Config;
        protected SQLiteServer sqlit;
        public ExcelExportBase(ConfigCodeOut config)
        {
            Config = config;
        }

        /// <summary>
        /// 工具生成Protobuf类
        /// </summary>
        /// <param name="callback"></param>
        public virtual void Generate(List<ExcelSheet> configList, Action callback = null)
        {
            if (Config.IsSqlite)
            {
                sqlit = new SQLiteServer();
                if (!sqlit.CreateDB(Config.OutSqlitePath.ToReality()))
                    return;
            }
            List<string> configName = new List<string>();
            Utils.ResetDirectory(SaveConfigClass); //清空下面所有文件
            if (!Config.IsSqlite)
                Utils.ResetDirectory(SaveConfigData); //清空下面所有文件
            GenerateClass(configList);            
            GenerateFile(configList);            
            callback?.Invoke();
            //sqlit = null;
        }

        /// <summary>
        /// 生成类文件
        /// </summary>
        protected virtual void GenerateClass(List<ExcelSheet> configList)
        {
            sqlit?.BeginTransaction();
            foreach (ExcelSheet sheet in configList)
            {
                CreateConfigClass(sheet);
                CreateConfigData(sheet);
            }
            CreateMapConfig();
            sqlit?.Commit();
            sqlit?.CloseDB();
        }
        protected virtual void CreateMapConfig()
        {

        }

        /// <summary>
        /// 生成配置表Calss类
        /// </summary>
        protected virtual void CreateConfigClass(ExcelSheet sheet)
        {

        }
        /// <summary>
        /// 生成配置表数据文件
        /// </summary>
        /// <param name="sheet"></param>
        protected virtual void CreateConfigData(ExcelSheet sheet)
        {

        }
        /// <summary>
        /// 生成相关文件
        /// </summary>
        protected virtual void GenerateFile(List<ExcelSheet> configList)
        {
            CreateConfigInit(configList);
            CreateConfigMgr(configList);
        }
        /// <summary>
        /// 生成ConfigInit.cs文件
        /// </summary>
        /// <param name="configList"></param>
        protected virtual void CreateConfigInit(List<ExcelSheet> configList)
        {
        }
        /// <summary>
        /// 生成ConfigMgr.cs文件
        /// </summary>
        /// <param name="configList">NetAction</param>
        protected virtual void CreateConfigMgr(List<ExcelSheet> configList)
        {
        }
    }
}
