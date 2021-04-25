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
    public class TMail : BaseTable
    {
        private static readonly object o = new object();
        static int _identityShortId = 0;
        private static Dictionary<int, ObjectId> shortObjidList = new Dictionary<int, ObjectId>();
        private static Dictionary<ObjectId, int> objShortIdList = new Dictionary<ObjectId, int>();
        /// <summary>邮件信息   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TMail(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>邮件信息   oid指定ObjectId</summary>
        public TMail(ObjectId oid) : base(oid){}

        /// <summary>
        /// 唯一Id
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
        /// 玩家Id(群发邮件为空)
        /// </summary>
        public ObjectId pId { get; set; }
        /// <summary>
        /// 邮件类型(1群发邮件,2个人邮件)
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 邮件消息内容
        /// </summary>
        public string cont { get; set; }
        /// <summary>
        /// 附件物品ID
        /// </summary>
        public int[] items { get; set; }
        /// <summary>
        /// 附件物品数量(长度和items对应)
        /// </summary>
        public int[] nums { get; set; }
        /// <summary>
        /// 是否打开(打开 个人邮件用)
        /// </summary>
        public bool isOpen { get; set; }
        /// <summary>
        /// 是否已领取附件(领取 个人邮件用)
        /// </summary>
        public bool isGet { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? sTime { get; set; }
        /// <summary>
        /// 是否为gm邮件
        /// </summary>
        public bool isGM { get; set; }

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
