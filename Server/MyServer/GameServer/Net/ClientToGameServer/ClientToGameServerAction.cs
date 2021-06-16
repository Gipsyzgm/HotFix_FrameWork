using System;
using System.Collections.Generic;
using CommonLib;
using CSocket;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        private Dictionary<ushort, Action<ClientToGameServerMessage>> _actionList = new Dictionary<ushort, Action<ClientToGameServerMessage>>();
        
        private static readonly ClientToGameServerAction instance = new ClientToGameServerAction();
        public static ClientToGameServerAction Instance => instance;
        private ClientToGameServerAction()
        {
            _actionList.Add(1, Sys_heartbeat);      //请求心跳包30秒一次
            _actionList.Add(3004, Save_guide);      //保存指引步骤
            _actionList.Add(3006, Player_changeName);      //请求玩家改名
            _actionList.Add(3008, Player_buyGold);      //请求购买金币
            _actionList.Add(3010, Save_guideStep);      //保存指引步骤
            _actionList.Add(3011, Player_changeIcon);      //请求玩家修改头像
            _actionList.Add(3017, Player_buyPower);      //请求钻石补满体力
            _actionList.Add(3026, Player_dowerLevelUp);      //请求天赋升级
            _actionList.Add(3029, Player_AdDouble);      //请求领取玩家广告双倍奖励
            _actionList.Add(3030, Player_FinishGuide);      //请求完成新手关卡
            _actionList.Add(3032, Player_SeasonGet);      //请求领取赛季令牌奖励
            _actionList.Add(21000, Login_verify);      //请求登录验证
            _actionList.Add(21006, Login_bind);      //请求游客绑定
            _actionList.Add(31003, Bag_useItem);      //请求使用物品
            _actionList.Add(31005, Bag_sellProp);      //请求出售道具
            _actionList.Add(31006, Bag_sellEquip);      //请求出售装备
            _actionList.Add(32004, Hero_levelUp);      //请求英雄升级
            _actionList.Add(32006, Hero_break);      //请求英雄突破
            _actionList.Add(32008, Hero_Get);      //请求英雄获取
            _actionList.Add(32010, Hero_Change);      //请求更换出战英雄
            _actionList.Add(34000, Equip_streng);      //请求装备升级
            _actionList.Add(34002, Equip_break);      //请求装备升阶
            _actionList.Add(34004, Equip_Change);      //请求更换装备
            _actionList.Add(34006, Equip_Resolve);      //请求分解装备
            _actionList.Add(34008, Equip_merge);      //请求装备融合
            _actionList.Add(34010, Gems_resolve);      //请求分解宝石
            _actionList.Add(34012, Gems_Inlay);      //请求镶嵌(卸下)宝石
            _actionList.Add(35001, SignIn_award);      //请求每日签到领奖
            _actionList.Add(35011, OnlineAward_get);      //请求领取在线奖励
            _actionList.Add(35021, OpenFund_get);      //请求领取开服基金
            _actionList.Add(35031, Cdkey_get);      //请求兑换CDKey
            _actionList.Add(35051, LevelAward_get);      //请求领取等级奖励
            _actionList.Add(35061, TaskNewbie_get);      //请求新手活动任务奖励
            _actionList.Add(35065, TaskNewbie_box);      //请求新手活动宝箱进度奖励
            _actionList.Add(35070, Treasure_spin);      //请求宝藏抽奖
            _actionList.Add(35080, Ads_award);      //请求领取广告奖励
            _actionList.Add(35082, Hang_Open);      //请求打开挂机奖励页面
            _actionList.Add(35084, Hang_Get);      //请求领取挂机奖励
            _actionList.Add(35086, Box_GetHero);      //请求单抽英雄
            _actionList.Add(35088, Box_GetHerofive);      //请求五连抽英雄
            _actionList.Add(35090, Threeads_award);      //请求三日奖励广告获取
            _actionList.Add(35092, Box_GetEquip);      //请求单抽装备
            _actionList.Add(35094, Box_GetEquipfive);      //请求五连抽装备
            _actionList.Add(35096, Circle_Get);      //请求转盘奖励
            _actionList.Add(35098, Circle_GetBox);      //请求开启转盘宝箱
            _actionList.Add(35100, FloatBox_award);      //请求悬浮宝箱广告获取
            _actionList.Add(35102, Achievement_List);      //请求成就列表
            _actionList.Add(35104, Achievement_GetAward);      //请求领取成就奖励
            _actionList.Add(35107, Gem_GetOne);      //请求单抽宝石
            _actionList.Add(35109, Gem_GetFive);      //请求五连抽宝石
            _actionList.Add(35111, Gem_FreshChange);      //请求刷新精粹兑换
            _actionList.Add(35113, Gem_Change);      //请求精粹兑换
            _actionList.Add(36010, Vip_buyGift);      //请求领取VIP礼包
            _actionList.Add(36020, MonthCard_get);      //请求领取月卡奖励
            _actionList.Add(36040, Pay_getFirstPay);      //请求领取首充奖励
            _actionList.Add(36050, Pay_order);      //请求充值下定单
            _actionList.Add(36052, Pay_succeed);      //请求支付成功
            _actionList.Add(36902, Store_buyItem);      //请求购买商城物品
            _actionList.Add(36904, Store_ADTimes);      //请求领取一次商城广告奖励
            _actionList.Add(36951, Summon_buy);      //请求召唤
            _actionList.Add(36953, Store_exchange);      //请求商城兑换金币
            _actionList.Add(50011, Mail_detail);      //请求邮件详细信息
            _actionList.Add(50021, Mail_open);      //请求打开邮件
            _actionList.Add(50023, Mail_delete);      //请求删除一个邮件
            _actionList.Add(50025, Mail_getAward);      //请求领取邮件附件奖励
            _actionList.Add(50027, Mail_openAll);      //请求一键领取所有未读邮件
            _actionList.Add(51003, TaskLine_get);      //请求领取任务线上的任务奖励
            _actionList.Add(51005, TaskLine_Add);      //请求增加广告进度（加一次广告次数）
            _actionList.Add(52001, Activity_Info);      //请求活动列表信息
            _actionList.Add(52004, Activity_Get);      //请求领取活动任务奖励
            _actionList.Add(52007, Activiyt_packOpen);      //请求开启活动礼包
            _actionList.Add(52009, Activiyt_AllOpen);      //请求一键领取节日奖励
            _actionList.Add(52011, Activiyt_packFresh);      //请求钻石刷新活动礼包
            _actionList.Add(52013, Activiyt_OpenOne);      //请求单个领取节日奖励
            _actionList.Add(60101, War_fb);      //请求副本挑战
            _actionList.Add(60103, War_fbRebirth);      //请求副本战斗重生
            _actionList.Add(60107, War_fbStageEnd);      //请求章节关卡结束
            _actionList.Add(60110, War_fbExit);      //请求退出章节
            _actionList.Add(60112, War_GetBox);      //请求领取章节宝箱
            _actionList.Add(60116, War_TraderBuy);      //请求随机商人购买
            _actionList.Add(60118, War_RebornByAD);      //请求章节内看广告复活
            _actionList.Add(60119, War_ExitAcFb);      //请求退出活动副本
            _actionList.Add(60121, War_worldBossFb);      //请求进入世界Boss副本
            _actionList.Add(60124, War_ExworldBossFb);      //请求退出世界Boss副本
            _actionList.Add(60126, War_worldBossFbGet);      //请求一键领取世界Boss副本奖励
            _actionList.Add(60128, War_TowerFbEnd);      //请求爬塔副本关卡结束
            _actionList.Add(60131, War_InfinityFbEnd);      //请求无尽副本关卡结束
            _actionList.Add(60133, War_InfinityFbGet);      //请求无尽副本进度奖励
            _actionList.Add(60135, War_InfinityFbStart);      //请求无尽副本关卡进入
            _actionList.Add(60137, War_InfinityFbExit);      //请求退出无尽副本
            _actionList.Add(8001, Rank_List);      //请求排行榜数据
            _actionList.Add(8003, Rank_lookPlayer);      //请求查看其他玩家信息
            _actionList.Add(9990, TestEncrypt);      //测试消息加密

        }
        protected void onDispatchMainThread(ClientToGameServerMessage e)
        {
            try
            {               
                 ushort protocol = e.Protocol;
                _actionList[protocol].Invoke(e); 
            }
            catch (Exception ex)
            {   
                Logger.LogError(ex.Message, ex.StackTrace);
            }
        }
        public void Dispatch(ClientToGameServerMessage e)
        {
            MainThreadContext.Instance.Post(o => { onDispatchMainThread(e); });
        }
    }
}
