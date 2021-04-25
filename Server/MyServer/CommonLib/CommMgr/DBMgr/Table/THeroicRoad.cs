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
    public class THeroicRoad : BaseTable
    {
        /// <summary>玩家英勇之路信息   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public THeroicRoad(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>玩家英勇之路信息   oid指定ObjectId</summary>
        public THeroicRoad(ObjectId oid) : base(oid){}

        /// <summary>
        /// 英勇点数
        /// </summary>
        public int heroicPoint { get; set; }
        /// <summary>
        /// 英勇奖励等级
        /// </summary>
        public int awardLv { get; set; }
        /// <summary>
        /// 是否购买英勇卡
        /// </summary>
        public bool isBuy { get; set; }
        /// <summary>
        /// 过期已完成未领取的点数
        /// </summary>
        public int expired { get; set; }
        /// <summary>
        /// 已领取的免费奖励ids
        /// </summary>
        public List<int> freeIds { get; set; }
        /// <summary>
        /// 已领取的英勇卡奖励ids
        /// </summary>
        public List<int> cardIds { get; set; }
       
    }
}
