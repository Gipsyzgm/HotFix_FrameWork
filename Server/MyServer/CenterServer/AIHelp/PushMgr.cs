using CenterServer.Player;
using CommonLib;
using MongoDB.Bson;
using PbCenterPlayer;
using PbCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterServer.Module
{
    /// <summary>
    /// AIHelp推送管理
    /// </summary>
    public class PushMgr
    {
        //已收到AIHelp推送消息 ，未发送给玩家的
        public DictionarySafe<string, List<One_PushMsg>> noSendPushMsgList = new DictionarySafe<string, List<One_PushMsg>>();

        public PushMgr()
        {

        }

        /// <summary>
        /// 增加到未发送推送通知列表
        /// </summary>
        /// <param name="order"></param>
        public void AddNoSendMsg(One_PushMsg msg)
        {
            List<One_PushMsg> list;
            if (!noSendPushMsgList.TryGetValue(msg.PId, out list))
            {
                list = new List<One_PushMsg>();
                noSendPushMsgList.Add(msg.PId, list);
            }
            list.Add(msg);
        }

        /// <summary>
        /// 玩家登录，未发送的推送消息进行补发
        /// </summary>
        /// <param name="player"></param>
        public void NoSendPushMsg(PlayerData player)
        {
            List<One_PushMsg> list; 
            List<One_PushMsg> sucId = new List<One_PushMsg>();
            if (noSendPushMsgList.TryGetValue(player.Id.ToString(), out list))
            {
                foreach (One_PushMsg msg in list)
                {
                    sucId.Add(msg);
                }
                PlayerSendPushMsg(player, sucId);
                if (sucId.Count > 0)
                {
                    foreach (One_PushMsg one in sucId)
                        list.Remove(one);
                    Logger.LogWarning($"玩家[{player.Id}] 未发送的推送已经补发,共{sucId.Count }条");
                }
            }
        }

        /// <summary>
        /// 通知GS发送推送通知
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <param name="config">推送消息</param>
        public void PlayerSendPushMsg(PlayerData player, List<One_PushMsg> msg)
        {
            SC_Center_AIHelpPush sendMsg = new SC_Center_AIHelpPush();
            sendMsg.PlayerId = player.Id.ToString();
            sendMsg.Info.Add(msg);
            player.Send(sendMsg);
        }
    }
}
