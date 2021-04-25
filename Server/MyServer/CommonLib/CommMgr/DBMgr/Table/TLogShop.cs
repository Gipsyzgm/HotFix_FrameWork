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
    public class TLogShop : BaseTable
    {
        /// <summary>商城日志   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogShop(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>商城日志   oid指定ObjectId</summary>
        public TLogShop(ObjectId oid) : base(oid){}

        /// <summary>
        /// 商城类型 0道具商店 1活动商店 2市场商店
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public int sId { get; set; }
        /// <summary>
        /// 玩家Id
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int bNum { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
