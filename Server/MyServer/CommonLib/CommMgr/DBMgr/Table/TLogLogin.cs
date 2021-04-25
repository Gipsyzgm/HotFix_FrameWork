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
    public class TLogLogin : BaseTable
    {
        /// <summary>#登录日志   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogLogin(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#登录日志   oid指定ObjectId</summary>
        public TLogLogin(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家Id
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
        /// <summary>
        /// 登录ip
        /// </summary>
        public string ip { get; set; }
       
    }
}
