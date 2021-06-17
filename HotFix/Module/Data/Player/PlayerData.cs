using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    public class PlayerData
    {
        public TPlayer Data { get; }
        /// <summary>等级</summary>
        public int Level
        {
            get => Data.Level;
            set
            {
                if (Data.Level != value)
                {
                    Data.Level = value;
                }
            }
        }
        /// <summary>最大等级</summary>
        public int MaxLevel;

        /// <summary>玩家当前经验</summary>
        public int Exp
        {
            get => Data.Exp;
            set
            {
                if (Data.Exp != value)
                {
                    Data.Exp = value;
                }
            }
        }

        /// <summary>玩家当前等级最大经验</summary>
        public int ExpMax;

        /// <summary>经验进度</summary>
        public float ExpProgress
        {
            get
            {
                if (ExpMax > 0)
                    return Exp / (float)ExpMax;
                return 1;
            }
        }

        /// <summary>金币</summary>
        public long Gold
        {
            get => Data.Gold;
            set
            {
                if (Data.Gold != value)
                {
                    Data.Gold = value;
                    TopMgr.I.goldValueChanged?.Invoke();
                }
            }
        }

        /// <summary>(点券)钻石</summary>
        public int Ticket
        {
            get => Data.Ticket;
            set
            {
                if (Data.Ticket != value)
                {
                    Data.Ticket = value;
                    TopMgr.I.ticketValueChanged?.Invoke();
                }
            }
        }

        /// <summary>体力</summary>
        public int Power
        {
            get
            {
                if (NextAddPowerTime == 0)
                {
                    if (Data.Power < 20)
                    {
                        NextAddPowerTime = DateTime.Now.ToTimestamp() + PowerRecoverTime;
                    }
                }
                else
                {
                    if (Data.Power < 20)
                    {
                        int tempTime = DateTime.Now.ToTimestamp() - NextAddPowerTime;
                        if (tempTime > 0)
                        {
                            Data.Power += (tempTime / PowerRecoverTime);
                            if (Data.Power > PowerMax)
                            {
                                Data.Power = PowerMax;
                                NextAddPowerTime = 0;
                            }
                            else
                            {
                                NextAddPowerTime = DateTime.Now.ToTimestamp() + PowerRecoverTime;
                            }
                        }

                    }
                    else
                    {
                        NextAddPowerTime = 0;
                    }

                }
                return Data.Power;
            }
            set
            {
                if (Data.Power != value)
                {
                    Data.Power = value;
                    if (NextAddPowerTime == 0)
                    {
                        if (Data.Power < 20)
                        {
                            NextAddPowerTime = DateTime.Now.ToTimestamp() + PowerRecoverTime;

                        }
                        else
                        {
                            NextAddPowerTime = 0;
                        }


                    }
                    TopMgr.I.powerValueChanged?.Invoke();
                }
            }
        }

        /// <summary>最大体力</summary>
        public int PowerMax = 20;

        /// <summary>体力进度</summary>
        public float PowerProgress => Power / (float)PowerMax;


        public int PowerRecoverTime = 600;

        /// <summary>下次恢复副本行动点时间戳</summary>
        public int NextAddPowerTime
        {
            get => Data.NextAddPowerTime;

            set
            {
                if (Data.NextAddPowerTime != value)
                {
                    Data.NextAddPowerTime = value;
                }
                PlayerMgr.I.SavePlayerData();
            }

        }

        /// <summary>最高通关副本章节Id</summary>
        public int FBChapterId
        {
            get => Data.FBChapterId;
            set => Data.FBChapterId = value;
        }

        /// <summary>最高通关副本关卡数</summary>
        public int FBLevelNo
        {
            get => Data.FBLevelNo;
            set => Data.FBLevelNo = value;
        }

        public PlayerData(TPlayer data)
        {
            Data = data;
            MaxLevel = (int)Mgr.Config.dicPlayerExp.Keys.Last(); //最大等级
        }


        /// <summary>
        /// 增加队伍经验
        /// </summary>
        /// <param name="val"></param>
        public int AddExp(int val)
        {
            if (val <= 0) return 0;
            int leftExp = val;
            PlayerExpConfig config;
            int lv = Level;
            int exp = Exp;

            while (leftExp > 0)
            {
                if (Mgr.Config.dicPlayerExp.TryGetValue(lv, out config))
                {
                    exp = exp + leftExp;
                    if (exp >= config.exp) //升级了
                    {
                        leftExp = exp - config.exp;
                        exp = 0;
                        lv += 1;
                    }
                    else
                        leftExp = 0;
                }
                else
                {
                    lv = lv - 1;
                    exp = Mgr.Config.dicPlayerExp[lv].exp;
                    leftExp = 0;
                }
            }

            int addLv = lv - Level;
            if (Level != lv || Exp != exp) //等级发生变化
            {
                if (Level != lv)
                    Level = lv;
                Exp = exp;
                ExpMax = Mgr.Config.dicPlayerExp[Level].exp;
                MaxLevel = Mgr.Config.dicPlayerExp.Values.Last().level;
            }

            return addLv; //增加等级
        }

        /// <summary>
        /// 设置通关的章节和编号
        /// </summary>
        /// <param name="chapterId">章节Id</param>
        /// <param name="no">通过编号</param>
        /// <returns></returns>
        public bool SetChapterPass(int chapterId, int no)
        {
            if (Mgr.Config.dicFBChapter.TryGetValue(chapterId, out var ch))
            {
                //没开放的关卡不记录
                if (!ch.isOpen)
                {
                    WarMapMgr.I.ChapterId--;
                    chapterId--;
                    return false;
                }
            }
            else
            {   //没有对应章节不记录
                chapterId--;
                WarMapMgr.I.ChapterId--;
            }
            if (chapterId > FBChapterId)
            {

                FBChapterId = chapterId;
                FBLevelNo = no;
                //PlayerMgr.I.SavePlayerData();
                return true;
            }
            if (FBChapterId == chapterId && no > FBLevelNo)
            {
                FBLevelNo = no;
                return true;
            }


            return false;
        }

        public void Dispose()
        {
            //PlayerPrefs.SetInt("HeroSortType", (int)EHeroSortType.Power);
            //PlayerPrefs.Save();
        }
    }
}
