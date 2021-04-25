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
    public class TTaskHeroic : BaseTable
    {
        /// <summary>玩家英勇任务信息   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TTaskHeroic(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>玩家英勇任务信息   oid指定ObjectId</summary>
        public TTaskHeroic(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家Id
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int taskId { get; set; }
        /// <summary>
        /// 任务进度
        /// </summary>
        public int pro { get; set; }
        /// <summary>
        /// 是否已领取
        /// </summary>
        public bool isGet { get; set; }
       
    }
}
