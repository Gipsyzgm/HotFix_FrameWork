using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using PbWar;

namespace GameServer.Module
{
    public class FBMgr
    {
        /// <summary>
        /// 副本阶段信息
        /// </summary>
        public DictionarySafe<ObjectId, DictionarySafe<int, TEventFB>> playerFbList = new DictionarySafe<ObjectId, DictionarySafe<int, TEventFB>>();

        public DictionarySafe<ObjectId, DictionarySafe<int, TEventFB>> playerhardFbList = new DictionarySafe<ObjectId, DictionarySafe<int, TEventFB>>();
        // 
        public FBMgr()
        {
            List<TEventFB> eventFBs = DBReader.Instance.SelectAllList<TEventFB>();
            foreach(TEventFB table in eventFBs)
            {
                if(table.type == 1)
                {
                    if (!playerFbList.TryGetValue(table.pid, out var pairs))
                    {
                        DictionarySafe<int, TEventFB> tmp = new DictionarySafe<int, TEventFB>();
                
                        playerFbList.Add(table.pid, tmp);
                    }
                    else
                    {
                     
                    }
                }
                else
                {
                    if (!playerhardFbList.TryGetValue(table.pid, out var pairs))
                    {
                        DictionarySafe<int, TEventFB> tmp = new DictionarySafe<int, TEventFB>();
                      
                        playerhardFbList.Add(table.pid, tmp);
                    }
                    else
                    {
                       
                    }
                }

            }
        }



        ///// <summary>
        ///// 获取玩家最高副本id
        ///// </summary>
        ///// <param name="player">玩家对象</param>
        public int GetTopStageId(Player player)
        {
            if (!playerFbList.TryGetValue(player.ID, out var pairs))
                return 0; 
            return 0;
        }

        ///// <summary>
        ///// 获取玩家最高困难副本id
        ///// </summary>
        ///// <param name="player">玩家对象</param>
        public int GetTopKnStageId(Player player)
        {
            if (!playerhardFbList.TryGetValue(player.ID, out var pairs))
                return 0;      
            return 0;
        }

        ///// <summary>
        ///// 发送玩家副本信息
        ///// </summary>
        public void SendPlayerFB(Player player)
        {
            if (!playerFbList.ContainsKey(player.ID) && !playerhardFbList.ContainsKey(player.ID))
                return;

            SC_war_fbInfo sendMsg = new SC_war_fbInfo();
            sendMsg.FBStageMax = GetTopStageId(player);
            sendMsg.FBKnStageMax = GetTopKnStageId(player);
            ChapterInfo info = new ChapterInfo();                     
            player.Send(sendMsg);
        }
      
    }
}
