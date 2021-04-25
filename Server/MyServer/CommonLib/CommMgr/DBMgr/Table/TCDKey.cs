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
    public class TCDKey : BaseTable
    {
        /// <summary>已使用礼品码信息   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TCDKey(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>已使用礼品码信息   oid指定ObjectId</summary>
        public TCDKey(ObjectId oid) : base(oid){}

        /// <summary>
        /// CDKeyId
        /// </summary>
        public int cdkId { get; set; }
        /// <summary>
        /// 已兑换的CDkey序号
        /// </summary>
        public int num { get; set; }
       
    }
}
