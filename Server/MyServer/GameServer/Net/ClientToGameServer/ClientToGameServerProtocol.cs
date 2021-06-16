using System;
using System.Collections.Generic;
using Google.Protobuf;
namespace GameServer.Net
{
    /// <summary>
    /// 工具生成，不要修改
    /// </summary>
    public class ClientToGameServerProtocol
    {
        private Dictionary<Type, ushort> typeToProtocolDic = new Dictionary<Type, ushort>();

        private static readonly ClientToGameServerProtocol instance = new ClientToGameServerProtocol();
        public static ClientToGameServerProtocol Instance => instance;
        private HashSet<int> _encryptList = new HashSet<int>();

        private ClientToGameServerProtocol()
        {
            typeToProtocolDic.Add(typeof(PbError.SC_error_code), 101); //公用错误提示
            typeToProtocolDic.Add(typeof(PbSystem.SC_sys_heartbeat), 2); //收到心跳包
            typeToProtocolDic.Add(typeof(PbSystem.SC_sys_offline), 103); //人物断线消息
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_resetData), 3001); //收到每天0点重置数据
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_updateVirtual), 3002); //收到玩家虚拟物品更新
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_exp), 3003); //收到玩家经验发生改变
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_redDot), 3005); //玩家红点用到的数据
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_changeName), 3007); //收到玩家改名
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_buyGold), 3009); //收到购买金币
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_changeIcon), 3012); //收到玩家修改头像
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_point), 3013); //收到玩家体力改变
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_buyPower), 3016); //收到钻石补满体力
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_newPush), 3019); //收到新客服推送消息
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_SeasonUpdate), 3025); //收到赛季更新
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_dowerLevelUp), 3027); //收到天赋升级
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_dowerPoint), 3028); //收到天赋点增加
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_SeasonVIPOpen), 3031); //收到赛季令牌开启通知
            typeToProtocolDic.Add(typeof(PbPlayer.SC_player_SeasonGet), 3033); //收到已领取赛季令牌奖励详情
            typeToProtocolDic.Add(typeof(PbLogin.SC_login_verify), 21001); //收到登录验证
            typeToProtocolDic.Add(typeof(PbLogin.SC_login_playerInfo), 21004); //收到登录成功
            typeToProtocolDic.Add(typeof(PbLogin.SC_login_enter), 21005); //收到服务器通知客户端进入游戏,进入游戏必要数据发送完成
            typeToProtocolDic.Add(typeof(PbLogin.SC_login_bind), 21007); //收到游客绑定
            typeToProtocolDic.Add(typeof(PbLogin.SC_login_reLogin), 21008); //收到断线重连数据
            typeToProtocolDic.Add(typeof(PbBag.SC_bag_list), 31000); //收到背包列表(登录发)
            typeToProtocolDic.Add(typeof(PbBag.SC_bag_newItems), 31001); //获得多个物品
            typeToProtocolDic.Add(typeof(PbBag.SC_bag_updateItemCount), 31002); //更新单个物品,道具或装备数量
            typeToProtocolDic.Add(typeof(PbBag.SC_bag_useItem), 31004); //收到使用物品
            typeToProtocolDic.Add(typeof(PbBag.SC_bag_sellEquip), 31007); //收到出售装备
            typeToProtocolDic.Add(typeof(PbHero.SC_hero_levelUp), 32005); //收到英雄升级
            typeToProtocolDic.Add(typeof(PbHero.SC_hero_break), 32007); //收到英雄突破
            typeToProtocolDic.Add(typeof(PbHero.SC_hero_Get), 32009); //收到英雄获取
            typeToProtocolDic.Add(typeof(PbHero.SC_hero_Change), 32011); //收到更换出战英雄
            typeToProtocolDic.Add(typeof(PbEquipUp.SC_equip_streng), 34001); //收到装备升级
            typeToProtocolDic.Add(typeof(PbEquipUp.SC_equip_break), 34003); //收到装备升阶
            typeToProtocolDic.Add(typeof(PbEquipUp.SC_equip_Change), 34005); //收到更换装备
            typeToProtocolDic.Add(typeof(PbEquipUp.SC_equip_Resolve), 34007); //收到分解装备
            typeToProtocolDic.Add(typeof(PbEquipUp.SC_equip_merge), 34009); //收到装备融合
            typeToProtocolDic.Add(typeof(PbEquipUp.SC_gems_resolve), 34011); //收到分解宝石
            typeToProtocolDic.Add(typeof(PbEquipUp.SC_gems_Inlay), 34013); //收到镶嵌(卸下)宝石
            typeToProtocolDic.Add(typeof(PbBonus.SC_bonus_info), 35000); //收到福利信息
            typeToProtocolDic.Add(typeof(PbBonus.SC_signIn_award), 35002); //收到每日签到领奖
            typeToProtocolDic.Add(typeof(PbBonus.SC_onlineAward_get), 35012); //收到领取在线奖励
            typeToProtocolDic.Add(typeof(PbBonus.SC_openFund_get), 35022); //收到领取开服基金
            typeToProtocolDic.Add(typeof(PbBonus.SC_cdkey_get), 35032); //收到兑换CDKey结果
            typeToProtocolDic.Add(typeof(PbBonus.SC_levelAward_get), 35052); //收到领取等级奖励
            typeToProtocolDic.Add(typeof(PbBonus.SC_taskNewbie_list), 35060); //收到新手活动任务列表
            typeToProtocolDic.Add(typeof(PbBonus.SC_taskNewbie_get), 35062); //收到新手活动任务奖励
            typeToProtocolDic.Add(typeof(PbBonus.SC_taskNewbie_change), 35063); //收到单个新手活动任务进度发生改变
            typeToProtocolDic.Add(typeof(PbBonus.SC_taskNewbie_box), 35066); //收到新手活动宝箱进度奖励
            typeToProtocolDic.Add(typeof(PbBonus.SC_treasure_spin), 35071); //收到宝藏抽奖
            typeToProtocolDic.Add(typeof(PbBonus.SC_treasure_state), 35072); //收到宝藏开启状态
            typeToProtocolDic.Add(typeof(PbBonus.SC_ads_award), 35081); //收到领取广告奖励
            typeToProtocolDic.Add(typeof(PbBonus.SC_hang_Open), 35083); //收到打开挂机奖励页面
            typeToProtocolDic.Add(typeof(PbBonus.SC_hang_Get), 35085); //收到领取挂机奖励结果
            typeToProtocolDic.Add(typeof(PbBonus.SC_Box_GetHero), 35087); //收到单抽英雄结果
            typeToProtocolDic.Add(typeof(PbBonus.SC_Box_GetHerofive), 35089); //收到五连抽英雄结果
            typeToProtocolDic.Add(typeof(PbBonus.SC_Threeads_award), 35091); //收到三日奖励广告获取结果
            typeToProtocolDic.Add(typeof(PbBonus.SC_Box_GetEquip), 35093); //收到单抽装备结果
            typeToProtocolDic.Add(typeof(PbBonus.SC_Box_GetEquipfive), 35095); //收到五连抽装备结果
            typeToProtocolDic.Add(typeof(PbBonus.SC_Circle_Get), 35097); //收到转盘奖励
            typeToProtocolDic.Add(typeof(PbBonus.SC_Circle_GetBox), 35099); //收到开启转盘宝箱
            typeToProtocolDic.Add(typeof(PbBonus.SC_FloatBox_award), 35101); //收到悬浮宝箱广告获取结果
            typeToProtocolDic.Add(typeof(PbBonus.SC_Achievement_List), 35103); //收到成就列表
            typeToProtocolDic.Add(typeof(PbBonus.SC_Achievement_GetAward), 35105); //收到领取成就奖励
            typeToProtocolDic.Add(typeof(PbBonus.SC_Achievement_finish), 35106); //收到成就达成信息
            typeToProtocolDic.Add(typeof(PbBonus.SC_Gem_GetOne), 35108); //收到单抽宝石
            typeToProtocolDic.Add(typeof(PbBonus.SC_Gem_GetFive), 35110); //收到五连抽宝石
            typeToProtocolDic.Add(typeof(PbBonus.SC_Gem_FreshChange), 35112); //收到刷新精粹兑换
            typeToProtocolDic.Add(typeof(PbBonus.SC_Gem_Change), 35114); //收到精粹兑换
            typeToProtocolDic.Add(typeof(PbPay.SC_vip_info), 36001); //收到VIP信息月卡信息充值信息(登录发)
            typeToProtocolDic.Add(typeof(PbPay.SC_vip_exp), 36002); //收到VIP经验发生改变
            typeToProtocolDic.Add(typeof(PbPay.SC_vip_buyGift), 36011); //收到领取VIP礼包结果
            typeToProtocolDic.Add(typeof(PbPay.SC_monthCard_get), 36021); //收到领取月卡奖励结果
            typeToProtocolDic.Add(typeof(PbPay.SC_pay_normal), 36031); //收到充值结果
            typeToProtocolDic.Add(typeof(PbPay.SC_pay_monthCard), 36033); //收到充值月卡结果
            typeToProtocolDic.Add(typeof(PbPay.SC_pay_getFirstPay), 36041); //收到领取首充奖励结果
            typeToProtocolDic.Add(typeof(PbPay.SC_pay_order), 36051); //收到充值定单信息
            typeToProtocolDic.Add(typeof(PbPay.SC_pay_succeed), 36053); //收到支付成功
            typeToProtocolDic.Add(typeof(PbPay.SC_pay_gift), 36060); //收到充值每日礼包结果
            typeToProtocolDic.Add(typeof(PbPay.SC_pay_fund), 36070); //收到购买基金结果
            typeToProtocolDic.Add(typeof(PbPay.SC_pay_heroicCard), 36080); //收到购买英勇卡结果
            typeToProtocolDic.Add(typeof(PbStore.SC_store_infos), 36901); //收到商城购买记录
            typeToProtocolDic.Add(typeof(PbStore.SC_store_buyItem), 36903); //收到购买商城物品
            typeToProtocolDic.Add(typeof(PbStore.SC_store_ADTimes), 36905); //收到商城广告获取物品剩余次数
            typeToProtocolDic.Add(typeof(PbStore.SC_store_DailyItems), 36906); //请求商城每日超级奖励内容
            typeToProtocolDic.Add(typeof(PbStore.SC_summon_buy), 36952); //收到召唤
            typeToProtocolDic.Add(typeof(PbStore.SC_store_exchange), 36954); //收到商城兑换金币
            typeToProtocolDic.Add(typeof(PbMail.SC_mail_list), 50001); //收到邮件列表
            typeToProtocolDic.Add(typeof(PbMail.SC_mail_one), 50002); //收到一个邮件
            typeToProtocolDic.Add(typeof(PbMail.SC_mail_detail), 50012); //收到邮件详细信息
            typeToProtocolDic.Add(typeof(PbMail.SC_mail_open), 50022); //收到打开邮件
            typeToProtocolDic.Add(typeof(PbMail.SC_mail_delete), 50024); //收到删除一个邮件
            typeToProtocolDic.Add(typeof(PbMail.SC_mail_getAward), 50026); //收到领取邮件附件奖励
            typeToProtocolDic.Add(typeof(PbTask.SC_taskLine_list), 51001); //收到任务列表信息
            typeToProtocolDic.Add(typeof(PbTask.SC_taskLine_get), 51004); //收到领取完奖励重发任务线信息
            typeToProtocolDic.Add(typeof(PbActivity.SC_Activity_Info), 52002); //收到活动列表信息(登录发)
            typeToProtocolDic.Add(typeof(PbActivity.SC_Activity_Change), 52003); //收到活动任务进度
            typeToProtocolDic.Add(typeof(PbActivity.SC_Activity_Get), 52005); //收到领取活动任务奖励
            typeToProtocolDic.Add(typeof(PbActivity.SC_Activiyt_Updata), 52006); //通知客户端更新活动
            typeToProtocolDic.Add(typeof(PbActivity.SC_Activiyt_packOpen), 52008); //收到开启活动礼包
            typeToProtocolDic.Add(typeof(PbActivity.SC_Activiyt_AllOpen), 52010); //收到一键领取节日奖励
            typeToProtocolDic.Add(typeof(PbActivity.SC_Activiyt_packFresh), 52012); //收到钻石刷新活动礼包
            typeToProtocolDic.Add(typeof(PbActivity.SC_Activiyt_OpenOne), 52014); //收到单个领取节日奖励
            typeToProtocolDic.Add(typeof(PbWar.SC_war_fb), 60102); //收到副本挑战
            typeToProtocolDic.Add(typeof(PbWar.SC_war_fbRebirth), 60104); //收到副本战斗重生
            typeToProtocolDic.Add(typeof(PbWar.SC_war_fbStageEnd), 60108); //收到章节关卡结束
            typeToProtocolDic.Add(typeof(PbWar.SC_war_fbInfo), 60109); //收到章节详情
            typeToProtocolDic.Add(typeof(PbWar.SC_war_fbUpdate), 60111); //收到普通章节挑战信息更新
            typeToProtocolDic.Add(typeof(PbWar.SC_war_GetBox), 60113); //收到领取章节宝箱
            typeToProtocolDic.Add(typeof(PbWar.SC_war_GetBoxUpdate), 60114); //收到章节宝箱可领取通知
            typeToProtocolDic.Add(typeof(PbWar.SC_war_fbknUpdate), 60115); //收到困难章节挑战信息更新
            typeToProtocolDic.Add(typeof(PbWar.SC_war_TraderBuy), 60117); //收到随机商人购买
            typeToProtocolDic.Add(typeof(PbWar.SC_war_ExitAcFb), 60120); //收到退出活动副本
            typeToProtocolDic.Add(typeof(PbWar.SC_war_worldBossFb), 60122); //收到进入世界Boss副本
            typeToProtocolDic.Add(typeof(PbWar.SC_war_worldBossFbInfo), 60123); //收到世界Boss副本信息
            typeToProtocolDic.Add(typeof(PbWar.SC_war_ExworldBossFb), 60125); //收到退出世界Boss副本
            typeToProtocolDic.Add(typeof(PbWar.SC_war_worldBossFbGet), 60127); //收到一键领取世界Boss副本奖励
            typeToProtocolDic.Add(typeof(PbWar.SC_war_TowerFbEnd), 60129); //收到爬塔副本关卡信息
            typeToProtocolDic.Add(typeof(PbWar.SC_war_InfinityFbInfo), 60130); //收到无尽副本信息
            typeToProtocolDic.Add(typeof(PbWar.SC_war_InfinityFbEnd), 60132); //收到无尽副本关卡结束
            typeToProtocolDic.Add(typeof(PbWar.SC_war_InfinityFbGet), 60134); //收到无尽副本进度奖励
            typeToProtocolDic.Add(typeof(PbWar.SC_war_InfinityFbStart), 60136); //收到无尽副本关卡进入
            typeToProtocolDic.Add(typeof(PbWar.SC_war_InfinityFbExit), 60138); //收到退出无尽副本
            typeToProtocolDic.Add(typeof(PbRank.SC_Rank_List), 8002); //收到排行榜数据
            typeToProtocolDic.Add(typeof(PbRank.SC_rank_lookPlayer), 8004); //收到查看其他玩家信息
            typeToProtocolDic.Add(typeof(PbWar.SC_TestEncrypt), 9991); //返回加密消息

            _encryptList.Add(9990);
            _encryptList.Add(9991);
        }

        /// <summary>
        /// 跟据发送消息类型获取消息协议号
        /// </summary>
        public ushort GetProtocolByType(Type type)
        {
            ushort protocol = 0;
            typeToProtocolDic.TryGetValue(type, out protocol);
            return protocol;
        }
        /// <summary>
        /// 跟据收到的请求协议号创建消息数据结构类
        /// </summary>
        public IMessage CreateMsgByProtocol(ushort protocl)
        {
            IMessage msg = null;
            switch (protocl)
            {
                case 1:msg = new PbSystem.CS_sys_heartbeat(); break;//请求心跳包30秒一次
                case 3004:msg = new PbPlayer.CS_save_guide(); break;//保存指引步骤
                case 3006:msg = new PbPlayer.CS_player_changeName(); break;//请求玩家改名
                case 3008:msg = new PbPlayer.CS_player_buyGold(); break;//请求购买金币
                case 3010:msg = new PbPlayer.CS_save_guideStep(); break;//保存指引步骤
                case 3011:msg = new PbPlayer.CS_player_changeIcon(); break;//请求玩家修改头像
                case 3017:msg = new PbPlayer.CS_player_buyPower(); break;//请求钻石补满体力
                case 3026:msg = new PbPlayer.CS_player_dowerLevelUp(); break;//请求天赋升级
                case 3029:msg = new PbPlayer.CS_player_AdDouble(); break;//请求领取玩家广告双倍奖励
                case 3030:msg = new PbPlayer.CS_player_FinishGuide(); break;//请求完成新手关卡
                case 3032:msg = new PbPlayer.CS_player_SeasonGet(); break;//请求领取赛季令牌奖励
                case 21000:msg = new PbLogin.CS_login_verify(); break;//请求登录验证
                case 21006:msg = new PbLogin.CS_login_bind(); break;//请求游客绑定
                case 31003:msg = new PbBag.CS_bag_useItem(); break;//请求使用物品
                case 31005:msg = new PbBag.CS_bag_sellProp(); break;//请求出售道具
                case 31006:msg = new PbBag.CS_bag_sellEquip(); break;//请求出售装备
                case 32004:msg = new PbHero.CS_hero_levelUp(); break;//请求英雄升级
                case 32006:msg = new PbHero.CS_hero_break(); break;//请求英雄突破
                case 32008:msg = new PbHero.CS_hero_Get(); break;//请求英雄获取
                case 32010:msg = new PbHero.CS_hero_Change(); break;//请求更换出战英雄
                case 34000:msg = new PbEquipUp.CS_equip_streng(); break;//请求装备升级
                case 34002:msg = new PbEquipUp.CS_equip_break(); break;//请求装备升阶
                case 34004:msg = new PbEquipUp.CS_equip_Change(); break;//请求更换装备
                case 34006:msg = new PbEquipUp.CS_equip_Resolve(); break;//请求分解装备
                case 34008:msg = new PbEquipUp.CS_equip_merge(); break;//请求装备融合
                case 34010:msg = new PbEquipUp.CS_gems_resolve(); break;//请求分解宝石
                case 34012:msg = new PbEquipUp.CS_gems_Inlay(); break;//请求镶嵌(卸下)宝石
                case 35001:msg = new PbBonus.CS_signIn_award(); break;//请求每日签到领奖
                case 35011:msg = new PbBonus.CS_onlineAward_get(); break;//请求领取在线奖励
                case 35021:msg = new PbBonus.CS_openFund_get(); break;//请求领取开服基金
                case 35031:msg = new PbBonus.CS_cdkey_get(); break;//请求兑换CDKey
                case 35051:msg = new PbBonus.CS_levelAward_get(); break;//请求领取等级奖励
                case 35061:msg = new PbBonus.CS_taskNewbie_get(); break;//请求新手活动任务奖励
                case 35065:msg = new PbBonus.CS_taskNewbie_box(); break;//请求新手活动宝箱进度奖励
                case 35070:msg = new PbBonus.CS_treasure_spin(); break;//请求宝藏抽奖
                case 35080:msg = new PbBonus.CS_ads_award(); break;//请求领取广告奖励
                case 35082:msg = new PbBonus.CS_hang_Open(); break;//请求打开挂机奖励页面
                case 35084:msg = new PbBonus.CS_hang_Get(); break;//请求领取挂机奖励
                case 35086:msg = new PbBonus.CS_Box_GetHero(); break;//请求单抽英雄
                case 35088:msg = new PbBonus.CS_Box_GetHerofive(); break;//请求五连抽英雄
                case 35090:msg = new PbBonus.CS_Threeads_award(); break;//请求三日奖励广告获取
                case 35092:msg = new PbBonus.CS_Box_GetEquip(); break;//请求单抽装备
                case 35094:msg = new PbBonus.CS_Box_GetEquipfive(); break;//请求五连抽装备
                case 35096:msg = new PbBonus.CS_Circle_Get(); break;//请求转盘奖励
                case 35098:msg = new PbBonus.CS_Circle_GetBox(); break;//请求开启转盘宝箱
                case 35100:msg = new PbBonus.CS_FloatBox_award(); break;//请求悬浮宝箱广告获取
                case 35102:msg = new PbBonus.CS_Achievement_List(); break;//请求成就列表
                case 35104:msg = new PbBonus.CS_Achievement_GetAward(); break;//请求领取成就奖励
                case 35107:msg = new PbBonus.CS_Gem_GetOne(); break;//请求单抽宝石
                case 35109:msg = new PbBonus.CS_Gem_GetFive(); break;//请求五连抽宝石
                case 35111:msg = new PbBonus.CS_Gem_FreshChange(); break;//请求刷新精粹兑换
                case 35113:msg = new PbBonus.CS_Gem_Change(); break;//请求精粹兑换
                case 36010:msg = new PbPay.CS_vip_buyGift(); break;//请求领取VIP礼包
                case 36020:msg = new PbPay.CS_monthCard_get(); break;//请求领取月卡奖励
                case 36040:msg = new PbPay.CS_pay_getFirstPay(); break;//请求领取首充奖励
                case 36050:msg = new PbPay.CS_pay_order(); break;//请求充值下定单
                case 36052:msg = new PbPay.CS_pay_succeed(); break;//请求支付成功
                case 36902:msg = new PbStore.CS_store_buyItem(); break;//请求购买商城物品
                case 36904:msg = new PbStore.CS_store_ADTimes(); break;//请求领取一次商城广告奖励
                case 36951:msg = new PbStore.CS_summon_buy(); break;//请求召唤
                case 36953:msg = new PbStore.CS_store_exchange(); break;//请求商城兑换金币
                case 50011:msg = new PbMail.CS_mail_detail(); break;//请求邮件详细信息
                case 50021:msg = new PbMail.CS_mail_open(); break;//请求打开邮件
                case 50023:msg = new PbMail.CS_mail_delete(); break;//请求删除一个邮件
                case 50025:msg = new PbMail.CS_mail_getAward(); break;//请求领取邮件附件奖励
                case 50027:msg = new PbMail.CS_mail_openAll(); break;//请求一键领取所有未读邮件
                case 51003:msg = new PbTask.CS_taskLine_get(); break;//请求领取任务线上的任务奖励
                case 51005:msg = new PbTask.CS_taskLine_Add(); break;//请求增加广告进度（加一次广告次数）
                case 52001:msg = new PbActivity.CS_Activity_Info(); break;//请求活动列表信息
                case 52004:msg = new PbActivity.CS_Activity_Get(); break;//请求领取活动任务奖励
                case 52007:msg = new PbActivity.CS_Activiyt_packOpen(); break;//请求开启活动礼包
                case 52009:msg = new PbActivity.CS_Activiyt_AllOpen(); break;//请求一键领取节日奖励
                case 52011:msg = new PbActivity.CS_Activiyt_packFresh(); break;//请求钻石刷新活动礼包
                case 52013:msg = new PbActivity.CS_Activiyt_OpenOne(); break;//请求单个领取节日奖励
                case 60101:msg = new PbWar.CS_war_fb(); break;//请求副本挑战
                case 60103:msg = new PbWar.CS_war_fbRebirth(); break;//请求副本战斗重生
                case 60107:msg = new PbWar.CS_war_fbStageEnd(); break;//请求章节关卡结束
                case 60110:msg = new PbWar.CS_war_fbExit(); break;//请求退出章节
                case 60112:msg = new PbWar.CS_war_GetBox(); break;//请求领取章节宝箱
                case 60116:msg = new PbWar.CS_war_TraderBuy(); break;//请求随机商人购买
                case 60118:msg = new PbWar.CS_war_RebornByAD(); break;//请求章节内看广告复活
                case 60119:msg = new PbWar.CS_war_ExitAcFb(); break;//请求退出活动副本
                case 60121:msg = new PbWar.CS_war_worldBossFb(); break;//请求进入世界Boss副本
                case 60124:msg = new PbWar.CS_war_ExworldBossFb(); break;//请求退出世界Boss副本
                case 60126:msg = new PbWar.CS_war_worldBossFbGet(); break;//请求一键领取世界Boss副本奖励
                case 60128:msg = new PbWar.CS_war_TowerFbEnd(); break;//请求爬塔副本关卡结束
                case 60131:msg = new PbWar.CS_war_InfinityFbEnd(); break;//请求无尽副本关卡结束
                case 60133:msg = new PbWar.CS_war_InfinityFbGet(); break;//请求无尽副本进度奖励
                case 60135:msg = new PbWar.CS_war_InfinityFbStart(); break;//请求无尽副本关卡进入
                case 60137:msg = new PbWar.CS_war_InfinityFbExit(); break;//请求退出无尽副本
                case 8001:msg = new PbRank.CS_Rank_List(); break;//请求排行榜数据
                case 8003:msg = new PbRank.CS_rank_lookPlayer(); break;//请求查看其他玩家信息
                case 9990:msg = new PbWar.CS_TestEncrypt(); break;//测试消息加密
            }
            return msg;
        }

        /// <summary>是否加密协议</summary>
        public bool IsEncryptProtocol(ushort protocl)
        {
            return _encryptList.Contains(protocl);
        }
    }
}
