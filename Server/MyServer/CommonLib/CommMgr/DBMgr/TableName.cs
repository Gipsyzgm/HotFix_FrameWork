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
            tableNameDic.Add("THero", "英雄表");
            tableNameDic.Add("THeroTeam", "英雄队伍表");
            tableNameDic.Add("TBuild", "建筑表");
            tableNameDic.Add("TItemProp", "物品道具表");
            tableNameDic.Add("TItemEquip", "物品装备表");
            tableNameDic.Add("TArena", "竞技场信息");
            tableNameDic.Add("TArenaRecord", "竞技场战报记录");
            tableNameDic.Add("TLeague", "玩家联赛数据");
            tableNameDic.Add("TLeagueRecord", "联赛日志");
            tableNameDic.Add("TLeagueMatch", "联赛赛事");
            tableNameDic.Add("TLeagueAward", "联赛奖励");
            tableNameDic.Add("TLeagueAwardLog", "联赛领奖日志");
            tableNameDic.Add("TTask", "");
            tableNameDic.Add("TTaskBounty", "玩家赏金任务信息");
            tableNameDic.Add("TAchievement", "");
            tableNameDic.Add("TPlayerAction", "");
            tableNameDic.Add("TEventFB", "活动副本表");
            tableNameDic.Add("TEventFBAward", "活动副本排名奖励表");
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
            tableNameDic.Add("TPayPack", "活动礼包");
            tableNameDic.Add("TPayPackData", "");
            tableNameDic.Add("TGMSetPayPack", "");
            tableNameDic.Add("TTaskChap", "玩家章节任务列表");
            tableNameDic.Add("TChat", "聊天信息");
            tableNameDic.Add("TFriend", "好友信息");
            tableNameDic.Add("TBlackList", "聊天黑名单");
            tableNameDic.Add("TCDKeyPlayer", "玩家CDKey兑换信息");
            tableNameDic.Add("TCDKey", "已使用礼品码信息");
            tableNameDic.Add("TClub", "俱乐部信息");
            tableNameDic.Add("TClubMember", "俱乐部成员信息");
            tableNameDic.Add("TClubApply", "俱乐部申请信息");
            tableNameDic.Add("TClubLog", "俱乐部日志");
            tableNameDic.Add("TTitan", "联盟泰坦战");
            tableNameDic.Add("TTitanLog", "联盟泰坦战日志");
            tableNameDic.Add("TTitanAward", "联盟泰坦战奖励");
            tableNameDic.Add("TTitanAwardLog", "联盟泰坦战奖励");
            tableNameDic.Add("TClubWar", "联盟战争对象");
            tableNameDic.Add("TClubWarPlayer", "联盟战争成员");
            tableNameDic.Add("TClubWarNpc", "联盟战争成员Npc");
            tableNameDic.Add("TClubWarLog", "联盟战争日志");
            tableNameDic.Add("TClubWarResult", "联盟战争结果");
            tableNameDic.Add("TClubWarAward", "联盟战争奖励");
            tableNameDic.Add("TClubWarAwardLog", "联盟战争领奖日志");
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
            tableNameDic.Add("TLogBuild", "#建筑升级统计");
            tableNameDic.Add("TLogBuildWork", "#建筑生产统计（统计建筑[工坊、训练营、兵营]中每一项的生产）");
            tableNameDic.Add("TLogFB", "#副本、探索挑战次数统计（统计玩家打每个副本关卡的次数和人数）");
            tableNameDic.Add("TLogFBWin", "#副本、探索通关次数统计（统计每个副本关卡通关的次数和人数）");
            tableNameDic.Add("TLogFBExit", "");
            tableNameDic.Add("TLogFBRebirth", "");
            tableNameDic.Add("TLogDower", "#天赋升级统计");
            tableNameDic.Add("TLogPlayerLv", "");
            tableNameDic.Add("TLogSummonBuy", "");
            tableNameDic.Add("TLogArena", "");
            tableNameDic.Add("TLogTask", "");
            tableNameDic.Add("TLogFBNum", "");
            tableNameDic.Add("TlogClubRank", "");
            tableNameDic.Add("TLogHeroLvUp", "");
            tableNameDic.Add("TServerInfo", "服务器信息");
            tableNameDic.Add("TVersion", "版本信息");
            tableNameDic.Add("TNotice", "公告信息");
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
