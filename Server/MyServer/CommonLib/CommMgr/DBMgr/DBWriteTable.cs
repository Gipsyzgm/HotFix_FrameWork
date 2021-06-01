/// <summary>
/// 工具生成，不要修改
/// 数据库写入操作,跟据表名转换成表对象
/// </summary>
namespace CommonLib.Comm.DBMgr
{
    public partial class DBWrite
    {
        private void insertDB(ITable data)
        {
            switch (data.GetType().Name)
            {
                case "TAccount"://账号表
                    MongoDBHelper.Instance.Insert(data as TAccount);
                    break;
                case "TPlayer"://玩家表
                    MongoDBHelper.Instance.Insert(data as TPlayer);
                    break;
                case "THero"://英雄表
                    MongoDBHelper.Instance.Insert(data as THero);
                    break;
                case "THeroTeam"://英雄队伍表
                    MongoDBHelper.Instance.Insert(data as THeroTeam);
                    break;
                case "TBuild"://建筑表
                    MongoDBHelper.Instance.Insert(data as TBuild);
                    break;
                case "TItemProp"://物品道具表
                    MongoDBHelper.Instance.Insert(data as TItemProp);
                    break;
                case "TItemEquip"://物品装备表
                    MongoDBHelper.Instance.Insert(data as TItemEquip);
                    break;
                case "TArena"://竞技场信息
                    MongoDBHelper.Instance.Insert(data as TArena);
                    break;
                case "TArenaRecord"://竞技场战报记录
                    MongoDBHelper.Instance.Insert(data as TArenaRecord);
                    break;
                case "TLeague"://玩家联赛数据
                    MongoDBHelper.Instance.Insert(data as TLeague);
                    break;
                case "TLeagueRecord"://联赛日志
                    MongoDBHelper.Instance.Insert(data as TLeagueRecord);
                    break;
                case "TLeagueMatch"://联赛赛事
                    MongoDBHelper.Instance.Insert(data as TLeagueMatch);
                    break;
                case "TLeagueAward"://联赛奖励
                    MongoDBHelper.Instance.Insert(data as TLeagueAward);
                    break;
                case "TLeagueAwardLog"://联赛领奖日志
                    MongoDBHelper.Instance.Insert(data as TLeagueAwardLog);
                    break;
                case "TTask"://
                    MongoDBHelper.Instance.Insert(data as TTask);
                    break;
                case "TTaskBounty"://玩家赏金任务信息
                    MongoDBHelper.Instance.Insert(data as TTaskBounty);
                    break;
                case "TAchievement"://
                    MongoDBHelper.Instance.Insert(data as TAchievement);
                    break;
                case "TPlayerAction"://
                    MongoDBHelper.Instance.Insert(data as TPlayerAction);
                    break;
                case "TEventFB"://活动副本表
                    MongoDBHelper.Instance.Insert(data as TEventFB);
                    break;
                case "TEventFBAward"://活动副本排名奖励表
                    MongoDBHelper.Instance.Insert(data as TEventFBAward);
                    break;
                case "TActivityList"://活动列表
                    MongoDBHelper.Instance.Insert(data as TActivityList);
                    break;
                case "TActivityTask"://活动任务列表
                    MongoDBHelper.Instance.Insert(data as TActivityTask);
                    break;
                case "TActivity"://玩家活动数据
                    MongoDBHelper.Instance.Insert(data as TActivity);
                    break;
                case "THeroicRoad"://玩家英勇之路信息
                    MongoDBHelper.Instance.Insert(data as THeroicRoad);
                    break;
                case "TTaskHeroic"://玩家英勇任务信息
                    MongoDBHelper.Instance.Insert(data as TTaskHeroic);
                    break;
                case "TMail"://邮件信息
                    MongoDBHelper.Instance.Insert(data as TMail);
                    break;
                case "TMailSub"://邮件子项(记录已打开的群邮件玩家ID)
                    MongoDBHelper.Instance.Insert(data as TMailSub);
                    break;
                case "TPay"://首充,月卡信息
                    MongoDBHelper.Instance.Insert(data as TPay);
                    break;
                case "TBonus"://福利信息
                    MongoDBHelper.Instance.Insert(data as TBonus);
                    break;
                case "TTaskNewbie"://玩家新手任务信息
                    MongoDBHelper.Instance.Insert(data as TTaskNewbie);
                    break;
                case "TStore"://商城购买次数
                    MongoDBHelper.Instance.Insert(data as TStore);
                    break;
                case "TPayOrder"://充值定单
                    MongoDBHelper.Instance.Insert(data as TPayOrder);
                    break;
                case "TPayPack"://活动礼包
                    MongoDBHelper.Instance.Insert(data as TPayPack);
                    break;
                case "TPayPackData"://
                    MongoDBHelper.Instance.Insert(data as TPayPackData);
                    break;
                case "TGMSetPayPack"://
                    MongoDBHelper.Instance.Insert(data as TGMSetPayPack);
                    break;
                case "TTaskChap"://玩家章节任务列表
                    MongoDBHelper.Instance.Insert(data as TTaskChap);
                    break;
                case "TChat"://聊天信息
                    MongoDBHelper.Instance.Insert(data as TChat);
                    break;
                case "TFriend"://好友信息
                    MongoDBHelper.Instance.Insert(data as TFriend);
                    break;
                case "TBlackList"://聊天黑名单
                    MongoDBHelper.Instance.Insert(data as TBlackList);
                    break;
                case "TCDKeyPlayer"://玩家CDKey兑换信息
                    MongoDBHelper.Instance.Insert(data as TCDKeyPlayer);
                    break;
                case "TCDKey"://已使用礼品码信息
                    MongoDBHelper.Instance.Insert(data as TCDKey);
                    break;
                case "TClub"://俱乐部信息
                    MongoDBHelper.Instance.Insert(data as TClub);
                    break;
                case "TClubMember"://俱乐部成员信息
                    MongoDBHelper.Instance.Insert(data as TClubMember);
                    break;
                case "TClubApply"://俱乐部申请信息
                    MongoDBHelper.Instance.Insert(data as TClubApply);
                    break;
                case "TClubLog"://俱乐部日志
                    MongoDBHelper.Instance.Insert(data as TClubLog);
                    break;
                case "TTitan"://联盟泰坦战
                    MongoDBHelper.Instance.Insert(data as TTitan);
                    break;
                case "TTitanLog"://联盟泰坦战日志
                    MongoDBHelper.Instance.Insert(data as TTitanLog);
                    break;
                case "TTitanAward"://联盟泰坦战奖励
                    MongoDBHelper.Instance.Insert(data as TTitanAward);
                    break;
                case "TTitanAwardLog"://联盟泰坦战奖励
                    MongoDBHelper.Instance.Insert(data as TTitanAwardLog);
                    break;
                case "TClubWar"://联盟战争对象
                    MongoDBHelper.Instance.Insert(data as TClubWar);
                    break;
                case "TClubWarPlayer"://联盟战争成员
                    MongoDBHelper.Instance.Insert(data as TClubWarPlayer);
                    break;
                case "TClubWarNpc"://联盟战争成员Npc
                    MongoDBHelper.Instance.Insert(data as TClubWarNpc);
                    break;
                case "TClubWarLog"://联盟战争日志
                    MongoDBHelper.Instance.Insert(data as TClubWarLog);
                    break;
                case "TClubWarResult"://联盟战争结果
                    MongoDBHelper.Instance.Insert(data as TClubWarResult);
                    break;
                case "TClubWarAward"://联盟战争奖励
                    MongoDBHelper.Instance.Insert(data as TClubWarAward);
                    break;
                case "TClubWarAwardLog"://联盟战争领奖日志
                    MongoDBHelper.Instance.Insert(data as TClubWarAwardLog);
                    break;
                case "TLogShop"://商城日志
                    MongoDBHelper.Instance.Insert(data as TLogShop);
                    break;
                case "TLogTicket"://#消费统计
                    MongoDBHelper.Instance.Insert(data as TLogTicket);
                    break;
                case "TLogFun"://#功能统计
                    MongoDBHelper.Instance.Insert(data as TLogFun);
                    break;
                case "TLogItem"://#道具使用统计
                    MongoDBHelper.Instance.Insert(data as TLogItem);
                    break;
                case "TLogActivity"://#活动领取统计
                    MongoDBHelper.Instance.Insert(data as TLogActivity);
                    break;
                case "TLogLogin"://#登录日志
                    MongoDBHelper.Instance.Insert(data as TLogLogin);
                    break;
                case "TLogReg"://
                    MongoDBHelper.Instance.Insert(data as TLogReg);
                    break;
                case "TLogGuide"://#登录日志
                    MongoDBHelper.Instance.Insert(data as TLogGuide);
                    break;
                case "TLogTenMin"://#10分钟统计
                    MongoDBHelper.Instance.Insert(data as TLogTenMin);
                    break;
                case "TLogSevenDay"://#10分钟统计
                    MongoDBHelper.Instance.Insert(data as TLogSevenDay);
                    break;
                case "TLogServer"://#服务器数据统计
                    MongoDBHelper.Instance.Insert(data as TLogServer);
                    break;
                case "TLogBuild"://#建筑升级统计
                    MongoDBHelper.Instance.Insert(data as TLogBuild);
                    break;
                case "TLogBuildWork"://#建筑生产统计（统计建筑[工坊、训练营、兵营]中每一项的生产）
                    MongoDBHelper.Instance.Insert(data as TLogBuildWork);
                    break;
                case "TLogFB"://#副本、探索挑战次数统计（统计玩家打每个副本关卡的次数和人数）
                    MongoDBHelper.Instance.Insert(data as TLogFB);
                    break;
                case "TLogFBWin"://#副本、探索通关次数统计（统计每个副本关卡通关的次数和人数）
                    MongoDBHelper.Instance.Insert(data as TLogFBWin);
                    break;
                case "TLogFBExit"://
                    MongoDBHelper.Instance.Insert(data as TLogFBExit);
                    break;
                case "TLogFBRebirth"://
                    MongoDBHelper.Instance.Insert(data as TLogFBRebirth);
                    break;
                case "TLogDower"://#天赋升级统计
                    MongoDBHelper.Instance.Insert(data as TLogDower);
                    break;
                case "TLogPlayerLv"://
                    MongoDBHelper.Instance.Insert(data as TLogPlayerLv);
                    break;
                case "TLogSummonBuy"://
                    MongoDBHelper.Instance.Insert(data as TLogSummonBuy);
                    break;
                case "TLogArena"://
                    MongoDBHelper.Instance.Insert(data as TLogArena);
                    break;
                case "TLogTask"://
                    MongoDBHelper.Instance.Insert(data as TLogTask);
                    break;
                case "TLogFBNum"://
                    MongoDBHelper.Instance.Insert(data as TLogFBNum);
                    break;
                case "TlogClubRank"://
                    MongoDBHelper.Instance.Insert(data as TlogClubRank);
                    break;
                case "TLogHeroLvUp"://
                    MongoDBHelper.Instance.Insert(data as TLogHeroLvUp);
                    break;
                case "TServerInfo"://服务器信息
                    MongoDBHelper.Instance.Insert(data as TServerInfo);
                    break;
                case "TVersion"://版本信息
                    MongoDBHelper.Instance.Insert(data as TVersion);
                    break;
                case "TNotice"://公告信息
                    MongoDBHelper.Instance.Insert(data as TNotice);
                    break;
            }           
        }
      
        private void updateDB(ITable data)
        {
            switch (data.GetType().Name)
            {
                case "TAccount"://账号表
                    MongoDBHelper.Instance.Update(data as TAccount);
                    break;
                case "TPlayer"://玩家表
                    MongoDBHelper.Instance.Update(data as TPlayer);
                    break;
                case "THero"://英雄表
                    MongoDBHelper.Instance.Update(data as THero);
                    break;
                case "THeroTeam"://英雄队伍表
                    MongoDBHelper.Instance.Update(data as THeroTeam);
                    break;
                case "TBuild"://建筑表
                    MongoDBHelper.Instance.Update(data as TBuild);
                    break;
                case "TItemProp"://物品道具表
                    MongoDBHelper.Instance.Update(data as TItemProp);
                    break;
                case "TItemEquip"://物品装备表
                    MongoDBHelper.Instance.Update(data as TItemEquip);
                    break;
                case "TArena"://竞技场信息
                    MongoDBHelper.Instance.Update(data as TArena);
                    break;
                case "TArenaRecord"://竞技场战报记录
                    MongoDBHelper.Instance.Update(data as TArenaRecord);
                    break;
                case "TLeague"://玩家联赛数据
                    MongoDBHelper.Instance.Update(data as TLeague);
                    break;
                case "TLeagueRecord"://联赛日志
                    MongoDBHelper.Instance.Update(data as TLeagueRecord);
                    break;
                case "TLeagueMatch"://联赛赛事
                    MongoDBHelper.Instance.Update(data as TLeagueMatch);
                    break;
                case "TLeagueAward"://联赛奖励
                    MongoDBHelper.Instance.Update(data as TLeagueAward);
                    break;
                case "TLeagueAwardLog"://联赛领奖日志
                    MongoDBHelper.Instance.Update(data as TLeagueAwardLog);
                    break;
                case "TTask"://
                    MongoDBHelper.Instance.Update(data as TTask);
                    break;
                case "TTaskBounty"://玩家赏金任务信息
                    MongoDBHelper.Instance.Update(data as TTaskBounty);
                    break;
                case "TAchievement"://
                    MongoDBHelper.Instance.Update(data as TAchievement);
                    break;
                case "TPlayerAction"://
                    MongoDBHelper.Instance.Update(data as TPlayerAction);
                    break;
                case "TEventFB"://活动副本表
                    MongoDBHelper.Instance.Update(data as TEventFB);
                    break;
                case "TEventFBAward"://活动副本排名奖励表
                    MongoDBHelper.Instance.Update(data as TEventFBAward);
                    break;
                case "TActivityList"://活动列表
                    MongoDBHelper.Instance.Update(data as TActivityList);
                    break;
                case "TActivityTask"://活动任务列表
                    MongoDBHelper.Instance.Update(data as TActivityTask);
                    break;
                case "TActivity"://玩家活动数据
                    MongoDBHelper.Instance.Update(data as TActivity);
                    break;
                case "THeroicRoad"://玩家英勇之路信息
                    MongoDBHelper.Instance.Update(data as THeroicRoad);
                    break;
                case "TTaskHeroic"://玩家英勇任务信息
                    MongoDBHelper.Instance.Update(data as TTaskHeroic);
                    break;
                case "TMail"://邮件信息
                    MongoDBHelper.Instance.Update(data as TMail);
                    break;
                case "TMailSub"://邮件子项(记录已打开的群邮件玩家ID)
                    MongoDBHelper.Instance.Update(data as TMailSub);
                    break;
                case "TPay"://首充,月卡信息
                    MongoDBHelper.Instance.Update(data as TPay);
                    break;
                case "TBonus"://福利信息
                    MongoDBHelper.Instance.Update(data as TBonus);
                    break;
                case "TTaskNewbie"://玩家新手任务信息
                    MongoDBHelper.Instance.Update(data as TTaskNewbie);
                    break;
                case "TStore"://商城购买次数
                    MongoDBHelper.Instance.Update(data as TStore);
                    break;
                case "TPayOrder"://充值定单
                    MongoDBHelper.Instance.Update(data as TPayOrder);
                    break;
                case "TPayPack"://活动礼包
                    MongoDBHelper.Instance.Update(data as TPayPack);
                    break;
                case "TPayPackData"://
                    MongoDBHelper.Instance.Update(data as TPayPackData);
                    break;
                case "TGMSetPayPack"://
                    MongoDBHelper.Instance.Update(data as TGMSetPayPack);
                    break;
                case "TTaskChap"://玩家章节任务列表
                    MongoDBHelper.Instance.Update(data as TTaskChap);
                    break;
                case "TChat"://聊天信息
                    MongoDBHelper.Instance.Update(data as TChat);
                    break;
                case "TFriend"://好友信息
                    MongoDBHelper.Instance.Update(data as TFriend);
                    break;
                case "TBlackList"://聊天黑名单
                    MongoDBHelper.Instance.Update(data as TBlackList);
                    break;
                case "TCDKeyPlayer"://玩家CDKey兑换信息
                    MongoDBHelper.Instance.Update(data as TCDKeyPlayer);
                    break;
                case "TCDKey"://已使用礼品码信息
                    MongoDBHelper.Instance.Update(data as TCDKey);
                    break;
                case "TClub"://俱乐部信息
                    MongoDBHelper.Instance.Update(data as TClub);
                    break;
                case "TClubMember"://俱乐部成员信息
                    MongoDBHelper.Instance.Update(data as TClubMember);
                    break;
                case "TClubApply"://俱乐部申请信息
                    MongoDBHelper.Instance.Update(data as TClubApply);
                    break;
                case "TClubLog"://俱乐部日志
                    MongoDBHelper.Instance.Update(data as TClubLog);
                    break;
                case "TTitan"://联盟泰坦战
                    MongoDBHelper.Instance.Update(data as TTitan);
                    break;
                case "TTitanLog"://联盟泰坦战日志
                    MongoDBHelper.Instance.Update(data as TTitanLog);
                    break;
                case "TTitanAward"://联盟泰坦战奖励
                    MongoDBHelper.Instance.Update(data as TTitanAward);
                    break;
                case "TTitanAwardLog"://联盟泰坦战奖励
                    MongoDBHelper.Instance.Update(data as TTitanAwardLog);
                    break;
                case "TClubWar"://联盟战争对象
                    MongoDBHelper.Instance.Update(data as TClubWar);
                    break;
                case "TClubWarPlayer"://联盟战争成员
                    MongoDBHelper.Instance.Update(data as TClubWarPlayer);
                    break;
                case "TClubWarNpc"://联盟战争成员Npc
                    MongoDBHelper.Instance.Update(data as TClubWarNpc);
                    break;
                case "TClubWarLog"://联盟战争日志
                    MongoDBHelper.Instance.Update(data as TClubWarLog);
                    break;
                case "TClubWarResult"://联盟战争结果
                    MongoDBHelper.Instance.Update(data as TClubWarResult);
                    break;
                case "TClubWarAward"://联盟战争奖励
                    MongoDBHelper.Instance.Update(data as TClubWarAward);
                    break;
                case "TClubWarAwardLog"://联盟战争领奖日志
                    MongoDBHelper.Instance.Update(data as TClubWarAwardLog);
                    break;
                case "TLogShop"://商城日志
                    MongoDBHelper.Instance.Update(data as TLogShop);
                    break;
                case "TLogTicket"://#消费统计
                    MongoDBHelper.Instance.Update(data as TLogTicket);
                    break;
                case "TLogFun"://#功能统计
                    MongoDBHelper.Instance.Update(data as TLogFun);
                    break;
                case "TLogItem"://#道具使用统计
                    MongoDBHelper.Instance.Update(data as TLogItem);
                    break;
                case "TLogActivity"://#活动领取统计
                    MongoDBHelper.Instance.Update(data as TLogActivity);
                    break;
                case "TLogLogin"://#登录日志
                    MongoDBHelper.Instance.Update(data as TLogLogin);
                    break;
                case "TLogReg"://
                    MongoDBHelper.Instance.Update(data as TLogReg);
                    break;
                case "TLogGuide"://#登录日志
                    MongoDBHelper.Instance.Update(data as TLogGuide);
                    break;
                case "TLogTenMin"://#10分钟统计
                    MongoDBHelper.Instance.Update(data as TLogTenMin);
                    break;
                case "TLogSevenDay"://#10分钟统计
                    MongoDBHelper.Instance.Update(data as TLogSevenDay);
                    break;
                case "TLogServer"://#服务器数据统计
                    MongoDBHelper.Instance.Update(data as TLogServer);
                    break;
                case "TLogBuild"://#建筑升级统计
                    MongoDBHelper.Instance.Update(data as TLogBuild);
                    break;
                case "TLogBuildWork"://#建筑生产统计（统计建筑[工坊、训练营、兵营]中每一项的生产）
                    MongoDBHelper.Instance.Update(data as TLogBuildWork);
                    break;
                case "TLogFB"://#副本、探索挑战次数统计（统计玩家打每个副本关卡的次数和人数）
                    MongoDBHelper.Instance.Update(data as TLogFB);
                    break;
                case "TLogFBWin"://#副本、探索通关次数统计（统计每个副本关卡通关的次数和人数）
                    MongoDBHelper.Instance.Update(data as TLogFBWin);
                    break;
                case "TLogFBExit"://
                    MongoDBHelper.Instance.Update(data as TLogFBExit);
                    break;
                case "TLogFBRebirth"://
                    MongoDBHelper.Instance.Update(data as TLogFBRebirth);
                    break;
                case "TLogDower"://#天赋升级统计
                    MongoDBHelper.Instance.Update(data as TLogDower);
                    break;
                case "TLogPlayerLv"://
                    MongoDBHelper.Instance.Update(data as TLogPlayerLv);
                    break;
                case "TLogSummonBuy"://
                    MongoDBHelper.Instance.Update(data as TLogSummonBuy);
                    break;
                case "TLogArena"://
                    MongoDBHelper.Instance.Update(data as TLogArena);
                    break;
                case "TLogTask"://
                    MongoDBHelper.Instance.Update(data as TLogTask);
                    break;
                case "TLogFBNum"://
                    MongoDBHelper.Instance.Update(data as TLogFBNum);
                    break;
                case "TlogClubRank"://
                    MongoDBHelper.Instance.Update(data as TlogClubRank);
                    break;
                case "TLogHeroLvUp"://
                    MongoDBHelper.Instance.Update(data as TLogHeroLvUp);
                    break;
                case "TServerInfo"://服务器信息
                    MongoDBHelper.Instance.Update(data as TServerInfo);
                    break;
                case "TVersion"://版本信息
                    MongoDBHelper.Instance.Update(data as TVersion);
                    break;
                case "TNotice"://公告信息
                    MongoDBHelper.Instance.Update(data as TNotice);
                    break;
            }
        }
      
        private void deleteDB(ITable data)
        {
            switch (data.GetType().Name)
            {
                case "TAccount"://账号表
                    MongoDBHelper.Instance.Delete(data as TAccount);
                    break;
                case "TPlayer"://玩家表
                    MongoDBHelper.Instance.Delete(data as TPlayer);
                    break;
                case "THero"://英雄表
                    MongoDBHelper.Instance.Delete(data as THero);
                    break;
                case "THeroTeam"://英雄队伍表
                    MongoDBHelper.Instance.Delete(data as THeroTeam);
                    break;
                case "TBuild"://建筑表
                    MongoDBHelper.Instance.Delete(data as TBuild);
                    break;
                case "TItemProp"://物品道具表
                    MongoDBHelper.Instance.Delete(data as TItemProp);
                    break;
                case "TItemEquip"://物品装备表
                    MongoDBHelper.Instance.Delete(data as TItemEquip);
                    break;
                case "TArena"://竞技场信息
                    MongoDBHelper.Instance.Delete(data as TArena);
                    break;
                case "TArenaRecord"://竞技场战报记录
                    MongoDBHelper.Instance.Delete(data as TArenaRecord);
                    break;
                case "TLeague"://玩家联赛数据
                    MongoDBHelper.Instance.Delete(data as TLeague);
                    break;
                case "TLeagueRecord"://联赛日志
                    MongoDBHelper.Instance.Delete(data as TLeagueRecord);
                    break;
                case "TLeagueMatch"://联赛赛事
                    MongoDBHelper.Instance.Delete(data as TLeagueMatch);
                    break;
                case "TLeagueAward"://联赛奖励
                    MongoDBHelper.Instance.Delete(data as TLeagueAward);
                    break;
                case "TLeagueAwardLog"://联赛领奖日志
                    MongoDBHelper.Instance.Delete(data as TLeagueAwardLog);
                    break;
                case "TTask"://
                    MongoDBHelper.Instance.Delete(data as TTask);
                    break;
                case "TTaskBounty"://玩家赏金任务信息
                    MongoDBHelper.Instance.Delete(data as TTaskBounty);
                    break;
                case "TAchievement"://
                    MongoDBHelper.Instance.Delete(data as TAchievement);
                    break;
                case "TPlayerAction"://
                    MongoDBHelper.Instance.Delete(data as TPlayerAction);
                    break;
                case "TEventFB"://活动副本表
                    MongoDBHelper.Instance.Delete(data as TEventFB);
                    break;
                case "TEventFBAward"://活动副本排名奖励表
                    MongoDBHelper.Instance.Delete(data as TEventFBAward);
                    break;
                case "TActivityList"://活动列表
                    MongoDBHelper.Instance.Delete(data as TActivityList);
                    break;
                case "TActivityTask"://活动任务列表
                    MongoDBHelper.Instance.Delete(data as TActivityTask);
                    break;
                case "TActivity"://玩家活动数据
                    MongoDBHelper.Instance.Delete(data as TActivity);
                    break;
                case "THeroicRoad"://玩家英勇之路信息
                    MongoDBHelper.Instance.Delete(data as THeroicRoad);
                    break;
                case "TTaskHeroic"://玩家英勇任务信息
                    MongoDBHelper.Instance.Delete(data as TTaskHeroic);
                    break;
                case "TMail"://邮件信息
                    MongoDBHelper.Instance.Delete(data as TMail);
                    break;
                case "TMailSub"://邮件子项(记录已打开的群邮件玩家ID)
                    MongoDBHelper.Instance.Delete(data as TMailSub);
                    break;
                case "TPay"://首充,月卡信息
                    MongoDBHelper.Instance.Delete(data as TPay);
                    break;
                case "TBonus"://福利信息
                    MongoDBHelper.Instance.Delete(data as TBonus);
                    break;
                case "TTaskNewbie"://玩家新手任务信息
                    MongoDBHelper.Instance.Delete(data as TTaskNewbie);
                    break;
                case "TStore"://商城购买次数
                    MongoDBHelper.Instance.Delete(data as TStore);
                    break;
                case "TPayOrder"://充值定单
                    MongoDBHelper.Instance.Delete(data as TPayOrder);
                    break;
                case "TPayPack"://活动礼包
                    MongoDBHelper.Instance.Delete(data as TPayPack);
                    break;
                case "TPayPackData"://
                    MongoDBHelper.Instance.Delete(data as TPayPackData);
                    break;
                case "TGMSetPayPack"://
                    MongoDBHelper.Instance.Delete(data as TGMSetPayPack);
                    break;
                case "TTaskChap"://玩家章节任务列表
                    MongoDBHelper.Instance.Delete(data as TTaskChap);
                    break;
                case "TChat"://聊天信息
                    MongoDBHelper.Instance.Delete(data as TChat);
                    break;
                case "TFriend"://好友信息
                    MongoDBHelper.Instance.Delete(data as TFriend);
                    break;
                case "TBlackList"://聊天黑名单
                    MongoDBHelper.Instance.Delete(data as TBlackList);
                    break;
                case "TCDKeyPlayer"://玩家CDKey兑换信息
                    MongoDBHelper.Instance.Delete(data as TCDKeyPlayer);
                    break;
                case "TCDKey"://已使用礼品码信息
                    MongoDBHelper.Instance.Delete(data as TCDKey);
                    break;
                case "TClub"://俱乐部信息
                    MongoDBHelper.Instance.Delete(data as TClub);
                    break;
                case "TClubMember"://俱乐部成员信息
                    MongoDBHelper.Instance.Delete(data as TClubMember);
                    break;
                case "TClubApply"://俱乐部申请信息
                    MongoDBHelper.Instance.Delete(data as TClubApply);
                    break;
                case "TClubLog"://俱乐部日志
                    MongoDBHelper.Instance.Delete(data as TClubLog);
                    break;
                case "TTitan"://联盟泰坦战
                    MongoDBHelper.Instance.Delete(data as TTitan);
                    break;
                case "TTitanLog"://联盟泰坦战日志
                    MongoDBHelper.Instance.Delete(data as TTitanLog);
                    break;
                case "TTitanAward"://联盟泰坦战奖励
                    MongoDBHelper.Instance.Delete(data as TTitanAward);
                    break;
                case "TTitanAwardLog"://联盟泰坦战奖励
                    MongoDBHelper.Instance.Delete(data as TTitanAwardLog);
                    break;
                case "TClubWar"://联盟战争对象
                    MongoDBHelper.Instance.Delete(data as TClubWar);
                    break;
                case "TClubWarPlayer"://联盟战争成员
                    MongoDBHelper.Instance.Delete(data as TClubWarPlayer);
                    break;
                case "TClubWarNpc"://联盟战争成员Npc
                    MongoDBHelper.Instance.Delete(data as TClubWarNpc);
                    break;
                case "TClubWarLog"://联盟战争日志
                    MongoDBHelper.Instance.Delete(data as TClubWarLog);
                    break;
                case "TClubWarResult"://联盟战争结果
                    MongoDBHelper.Instance.Delete(data as TClubWarResult);
                    break;
                case "TClubWarAward"://联盟战争奖励
                    MongoDBHelper.Instance.Delete(data as TClubWarAward);
                    break;
                case "TClubWarAwardLog"://联盟战争领奖日志
                    MongoDBHelper.Instance.Delete(data as TClubWarAwardLog);
                    break;
                case "TLogShop"://商城日志
                    MongoDBHelper.Instance.Delete(data as TLogShop);
                    break;
                case "TLogTicket"://#消费统计
                    MongoDBHelper.Instance.Delete(data as TLogTicket);
                    break;
                case "TLogFun"://#功能统计
                    MongoDBHelper.Instance.Delete(data as TLogFun);
                    break;
                case "TLogItem"://#道具使用统计
                    MongoDBHelper.Instance.Delete(data as TLogItem);
                    break;
                case "TLogActivity"://#活动领取统计
                    MongoDBHelper.Instance.Delete(data as TLogActivity);
                    break;
                case "TLogLogin"://#登录日志
                    MongoDBHelper.Instance.Delete(data as TLogLogin);
                    break;
                case "TLogReg"://
                    MongoDBHelper.Instance.Delete(data as TLogReg);
                    break;
                case "TLogGuide"://#登录日志
                    MongoDBHelper.Instance.Delete(data as TLogGuide);
                    break;
                case "TLogTenMin"://#10分钟统计
                    MongoDBHelper.Instance.Delete(data as TLogTenMin);
                    break;
                case "TLogSevenDay"://#10分钟统计
                    MongoDBHelper.Instance.Delete(data as TLogSevenDay);
                    break;
                case "TLogServer"://#服务器数据统计
                    MongoDBHelper.Instance.Delete(data as TLogServer);
                    break;
                case "TLogBuild"://#建筑升级统计
                    MongoDBHelper.Instance.Delete(data as TLogBuild);
                    break;
                case "TLogBuildWork"://#建筑生产统计（统计建筑[工坊、训练营、兵营]中每一项的生产）
                    MongoDBHelper.Instance.Delete(data as TLogBuildWork);
                    break;
                case "TLogFB"://#副本、探索挑战次数统计（统计玩家打每个副本关卡的次数和人数）
                    MongoDBHelper.Instance.Delete(data as TLogFB);
                    break;
                case "TLogFBWin"://#副本、探索通关次数统计（统计每个副本关卡通关的次数和人数）
                    MongoDBHelper.Instance.Delete(data as TLogFBWin);
                    break;
                case "TLogFBExit"://
                    MongoDBHelper.Instance.Delete(data as TLogFBExit);
                    break;
                case "TLogFBRebirth"://
                    MongoDBHelper.Instance.Delete(data as TLogFBRebirth);
                    break;
                case "TLogDower"://#天赋升级统计
                    MongoDBHelper.Instance.Delete(data as TLogDower);
                    break;
                case "TLogPlayerLv"://
                    MongoDBHelper.Instance.Delete(data as TLogPlayerLv);
                    break;
                case "TLogSummonBuy"://
                    MongoDBHelper.Instance.Delete(data as TLogSummonBuy);
                    break;
                case "TLogArena"://
                    MongoDBHelper.Instance.Delete(data as TLogArena);
                    break;
                case "TLogTask"://
                    MongoDBHelper.Instance.Delete(data as TLogTask);
                    break;
                case "TLogFBNum"://
                    MongoDBHelper.Instance.Delete(data as TLogFBNum);
                    break;
                case "TlogClubRank"://
                    MongoDBHelper.Instance.Delete(data as TlogClubRank);
                    break;
                case "TLogHeroLvUp"://
                    MongoDBHelper.Instance.Delete(data as TLogHeroLvUp);
                    break;
                case "TServerInfo"://服务器信息
                    MongoDBHelper.Instance.Delete(data as TServerInfo);
                    break;
                case "TVersion"://版本信息
                    MongoDBHelper.Instance.Delete(data as TVersion);
                    break;
                case "TNotice"://公告信息
                    MongoDBHelper.Instance.Delete(data as TNotice);
                    break;
            }
        }
    }
}
