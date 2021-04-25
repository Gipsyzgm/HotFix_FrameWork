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
    public class TSeason : BaseTable
    {
        /// <summary>服务器赛季   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TSeason(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>服务器赛季   oid指定ObjectId</summary>
        public TSeason(ObjectId oid) : base(oid){}

        /// <summary>
        /// 赛季开始时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? opendate { get; set; }
        /// <summary>
        /// 赛季序号（从1开始）
        /// </summary>
        public int seasonNo { get; set; }
       
    }
}
