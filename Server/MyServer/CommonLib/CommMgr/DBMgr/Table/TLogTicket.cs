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
    public class TLogTicket : BaseTable
    {
        /// <summary>#消费统计   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogTicket(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#消费统计   oid指定ObjectId</summary>
        public TLogTicket(ObjectId oid) : base(oid){}

        /// <summary>
        /// 消费类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public int ticket { get; set; }
        /// <summary>
        /// 剩余数量
        /// </summary>
        public int surplus { get; set; }
        /// <summary>
        /// 1获得还是-1使用
        /// </summary>
        public int action { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
