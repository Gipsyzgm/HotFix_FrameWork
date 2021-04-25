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
    public class TBonus : BaseTable
    {
        /// <summary>福利信息   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TBonus(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>福利信息   oid指定ObjectId</summary>
        public TBonus(ObjectId oid) : base(oid){}

        /// <summary>
        /// 已领取等级奖励ID
        /// </summary>
        public int[] levelIds { get; set; }
        /// <summary>
        /// 当前签到组id
        /// </summary>
        public int signInGroup { get; set; }
        /// <summary>
        /// 当前签到已领奖的Ids
        /// </summary>
        public List<int> signInAwards { get; set; }
        /// <summary>
        /// 当天是否已签到
        /// </summary>
        public bool signDone { get; set; }
        /// <summary>
        /// 副本荣耀奖励已领取Ids
        /// </summary>
        public List<int> fbHonorIds { get; set; }
        /// <summary>
        /// 挂机奖励道具id_num
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int> hangAwards { get; set; }
        /// <summary>
        /// 已领取挂机奖励时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? hangGetTime { get; set; }
        /// <summary>
        /// 挂机奖励金币
        /// </summary>
        public int hangGold { get; set; }
        /// <summary>
        /// 挂接奖励钻石
        /// </summary>
        public int hangTicket { get; set; }
        /// <summary>
        /// 副本荣耀奖励待领取ids
        /// </summary>
        public List<int> fbHonorWaitIds { get; set; }
        /// <summary>
        /// 英雄免费抽取时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? heroFreeTime { get; set; }
        /// <summary>
        /// 道具免费抽取时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? equipFreeTime { get; set; }
        /// <summary>
        /// 英雄抽取失败次数
        /// </summary>
        public int herofailNum { get; set; }
        /// <summary>
        /// 装备抽取失败次数
        /// </summary>
        public int equipfailNum { get; set; }
        /// <summary>
        /// 转盘次数
        /// </summary>
        public int circleNum { get; set; }
        /// <summary>
        /// 转盘重置时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? circleResetTime { get; set; }
        /// <summary>
        /// 转盘免费时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? circleFreeTime { get; set; }
        /// <summary>
        /// 已领取转盘宝箱ids
        /// </summary>
        public List<int> circleBoxIds { get; set; }
        /// <summary>
        /// 转盘组id
        /// </summary>
        public int circleGroupId { get; set; }
        /// <summary>
        /// 三日奖励当前广告数
        /// </summary>
        public int threeAdnum { get; set; }
        /// <summary>
        /// 三日奖励已领取id
        /// </summary>
        public int[] threeIds { get; set; }
        /// <summary>
        /// 三日奖励已领取时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? threeTime { get; set; }
        /// <summary>
        /// 悬浮宝箱领取时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? floatboxTime { get; set; }
        /// <summary>
        /// 悬浮宝箱待领取Id
        /// </summary>
        public int floatboxId { get; set; }
       
    }
}
