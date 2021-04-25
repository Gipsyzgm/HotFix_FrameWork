﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm.DBMgr
{    
    [BsonIgnoreExtraElements]
    public class TLogTenMin : BaseTable
    {
        /// <summary>#10分钟统计   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TLogTenMin(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>#10分钟统计   oid指定ObjectId</summary>
        public TLogTenMin(ObjectId oid) : base(oid){}

        /// <summary>
        /// 统计日期(年月日)
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? time { get; set; }
        /// <summary>
        /// 10分钟累注册累加
        /// </summary>
        public int[] reg { get; set; }
        /// <summary>
        /// 10分钟在线
        /// </summary>
        public int[] online { get; set; }
       
    }
}
