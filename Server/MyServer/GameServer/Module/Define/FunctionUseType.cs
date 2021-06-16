using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 功能使用统计类型
    /// </summary>
    public enum FunctionUseType
    {
        Normal = 0,
        /// <summary>副本挑战次数</summary>
        FBWar = 1,
        /// <summary>副本扫荡次数</summary>
        FBQuickWar,
        /// <summary>探索挑战次数</summary>
        EventFBWar,
        /// <summary>角斗场挑战次数</summary>
        ArenaWar,
        /// <summary>角斗场刷新对手次数</summary>
        ArenaReMatch,
        /// <summary>泰坦挑战次数</summary>
        TitanWar,
        /// <summary>联盟战争挑战次数</summary>
        ClubWar,
        /// <summary>联赛次数</summary>
        LeagueWar,

        /// <summary>英雄升阶（1-2）</summary>
        HeroStarLvUp1To2 = 11,
        /// <summary>英雄升阶（2-3）</summary>
        HeroStarLvUp2To3,
        /// <summary>英雄升阶（3-4）</summary>
        HeroStarLvUp3To4,
        ///// <summary>英雄升阶（4-5）</summary>
        HeroStarLvUp4To5,

        /// <summary>主城升级次数</summary>
        CityLevelUp = 21,
        /// <summary>农场升级次数</summary>
        FramLevelUp,
        /// <summary>矿场升级次数</summary>
        MineLevelUp,
        /// <summary>房屋升级次数</summary>
        HouseLevelUp,
        /// <summary>仓库升级次数</summary>
        StorageLevelUp,
        /// <summary>角斗场升级次数</summary>
        TowerLevel,
        /// <summary>工坊升级次数</summary>
        ForgeLevelUp,
        /// <summary>训练营升级次数</summary>
        TrainLevelUp,
        /// <summary>铁匠铺升级次数</summary>
        SmithyLevelUp,

        
        /// <summary>怪物赏金任务完成次数</summary>
        MonsterTaskBounty = 31,
        /// <summary>英雄赏金任务完成次数</summary>
        HeroTaskBounty,
        /// <summary>泰坦赏金任务完成次数</summary>
        TitanTaskBounty,


        /// <summary>工坊制造，只统计领取的</summary>
        ForgeWork = 37,
        /// <summary>训练营训练次数，只统计领取的</summary>
        TrainWork,
        /// <summary>兵营制造，只统计领取的</summary>
        SmithyWork,


        /// <summary>升级部队</summary>
        EquipLevelUp = 40,
        
        /// <summary>元素召唤单抽</summary>
        ElementSummonOne,
        /// <summary>元素召唤十连抽</summary>
        ElementSummonTen,
        /// <summary>传奇英雄召唤单抽</summary>
        HeroSummonOne,
        /// <summary>传奇英雄召唤十连抽</summary>
        HeroSummonTen,
        /// <summary>传奇部队召唤单抽</summary>
        EquipSummonOne,
        /// <summary>传奇部队召唤十连抽</summary>
        EquipSummonTen,
        /// <summary>每日召唤单抽</summary>
        DailySummonOne,
        /// <summary>每日召唤十连抽</summary>
        DailySummonTen,

        /// <summary>天赋重置次数</summary>
        //DowerReset,
        /// <summary>玩家改名</summary>
        ReName,
        /// <summary>英雄升级</summary>
        HeroLevelUp,
        /// <summary>天赋升级次数</summary>
        DowerLevelUp,
        /// <summary>天赋激活次数</summary>
        //DowerActive,

        /// <summary>领取7天奖励第一天</summary>
        SevenAward1 = 61,
        /// <summary>领取7天奖励第二天</summary>
        SevenAward2,
        /// <summary>领取7天奖励第三天</summary>
        SevenAward3,
        /// <summary>领取7天奖励第四天</summary>
        SevenAward4,
        /// <summary>领取7天奖励第五天</summary>
        SevenAward5,
        /// <summary>领取7天奖励第六天</summary>
        SevenAward6,
        /// <summary>领取7天奖励第七天</summary>
        SevenAward7,

        /// <summary>挑战召唤钻石单抽</summary>
        ChallengeSummonOne = 71,
        /// <summary>挑战召唤钻石十连抽</summary>
        ChallengeSummonTen,
        /// <summary>节日召唤钻石单抽</summary>
        FestivalSummonOne,
        /// <summary>节日召唤钻石十连抽</summary>
        FestivalSummonTen,
    }
}
