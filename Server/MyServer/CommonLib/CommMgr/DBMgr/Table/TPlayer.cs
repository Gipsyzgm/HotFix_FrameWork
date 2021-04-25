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
        /// 体力
        /// </summary>
        public int power { get; set; }
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
        /// 玩家累计广告次数
        /// </summary>
        public int adNum { get; set; }
        /// <summary>
        /// 是否禁言
        /// </summary>
        public bool isTalk { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public int payMoney { get; set; }
        /// <summary>
        /// 俱乐部贡献值(现用于累计登录天数)
        /// </summary>
        public int contri { get; set; }
        /// <summary>
        /// 上次恢复体力时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? lastAddApTime { get; set; }
        /// <summary>
        /// 是否购买赛季令牌
        /// </summary>
        public bool isSeasonVip { get; set; }
        /// <summary>
        /// 每日获取体力广告观看次数
        /// </summary>
        public int adtimes { get; set; }
        /// <summary>
        /// 每日购买体力次数
        /// </summary>
        public int buyPowerNum { get; set; }
        /// <summary>
        /// 赛季令牌经验
        /// </summary>
        public int seasonExp { get; set; }
        /// <summary>
        /// 升级天赋点次数
        /// </summary>
        public int dowerTimes { get; set; }
        /// <summary>
        /// 可用天赋点数
        /// </summary>
        public int dowerPoint { get; set; }
        /// <summary>
        /// 天赋点等级（9个天赋点）
        /// </summary>
        public int[] dowerLevel { get; set; }
        /// <summary>
        /// 可升级天赋id
        /// </summary>
        public List<int> dowerLeftId { get; set; }
        /// <summary>
        /// 最高章节id（简单）
        /// </summary>
        public int maxChapter { get; set; }
        /// <summary>
        /// 当前章节id（简单）
        /// </summary>
        public int curChapter { get; set; }
        /// <summary>
        /// 当日副本重生次数
        /// </summary>
        public int fbRebirthNum { get; set; }
        /// <summary>
        /// 上次叠加挂机奖励时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? lastAddhangTime { get; set; }
        /// <summary>
        /// 最高章节id（困难）
        /// </summary>
        public int maxhardChapter { get; set; }
        /// <summary>
        /// 当前章节id（困难）
        /// </summary>
        public int curhardChapter { get; set; }
        /// <summary>
        /// 当前副本类型
        /// </summary>
        public int curFbType { get; set; }
        /// <summary>
        /// 是否完成新手指引
        /// </summary>
        public bool isFinishGuide { get; set; }
        /// <summary>
        /// 已获得赛季奖励id 数量
        /// </summary>
        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int,int[]> seasonAwards { get; set; }
        /// <summary>
        /// 赛季id
        /// </summary>
        public int seasonId { get; set; }
        /// <summary>
        /// 赛季过期时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? seasonFreshTime { get; set; }
        /// <summary>
        /// 最高章节最高关卡数
        /// </summary>
        public int maxChpStage { get; set; }
        /// <summary>
        /// 最高困难章节最高关卡数
        /// </summary>
        public int maxHardStage { get; set; }
        /// <summary>
        /// 金币副本剩余次数
        /// </summary>
        public int goldFbLeft { get; set; }
        /// <summary>
        /// 金币副本广告剩余次数
        /// </summary>
        public int goldFbAdLeft { get; set; }
        /// <summary>
        /// 装备副本剩余次数
        /// </summary>
        public int eqFbLeft { get; set; }
        /// <summary>
        /// 装备副本广告剩余次数
        /// </summary>
        public int eqFbAdLeft { get; set; }

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
