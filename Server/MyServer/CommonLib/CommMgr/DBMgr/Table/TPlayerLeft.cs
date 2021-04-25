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
    public class TPlayerLeft : BaseTable
    {
        /// <summary>   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TPlayerLeft(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>   oid指定ObjectId</summary>
        public TPlayerLeft(ObjectId oid) : base(oid){}

        /// <summary>
        /// 物品id，数量
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int> LevelItems { get; set; }
        /// <summary>
        /// 物品id，数量
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int> FbItems { get; set; }
       
    }
}
