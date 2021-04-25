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
    public class TActivityTask : BaseTable
    {
        /// <summary>活动任务列表   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TActivityTask(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>活动任务列表   oid指定ObjectId</summary>
        public TActivityTask(ObjectId oid) : base(oid){}

        /// <summary>
        /// 模板编号
        /// </summary>
        public int mid { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public int taskId { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int orderid { get; set; }
        /// <summary>
        /// 任务名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 任务说明
        /// </summary>
        public string des { get; set; }
        /// <summary>
        /// 对应的活动Id
        /// </summary>
        public int actId { get; set; }
        /// <summary>
        /// 是否重复领取0:否1:是
        /// </summary>
        public int times { get; set; }
        /// <summary>
        /// 是否每日重置0:否1:是
        /// </summary>
        public int daily { get; set; }
        /// <summary>
        /// 任务完成类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 完成条件参数
        /// </summary>
        public int[] condition { get; set; }
        /// <summary>
        /// 任务奖励
        /// </summary>
        public List<int[]> award { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? endTime { get; set; }
        /// <summary>
        /// 活动生成时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Mark { get; set; }
       
    }
}
