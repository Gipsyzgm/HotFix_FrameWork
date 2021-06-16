using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// VIP权限类型
    /// </summary>
    public enum VipRightType
    {
        /// <summary>跳过比赛动画</summary>
        SkipWar,
        /// <summary>骑师就餐次数</summary>
        JocDiningEatNum,
        /// <summary>探索次数</summary>
        ExploreNum,
        /// <summary>基础治疗每天可使用次数</summary>
        CureBaseNum,
        /// <summary>广告次数</summary>
        BusAdNum,
        /// <summary>采矿次数</summary>
        MineNum,
        /// <summary>度假次数</summary>
        HolidayNum,
        /// <summary>俱乐部研究次数</summary>
        ClubResearchNum,
    }

}
