using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using PbStore;

namespace GameServer.Module
{
    
    public class StoreMgr
    {
        //广告获取资源列表
        public DictionarySafe<int, int> AdItemsList = new DictionarySafe<int, int>();
        public StoreMgr()
        {
         
        }

        /// <summary>
        /// 创建玩家初始商城信息
        /// </summary>
        public void PlayerCreatStoreData(Player player)
        {
            if (player.storeData != null)
                return;
       
            ResetStoreGift(player);
        }

        /// <summary>
        /// 每日0点刷新广告商品
        /// </summary>
        public void RefreshADitems(Player player)
        {
            if (player.storeData == null)
            {
                PlayerCreatStoreData(player);
            }
            else
            {
                foreach (var p in AdItemsList)
                {
                 
                }
                player.storeData.Update();
            }
            SendPlayerAdTimes(player);
        }

        //重置礼包奖励
        public void ResetStoreGift(Player player)
        {
            if (player.storeData == null)
                return;
         
              
            
        }
        public void SendPlayerAdTimes(Player player,bool send = true)
        {
            SC_store_ADTimes msg = new SC_store_ADTimes();
          
            player.Send(msg);
            if(send)
            {
                SC_store_DailyItems dailyItems = new SC_store_DailyItems();
              
                player.Send(dailyItems);
            }

        }
       

    }
}
