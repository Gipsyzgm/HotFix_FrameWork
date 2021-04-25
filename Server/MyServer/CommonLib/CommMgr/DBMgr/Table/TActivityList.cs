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
    public class TActivityList : BaseTable
    {
        /// <summary>活动列表   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TActivityList(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>活动列表   oid指定ObjectId</summary>
        public TActivityList(ObjectId oid) : base(oid){}

        /// <summary>
        /// 活动编号
        /// </summary>
        public int mid { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int orderid { get; set; }
        /// <summary>
        /// 活动名
        /// </summary>
        public string name { get; set; }
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
        /// 活动图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 活动描述
        /// </summary>
        public string des { get; set; }
        /// <summary>
        /// 功能开放参数
        /// </summary>
        public string funOpen { get; set; }
        /// <summary>
        /// 前往类型 0无 1:打开UI
        /// </summary>
        public int goType { get; set; }
        /// <summary>
        /// 前往字符串参数
        /// </summary>
        public string goStrArg { get; set; }
        /// <summary>
        /// 前往Int参数
        /// </summary>
        public int[] goIntArg { get; set; }
       
    }
}
