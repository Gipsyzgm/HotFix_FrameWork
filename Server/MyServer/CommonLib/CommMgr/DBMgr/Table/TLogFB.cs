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
    public class TLogFB : BaseTable
    {
        /// <summary>#副本、探索挑战次数统计（统计玩家打每个副本关卡的次数和人数）   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogFB(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#副本、探索挑战次数统计（统计玩家打每个副本关卡的次数和人数）   oid指定ObjectId</summary>
        public TLogFB(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家ID
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 战斗类型（1普通/2扫荡）
        /// </summary>
        public int battleType { get; set; }
        /// <summary>
        /// 探索副本关卡Id
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 类型 1副本 2探索
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 普通挑战次数
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 英雄列表
        /// </summary>
        public List<string> heroList { get; set; }
        /// <summary>
        /// 装备列表
        /// </summary>
        public List<string> equipList { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
       
    }
}
