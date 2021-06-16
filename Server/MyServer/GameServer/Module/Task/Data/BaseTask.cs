using CommonLib.Comm.DBMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 任务活动其类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseTask<T> : IDisposable where T : ITable
    {
        /// <summary>所属玩家</summary>
        public Player player { get; protected set; }

       

        /// <summary>实体数据</summary>
        public T Data { get; protected set; }

        /// <summary>任务进度</summary>
        public virtual int Progress { get; protected set; }
              
        public virtual int[] Condition { get; protected set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public virtual ETaskType Type { get; protected set; }

        /// <summary>
        /// 当前任务活动是否完成
        /// </summary>
        public virtual bool IsComplete { get; protected set; }

        /// <summary>当前任务状态</summary>
        public virtual EAwardState State
        {
            get
            {
                if (IsComplete)
                    return EAwardState.HaveGet;
                else if (Progress >= Condition[0])
                    return EAwardState.Done;
                else
                    return EAwardState.Undone;
            }
        }

        /// <summary>
        /// 判断增加进进度值是否符合条件，符合条件返回进度值，否则返回0
        /// </summary>
        /// <param name="addPro">进度值(参数0)</param>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        /// <returns>新的总进度</returns>
        public int checkAddProgress(int addPro = 1, int arg1 = -1, int arg2 = -1)
        {
            if (addPro <= 0) return 0;
            int progress = 0;   //总进度，如果某个类型修改了，最后不要再重新计算总进度
            switch (Type)
            {
                //=============次数+一个任意参数==========
                case ETaskType.Pay_Once://	单笔充值达到指定金额
                case ETaskType.Summon_Num://	召唤次数 指定召唤类型
                case ETaskType.War_ItemGet://	从地图关卡收集指定道具
                case ETaskType.EventFB_war://	通关探索活动关卡次数
                case ETaskType.EventFB_pass://	通关探索活动次数
                case ETaskType.Event_challenge://	通关挑战活动次数（每通关1种难度算1次）
                    if (!checkAny(arg1))
                        addPro = 0;
                    break;               
                //============次数加二个任意参数===========
                case ETaskType.FBRach_KillMonster://	副本战斗中击杀怪物数量
                case ETaskType.Arena_KillHero://	角斗场战斗击杀英雄次数
                case ETaskType.Alliance_TitanKill://	联盟Boss战击杀Boss次数
                case ETaskType.Item_Make://	制造道具个数（收取道具时计算个数）
                case ETaskType.ItemWar_Use://	战斗中使用道具次数
                case ETaskType.Equip_Make://	制造装备次数（制造完成才算）
                case ETaskType.Hero_Train://	训练英雄次数（训练完成才算）
                case ETaskType.War_MaterialGet://	从地图关卡收集材料
                case ETaskType.FBRach_KillType://	副本战斗中击杀指定类型怪物数量
                case ETaskType.FBRach_Special://	通关指定类型副本关卡次数
                case ETaskType.Hero_LevelUpCost://	英雄升级消耗卡牌数量
                case ETaskType.Hero_LevelUpNum://	英雄升级次数
                case ETaskType.Hero_BreakNum://     英雄突破次数
                    if (!checkAny(arg1) || !checkAny(arg2,2))
                        addPro = 0;
                    break;
                //============特殊处理==============
                case ETaskType.FBRachLevel_War://通关副本关卡                    
                case ETaskType.PlayerLv://升到指定等级
                case ETaskType.Build_LevelUp://	指定个数的建筑升到指定等级
                    progress = recountProgress();
                    addPro = 0;
                    break;
                case ETaskType.Arena_War://	角斗场战斗挑战次数
                case ETaskType.Summon_Hero://	召唤指定类型、星级英雄个数（召唤到装备不算）
                case ETaskType.Summon_Equip://	召唤指定类型、星级装备个数（召唤到英雄不算）
                    if (checkAny(arg1) && arg2 >= Condition[2])
                        addPro = 1;
                    else
                        addPro = 0;
                    break;
                case ETaskType.Equip_LevelUp:// 装备升级（从低于指定等级升到指定等级（或以上）时才算）
                case ETaskType.Hero_LevelUp://	英雄升级（从低于指定等级升到指定等级（或以上）时才算）
                case ETaskType.Hero_Break://	英雄突破到指定等级次数
                    if (addPro < Condition[2] && checkAny(arg1) && (arg2 <= 0 || arg2 >= Condition[2]))
                        progress = 1;
                    addPro = 0;
                    break;
                case ETaskType.Pay_Total:                    
                case ETaskType.Ticket_Cost:                   
                    break;
                case ETaskType.Ticket_CostNum://	消费钻石达到指定次数
                    if (arg1 < Condition[1])
                        addPro = 0;                    
                    break;
                case ETaskType.Connect_Pay:
                    
                    break;

                    //============没有条件的==============
                    //case ETaskType.Build_FoodGet://	收取食物数量（只算从农场中点击收取获得的）
                    //case ETaskType.Build_StoneGet://	收取矿石数量（只算从矿场中点击收取获得的）
                    //case ETaskType.Any_FoodGet://	    任意方式收集食物数量
                    //case ETaskType.Any_StoneGet://	任意方式收集矿石数量
                    //case ETaskType.Alliance_WarWin://	联盟战胜利次数（联盟战争结束时己方胜利）
                    //case ETaskType.Alliance_WarAttack://	联盟战中进攻次数（主动攻击敌方的战斗获得胜利，防守不算）
                    //case ETaskType.Alliance_TitanWar://	联盟Boss战次数
                    //case ETaskType.War_FoodGet://	从地图关卡收集食物
                    //case ETaskType.War_StoneGet://	从地图关卡收集矿石
                    //case ETaskType.War_PeopleGet://	从地图关卡收集人口
                    //case ETaskType.PlayerExp://	获得经验
                    //case ETaskType.First_Pay://	第一次充值

                    //case ETaskType.Arena_Winning://	竞技场战斗连胜次数


            }
            if (progress > 0)
                return progress;
            else
                return Math.Min(Progress + addPro,Condition[0]);
        }
        /// <summary>
        /// 判断条件参数是否符合(或-1任意参数)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool checkAny(int arg, int index=1)
        {
            return Condition[index] == -1 || Condition[index] == arg;
        }
        /// <summary>
        /// 判断条件参数是否符合
        /// </summary>
        /// <param name="index"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool check(int arg, int index=1)
        {
            return Condition[index] == arg;
        }

        /// <summary>
        /// 重新计算进度,某些活动接到任务时会跟据玩家当前数据计算进度
        /// </summary>
        /// <returns></returns>
        protected int recountProgress()
        {
            int progress = 0;
            switch (Type)
            {
                //case ETaskType.Build_LevelUp://	指定个数的建筑升到指定等级
                //    foreach(Build build in player.buildList.Values)
                //    {
                //        if (build.BuildType == (EBuildType)Condition[1] && build.Level >= Condition[2])
                //            progress++;
                //    }
                //    break;
                case ETaskType.PlayerLv://升到指定等级
                    progress = player.Level;
                    break;
                case ETaskType.FBRachLevel_War://通关副本关卡
                    //if(Glob.fbMgr.CheckIsPassLv(player, Condition[1]))
                    //    progress = 1;
                    break;


                //case ETaskType.Club_Join://	玩家加入俱乐部                    
                //    if(Glob.clubMgr.GetPlayerClub(player.ID)!=null)
                //        progress = 1;
                //    break;
                //case ETaskType.Club_Member://	俱乐部人数达到或超过指定数值
                //    ClubData club = Glob.clubMgr.GetPlayerClub(player.ID);
                //    if (club != null)
                //    {
                //        progress = club.dicMemberList.Count;
                //    }
                //    break;
                //case ETaskType.Club_Lv://俱乐部 达到 指定等级
                //    ClubData clubD = Glob.clubMgr.GetPlayerClub(player.ID);
                //    if (clubD != null)
                //        progress = clubD.Data.level;
                //    break;
                //case ETaskType.Club_ResearchLv://指定俱乐部科技 达到 指定等级
                //    switch ((EClubRsType)Condition[1])
                //    {
                //        case EClubRsType.EquStreng:
                //            if (player.Club.Data.rsLv1 >= Condition[0])
                //                progress = Condition[0];
                //            break;
                //        case EClubRsType.GemInset:
                //            if (player.Club.Data.rsLv2 >= Condition[0])
                //                progress = Condition[0];
                //            break;
                //        case EClubRsType.HorBreed:
                //            if (player.Club.Data.rsLv3 >= Condition[0])
                //                progress = Condition[0];
                //            break;
                //        case EClubRsType.HorQualityUp:
                //            if (player.Club.Data.rsLv4 >= Condition[0])
                //                progress = Condition[0];
                //            break;
                //    }
                //    break;
            }
            return progress;
        }

        public virtual void Dispose()
        {
            player = null;
        }
    }
}


