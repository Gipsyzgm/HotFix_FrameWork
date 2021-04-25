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
    public class TLogGuide : BaseTable
    {
        /// <summary>#登录日志   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogGuide(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#登录日志   oid指定ObjectId</summary>
        public TLogGuide(ObjectId oid) : base(oid){}

        /// <summary>
        /// 完成指引Id
        /// </summary>
        public int gId { get; set; }
        /// <summary>
        /// 完成次数
        /// </summary>
        public int Num { get; set; }
       
    }
}
