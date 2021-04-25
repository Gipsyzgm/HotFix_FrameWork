/// <summary>
/// 工具生成，不要修改
/// </summary>
using CommonLib;
using System.Collections.Generic;

namespace CommonLib.Comm
{
    public partial class ConfigMgr
    {
        public static ConfigMgr I { get; protected set; }
        /// <summary> 物品配置</summary>
        public readonly Dictionary<object, ItemConfig> dicItem = new Dictionary<object, ItemConfig>();
        /// <summary> 道具礼包配置</summary>
        public readonly Dictionary<object, ItemPackConfig> dicItemPack = new Dictionary<object, ItemPackConfig>();
        /// <summary> 物品背景配置</summary>
        public readonly Dictionary<object, ItemBgConfig> dicItemBg = new Dictionary<object, ItemBgConfig>();
        /// <summary> 物品升星升级</summary>
        public readonly Dictionary<object, ItemStarLevelConfig> dicItemStarLevel = new Dictionary<object, ItemStarLevelConfig>();
        /// <summary> 副本章节</summary>
        public readonly Dictionary<object, FBChapterConfig> dicFBChapter = new Dictionary<object, FBChapterConfig>();
        /// <summary> 副本关卡</summary>
        public readonly Dictionary<object, FBLevelConfig> dicFBLevel = new Dictionary<object, FBLevelConfig>();
        /// <summary> 副本列表</summary>
        public readonly Dictionary<object, FBListConfig> dicFBList = new Dictionary<object, FBListConfig>();
        /// <summary> 副本荣耀奖励</summary>
        public readonly Dictionary<object, FBHornorAwardConfig> dicFBHornorAward = new Dictionary<object, FBHornorAwardConfig>();
        /// <summary> 副本等级技能</summary>
        public readonly Dictionary<object, FBLvSkillConfig> dicFBLvSkill = new Dictionary<object, FBLvSkillConfig>();
        /// <summary> 副本章节</summary>
        public readonly Dictionary<object, KNFBChapterConfig> dicKNFBChapter = new Dictionary<object, KNFBChapterConfig>();
        /// <summary> 副本关卡</summary>
        public readonly Dictionary<object, KNFBLevelConfig> dicKNFBLevel = new Dictionary<object, KNFBLevelConfig>();
        /// <summary> 副本列表</summary>
        public readonly Dictionary<object, KNFBListConfig> dicKNFBList = new Dictionary<object, KNFBListConfig>();
        /// <summary> 困难副本等级技能</summary>
        public readonly Dictionary<object, KNFBLvSkillConfig> dicKNFBLvSkill = new Dictionary<object, KNFBLvSkillConfig>();
        /// <summary> 天赋配置</summary>
        public readonly Dictionary<object, DowerConfig> dicDower = new Dictionary<object, DowerConfig>();
        /// <summary> 天赋等级</summary>
        public readonly Dictionary<object, DowerLevelConfig> dicDowerLevel = new Dictionary<object, DowerLevelConfig>();
        /// <summary> 玩家升级经验</summary>
        public readonly Dictionary<object, PlayerExpConfig> dicPlayerExp = new Dictionary<object, PlayerExpConfig>();
        /// <summary> 战斗升级经验</summary>
        public readonly Dictionary<object, WarExpConfig> dicWarExp = new Dictionary<object, WarExpConfig>();
        /// <summary> 每日任务</summary>
        public readonly Dictionary<object, TaskDayConfig> dicTaskDay = new Dictionary<object, TaskDayConfig>();
        /// <summary> 商城配置</summary>
        public readonly Dictionary<object, StoreConfig> dicStore = new Dictionary<object, StoreConfig>();
        /// <summary> 金券购买礼包</summary>
        public readonly Dictionary<object, GiftPackConfig> dicGiftPack = new Dictionary<object, GiftPackConfig>();
        /// <summary> 每日签到</summary>
        public readonly Dictionary<object, SignInConfig> dicSignIn = new Dictionary<object, SignInConfig>();
        /// <summary> 等级奖励</summary>
        public readonly Dictionary<object, LevelAwardConfig> dicLevelAward = new Dictionary<object, LevelAwardConfig>();

        /// <summary> 在线奖励</summary>
        public readonly Dictionary<object, OnlineAwardConfig> dicOnlineAward = new Dictionary<object, OnlineAwardConfig>();
        /// <summary> CDKey配置</summary>
        public readonly Dictionary<object, CDKeyConfig> dicCDKey = new Dictionary<object, CDKeyConfig>();
        /// <summary> 充值商品配置</summary>
        public readonly Dictionary<object, PayGoodsConfig> dicPayGoods = new Dictionary<object, PayGoodsConfig>();
        /// <summary> 充值配置</summary>
        public readonly Dictionary<object, PayConfig> dicPay = new Dictionary<object, PayConfig>();
        /// <summary> 充值月卡</summary>
        public readonly Dictionary<object, MonthCardConfig> dicMonthCard = new Dictionary<object, MonthCardConfig>();
        /// <summary> 基金</summary>
        public readonly Dictionary<object, FundConfig> dicFund = new Dictionary<object, FundConfig>();
        /// <summary> 礼包配置</summary>
        public readonly Dictionary<object, PayPackConfig> dicPayPack = new Dictionary<object, PayPackConfig>();
        /// <summary> VIP配置</summary>
        public readonly Dictionary<object, VipConfig> dicVip = new Dictionary<object, VipConfig>();
        /// <summary> 充值购买礼包</summary>
        public readonly Dictionary<object, PayGiftConfig> dicPayGift = new Dictionary<object, PayGiftConfig>();
        /// <summary> VIP礼包</summary>
        public readonly Dictionary<object, VipGiftConfig> dicVipGift = new Dictionary<object, VipGiftConfig>();
        /// <summary> VIP&充值相关_商品名</summary>
        public readonly Dictionary<object, LanguageConfig> dicLanguage = new Dictionary<object, LanguageConfig>();
        /// <summary> 新手指引</summary>
        public readonly Dictionary<object, GuideConfig> dicGuide = new Dictionary<object, GuideConfig>();
        /// <summary> 新手指引步骤</summary>
        public readonly Dictionary<object, GuideStepConfig> dicGuideStep = new Dictionary<object, GuideStepConfig>();

        /// <summary> 系统设置</summary>
        public readonly SettingConfig settingConfig;
        /// <summary> 天赋设置</summary>
        public readonly DowerSettingConfig dowerSettingConfig;
        /// <summary> 人物初始配置</summary>
        public readonly PlayerInitConfig playerInitConfig;
        /// <summary> 福利设置</summary>
        public readonly BonusSettingsConfig bonusSettingsConfig;

        public ConfigMgr()
        {
            I = this;
            Logger.Sys("开始读取所有配置表文件...");
            configInit();
            readConfig(dicItem);
            readConfig(dicItemPack);
            readConfig(dicItemBg);
            readConfig(dicItemStarLevel);
            readConfig(dicFBChapter);
            readConfig(dicFBLevel);
            readConfig(dicFBList);
            readConfig(dicFBHornorAward);
            readConfig(dicFBLvSkill);
            readConfig(dicKNFBChapter);
            readConfig(dicKNFBLevel);
            readConfig(dicKNFBList);
            readConfig(dicKNFBLvSkill);
            readConfig(dicDower);
            readConfig(dicDowerLevel);
            readConfig(dicPlayerExp);
            readConfig(dicWarExp);
            readConfig(dicTaskDay);
            readConfig(dicStore);
            readConfig(dicGiftPack);
            readConfig(dicSignIn);
            readConfig(dicLevelAward);

            readConfig(dicOnlineAward);
            readConfig(dicCDKey);
            readConfig(dicPayGoods);
            readConfig(dicPay);
            readConfig(dicMonthCard);
            readConfig(dicFund);
            readConfig(dicPayPack);
            readConfig(dicVip);
            readConfig(dicPayGift);
            readConfig(dicVipGift);
            readConfig(dicLanguage);
            readConfig(dicGuide);
            readConfig(dicGuideStep);

            //读取竖表配置
            readConfigV(ref settingConfig);
            readConfigV(ref dowerSettingConfig);
            readConfigV(ref playerInitConfig);
            readConfigV(ref bonusSettingsConfig);

            customRead();
            Logger.Sys("读取配置表文件完成");
        }

        protected virtual void reloadAll()
        {
            copyDictionary(dicItem, reloadConfig<ItemConfig>());
            copyDictionary(dicItemPack, reloadConfig<ItemPackConfig>());
            copyDictionary(dicItemBg, reloadConfig<ItemBgConfig>());
            copyDictionary(dicItemStarLevel, reloadConfig<ItemStarLevelConfig>());
            copyDictionary(dicFBChapter, reloadConfig<FBChapterConfig>());
            copyDictionary(dicFBLevel, reloadConfig<FBLevelConfig>());
            copyDictionary(dicFBList, reloadConfig<FBListConfig>());
            copyDictionary(dicFBHornorAward, reloadConfig<FBHornorAwardConfig>());
            copyDictionary(dicFBLvSkill, reloadConfig<FBLvSkillConfig>());
            copyDictionary(dicKNFBChapter, reloadConfig<KNFBChapterConfig>());
            copyDictionary(dicKNFBLevel, reloadConfig<KNFBLevelConfig>());
            copyDictionary(dicKNFBList, reloadConfig<KNFBListConfig>());
            copyDictionary(dicKNFBLvSkill, reloadConfig<KNFBLvSkillConfig>());
            copyDictionary(dicDower, reloadConfig<DowerConfig>());
            copyDictionary(dicDowerLevel, reloadConfig<DowerLevelConfig>());
            copyDictionary(dicPlayerExp, reloadConfig<PlayerExpConfig>());
            copyDictionary(dicWarExp, reloadConfig<WarExpConfig>());
            copyDictionary(dicTaskDay, reloadConfig<TaskDayConfig>());
            copyDictionary(dicStore, reloadConfig<StoreConfig>());
            copyDictionary(dicGiftPack, reloadConfig<GiftPackConfig>());
            copyDictionary(dicSignIn, reloadConfig<SignInConfig>());
            copyDictionary(dicLevelAward, reloadConfig<LevelAwardConfig>());

            copyDictionary(dicOnlineAward, reloadConfig<OnlineAwardConfig>());
            copyDictionary(dicCDKey, reloadConfig<CDKeyConfig>());
            copyDictionary(dicPayGoods, reloadConfig<PayGoodsConfig>());
            copyDictionary(dicPay, reloadConfig<PayConfig>());
            copyDictionary(dicMonthCard, reloadConfig<MonthCardConfig>());
            copyDictionary(dicFund, reloadConfig<FundConfig>());
            copyDictionary(dicPayPack, reloadConfig<PayPackConfig>());
            copyDictionary(dicVip, reloadConfig<VipConfig>());
            copyDictionary(dicPayGift, reloadConfig<PayGiftConfig>());
            copyDictionary(dicVipGift, reloadConfig<VipGiftConfig>());
            copyDictionary(dicLanguage, reloadConfig<LanguageConfig>());
            copyDictionary(dicGuide, reloadConfig<GuideConfig>());
            copyDictionary(dicGuideStep, reloadConfig<GuideStepConfig>());

            copyClassValue(settingConfig, reloadConfigV<SettingConfig>());
            copyClassValue(dowerSettingConfig, reloadConfigV<DowerSettingConfig>());
            copyClassValue(playerInitConfig, reloadConfigV<PlayerInitConfig>());
            copyClassValue(bonusSettingsConfig, reloadConfigV<BonusSettingsConfig>());

        }
    }
}
