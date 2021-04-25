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
    public class TLogItem : BaseTable
    {
        /// <summary>#道具使用统计   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogItem(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#道具使用统计   oid指定ObjectId</summary>
        public TLogItem(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 道具id
        /// </summary>
        public int itemId { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 剩余数量
        /// </summary>
        public int surplus { get; set; }
        /// <summary>
        /// 1获得还是-1使用
        /// </summary>
        public int action { get; set; }
        /// <summary>
        /// 使用获得位置
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
