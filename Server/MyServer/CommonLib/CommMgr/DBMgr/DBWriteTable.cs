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
                case "TSeason"://服务器赛季
                    MongoDBHelper.Instance.Insert(data as TSeason);
                    break;
                case "THero"://英雄表
                    MongoDBHelper.Instance.Insert(data as THero);
                    break;
                case "TItemProp"://物品道具表
                    MongoDBHelper.Instance.Insert(data as TItemProp);
                    break;
                case "TItemEquip"://物品装备表
                    MongoDBHelper.Instance.Insert(data as TItemEquip);
                    break;
                case "TTaskDay"://
                    MongoDBHelper.Instance.Insert(data as TTaskDay);
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
                case "TPlayerLeft"://
                    MongoDBHelper.Instance.Insert(data as TPlayerLeft);
                    break;
                case "TCDKeyPlayer"://玩家CDKey兑换信息
                    MongoDBHelper.Instance.Insert(data as TCDKeyPlayer);
                    break;
                case "TCDKey"://已使用礼品码信息
                    MongoDBHelper.Instance.Insert(data as TCDKey);
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
                case "TSeason"://服务器赛季
                    MongoDBHelper.Instance.Update(data as TSeason);
                    break;
                case "THero"://英雄表
                    MongoDBHelper.Instance.Update(data as THero);
                    break;
                case "TItemProp"://物品道具表
                    MongoDBHelper.Instance.Update(data as TItemProp);
                    break;
                case "TItemEquip"://物品装备表
                    MongoDBHelper.Instance.Update(data as TItemEquip);
                    break;
                case "TTaskDay"://
                    MongoDBHelper.Instance.Update(data as TTaskDay);
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
                case "TPlayerLeft"://
                    MongoDBHelper.Instance.Update(data as TPlayerLeft);
                    break;
                case "TCDKeyPlayer"://玩家CDKey兑换信息
                    MongoDBHelper.Instance.Update(data as TCDKeyPlayer);
                    break;
                case "TCDKey"://已使用礼品码信息
                    MongoDBHelper.Instance.Update(data as TCDKey);
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
                case "TSeason"://服务器赛季
                    MongoDBHelper.Instance.Delete(data as TSeason);
                    break;
                case "THero"://英雄表
                    MongoDBHelper.Instance.Delete(data as THero);
                    break;
                case "TItemProp"://物品道具表
                    MongoDBHelper.Instance.Delete(data as TItemProp);
                    break;
                case "TItemEquip"://物品装备表
                    MongoDBHelper.Instance.Delete(data as TItemEquip);
                    break;
                case "TTaskDay"://
                    MongoDBHelper.Instance.Delete(data as TTaskDay);
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
                case "TPlayerLeft"://
                    MongoDBHelper.Instance.Delete(data as TPlayerLeft);
                    break;
                case "TCDKeyPlayer"://玩家CDKey兑换信息
                    MongoDBHelper.Instance.Delete(data as TCDKeyPlayer);
                    break;
                case "TCDKey"://已使用礼品码信息
                    MongoDBHelper.Instance.Delete(data as TCDKey);
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
            }
        }
    }
}
