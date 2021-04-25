using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm.DBMgr
{
    public class TableName
    {
        private Dictionary<string, string> tableNameDic = new Dictionary<string, string>();
        private static readonly TableName instance = new TableName();
        public static TableName Instance => instance;

        private TableName()
        {
            tableNameDic.Add("TAccount", "账号表");
            tableNameDic.Add("TPlayer", "玩家表");
            tableNameDic.Add("TSeason", "服务器赛季");
            tableNameDic.Add("THero", "英雄表");
            tableNameDic.Add("TItemProp", "物品道具表");
            tableNameDic.Add("TItemEquip", "物品装备表");
            tableNameDic.Add("TTaskDay", "");
            tableNameDic.Add("TAchievement", "");
            tableNameDic.Add("TPlayerAction", "");
            tableNameDic.Add("TEventFB", "活动副本表");
            tableNameDic.Add("TActivityList", "活动列表");
            tableNameDic.Add("TActivityTask", "活动任务列表");
            tableNameDic.Add("TActivity", "玩家活动数据");
            tableNameDic.Add("THeroicRoad", "玩家英勇之路信息");
            tableNameDic.Add("TTaskHeroic", "玩家英勇任务信息");
            tableNameDic.Add("TMail", "邮件信息");
            tableNameDic.Add("TMailSub", "邮件子项(记录已打开的群邮件玩家ID)");
            tableNameDic.Add("TPay", "首充,月卡信息");
            tableNameDic.Add("TBonus", "福利信息");
            tableNameDic.Add("TTaskNewbie", "玩家新手任务信息");
            tableNameDic.Add("TStore", "商城购买次数");
            tableNameDic.Add("TPayOrder", "充值定单");
            tableNameDic.Add("TPlayerLeft", "");
            tableNameDic.Add("TCDKeyPlayer", "玩家CDKey兑换信息");
            tableNameDic.Add("TCDKey", "已使用礼品码信息");
            tableNameDic.Add("TLogShop", "商城日志");
            tableNameDic.Add("TLogTicket", "#消费统计");
            tableNameDic.Add("TLogFun", "#功能统计");
            tableNameDic.Add("TLogItem", "#道具使用统计");
            tableNameDic.Add("TLogActivity", "#活动领取统计");
            tableNameDic.Add("TLogLogin", "#登录日志");
            tableNameDic.Add("TLogReg", "");
            tableNameDic.Add("TLogGuide", "#登录日志");
            tableNameDic.Add("TLogTenMin", "#10分钟统计");
            tableNameDic.Add("TLogSevenDay", "#10分钟统计");
            tableNameDic.Add("TLogServer", "#服务器数据统计");
            tableNameDic.Add("TLogFB", "#副本、探索挑战次数统计（统计玩家打每个副本关卡的次数和人数）");
            tableNameDic.Add("TLogFBWin", "#副本、探索通关次数统计（统计每个副本关卡通关的次数和人数）");
            tableNameDic.Add("TLogFBExit", "");
            tableNameDic.Add("TLogFBRebirth", "");
            tableNameDic.Add("TLogDower", "#天赋升级统计");
            tableNameDic.Add("TLogPlayerLv", "");
            tableNameDic.Add("TLogSummonBuy", "");
            tableNameDic.Add("TLogTask", "");
            tableNameDic.Add("TLogFBNum", "");
            tableNameDic.Add("TlogClubRank", "");
            tableNameDic.Add("TLogHeroLvUp", "");
        }
        /// <summary>获取有中文名字</summary>
        public string GetName(string name)
        {
            if (tableNameDic.ContainsKey(name))
                return tableNameDic[name];
            return string.Empty;
        }
    }
}
