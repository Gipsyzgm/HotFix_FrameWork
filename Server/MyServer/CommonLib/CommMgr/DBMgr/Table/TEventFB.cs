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
        /// 章节Id
        /// </summary>
        public int chapterId { get; set; }
        /// <summary>
        /// 章节模式(1 简单 2 困难)
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 章节随机关卡id（用于恢复客户端数据）
        /// </summary>
        public List<int> stageIds { get; set; }
        /// <summary>
        /// 当前关卡id
        /// </summary>
        public int   curStageId { get; set; }
        /// <summary>
        /// 英雄当前血量
        /// </summary>
        public int heroHP { get; set; }
        /// <summary>
        /// 英雄最大血量
        /// </summary>
        public int heroHPmax { get; set; }
        /// <summary>
        /// 技能id，等级
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int> skills { get; set; }
        /// <summary>
        /// 已获得奖励id 数量
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int> awards { get; set; }
        /// <summary>
        /// 是否退出章节
        /// </summary>
        public bool isExit { get; set; }
        /// <summary>
        /// 击杀小怪数
        /// </summary>
        public int killnum { get; set; }
        /// <summary>
        /// 击杀boss数
        /// </summary>
        public int killbossnum { get; set; }
        /// <summary>
        /// 战斗等级
        /// </summary>
        public int warlevel { get; set; }
        /// <summary>
        /// 战斗经验
        /// </summary>
        public int warexp { get; set; }
        /// <summary>
        /// 最高关卡数
        /// </summary>
        public int maxStage { get; set; }
        /// <summary>
        /// 通关次数
        /// </summary>
        public int finishnum { get; set; }
        /// <summary>
        /// 是否商人已出现
        /// </summary>
        public bool IsTraderOpen { get; set; }
        /// <summary>
        /// 是否已使用广告复活
        /// </summary>
        public bool IsUseReBorn { get; set; }
        /// <summary>
        /// 剩余技能复活次数
        /// </summary>
        public int leftRebirthnum { get; set; }
       
    }
}
