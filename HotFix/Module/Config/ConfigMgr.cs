/// <summary>
/// 工具生成，不要修改
/// </summary>
using System.Collections.Generic;
namespace HotFix
{
    public partial class ConfigMgr
    {
        private bool IsCSV = false;
        /// <summary> 副本章节</summary>
        public readonly Dictionary<object, FBChapterConfig> FBChapter = new Dictionary<object, FBChapterConfig>();
        /// <summary> 副本关卡配置</summary>
        public readonly Dictionary<object, FBLevelConfig> FBLevel = new Dictionary<object, FBLevelConfig>();
        /// <summary> 关卡阶段</summary>
        public readonly Dictionary<object, FBLevelStageConfig> FBLevelStage = new Dictionary<object, FBLevelStageConfig>();
        /// <summary> 关卡名称</summary>
        public readonly Dictionary<object, LanguageConfig> Language = new Dictionary<object, LanguageConfig>();
        /// <summary> 怪物设置</summary>
        public readonly Dictionary<object, MonsterConfig> Monster = new Dictionary<object, MonsterConfig>();
        /// <summary> 战斗符号</summary>
        public readonly Dictionary<object, WarSymbolConfig> WarSymbol = new Dictionary<object, WarSymbolConfig>();
        /// <summary> 战斗箱子产出配置</summary>
        public readonly Dictionary<object, WarBoxConfig> WarBox = new Dictionary<object, WarBoxConfig>();
        /// <summary> 特殊关卡配置</summary>
        public readonly Dictionary<object, WarSpecialConfig> WarSpecial = new Dictionary<object, WarSpecialConfig>();
        /// <summary> 特殊元素配置</summary>
        public readonly Dictionary<object, WarSpecialSymbolConfig> WarSpecialSymbol = new Dictionary<object, WarSpecialSymbolConfig>();
        /// <summary> 技能配置</summary>
        public readonly Dictionary<object, SkillConfig> Skill = new Dictionary<object, SkillConfig>();
        /// <summary> 技能Buff</summary>
        public readonly Dictionary<object, SkillBuffConfig> SkillBuff = new Dictionary<object, SkillBuffConfig>();
        /// <summary> 技能附加状态</summary>
        public readonly Dictionary<object, SkillStateConfig> SkillState = new Dictionary<object, SkillStateConfig>();
        /// <summary> 英雄配置</summary>
        public readonly Dictionary<object, HeroConfig> Hero = new Dictionary<object, HeroConfig>();
        /// <summary> 英雄星级</summary>
        public readonly Dictionary<object, HeroStarConfig> HeroStar = new Dictionary<object, HeroStarConfig>();
        /// <summary> 英雄进阶消耗</summary>
        public readonly Dictionary<object, HeroBreakConfig> HeroBreak = new Dictionary<object, HeroBreakConfig>();
        /// <summary> 英雄升级</summary>
        public readonly Dictionary<object, HeroExpConfig> HeroExp = new Dictionary<object, HeroExpConfig>();
        /// <summary> 充值商品配置</summary>
        public readonly Dictionary<object, PayGoodsConfig> PayGoods = new Dictionary<object, PayGoodsConfig>();
        /// <summary> 充值配置</summary>
        public readonly Dictionary<object, PayConfig> Pay = new Dictionary<object, PayConfig>();
        /// <summary> 充值月卡</summary>
        public readonly Dictionary<object, MonthCardConfig> MonthCard = new Dictionary<object, MonthCardConfig>();
        /// <summary> 基金</summary>
        public readonly Dictionary<object, FundConfig> Fund = new Dictionary<object, FundConfig>();
        /// <summary> 英勇卡</summary>
        public readonly Dictionary<object, HeroicCardConfig> HeroicCard = new Dictionary<object, HeroicCardConfig>();
        /// <summary> 礼包配置</summary>
        public readonly Dictionary<object, PayPackConfig> PayPack = new Dictionary<object, PayPackConfig>();
        /// <summary> VIP配置</summary>
        public readonly Dictionary<object, VipConfig> Vip = new Dictionary<object, VipConfig>();
        /// <summary> 充值购买礼包</summary>
        public readonly Dictionary<object, PayGiftConfig> PayGift = new Dictionary<object, PayGiftConfig>();
        /// <summary> VIP礼包</summary>
        public readonly Dictionary<object, VipGiftConfig> VipGift = new Dictionary<object, VipGiftConfig>();
        /// <summary> 任务</summary>
        public readonly Dictionary<object, TaskConfig> Task = new Dictionary<object, TaskConfig>();
        /// <summary> 赏金任务</summary>
        public readonly Dictionary<object, TaskBountyConfig> TaskBounty = new Dictionary<object, TaskBountyConfig>();
        /// <summary> 召唤配置</summary>
        public readonly Dictionary<object, SummonConfig> Summon = new Dictionary<object, SummonConfig>();
        /// <summary> 商城配置</summary>
        public readonly Dictionary<object, StoreConfig> Store = new Dictionary<object, StoreConfig>();
        /// <summary> 商城礼包</summary>
        public readonly Dictionary<object, StorePackConfig> StorePack = new Dictionary<object, StorePackConfig>();
        /// <summary> 头像商店</summary>
        public readonly Dictionary<object, StoreIconConfig> StoreIcon = new Dictionary<object, StoreIconConfig>();
        /// <summary> 资源商店</summary>
        public readonly Dictionary<object, StoreResConfig> StoreRes = new Dictionary<object, StoreResConfig>();
        /// <summary> 地图配色</summary>
        public readonly Dictionary<object, MapColorConfig> MapColor = new Dictionary<object, MapColorConfig>();
        /// <summary> 天赋树</summary>
        public readonly Dictionary<object, DowerTreeConfig> DowerTree = new Dictionary<object, DowerTreeConfig>();
        /// <summary> 天赋配置</summary>
        public readonly Dictionary<object, DowerConfig> Dower = new Dictionary<object, DowerConfig>();
        /// <summary> 天赋Buff</summary>
        public readonly Dictionary<object, DowerBuffConfig> DowerBuff = new Dictionary<object, DowerBuffConfig>();
        /// <summary> 奖励配置</summary>
        public readonly Dictionary<object, AwardConfig> Award = new Dictionary<object, AwardConfig>();
        /// <summary> 平台配置</summary>
        public readonly Dictionary<object, PlatformConfig> Platform = new Dictionary<object, PlatformConfig>();
        /// <summary> 工坊产出</summary>
        public readonly Dictionary<object, BForgeOutputConfig> BForgeOutput = new Dictionary<object, BForgeOutputConfig>();
        /// <summary> 训练场产出</summary>
        public readonly Dictionary<object, BTrainingOutputConfig> BTrainingOutput = new Dictionary<object, BTrainingOutputConfig>();
        /// <summary> 铁匠铺产出</summary>
        public readonly Dictionary<object, BSmithyOutputConfig> BSmithyOutput = new Dictionary<object, BSmithyOutputConfig>();
        /// <summary> 建筑配置</summary>
        public readonly Dictionary<object, BuildConfig> Build = new Dictionary<object, BuildConfig>();
        /// <summary> 建筑升级配置</summary>
        public readonly Dictionary<object, BuildLvConfig> BuildLv = new Dictionary<object, BuildLvConfig>();
        /// <summary> 主城堡配置</summary>
        public readonly Dictionary<object, BStrongHoldConfig> BStrongHold = new Dictionary<object, BStrongHoldConfig>();
        /// <summary> 建筑区域划分</summary>
        public readonly Dictionary<object, BuildAreaConfig> BuildArea = new Dictionary<object, BuildAreaConfig>();
        /// <summary> 主线任务</summary>
        public readonly Dictionary<object, TaskChapConfig> TaskChap = new Dictionary<object, TaskChapConfig>();
        /// <summary> 主线任务列表</summary>
        public readonly Dictionary<object, TaskChapListConfig> TaskChapList = new Dictionary<object, TaskChapListConfig>();
        /// <summary> 新手指引</summary>
        public readonly Dictionary<object, GuideConfig> Guide = new Dictionary<object, GuideConfig>();
        /// <summary> 新手指引步骤</summary>
        public readonly Dictionary<object, GuideStepConfig> GuideStep = new Dictionary<object, GuideStepConfig>();
        /// <summary> 剧情</summary>
        public readonly Dictionary<object, StoryConfig> Story = new Dictionary<object, StoryConfig>();
        /// <summary> 指引战斗关</summary>
        public readonly Dictionary<object, GuideLevelConfig> GuideLevel = new Dictionary<object, GuideLevelConfig>();
        /// <summary> 功能开放</summary>
        public readonly Dictionary<object, FunOpenConfig> FunOpen = new Dictionary<object, FunOpenConfig>();
        /// <summary> 活动副本</summary>
        public readonly Dictionary<object, EventFBConfig> EventFB = new Dictionary<object, EventFBConfig>();
        /// <summary> 活动关卡</summary>
        public readonly Dictionary<object, EventFBLevelConfig> EventFBLevel = new Dictionary<object, EventFBLevelConfig>();
        /// <summary> 活动关卡战斗</summary>
        public readonly Dictionary<object, EventFBWarConfig> EventFBWar = new Dictionary<object, EventFBWarConfig>();
        /// <summary> 挑战活动排名奖励</summary>
        public readonly Dictionary<object, EventFBAwardConfig> EventFBAward = new Dictionary<object, EventFBAwardConfig>();
        /// <summary> 活动对话</summary>
        public readonly Dictionary<object, EventFBDialogConfig> EventFBDialog = new Dictionary<object, EventFBDialogConfig>();
        /// <summary> 活动</summary>
        public readonly Dictionary<object, ActivityConfig> Activity = new Dictionary<object, ActivityConfig>();
        /// <summary> 物品配置</summary>
        public readonly Dictionary<object, ItemConfig> Item = new Dictionary<object, ItemConfig>();
        /// <summary> 战斗物品配置</summary>
        public readonly Dictionary<object, ItemWarConfig> ItemWar = new Dictionary<object, ItemWarConfig>();
        /// <summary> 战斗物品Buff</summary>
        public readonly Dictionary<object, ItemWarBuffConfig> ItemWarBuff = new Dictionary<object, ItemWarBuffConfig>();
        /// <summary> 装备配置</summary>
        public readonly Dictionary<object, ItemEquipConfig> ItemEquip = new Dictionary<object, ItemEquipConfig>();
        /// <summary> 装备升级</summary>
        public readonly Dictionary<object, ItemEquipExpConfig> ItemEquipExp = new Dictionary<object, ItemEquipExpConfig>();
        /// <summary> 道具礼包配置</summary>
        public readonly Dictionary<object, ItemPackConfig> ItemPack = new Dictionary<object, ItemPackConfig>();
        /// <summary> 常规特效</summary>
        public readonly Dictionary<object, EffectConfig> Effect = new Dictionary<object, EffectConfig>();
        /// <summary> 玩家升级经验</summary>
        public readonly Dictionary<object, PlayerExpConfig> PlayerExp = new Dictionary<object, PlayerExpConfig>();
        /// <summary> 每日签到</summary>
        public readonly Dictionary<object, SignInConfig> SignIn = new Dictionary<object, SignInConfig>();
        /// <summary> 在线奖励</summary>
        public readonly Dictionary<object, OnlineAwardConfig> OnlineAward = new Dictionary<object, OnlineAwardConfig>();
        /// <summary> 开服基金</summary>
        public readonly Dictionary<object, OpenFundConfig> OpenFund = new Dictionary<object, OpenFundConfig>();
        /// <summary> 七天登录奖励</summary>
        public readonly Dictionary<object, SevenAwardConfig> SevenAward = new Dictionary<object, SevenAwardConfig>();
        /// <summary> 新手任务</summary>
        public readonly Dictionary<object, TaskNewbieConfig> TaskNewbie = new Dictionary<object, TaskNewbieConfig>();
        /// <summary> 新手任务宝箱</summary>
        public readonly Dictionary<object, NewbieAwardConfig> NewbieAward = new Dictionary<object, NewbieAwardConfig>();
        /// <summary> 等级奖励</summary>
        public readonly Dictionary<object, LevelAwardConfig> LevelAward = new Dictionary<object, LevelAwardConfig>();
        /// <summary> 宝藏</summary>
        public readonly Dictionary<object, TreasureConfig> Treasure = new Dictionary<object, TreasureConfig>();
        /// <summary> 广告奖励</summary>
        public readonly Dictionary<object, AdsAwardConfig> AdsAward = new Dictionary<object, AdsAwardConfig>();
        /// <summary> 竞技场战斗关</summary>
        public readonly Dictionary<object, ArenaLevelConfig> ArenaLevel = new Dictionary<object, ArenaLevelConfig>();
        /// <summary> 系统分享</summary>
        public readonly Dictionary<object, SysShareConfig> SysShare = new Dictionary<object, SysShareConfig>();
        /// <summary> 老虎机种类</summary>
        public readonly Dictionary<object, SlotZoneTypeConfig> SlotZoneType = new Dictionary<object, SlotZoneTypeConfig>();
        /// <summary> 元素表</summary>
        public readonly Dictionary<object, SlotZoneElementConfig> SlotZoneElement = new Dictionary<object, SlotZoneElementConfig>();
        /// <summary> 规则总表</summary>
        public readonly Dictionary<object, RuleConfig> Rule = new Dictionary<object, RuleConfig>();
        /// <summary> 规则集合配置</summary>
        public readonly Dictionary<object, RuleCollectionConfig> RuleCollection = new Dictionary<object, RuleCollectionConfig>();
        /// <summary> 3x5老虎机赔率表</summary>
        public readonly Dictionary<object, SlotZoneRateConfig> SlotZoneRate = new Dictionary<object, SlotZoneRateConfig>();
        /// <summary> 聊天房间</summary>
        public readonly Dictionary<object, ChatRoomConfig> ChatRoom = new Dictionary<object, ChatRoomConfig>();
        /// <summary> 联盟战</summary>
        public readonly Dictionary<object, ClubWarConfig> ClubWar = new Dictionary<object, ClubWarConfig>();
        /// <summary> 联盟战Npc联盟</summary>
        public readonly Dictionary<object, ClubWarNpcConfig> ClubWarNpc = new Dictionary<object, ClubWarNpcConfig>();
        /// <summary> 联盟战Npc成员</summary>
        public readonly Dictionary<object, ClubWarNpcMemberConfig> ClubWarNpcMember = new Dictionary<object, ClubWarNpcMemberConfig>();
        /// <summary> 联盟战奖励</summary>
        public readonly Dictionary<object, ClubWarAwardConfig> ClubWarAward = new Dictionary<object, ClubWarAwardConfig>();
        /// <summary> 联盟战宝箱奖励</summary>
        public readonly Dictionary<object, ClubChestAwardConfig> ClubChestAward = new Dictionary<object, ClubChestAwardConfig>();
        /// <summary> 俱乐部等级</summary>
        public readonly Dictionary<object, ClubExpConfig> ClubExp = new Dictionary<object, ClubExpConfig>();
        /// <summary> 联盟泰坦</summary>
        public readonly Dictionary<object, TitanConfig> Titan = new Dictionary<object, TitanConfig>();
        /// <summary> 泰坦战斗关</summary>
        public readonly Dictionary<object, TitanLevelConfig> TitanLevel = new Dictionary<object, TitanLevelConfig>();
        /// <summary> 泰坦奖励</summary>
        public readonly Dictionary<object, TitanAwardConfig> TitanAward = new Dictionary<object, TitanAwardConfig>();
        /// <summary> 联赛战斗</summary>
        public readonly Dictionary<object, LeagueSceneConfig> LeagueScene = new Dictionary<object, LeagueSceneConfig>();
        /// <summary> 联赛奖励</summary>
        public readonly Dictionary<object, LeagueAwardConfig> LeagueAward = new Dictionary<object, LeagueAwardConfig>();
        /// <summary> 英勇任务</summary>
        public readonly Dictionary<object, TaskHeroicConfig> TaskHeroic = new Dictionary<object, TaskHeroicConfig>();
        /// <summary> 英勇奖励</summary>
        public readonly Dictionary<object, HeroicAwardConfig> HeroicAward = new Dictionary<object, HeroicAwardConfig>();
        /// <summary> 随机名字（男）</summary>
        public readonly Dictionary<object, RandomNameConfig> RandomName = new Dictionary<object, RandomNameConfig>();

        /// <summary> 战斗设置</summary>
        public WarSettingConfig WarSetting;
        /// <summary> 英雄设置</summary>
        public HeroSettingConfig HeroSetting;
        /// <summary> BI打点</summary>
        public BITrackConfig BITrack;
        /// <summary> ADjust打点</summary>
        public ADjustTrackConfig ADjustTrack;
        /// <summary> 召唤设置</summary>
        public SummonSettingConfig SummonSetting;
        /// <summary> 天赋设置</summary>
        public DowerSettingConfig DowerSetting;
        /// <summary> 建筑设置</summary>
        public BuildSettingConfig BuildSetting;
        /// <summary> 新手指引设置</summary>
        public GuideSettingConfig GuideSetting;
        /// <summary> 物品设置</summary>
        public ItemSettingConfig ItemSetting;
        /// <summary> 福利设置</summary>
        public BonusSettingsConfig BonusSettings;
        /// <summary> 系统设置</summary>
        public SettingConfig Setting;
        /// <summary> 联盟战设置</summary>
        public ClubWarSettingConfig ClubWarSetting;
        /// <summary> 俱乐部设置</summary>
        public ClubSettingConfig ClubSetting;
        /// <summary> 联赛设置</summary>
        public LeagueSettingConfig LeagueSetting;
        /// <summary> 英勇之路设置</summary>
        public HeroicSettingsConfig HeroicSettings;

        public async CTask Initialize()
        {
            readConfig(FBChapter,false).Run();
            readConfig(FBLevel,false).Run();
            readConfig(FBLevelStage,false).Run();
            readConfig(Language,false).Run();
            readConfig(Monster,false).Run();
            readConfig(WarSymbol,false).Run();
            readConfig(WarBox,false).Run();
            readConfig(WarSpecial,false).Run();
            readConfig(WarSpecialSymbol,false).Run();
            readConfig(Skill,false).Run();
            readConfig(SkillBuff,false).Run();
            readConfig(SkillState,false).Run();
            readConfig(Hero,true).Run();
            readConfig(HeroStar,false).Run();
            readConfig(HeroBreak,false).Run();
            readConfig(HeroExp,false).Run();
            readConfig(PayGoods,false).Run();
            readConfig(Pay,false).Run();
            readConfig(MonthCard,false).Run();
            readConfig(Fund,false).Run();
            readConfig(HeroicCard,false).Run();
            readConfig(PayPack,false).Run();
            readConfig(Vip,false).Run();
            readConfig(PayGift,false).Run();
            readConfig(VipGift,false).Run();
            readConfig(Task,false).Run();
            readConfig(TaskBounty,false).Run();
            readConfig(Summon,false).Run();
            readConfig(Store,false).Run();
            readConfig(StorePack,false).Run();
            readConfig(StoreIcon,false).Run();
            readConfig(StoreRes,false).Run();
            readConfig(MapColor,false).Run();
            readConfig(DowerTree,false).Run();
            readConfig(Dower,false).Run();
            readConfig(DowerBuff,false).Run();
            readConfig(Award,false).Run();
            readConfig(Platform,false).Run();
            readConfig(BForgeOutput,false).Run();
            readConfig(BTrainingOutput,false).Run();
            readConfig(BSmithyOutput,false).Run();
            readConfig(Build,false).Run();
            readConfig(BuildLv,false).Run();
            readConfig(BStrongHold,false).Run();
            readConfig(BuildArea,false).Run();
            readConfig(TaskChap,false).Run();
            readConfig(TaskChapList,false).Run();
            readConfig(Guide,false).Run();
            readConfig(GuideStep,false).Run();
            readConfig(Story,false).Run();
            readConfig(GuideLevel,false).Run();
            readConfig(FunOpen,false).Run();
            readConfig(EventFB,false).Run();
            readConfig(EventFBLevel,false).Run();
            readConfig(EventFBWar,false).Run();
            readConfig(EventFBAward,false).Run();
            readConfig(EventFBDialog,false).Run();
            readConfig(Activity,false).Run();
            readConfig(Item,false).Run();
            readConfig(ItemWar,false).Run();
            readConfig(ItemWarBuff,false).Run();
            readConfig(ItemEquip,false).Run();
            readConfig(ItemEquipExp,false).Run();
            readConfig(ItemPack,false).Run();
            readConfig(Effect,false).Run();
            readConfig(PlayerExp,false).Run();
            readConfig(SignIn,false).Run();
            readConfig(OnlineAward,false).Run();
            readConfig(OpenFund,false).Run();
            readConfig(SevenAward,false).Run();
            readConfig(TaskNewbie,false).Run();
            readConfig(NewbieAward,false).Run();
            readConfig(LevelAward,false).Run();
            readConfig(Treasure,false).Run();
            readConfig(AdsAward,false).Run();
            readConfig(ArenaLevel,false).Run();
            readConfig(SysShare,false).Run();
            readConfig(SlotZoneType,false).Run();
            readConfig(SlotZoneElement,false).Run();
            readConfig(Rule,false).Run();
            readConfig(RuleCollection,false).Run();
            readConfig(SlotZoneRate,false).Run();
            readConfig(ChatRoom,false).Run();
            readConfig(ClubWar,false).Run();
            readConfig(ClubWarNpc,false).Run();
            readConfig(ClubWarNpcMember,false).Run();
            readConfig(ClubWarAward,false).Run();
            readConfig(ClubChestAward,false).Run();
            readConfig(ClubExp,false).Run();
            readConfig(Titan,false).Run();
            readConfig(TitanLevel,false).Run();
            readConfig(TitanAward,false).Run();
            readConfig(LeagueScene,false).Run();
            readConfig(LeagueAward,false).Run();
            readConfig(TaskHeroic,false).Run();
            readConfig(HeroicAward,false).Run();
            readConfig(RandomName,false).Run();

            //读取竖表配置
            WarSetting = await readConfigV<WarSettingConfig>(false);
            HeroSetting = await readConfigV<HeroSettingConfig>(false);
            BITrack = await readConfigV<BITrackConfig>(false);
            ADjustTrack = await readConfigV<ADjustTrackConfig>(false);
            SummonSetting = await readConfigV<SummonSettingConfig>(false);
            DowerSetting = await readConfigV<DowerSettingConfig>(false);
            BuildSetting = await readConfigV<BuildSettingConfig>(false);
            GuideSetting = await readConfigV<GuideSettingConfig>(false);
            ItemSetting = await readConfigV<ItemSettingConfig>(false);
            BonusSettings = await readConfigV<BonusSettingsConfig>(false);
            Setting = await readConfigV<SettingConfig>(false);
            ClubWarSetting = await readConfigV<ClubWarSettingConfig>(false);
            ClubSetting = await readConfigV<ClubSettingConfig>(false);
            LeagueSetting = await readConfigV<LeagueSettingConfig>(false);
            HeroicSettings = await readConfigV<HeroicSettingsConfig>(false);

            //等待全部加载完再执行自定义解析
            await waitLoadComplate();            
            customRead();
        }
    }
}
