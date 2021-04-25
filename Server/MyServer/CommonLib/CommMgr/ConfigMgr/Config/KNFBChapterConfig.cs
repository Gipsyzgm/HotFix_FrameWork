using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>副本章节</summary>
    public class KNFBChapterConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 章节Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 是否对外开放
        /// </summary>
        public bool isOpen { get; set; }
        /// <summary>
        /// 关卡数量
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 副本章节描述
        /// </summary>
        public Lang desc { get; set; }
        /// <summary>
        /// 章节首次通关荣耀点
        /// </summary>
        public int Horner { get; set; }
        /// <summary>
        /// 商店物品1；商品id_数量_货币（金币1钻石2）_价格_权重
        /// </summary>
        public List<int[]> shopItems { get; set; }
    }
}
