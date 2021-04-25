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
    public class TLogSummonBuy : BaseTable
    {
        /// <summary>   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogSummonBuy(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>   oid指定ObjectId</summary>
        public TLogSummonBuy(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 召唤类型
        /// </summary>
        public int summonType { get; set; }
        /// <summary>
        /// 召唤类型 次数 1单次 2十连抽
        /// </summary>
        public int numType { get; set; }
        /// <summary>
        /// 下次免费抽时间
        /// </summary>
        public int freeTime { get; set; }
        /// <summary>
        /// 英雄抽取列表
        /// </summary>
        public List<int> heroList { get; set; }
        /// <summary>
        /// 装备抽取列表
        /// </summary>
        public List<int> equipList { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
