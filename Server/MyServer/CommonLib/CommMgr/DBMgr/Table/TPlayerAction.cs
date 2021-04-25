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
    public class TPlayerAction : BaseTable
    {
        /// <summary>   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TPlayerAction(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>   oid指定ObjectId</summary>
        public TPlayerAction(ObjectId oid) : base(oid){}

        /// <summary>
        /// 挑战世界BOSS次数
        /// </summary>
        public int wdBoss { get; set; }
        /// <summary>
        /// 击杀世界BOSS次数
        /// </summary>
        public int wdBossK { get; set; }
        /// <summary>
        /// 随机制造次数[品质1,品质2...]
        /// </summary>
        public int[] makeRd { get; set; }
        /// <summary>
        /// 使用图纸制造次数
        /// </summary>
        public int makeFixed { get; set; }
        /// <summary>
        /// 分解英雄次数
        /// </summary>
        public int heroRe { get; set; }
        /// <summary>
        /// 分解装备次数
        /// </summary>
        public int equipRe { get; set; }
        /// <summary>
        /// 钻石招募英雄
        /// </summary>
        public int recTicket { get; set; }
        /// <summary>
        /// 获得过的英雄ID
        /// </summary>
        public int[] hero { get; set; }
       
    }
}
