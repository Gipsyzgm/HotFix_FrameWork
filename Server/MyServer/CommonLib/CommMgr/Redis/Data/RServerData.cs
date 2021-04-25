using CommonLib.Redis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Redis
{
    /// <summary>
    /// 服务器公用数据-召唤
    /// </summary>
    public class RSummon : RBase
    {
        /// <summary>元素召唤的元素索引(每日轮换)</summary>
        public int ElementIndex = 0;

        /// <summary>元素召唤的元素索引更换时间</summary>
        public DateTime ElementTime = DateTime.Now;

        public RSummon() { }

        public RSummon(string key) : base(key) { }
    }

    /// <summary>
    /// 服务器公用数据-联盟战
    /// </summary>
    public class RClubWar : RBase
    {
        /// <summary>联盟战争状态</summary>
        public int ClubWarState = 0;

        /// <summary>联盟战争状态刷新时间</summary>
        public DateTime? ClubWarTime = DateTime.MinValue;

        public RClubWar() { }

        public RClubWar(string key) : base(key) { }
    }

    /// <summary>
    /// 服务器公用数据-英勇之路
    /// </summary>
    public class RTaskHeroic : RBase
    {
        /// <summary>英勇之路每日任务Ids</summary>
        public int[] TaskHeroicIds = { 0, 0, 0 };

        public RTaskHeroic() { }

        public RTaskHeroic(string key) : base(key) { }
    }

    /// <summary>
    /// 服务器公用数据-头像商店
    /// </summary>
    public class RShopIcon : RBase
    {
        /// <summary>头像商店特殊头像商品id</summary>
        public int SpecialIconId = 0;

        /// <summary>头像商店特殊头像商品上次刷新时间</summary>
        public DateTime SpecialIconLastTime = DateTime.MinValue;

        /// <summary>头像商店每日刷新的头像商品ids</summary>
        public int[] DailyIconIds = { 0, 0, 0 };

        public RShopIcon() { }

        public RShopIcon(string key) : base(key) { }
    }

    /// <summary>
    /// 服务器公用数据-联赛赛事
    /// </summary>
    public class RLeagueMatch : RBase
    {
        /// <summary>英雄元素限制</summary>
        public int Element = 0;

        /// <summary>英雄星级限制</summary>
        public int Star = 0;

        /// <summary>战斗特殊规则</summary>
        public int Rule = 0;

        /// <summary>开始时间</summary>
        public DateTime? Time;

        /// <summary>状态</summary>
        public int State = 0;

        /// <summary>下次更新状态时间</summary>
        public DateTime? NextTime;

        public RLeagueMatch() { }

        public RLeagueMatch(string key) : base(key) { }
    }
}
