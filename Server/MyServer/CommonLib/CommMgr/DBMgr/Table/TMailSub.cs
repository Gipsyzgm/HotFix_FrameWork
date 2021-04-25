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
    public class TMailSub : BaseTable
    {
        /// <summary>邮件子项(记录已打开的群邮件玩家ID)   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TMailSub(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>邮件子项(记录已打开的群邮件玩家ID)   oid指定ObjectId</summary>
        public TMailSub(ObjectId oid) : base(oid){}

        /// <summary>
        /// 邮件Id
        /// </summary>
        public ObjectId mId { get; set; }
        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 是否已领取(群邮件)
        /// </summary>
        public bool isGet { get; set; }
        /// <summary>
        /// 是否已删除(群邮件)
        /// </summary>
        public bool isDel { get; set; }
       
    }
}
