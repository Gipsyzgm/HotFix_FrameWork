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
    public class TLogFBExit : BaseTable
    {
        /// <summary>   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogFBExit(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>   oid指定ObjectId</summary>
        public TLogFBExit(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 副本关卡Id
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 类型 1副本 2探索
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 逃跑次数
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 使用道具ID
        /// </summary>
        public int[] itemid { get; set; }
        /// <summary>
        /// 使用道具数量
        /// </summary>
        public int[] itemnum { get; set; }
        /// <summary>
        /// 怪物ID
        /// </summary>
        public int[] killid { get; set; }
        /// <summary>
        /// 怪物数量
        /// </summary>
        public int[] killnum { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
