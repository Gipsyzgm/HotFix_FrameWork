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
    public class TEventFB : BaseTable
    {
        /// <summary>活动副本表   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TEventFB(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>活动副本表   oid指定ObjectId</summary>
        public TEventFB(ObjectId oid) : base(oid){}

        /// <summary>
        /// 所属玩家id
        /// </summary>
        public ObjectId pid { get; set; }
        /// <summary>
        /// 活动副本Id
        /// </summary>
        public int eventId { get; set; }
        /// <summary>
        /// 已通过最大关卡Id
        /// </summary>
        public int[] maxId { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 挑战活动3个难度的分数
        /// </summary>
        public int[] score { get; set; }
        /// <summary>
        /// 挑战活动3个难度的通关奖励状态
        /// </summary>
        public int[] award { get; set; }
        /// <summary>
        /// 3个难度攻击队伍英雄Ids
        /// </summary>
        public List<ObjectId[]> heroIds { get; set; }
        /// <summary>
        /// 3个难度攻击队伍装备Ids
        /// </summary>
        public List<ObjectId[]> equipIds { get; set; }
        /// <summary>
        /// 3个难度攻击队伍道具Ids
        /// </summary>
        public List<int[]> itemids { get; set; }
       
    }
}
