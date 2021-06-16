using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 钻石消费统计类型
    /// </summary>
    public enum TicketCostType
    {
        Normal = 0,
        /// <summary>主城建造加速</summary>
        CityLevelUpSkipWait = 1,
        /// <summary>农场建造加速</summary>
        FramLevelUpSkipWait,
        /// <summary>矿场建造加速</summary>
        MineLevelUpSkipWait,
        /// <summary>房屋建造加速</summary>
        HouseLevelUpSkipWait,
        /// <summary>仓库建造加速</summary>
        StorageLevelUpSkipWait,
        /// <summary>角斗场建造加速</summary>
        TowerLevelSkipWait,
        /// <summary>工坊建造加速</summary>
        ForgeLevelUpSkipWait,
        /// <summary>训练场建造加速</summary>
        TrainLevelUpSkipWait,
        /// <summary>铁匠铺建造加速</summary>
        SmithyLevelUpSkipWait,
        /// <summary>商城购买</summary>
        StoreBuy,


        /// <summary>怪物赏金任务加速</summary>
        MonsterTaskBountyClearCD = 11,
        /// <summary>英雄赏金任务加速</summary>
        HeroTaskBountyClearCD,
        /// <summary>泰坦赏金任务加速</summary>
        TitanTaskBountyClearCD,


        /// <summary>工坊生产加速</summary>
        ForgeWorkQuickly = 17,
        /// <summary>训练场生产加速</summary>
        TrainWorkQuickly,
        /// <summary>铁匠铺生产加速</summary>
        SmithyWorkQuickly,
        


        /// <summary>补齐资源</summary>
        TicketFillRes = 20,
        
        /// <summary>传奇英雄召唤钻石单抽</summary>
        HeroSummonTicketOne,
        /// <summary>传奇英雄召唤钻石十连抽</summary>
        HeroSummonTicketTen,
        /// <summary>传奇部队召唤钻石单抽</summary>
        EquipSummonTicketOne,
        /// <summary>传奇部队召唤钻石十连抽</summary>
        EquipSummonTicketTen,

        /// <summary>重置英雄天赋</summary>
        ResetHeroDower,
        /// <summary>副本战斗重生</summary>
        FBWarRebirth,
        /// <summary>探索副本战斗重生</summary>
        EventFBWarRebirth,
        /// <summary>玩家改名</summary>
        ReName,
        /// <summary>创建联盟</summary>
        ClubCreate,
        /// <summary>购买体力</summary>
        BuyPower,
        /// <summary>补满竞技体力</summary>
        BuyArenaEnergy,
        /// <summary>补满泰坦体力</summary>
        BuyTitanEnergy,
        /// <summary>联赛重置次数</summary>
        ReSetLeagueNum,

        /// <summary>购买额外英雄卡槽</summary>
        BuyHeroCard,
        /// <summary>购买额外战队</summary>
        BuyHeroTeam,
        /// <summary>购买竞技护盾</summary>
        BuyArenaShield,
        /// <summary>签到补签</summary>
        SignInCost,
        /// <summary>购买宝藏</summary>
        BuyTreasure,


        /// <summary>购买英雄</summary>
        BuyHero = 51,
    }
}
