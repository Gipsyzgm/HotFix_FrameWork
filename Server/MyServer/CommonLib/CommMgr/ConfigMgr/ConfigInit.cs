/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    public partial class ConfigMgr
    {
        protected virtual void configInit()
        {            
            loadConfig<ItemConfig>(); //物品配置
            loadConfig<ItemPackConfig>(); //道具礼包配置
            loadConfig<ItemBgConfig>(); //物品背景配置
            loadConfig<SettingConfig>(); //系统设置
            loadConfig<ItemStarLevelConfig>(); //物品升星升级
            loadConfig<FBChapterConfig>(); //副本章节
            loadConfig<FBLevelConfig>(); //副本关卡
            loadConfig<FBListConfig>(); //副本列表
            loadConfig<FBHornorAwardConfig>(); //副本荣耀奖励
            loadConfig<FBLvSkillConfig>(); //副本等级技能
            loadConfig<KNFBChapterConfig>(); //副本章节
            loadConfig<KNFBLevelConfig>(); //副本关卡
            loadConfig<KNFBListConfig>(); //副本列表
            loadConfig<KNFBLvSkillConfig>(); //困难副本等级技能
            loadConfig<DowerConfig>(); //天赋配置
            loadConfig<DowerLevelConfig>(); //天赋等级
            loadConfig<DowerSettingConfig>(); //天赋设置
            loadConfig<PlayerExpConfig>(); //玩家升级经验
            loadConfig<WarExpConfig>(); //战斗升级经验
            loadConfig<PlayerInitConfig>(); //人物初始配置
            loadConfig<TaskDayConfig>(); //每日任务
            loadConfig<StoreConfig>(); //商城配置
            loadConfig<GiftPackConfig>(); //金券购买礼包
            loadConfig<SignInConfig>(); //每日签到
            loadConfig<LevelAwardConfig>(); //等级奖励
            loadConfig<BonusSettingsConfig>(); //福利设置

            loadConfig<OnlineAwardConfig>(); //在线奖励
            loadConfig<CDKeyConfig>(); //CDKey配置
            loadConfig<PayGoodsConfig>(); //充值商品配置
            loadConfig<PayConfig>(); //充值配置
            loadConfig<MonthCardConfig>(); //充值月卡
            loadConfig<FundConfig>(); //基金
            loadConfig<PayPackConfig>(); //礼包配置
            loadConfig<VipConfig>(); //VIP配置
            loadConfig<PayGiftConfig>(); //充值购买礼包
            loadConfig<VipGiftConfig>(); //VIP礼包
            loadConfig<LanguageConfig>(); //VIP&充值相关_商品名
            loadConfig<GuideConfig>(); //新手指引
            loadConfig<GuideStepConfig>(); //新手指引步骤
        }
    }
}
