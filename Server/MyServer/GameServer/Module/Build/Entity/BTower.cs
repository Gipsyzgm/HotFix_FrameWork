using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 建筑-角斗场
    /// </summary>
    public class BTower : Build
    {
        /// <summary>
        /// 产出类型
        /// </summary>
        public EBuildOutputType OutputType = EBuildOutputType.FoodStone;

//         public BTower(Player p, TBuild data) : base(p, data)
//         {
// 
//         }

        /// <summary>
        /// 角斗场升级（升级之前先收取资源）
        /// </summary>
        /// <param name="lvConfig"></param>
        public override void BuildLevelUp(BuildLvConfig lvConfig)
        {
            GetRes();
            base.BuildLevelUp(lvConfig);
        }

        /// <summary>
        /// 防守失败扣除资源
        /// </summary>
        /// <param name="food">20</param>
        /// <param name="stone"></param>
        public void CutRes(int food,int stone) 
        {
            int time = DateTime.Now.ToTimestamp() - Data.outputId;
            int num = LevelConfig.output[(int)EBuildOutputType.Food];   
            int put = Convert.ToInt32(num / 60f * time);//50
            int max = LevelConfig.storage[(int)EBuildOutputType.Food];  //60
            put = put > max ? max : put;
            if (put < max / 2)
                food = 0;
            else
            {
                int cut = (put - max / 2) - food;
                if (cut < 0)
                    food = put - max / 2;
            }
            double s = (put - food) / (num / 60f);
            if (s > 0)
                Data.outputId = DateTime.Now.AddSeconds(-s).ToTimestamp();

            time = DateTime.Now.ToTimestamp() - Data.outputNum;
            num = LevelConfig.output[(int)EBuildOutputType.Stone];
            put = Convert.ToInt32(num / 60f * time);
            max = LevelConfig.storage[(int)EBuildOutputType.Stone];
            put = put > max ? max : put;

            if (put < max / 2)
                stone = 0;
            else
            {
                int cut = (put - max / 2) - food;
                if (cut < 0)
                    stone = put - max / 2;
            }            
            s = (put - stone) / (num / 60f);
            if (s > 0)
                Data.outputNum = DateTime.Now.AddSeconds(-s).ToTimestamp();

            Data.Update();
        }
        ///// <summary>
        ///// 收取资源
        ///// </summary>
        //public override void GetRes()
        //{
        //    if (Data.getTime == null)
        //    {
        //        player.SendError(typeof(SC_build_getRes), "竞技场收取CD时间未到");
        //        return;
        //    }
        //    if (DateTime.Now.ToTimestamp() - Data.getTime.ToTimestamp() <= 3600)
        //    {
        //        player.SendError(typeof(SC_build_getRes), "竞技场收取CD时间未到");
        //        return;
        //    }
        //    int time = DateTime.Now.ToTimestamp() - Data.outputId;
        //    int num = LevelConfig.output[(int)EBuildOutputType.Food];
        //    int put = Convert.ToInt32(num / 60f * time);
        //    DateTime proTime = DateTime.Now;
        //    if (put > 0)
        //    {
        //        int max = LevelConfig.storage[(int)EBuildOutputType.Food];
        //        if (put > max)//不能超过建筑自己的存储上限
        //            put = max;
        //        if (put + player.Data.food >= player.FoodMax)//不能超过玩家最大存储上限
        //        {
        //            put = player.FoodMax - player.Data.food;
        //            if (put > 0 && max - put > 0)
        //            {
        //                double s = (max - put) / (num / 60f);
        //                if (s > 0)
        //                {
        //                    proTime = DateTime.Now.AddSeconds(-s);
        //                }
        //            }
        //        }
        //        if (put > 0)
        //        {
        //            Data.outputId = proTime.ToTimestamp();
        //            player.AddVirtualItemNum(EItemSubTypeVirtual.Food, put, true);
        //            player.TriggerTask(ETaskType.Build_FoodGet, put);
        //        }
        //    }

        //    proTime = DateTime.Now;
        //    time = DateTime.Now.ToTimestamp() - Data.outputNum;
        //    num = LevelConfig.output[(int)EBuildOutputType.Stone];
        //    put = Convert.ToInt32(num / 60f * time);
        //    if(put > 0)
        //    {
        //        int max = LevelConfig.storage[(int)EBuildOutputType.Stone];
        //        if (put > max)//不能超过建筑自己的存储上限
        //            put = max;
        //        if (put + player.Data.stone >= player.StoneMax)//不能超过玩家最大存储上限
        //        {
        //            put = player.StoneMax - player.Data.stone;
        //            if (put > 0 && max - put > 0)
        //            {
        //                double s = (max - put) / (num / 60f);
        //                if (s > 0)
        //                {
        //                    proTime = DateTime.Now.AddSeconds(-s);
        //                }
        //            }
        //        }
        //        if (put > 0)
        //        {
        //            Data.outputNum = proTime.ToTimestamp();
        //            player.AddVirtualItemNum(EItemSubTypeVirtual.Stone, put, true);
        //            player.TriggerTask(ETaskType.Build_StoneGet, put);
        //        }
        //    }
        //    Data.getTime = DateTime.Now; 
        //    Data.Update();
        //    SC_build_getRes msg = new SC_build_getRes();
        //    msg.SID = SID;
        //    msg.GetTime = GetLastResTime();
        //    msg.OutTime = GetOutputTime();
        //    msg.State = State;
        //    msg.OpenId = Data.outputId;
        //    msg.OutNum = Data.outputNum;
        //    player.Send(msg);
        //}

        /// <summary>
        /// 被挑战对手可获得的资源数量 
        /// </summary>
        /// <returns></returns>
        public int[] GetResNum()
        {
            int[] arrNum = new int[2] { 0, 0 };
            int time = 0;
            int num = 0;
            int put = 0;
            int max = 0;
            if (DateTime.Now.ToTimestamp() - Data.outputId > 1)
            {
                time = DateTime.Now.ToTimestamp() - Data.outputId;
                num = LevelConfig.output[(int)EBuildOutputType.Food];
                put = Convert.ToInt32(num / 60f * time);
                max = LevelConfig.storage[(int)EBuildOutputType.Food];
                if (put > max)
                    put = max;
                if (put > 0)
                    arrNum[0] = Convert.ToInt32(put * Glob.config.settingConfig.ArenaLoseResRate);
            }
            if (DateTime.Now.ToTimestamp() - Data.outputNum > 1)
            {
                time = DateTime.Now.ToTimestamp() - Data.outputNum;
                num = LevelConfig.output[(int)EBuildOutputType.Stone];
                put = Convert.ToInt32(num / 60f * time);
                max = LevelConfig.storage[(int)EBuildOutputType.Stone];
                if (put > max)
                    put = max;
                if (put > 0)
                    arrNum[1] = Convert.ToInt32(put * Glob.config.settingConfig.ArenaLoseResRate);
            }
            return arrNum;
        }
    }*/
}
