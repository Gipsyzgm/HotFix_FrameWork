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
    public class TLogFBWin : BaseTable
    {
        /// <summary>#副本、探索通关次数统计（统计每个副本关卡通关的次数和人数）   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogFBWin(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#副本、探索通关次数统计（统计每个副本关卡通关的次数和人数）   oid指定ObjectId</summary>
        public TLogFBWin(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 副本关卡Id
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 战斗类型（1普通/2扫荡）
        /// </summary>
        public int battleType { get; set; }
        /// <summary>
        /// 类型 1副本 2探索
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 通关次数
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 奖励
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int> items { get; set; }
        /// <summary>
        /// 最高连击
        /// </summary>
        public int comboMax { get; set; }
        /// <summary>
        /// 总伤害
        /// </summary>
        public int damage { get; set; }
        /// <summary>
        /// 使用道具
        /// </summary>
        public int[] useitemid { get; set; }
        /// <summary>
        /// 使用道具
        /// </summary>
        public int[] useitemnum { get; set; }
        /// <summary>
        /// 怪物ID
        /// </summary>
        public int[] killid { get; set; }
        /// <summary>
        /// 怪物数量
        /// </summary>
        public int[] killnum { get; set; }
        /// <summary>
        /// 总杀数
        /// </summary>
        public int kisNum { get; set; }
        /// <summary>
        /// 英雄剩余血量
        /// </summary>
        public int[] HeroHP { get; set; }
        /// <summary>
        /// 是否胜利
        /// </summary>
        public bool IsWin { get; set; }
        /// <summary>
        /// 操作方式 true 自动false 手动
        /// </summary>
        public bool PlayerAtion { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
