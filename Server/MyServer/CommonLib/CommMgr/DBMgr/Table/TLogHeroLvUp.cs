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
    public class TLogHeroLvUp : BaseTable
    {
        /// <summary>   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogHeroLvUp(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>   oid指定ObjectId</summary>
        public TLogHeroLvUp(ObjectId oid) : base(oid){}

        /// <summary>
        /// 
        /// </summary>
        public ObjectId pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ObjectId heroId { get; set; }
        /// <summary>
        /// 0升级前1升级后 英雄等级
        /// </summary>
        public string heroStr { get; set; }
        /// <summary>
        /// 0升级前1升级后 经验
        /// </summary>
        public string oHeroStr { get; set; }
        /// <summary>
        /// 被吃掉的英雄ID
        /// </summary>
        public int[] resHeroId { get; set; }
        /// <summary>
        /// 被吃掉的英雄经验
        /// </summary>
        public int[] resHeroExp { get; set; }
        /// <summary>
        /// 0食物 2钻石
        /// </summary>
        public int[] Cost { get; set; }
        /// <summary>
        /// 升级概率
        /// </summary>
        public int skillLv { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
