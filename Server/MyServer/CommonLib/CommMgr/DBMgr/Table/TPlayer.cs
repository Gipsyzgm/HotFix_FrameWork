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
    public class TPlayer : BaseTable
    {
        private static readonly object o = new object();
        static int _identityShortId = 0;
        private static Dictionary<int, ObjectId> shortObjidList = new Dictionary<int, ObjectId>();
        private static Dictionary<ObjectId, int> objShortIdList = new Dictionary<ObjectId, int>();
        /// <summary>玩家表   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public TPlayer(bool isCreadId = false) : base(isCreadId){}
        
        /// <summary>玩家表   oid指定ObjectId</summary>
        public TPlayer(ObjectId oid) : base(oid){}

        /// <summary>
        /// 玩家Id,与账号id保持一致
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
        /// 玩家角色名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 玩家头像[头像、背景、角标]
        /// </summary>
        public int[] icon { get; set; }
        /// <summary>
        /// 玩家等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 玩家当前经验
        /// </summary>
        public int exp { get; set; }
        /// <summary>
        /// VIP等级
        /// </summary>
        public int vipLv { get; set; }
        /// <summary>
        /// VIP经验
        /// </summary>
        public int vipExp { get; set; }
        /// <summary>
        /// 记录选择的语言类型
        /// </summary>
        public int lang { get; set; }
        /// <summary>
        /// 金币
        /// </summary>
        public long gold { get; set; }
        /// <summary>
        /// 点券
        /// </summary>
        public int ticket { get; set; }
        /// <summary>
        /// 食物
        /// </summary>
        public int food { get; set; }
        /// <summary>
        /// 石头
        /// </summary>
        public int stone { get; set; }
        /// <summary>
        /// 人口
        /// </summary>
        public int people { get; set; }
        /// <summary>
        /// 副本行动点数
        /// </summary>
        public int actionPoint { get; set; }
        /// <summary>
        /// 竞技行动点数
        /// </summary>
        public int arenaPoint { get; set; }
        /// <summary>
        /// 公会Boss行动点
        /// </summary>
        public int bossPoint { get; set; }
        /// <summary>
        /// 上次恢复副本行动点时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? lastAddApTime { get; set; }
        /// <summary>
        /// 上次恢复副本行动点时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? lastAddArenaPTime { get; set; }
        /// <summary>
        /// 上次恢复公会行动点时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? lastAddBpTime { get; set; }
        /// <summary>
        /// 同时可升级建筑数量
        /// </summary>
        public int buildLvupNum { get; set; }
        /// <summary>
        /// 竞技积分
        /// </summary>
        public int arenaScore { get; set; }
        /// <summary>
        /// 剩余签到奖励次数
        /// </summary>
        public int singInNum { get; set; }
        /// <summary>
        /// 已领在线奖励档位
        /// </summary>
        public int onlineAwardId { get; set; }
        /// <summary>
        /// 领取奖励在线时间(秒)(下线时记录)
        /// </summary>
        public int onlineAwardTime { get; set; }
        /// <summary>
        /// 已完成的指引Id
        /// </summary>
        public List<int> guides { get; set; }
        /// <summary>
        /// 完成指引最大步骤Id
        /// </summary>
        public int guideStep { get; set; }
        /// <summary>
        /// 俱乐部贡献值(现用于累计登录天数)
        /// </summary>
        public int contri { get; set; }
        /// <summary>
        /// 副本信息[最大通关StageId]
        /// </summary>
        public List<int> fbInfo { get; set; }
        /// <summary>
        /// 工坊已开启的生产项
        /// </summary>
        public List<int> forgeIds { get; set; }
        /// <summary>
        /// 训练场已开启的生产项
        /// </summary>
        public List<int> trainingIds { get; set; }
        /// <summary>
        /// 铁匠铺已开启的生产项
        /// </summary>
        public List<int> smithyIds { get; set; }
        /// <summary>
        /// 玩家改名次数
        /// </summary>
        public int renameNum { get; set; }
        /// <summary>
        /// 上次每日召唤时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? lastSummonTime { get; set; }
        /// <summary>
        /// 四种召唤的次数(每10次出高级产出)
        /// </summary>
        public int[] summonNum { get; set; }
        /// <summary>
        /// 主城等级
        /// </summary>
        public int cityLv { get; set; }
        /// <summary>
        /// 是否禁言
        /// </summary>
        public bool isTalk { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public int payMoney { get; set; }
        /// <summary>
        /// 最高连击
        /// </summary>
        public int maxCombo { get; set; }
        /// <summary>
        /// 额外增加的数量[0额外英雄卡槽]
        /// </summary>
        public int[] extraNum { get; set; }
        /// <summary>
        /// 是否评分 0否 1是
        /// </summary>
        public int estimate { get; set; }

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
