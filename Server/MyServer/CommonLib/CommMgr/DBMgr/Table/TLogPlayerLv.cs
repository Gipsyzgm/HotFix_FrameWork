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
    public class TLogPlayerLv : BaseTable
    {
        /// <summary>   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogPlayerLv(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>   oid指定ObjectId</summary>
        public TLogPlayerLv(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 升级前等级
        /// </summary>
        public int usedLv { get; set; }
        /// <summary>
        /// 当前等级
        /// </summary>
        public int lv { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
