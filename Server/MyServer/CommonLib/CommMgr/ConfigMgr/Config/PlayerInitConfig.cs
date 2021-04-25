using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>人物初始配置</summary>
    public class PlayerInitConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => initLevel;
        /// <summary>
        /// 初始等级
        /// </summary>
        public int initLevel { get; set; }
        /// <summary>
        /// 初始金币
        /// </summary>
        public int initGold { get; set; }
        /// <summary>
        /// 初始钻石
        /// </summary>
        public int initTicket { get; set; }
        /// <summary>
        /// 初始体力
        /// </summary>
        public int initPower { get; set; }
        /// <summary>
        /// 初始英雄
        /// </summary>
        public int initHeros { get; set; }
        /// <summary>
        /// 初始道具
        /// </summary>
        public List<int[]> initItems { get; set; }
        /// <summary>
        /// 初始装备
        /// </summary>
        public int[] initEpuips { get; set; }
        /// <summary>
        /// 初始头像[头像、背景、图钉]
        /// </summary>
        public int[] initIcon { get; set; }
    }
}
