using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm.DBMgr
{
    public class TableDBIndex
    {
        private Dictionary<Type, int> tableIndexs = new Dictionary<Type, int>();
        private static readonly TableDBIndex instance = new TableDBIndex();
        public static TableDBIndex Instance => instance;

        private TableDBIndex()
        {
            tableIndexs.Add(typeof(TChat), 2);
            tableIndexs.Add(typeof(TLogShop), 1);
            tableIndexs.Add(typeof(TLogTicket), 1);
            tableIndexs.Add(typeof(TLogFun), 1);
            tableIndexs.Add(typeof(TLogItem), 1);
            tableIndexs.Add(typeof(TLogActivity), 1);
            tableIndexs.Add(typeof(TLogLogin), 1);
            tableIndexs.Add(typeof(TLogReg), 1);
            tableIndexs.Add(typeof(TLogGuide), 1);
            tableIndexs.Add(typeof(TLogTenMin), 1);
            tableIndexs.Add(typeof(TLogSevenDay), 1);
            tableIndexs.Add(typeof(TLogServer), 1);
            tableIndexs.Add(typeof(TLogBuild), 1);
            tableIndexs.Add(typeof(TLogBuildWork), 1);
            tableIndexs.Add(typeof(TLogFB), 1);
            tableIndexs.Add(typeof(TLogFBWin), 1);
            tableIndexs.Add(typeof(TLogFBExit), 1);
            tableIndexs.Add(typeof(TLogFBRebirth), 1);
            tableIndexs.Add(typeof(TLogDower), 1);
            tableIndexs.Add(typeof(TLogPlayerLv), 1);
            tableIndexs.Add(typeof(TLogSummonBuy), 1);
            tableIndexs.Add(typeof(TLogArena), 1);
            tableIndexs.Add(typeof(TLogTask), 1);
            tableIndexs.Add(typeof(TLogFBNum), 1);
            tableIndexs.Add(typeof(TlogClubRank), 1);
            tableIndexs.Add(typeof(TLogHeroLvUp), 1);
        }
        /// <summary>
        /// 跟据类型获取DB对应的库索引
        /// </summary>
        public int GetDB(Type type)
        {
            int index = 0;
            tableIndexs.TryGetValue(type, out index);
            return index;
        }      
    }
}
