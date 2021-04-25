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
    public class TAccount : BaseTable
    {
        /// <summary>账号表   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TAccount(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>账号表   oid指定ObjectId</summary>
        public TAccount(ObjectId oid) : base(oid){}

        /// <summary>
        /// 平台类型(PlatformConfig)
        /// </summary>
        public int pfType { get; set; }
        /// <summary>
        /// 平台用户Id(平台账号Id)
        /// </summary>
        public string pfId { get; set; }
        /// <summary>
        /// 平台渠道配置Id(最后登录的渠道)
        /// </summary>
        public int pfCh { get; set; }
        /// <summary>
        /// 平台用户邮箱(不一定可以获取得到)
        /// </summary>
        public string pfEmail { get; set; }
        /// <summary>
        /// 登录类型
        /// </summary>
        public int loginType { get; set; }
        /// <summary>
        /// 服务器id
        /// </summary>
        public int serverId { get; set; }
        /// <summary>
        /// 注册时间,第一次登录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? regDate { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? lastLoginDate { get; set; }
        /// <summary>
        /// 最后退出时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? lastLogoutDate { get; set; }
        /// <summary>
        /// 连续登录天数
        /// </summary>
        public int keepLoginNum { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public int payMoney { get; set; }
        /// <summary>
        /// 是否禁止登录
        /// </summary>
        public bool isBan { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public string deviceId { get; set; }
        /// <summary>
        /// sdk推广渠道号
        /// </summary>
        public string sdkCh { get; set; }
        /// <summary>
        /// 客户端版本号
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// sdk充值渠道号
        /// </summary>
        public string sdkPayCh { get; set; }
        /// <summary>
        /// 玩家绑定ID
        /// </summary>
        public string[] palyerBindId { get; set; }
       
    }
}
