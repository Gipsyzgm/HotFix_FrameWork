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
    public class TLogFun : BaseTable
    {
        /// <summary>#功能统计   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogFun(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#功能统计   oid指定ObjectId</summary>
        public TLogFun(ObjectId oid) : base(oid){}

        /// <summary>
        /// 功能类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int num { get; set; }
       
    }
}
