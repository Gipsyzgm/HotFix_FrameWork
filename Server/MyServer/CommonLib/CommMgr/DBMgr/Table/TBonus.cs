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
        /// 已领取7天奖励ID
        /// </summary>
        public int[] sevenIds { get; set; }
        /// <summary>
        /// 已领取等级奖励ID
        /// </summary>
        public int[] levelIds { get; set; }
        /// <summary>
        /// 已领取新手活动宝箱奖励ID
        /// </summary>
        public List<int> newbieIds { get; set; }
        /// <summary>
        /// 本月已签到的Ids
        /// </summary>
        public List<int> signInIds { get; set; }
        /// <summary>
        /// 本月签到已领奖的Ids
        /// </summary>
        public List<int> signInAwards { get; set; }
        /// <summary>
        /// 本月累计签到已领奖的Ids
        /// </summary>
        public List<int> signInTotals { get; set; }
        /// <summary>
        /// 宝藏已抽取的Ids
        /// </summary>
        public List<int> treasureIds { get; set; }
        /// <summary>
        /// 广告奖励下次可领取时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? adsTime { get; set; }
        /// <summary>
        /// 新手任务是否全部完成宝箱全部领取
        /// </summary>
        public bool newbieOver { get; set; }
       
    }
}
