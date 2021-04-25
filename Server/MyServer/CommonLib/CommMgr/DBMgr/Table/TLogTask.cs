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
    public class TLogTask : BaseTable
    {
        /// <summary>   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogTask(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>   oid指定ObjectId</summary>
        public TLogTask(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 任务ID
        /// </summary>
        public int taskId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int taskType { get; set; }
        /// <summary>
        /// 进度
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
