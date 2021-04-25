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
    public class TActivity : BaseTable
    {
        /// <summary>玩家活动数据   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TActivity(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>玩家活动数据   oid指定ObjectId</summary>
        public TActivity(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家Id
        /// </summary>
        public ObjectId pid { get; set; }
        /// <summary>
        /// 活动任务Id
        /// </summary>
        public int taskId { get; set; }
        /// <summary>
        /// 任务进度
        /// </summary>
        public int pro { get; set; }
        /// <summary>
        /// 是否已领取
        /// </summary>
        public bool isGet { get; set; }
        /// <summary>
        /// 活动生成时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Mark { get; set; }
       
    }
}
