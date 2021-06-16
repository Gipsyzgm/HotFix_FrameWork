using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public enum ETaskType
    {
        None = 0,
        //////////////////战斗//////////////////
        /// <summary>通关副本关卡</summary>
        FBRachLevel_War = 1,
        /// <summary>副本战斗中击杀怪物数量</summary>
        FBRach_KillMonster,
        /// <summary>竞技场战斗挑战次数</summary>
        Arena_War,
        /// <summary>竞技场战斗击杀英雄次数（突袭、联赛都算）</summary>
        Arena_KillHero,
        /// <summary>联盟战胜利次数</summary>
        Alliance_WarWin,
        /// <summary>联盟战中进攻次数</summary>
        Alliance_WarAttack,
        /// <summary>联盟Boss战次数</summary>
        Alliance_TitanWar,
        /// <summary>联盟Boss战击杀Boss次数</summary>
        Alliance_TitanKill,
        /// <summary>联赛战斗次数</summary>
        League_War,
        /// <summary>竞技场战斗连胜次数</summary>
        Arena_Winning,
        /// <summary>通关探索活动关卡次数</summary>
        EventFB_war,
        /// <summary>通关探索活动次数</summary>
        EventFB_pass,
        /// <summary>通关挑战活动次数（每通关1种难度算1次）</summary>
        Event_challenge,
        /// <summary>副本战斗中击杀指定类型怪物数量（复活再击杀也算），不计胜负</summary>
        FBRach_KillType,
        /// <summary>通关指定类型副本关卡次数</summary>
        FBRach_Special,

        //////////////////建筑//////////////////
        /// <summary>指定个数的建筑升到指定等级</summary>
        Build_LevelUp = 20,
        /// <summary>收取食物数量</summary>
        Build_FoodGet,
        /// <summary>收取矿石数量</summary>
        Build_StoneGet,
        /// <summary>任意方式收集食物数量</summary>
        Any_FoodGet,
        /// <summary>任意方式收集矿石数量</summary>
        Any_StoneGet,

        //////////////////战斗收集//////////////////
        /// <summary>从地图关卡收集指定道具</summary>
        War_ItemGet = 30,
        /// <summary>从地图关卡收集材料</summary>
        War_MaterialGet,
        /// <summary>从地图关卡收集食物</summary>
        War_FoodGet,
        /// <summary>从地图关卡收集矿石</summary>
        War_StoneGet,
        /// <summary>从地图关卡收集人口</summary>
        War_PeopleGet,

        //////////////////召唤//////////////////
        /// <summary>召唤次数 指定召唤类型</summary>
        Summon_Num = 40,
        /// <summary>召唤指定类型、星级英雄个数</summary>
        Summon_Hero,
        /// <summary>召唤指定类型、星级装备个数</summary>
        Summon_Equip,


        //////////////////装备+道具//////////////////
        /// <summary>制造装备次数</summary>
        Equip_Make = 50,
        /// <summary>装备升级次数</summary>
        Equip_LevelUp,
        /// <summary>制造道具次数</summary>
        Item_Make,
        /// <summary>战斗中使用道具次数</summary>
        ItemWar_Use,


        //////////////////英雄//////////////////
        /// <summary>训练英雄次数</summary>
        Hero_Train = 60,
        /// <summary>英雄升级到指定等级次数</summary>
        Hero_LevelUp,
        /// <summary>英雄突破到指定等级次数</summary>
        Hero_Break,
        /// <summary>英雄升级消耗卡牌数量</summary>
        Hero_LevelUpCost,
        /// <summary>英雄升级次数</summary>
        Hero_LevelUpNum,
        /// <summary>英雄突破次数</summary>
        Hero_BreakNum,


        //////////////////玩家//////////////////
        /// <summary>升到指定等级</summary>
        PlayerLv = 90,
        /// <summary>升到指定等级</summary>
        PlayerExp,


        //////////////////充值消费//////////////////
        /// <summary>累计充值达到指定金额(指定时间段内要填起止时间戳)</summary>
        Pay_Total = 95,
        /// <summary>累计消费达到指定金额(指定时间段内要填起止时间戳)</summary>
        Ticket_Cost,
        /// <summary>单笔充值达到指定金额</summary>
        Pay_Once,
        /// <summary>消费钻石达到指定次数</summary>
        Ticket_CostNum,
        /// <summary>第一次充值</summary>
        First_Pay,
        /// <summary>连续充值</summary>
        Connect_Pay,
    }

    //每日任务
    public enum ETaskDay
    {
        None = 0,
        /// <summary>击杀小怪</summary>
        KillNum = 1,
        /// <summary>击杀boss</summary>
        KillBossNum = 2,
        /// <summary>通关章节</summary>
        FinishChapter = 3,
        /// <summary>观看广告次数</summary>
        ADNum = 4,
        /// <summary>完成关卡次数</summary>
        FinishSatge = 5,
    }
}
