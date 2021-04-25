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
    public class TItemEquip : BaseTable
    {
        private static readonly object o = new object();
        static int _identityShortId = 0;
        private static Dictionary<int, ObjectId> shortObjidList = new Dictionary<int, ObjectId>();
        private static Dictionary<ObjectId, int> objShortIdList = new Dictionary<ObjectId, int>();
        /// <summary>物品装备表   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TItemEquip(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>物品装备表   oid指定ObjectId</summary>
        public TItemEquip(ObjectId oid) : base(oid){}

        /// <summary>
        /// 物品Id
        /// </summary>
        public override ObjectId id
        {
            get { return base.id; }
            protected set
            {
                lock (o)
                {
                    if (base.id == ObjectId.Empty)
                    {
                        base.id = value;
                        if(!objShortIdList.TryGetValue(base.id,out _shortId))                        
                        {
                            _shortId = ++_identityShortId;
                            shortObjidList.Add(_identityShortId, base.id);
                            objShortIdList.Add(base.id, _identityShortId);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 所属玩家Id
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 模板Id
        /// </summary>
        public int templId { get; set; }
        /// <summary>
        /// 装备等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 装备品阶等级
        /// </summary>
        public int breakLv { get; set; }
        /// <summary>
        /// 装备位置（0 未装备）
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// 碎片数量
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 装备经验
        /// </summary>
        public int exp { get; set; }

        /// <summary>
        /// 简短Id转ObjectId
        /// </summary>
        /// <param name="shortid">简短Id</param>
        /// <returns></returns>
        public static ObjectId ToObjectId(int shortid)
        {
            ObjectId oid = ObjectId.Empty;
            shortObjidList.TryGetValue(shortid, out oid);
            return oid;
        }
        /// <summary>
        /// ObjectId转简短Id
        /// </summary>
        /// <param name="oid">ObjectId</param>
        /// <returns></returns>
        public static int ToShortId(ObjectId oid)
        {
            int shortid = 0;
            objShortIdList.TryGetValue(oid, out shortid);
            return shortid;
        }
    }
}
