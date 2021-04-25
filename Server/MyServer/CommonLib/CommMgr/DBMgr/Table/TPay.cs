using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm.DBMgr
{    
    [BsonIgnoreExtraElements]
    public class TPay : BaseTable
    {
        /// <summary>首充,月卡信息   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TPay(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>首充,月卡信息   oid指定ObjectId</summary>
        public TPay(ObjectId oid) : base(oid){}

        /// <summary>
        /// 是否购买了开服基金
        /// </summary>
        public bool openFundBuy { get; set; }
        /// <summary>
        /// 已购买(领取)开服基金ID
        /// </summary>
        public int[] openFundIds { get; set; }
        /// <summary>
        /// 首充奖励状态0未完成 1可领取 2已领取
        /// </summary>
        public int firstPay { get; set; }
        /// <summary>
        /// 已领取VIP礼包ID
        /// </summary>
        public int[] vipGifts { get; set; }
        /// <summary>
        /// 月卡剩余天数
        /// </summary>
        public int MC { get; set; }
        /// <summary>
        /// 月卡今日是否已领取
        /// </summary>
        public bool isGetMC { get; set; }
        /// <summary>
        /// 已购买礼包Id,<购买数量,首次购买日期>
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int[]> giftInfos { get; set; }
        /// <summary>
        /// 月卡最大档次（101周卡102月卡103年卡）
        /// </summary>
        public int MCLv { get; set; }
       
    }
}
