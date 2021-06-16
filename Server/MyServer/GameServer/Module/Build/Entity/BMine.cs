using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 矿场
    /// </summary>
    public class BMine : Build
    {
        /// <summary>
        /// 产出类型
        /// </summary>
        public EBuildOutputType OutputType = EBuildOutputType.Stone;

        public BMine(Player p, TBuild data) : base(p, data)
        {

        }

        /// <summary>
        /// 矿场升级（升级之前先收取资源）
        /// </summary>
        /// <param name="lvConfig"></param>
        public override void BuildLevelUp(BuildLvConfig lvConfig)
        {
            GetRes();
            base.BuildLevelUp(lvConfig);
        }

        /// <summary>
        /// 设置矿场生产达到存储上限的完成时间
        /// </summary>
        private void SetOutCompleteTime()
        {
            int num = LevelConfig.output[(int)OutputType];
            int max = LevelConfig.output[(int)OutputType];
            double time = max / (num * 1.0);
            Data.outTime = ((DateTime)Data.getTime).AddMinutes(time);
        }

        /// <summary>
        /// 矿场收取资源
        /// </summary>
        public override void GetRes()
        {
            //if (Data.getTime == null)
            //{
            //    player.SendError(typeof(SC_build_getRes), "BMine未到收取时间");
            //    return;
            //}
            //if (DateTime.Now.ToTimestamp() - Data.getTime.ToTimestamp() <= 0)
            //{
            //    player.SendError(typeof(SC_build_getRes), "BMine未到收取时间");
            //    return;
            //}
            //int time = DateTime.Now.ToTimestamp() - Data.getTime.ToTimestamp();
            //int num = LevelConfig.output[(int)OutputType];
            //int put = Convert.ToInt32(num / 60f * time);
            //int sum = put;
            //if (put > 0)
            //{
            //    DateTime proTime = DateTime.Now;
            //    int max = LevelConfig.storage[(int)OutputType];
            //    if (put > max)//不能超过建筑自己的存储上限
            //        put = max;
            //    if (put + player.Data.stone > player.StoneMax)//不能超过玩家最大存储上限
            //    {
            //        put = player.StoneMax - player.Data.stone;
            //        if (put < 0)
            //            put = 0;
            //        double s = 0;
            //        if (sum >= max)
            //        {
            //            s = (max - put) / (num / 60f);
            //            if (s > 0)
            //                proTime = DateTime.Now.AddSeconds(-s);
            //        }
            //        else
            //        {
            //            s = (sum - put) / (num / 60f);
            //            if (s > 0)
            //                proTime = DateTime.Now.AddSeconds(-s);
            //        }
            //    }
            //    if (put > 0)
            //    {
            //        player.AddVirtualItemNum(EItemSubTypeVirtual.Stone, put, true);
            //        player.TriggerTask(ETaskType.Build_StoneGet, put);
            //    }
            //    Data.getTime = proTime;
            //    SetOutCompleteTime();
            //    Data.Update();
            //    SC_build_getRes msg = new SC_build_getRes();
            //    msg.SID = SID;
            //    msg.GetTime = GetLastResTime();
            //    msg.OutTime = GetOutputTime();
            //    msg.State = State;
            //    player.Send(msg);
            //}
            //base.GetRes();
        }
    }*/
}
