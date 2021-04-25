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
    public class TCDKeyPlayer : BaseTable
    {
        /// <summary>玩家CDKey兑换信息   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TCDKeyPlayer(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>玩家CDKey兑换信息   oid指定ObjectId</summary>
        public TCDKeyPlayer(ObjectId oid) : base(oid){}

        /// <summary>
        /// 已兑换过的CDKeyId
        /// </summary>
        public int[] cdkIds { get; set; }
        /// <summary>
        /// 已兑换过的CDKey数量
        /// </summary>
        public int[] cdkCounts { get; set; }
       
    }
}
