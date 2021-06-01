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
    public class TLogDower : BaseTable
    {
        /// <summary>#天赋升级统计   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogDower(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#天赋升级统计   oid指定ObjectId</summary>
        public TLogDower(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 英雄ID
        /// </summary>
        public ObjectId hId { get; set; }
        /// <summary>
        /// 天赋树Id
        /// </summary>
        public int treeId { get; set; }
        /// <summary>
        /// 节点Id（等级=nodeId/10）
        /// </summary>
        public int nodeId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
