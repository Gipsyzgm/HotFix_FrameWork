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
    public class TStore : BaseTable
    {
        /// <summary>商城购买次数   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TStore(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>商城购买次数   oid指定ObjectId</summary>
        public TStore(ObjectId oid) : base(oid){}

        /// <summary>
        /// 已购买Id,<购买数量,首次购买日期>
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int[]> buyInfos { get; set; }
        /// <summary>
        /// 广告物品Id，<当日广告次数,当日领取次数>
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int[]> adItems { get; set; }
        /// <summary>
        /// 礼包id，<礼包内物品id，数量>
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,List<int[]>> gifts { get; set; }
       
    }
}
