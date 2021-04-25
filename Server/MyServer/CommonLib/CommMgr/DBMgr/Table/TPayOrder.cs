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
    public class TPayOrder : BaseTable
    {
        /// <summary>充值定单   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TPayOrder(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>充值定单   oid指定ObjectId</summary>
        public TPayOrder(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家Id
        /// </summary>
        public ObjectId pid { get; set; }
        /// <summary>
        /// 商品配置Id
        /// </summary>
        public int gId { get; set; }
        /// <summary>
        /// 平台(玩家所属平台)
        /// </summary>
        public int platform { get; set; }
        /// <summary>
        /// 平台定单号
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// 是否支付
        /// </summary>
        public bool isPay { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? orderData { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? payData { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public int payMoney { get; set; }
        /// <summary>
        /// 是否发货
        /// </summary>
        public bool isSend { get; set; }
       
    }
}
