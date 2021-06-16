using CommonLib;
using CommonLib.Comm;
using CommonLib.Comm.DBMgr;
using GameServer.Net;
using Google.Protobuf;
using PbError;
using PbPlayer;
using System;
using System.Collections.Generic;

namespace GameServer.Module
{
    /// <summary>
    /// 活动跃玩家实体信息
    /// </summary>
    public partial class Player : BaseEntity<TPlayer>
    {
        /// <summary>玩家账号数据</summary>
        public TAccount AccountData { get; protected set; }

        /// <summary>客户端Session,登录成功时设置</summary>        
        public ClientToGameServerMessage Session { get; set; }

        /// <summary>客户端Session,登录成功时设置</summary>        
        public int SessionID { get; set; }
        /// <summary>
        /// 玩家是否在线
        /// </summary>
        public bool IsOnline = false;

        /// <summary>
        /// 玩家等级
        /// </summary>
        public int Level => Data.level;

        /// <summary>
        /// 玩家选择的语言类型
        /// </summary>
        public ELangType Lang => (ELangType)Data.lang;

       // public bool IsDispose = false;

        /// <summary>
        /// 玩家Vip等级,体验卡等级最大值
        /// </summary>
        public int VipLevel
        {
            get
            {
                return Data.vipLv;
            }
        }
        
        /// <summary>
        /// CDKey使用数据
        /// </summary>
        public TCDKeyPlayer cdkeyData;

        /// <summary>
        /// 月卡，充值数据
        /// </summary>
        public TPay payData;

        /// <summary>
        /// 商城购买记录
        /// </summary>
        public TStore storeData;

        /// <summary>
        /// 玩家福利信息
        /// </summary>
        public TBonus bonusData;

     
        /// <summary>
        /// 上次心跳包时间
        /// </summary>
        public DateTime lastBeartbeatTime;

        /// <summary>
        /// 上次领取在线奖励时间
        /// </summary>
        public DateTime lastGetOnlineAwardTime;

        /// <summary>
        /// 战力是否发生改变
        /// </summary>
        public bool IsFCChange = false;

        /// <summary>
        /// 上次发送聊天消息的时间
        /// </summary>
        public DateTime LastChatSendTime = DateTime.MinValue;

        /// <summary>
        /// 上次通关时间
        /// </summary>
        public DateTime LastBattleLevelTime = DateTime.MinValue;



        /// <summary>玩家指引数据</summary>
        public GuideData Guide;
        
  

        /// <summary>
        /// 上次查询时间戳(临时缓存用，短时间内不用再次查询DB)
        /// </summary>
        public int DBTimestamp;


        /// <summary>
        /// 是否为临时对像
        /// </summary>
        public bool IsTemporary = false;

        public int[] PkType = new int[3];
        public Player(TPlayer player, TAccount account)
        {
            Data = player;
            AccountData = account;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data"></param>
        public void Send<T>(T data) where T : IMessage
        {
            if (Session != null)
                Session.Send(data);
            if(data != null)
                data = default(T);
        }

        /// <summary>
        /// 给玩家返回一个指定消息的错误码
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="msg">错误文字</param>
        public void SendError(Type msgType, string msg)
        {
            ushort protocol = ClientToGameServerProtocol.Instance.GetProtocolByType(msgType);
            if (protocol == 0)
                return;
            SC_error_code err = new SC_error_code();
            err.Protocol = protocol;
            err.Msg = msg;
            Send(err);
        }

        /// <summary>
        /// 保存玩家Data信息
        /// 玩家信息一般10秒执行一次保存,退出时立即保存
        /// </summary>
        public void SaveData()
        {
            Data.Update(false);
        }


        /// <summary>
        /// 检查上次查询时间是否过期
        /// </summary>
        public bool CheckDBTimestamp
        {
            get
            {
                if (DateTime.Now.ToTimestamp() - DBTimestamp >= 300)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// 更新玩家联盟数据（收到Cross联盟信息改变的通知）
        /// </summary>
        public void ReGetClub()
        {
            //DBReaderPlayer.Instance.readPlayerClub(this);
        }

        public override void Dispose()
        {
            IsDispose = true;
            if (playerTimer != null)
            {
                playerTimer.Stop();
                playerTimer.Dispose();
                playerTimer = null;
            }
   
            payData = null;
            cdkeyData = null;
            bonusData = null;
            
            AccountData = null;

            
            equipList.Clear();
            equipList = null;
            
            propList.Clear();
            propList = null;

            placeList.Clear();
            placeList = null;

            if (Guide != null)
            {
                Guide.Dispose();
                Guide = null;
            }


            heroList.Clear();
            heroList = null;

            PlayerMailList.Clear();
            PlayerMailList = null;
            MassMailList.Clear();
            MassMailList = null;
            MassMailSubList.Clear();
            MassMailSubList = null;


            activityList.Clear();
            activityList = null;
            activityListener.Dispose();
            activityListener = null;


            base.Dispose();
            Data = null;
        }
    }
}
