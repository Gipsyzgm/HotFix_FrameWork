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
    public class TLogFBNum : BaseTable
    {
        /// <summary>   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogFBNum(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>   oid指定ObjectId</summary>
        public TLogFBNum(ObjectId oid) : base(oid){}

        /// <summary>
        /// 章节ID1
        /// </summary>
        public int chId { get; set; }
        /// <summary>
        /// 是否通关
        /// </summary>
        public bool isFinish { get; set; }
        /// <summary>
        /// 玩家id
        /// </summary>
        public ObjectId pid { get; set; }
       
    }
}
