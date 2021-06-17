using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    //主要处理在游戏过程中玩家的游戏数据
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

       

        public PlayerData(TPlayer data)
        {
            Data = data;
            MaxLevel = (int)HotFix.Config.dicPlayerExp.Keys.Last(); //最大等级
        }


      
       

        public void Dispose()
        {
            //PlayerPrefs.SetInt("HeroSortType", (int)EHeroSortType.Power);
            //PlayerPrefs.Save();
        }
    }
}
