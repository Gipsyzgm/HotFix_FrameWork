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
    public class TLogServer : BaseTable
    {
        /// <summary>#服务器数据统计   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogServer(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#服务器数据统计   oid指定ObjectId</summary>
        public TLogServer(ObjectId oid) : base(oid){}

        /// <summary>
        /// 统计日期(年月日)
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
        /// <summary>
        /// 最高在线人数
        /// </summary>
        public int online { get; set; }
        /// <summary>
        /// 活跃用户
        /// </summary>
        public int activeP { get; set; }
        /// <summary>
        /// 新注册用户
        /// </summary>
        public int newAcc { get; set; }
        /// <summary>
        /// 新增付费玩家
        /// </summary>
        public int newPayP { get; set; }
        /// <summary>
        /// 付费玩家数
        /// </summary>
        public int payP { get; set; }
        /// <summary>
        /// 充值总额
        /// </summary>
        public float payMoney { get; set; }
       
    }
}
