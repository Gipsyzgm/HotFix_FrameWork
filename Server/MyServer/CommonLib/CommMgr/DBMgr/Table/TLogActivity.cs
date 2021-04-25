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
    public class TLogActivity : BaseTable
    {
        /// <summary>#活动领取统计   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogActivity(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#活动领取统计   oid指定ObjectId</summary>
        public TLogActivity(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 活动任务Id
        /// </summary>
        public int taskId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
