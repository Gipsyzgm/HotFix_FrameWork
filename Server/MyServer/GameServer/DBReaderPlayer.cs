using CommonLib;
using CommonLib.Comm.DBMgr;
using GameServer.Module;
using GameServer.XGame.Module;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    /// <summary>
    /// 读取用户数据
    /// </summary>
    public partial class DBReaderPlayer
    {
        private static readonly DBReaderPlayer instance = new DBReaderPlayer();
        public static DBReaderPlayer Instance => instance;

        /// <summary>玩家数据临时缓存</summary>
        private DictionarySafe<ObjectId, Player> playerTempDataCache = new DictionarySafe<ObjectId, Player>();

        public bool Initialize()
        {           
            if (MongoDBHelper.Instance.IsConnect)
                return true;
            return false;
        }
        
        /// <summary>
        /// 读取玩家数据(上线)
        /// </summary>
        /// <param name="player"></param>
        public void ReadPlayerData(Player player)
        {
                 
            readPlayerPay(player);                //VIP充值信息
            readPlayerCDKey(player);            //CDKey信息
            readPlayerBonus(player);            //读取玩家福利数据
            readActivity(player);
            readPlayerHero(player);        
            readPlayerEquip(player);            
            readStore(player);
            readPlayerMail(player);
            readPlayerItem(player);               //读取玩家物品数据,放后面
        }

        public TPlayer ReadPlayerInfo(ObjectId id)
        {
            TPlayer tp = MongoDBHelper.Instance.Select<TPlayer>(id);
            return tp;
        }
        

        /// <summary>
        /// 读取其他玩家数据(只查角色、英雄、装备、联盟)
        /// </summary>
        /// <param name="player"></param>
        /// <param name="getRecord">是否读取战报</param>
        public Player ReadPlayerTempData(ObjectId playerId, bool getRecord = false)
        {
            //if (playerTempDataCache.TryGetValue(playerId, out Player p))
            //{
            //    if (!p.CheckDBTimestamp)
            //        return p;
            //}
            TAccount account = MongoDBHelper.Instance.Select<TAccount>(playerId);
            TPlayer tp = ReadPlayerInfo(playerId);
            Player player = new Player(tp, account);
            readPlayerHero(player);
            //readPlayerBuild(player);
            readPlayerEquip(player);
            //readPlayerClub(player);
            //readPlayerArena(player, getRecord);
            //readPlayerLeague(player, getRecord);
            player.IsTemporary = true;
            //player.DBTimestamp = DateTime.Now.ToTimestamp();
            //playerTempDataCache.AddOrUpdate(playerId, player);
            return player;
        }

        ///// <summary>
        ///// 从缓存中读取数据
        ///// </summary>
        public void readCacheData(Player player)
        {
            //读取玩家英雄数据
            Glob.hreoMgr.ReadPlayerHero(player);

            //读取玩家装备数据
            Glob.itemMgr.ReadPlayerEquip(player);
            //读取商城数据
            readStore(player);
        }

     

        /// <summary>读取玩家英雄数据</summary>
        public void readPlayerHero(Player player)
        {
            List<THero> herolist = MongoDBHelper.Instance.Select<THero>(t => t.pId == player.ID);
            foreach (THero thero in herolist)
                Glob.hreoMgr.PlayerAddHero(player, thero);
 
        }

        /// <summary>读取玩家装备数据</summary>
        public void readPlayerEquip(Player player)
        {
            List<TItemEquip> equiplist = MongoDBHelper.Instance.Select<TItemEquip>(t => t.pId == player.ID);
            foreach (TItemEquip thero in equiplist)
                Glob.itemMgr.PlayerAddEquip(player, thero);

            List<THero> herolist = MongoDBHelper.Instance.Select<THero>(t => t.pId == player.ID);
            foreach (THero thero in herolist)
            {
               
                
            }
        }

        /// <summary>读取玩家物品数据</summary>
        public void readPlayerItem(Player player)
        {
            List<TItemProp> propList = MongoDBHelper.Instance.Select<TItemProp>(t => t.pId == player.ID);
            foreach (TItemProp thero in propList)
                Glob.itemMgr.PlayerAddProp(player, thero);
        }

        ///// <summary>
        ///// 读取副本挑战赛
        ///// </summary>
        //private void readFBRach(Player player)
        //{
        //    //玩家章节数据
        //    List<TFBRach> list = MongoDBHelper.Instance.Select<TFBRach>(t => t.pId == player.ID);
        //    foreach (TFBRach ch in list)
        //    {
        //        Glob.fbMgr.PlayerAddFBRachItem(player, ch);
        //    }
        //}

        /// <summary>
        /// 充值，月卡信息，VIP礼包购买
        /// </summary>
        /// <param name="player"></param>
        private void readPlayerPay(Player player)
        {
            List<TPay> payList = MongoDBHelper.Instance.Select<TPay>(t => t.id == player.ID);
            if (payList.Count > 0)
                player.payData = payList[0];
        }

        /// <summary>
        /// 读取商城购买信息
        /// </summary>
        /// <param name="player"></param>
        private void readStore(Player player)
        {
            List<TStore> list = MongoDBHelper.Instance.Select<TStore>(t => t.id == player.ID);
            if (list.Count > 0)
                player.storeData = list[0];
        }


        /// <summary>
        /// CDKey信息
        /// </summary>
        /// <param name="player"></param>
        private void readPlayerCDKey(Player player)
        {
            List<TCDKeyPlayer> cdkeyList = MongoDBHelper.Instance.Select<TCDKeyPlayer>(t => t.id == player.ID);
            if (cdkeyList.Count > 0)
                player.cdkeyData = cdkeyList[0];
        }
       
        /// <summary>
        /// 玩家福利数据
        /// </summary>
        /// <param name="player"></param>
        private void readPlayerBonus(Player player)
        {
            List<TBonus> list = MongoDBHelper.Instance.Select<TBonus>(t => t.id == player.ID);
            if (list.Count > 0)
                player.bonusData = list[0];
        }

  

        /// <summary>
        /// 活动数据
        /// </summary>
        /// <param name="player"></param>
        private void readActivity(Player player)
        {
            List<TActivity> list = MongoDBHelper.Instance.Select<TActivity>(t => t.pid == player.ID);
            player.SetActivityList(list);
        }

    
        private void readPlayerMail(Player player)
        {
            FilterDefinitionBuilder<TMail> builderFilter = Builders<TMail>.Filter;
            //个人邮件
            FilterDefinition<TMail> filter1 = builderFilter.And
            (
                builderFilter.Eq(t => t.pId, player.ID), 
                builderFilter.Eq(t => t.type, 2),
                builderFilter.Gte(t => t.sTime, DateTime.Now.AddDays(-30))
            );
            //全服邮件
            FilterDefinition<TMail> filter2 = builderFilter.And
                (
                    builderFilter.Eq(t => t.type, 1),
                    builderFilter.Gte(t => t.sTime, DateTime.Now.AddDays(-30))
                );

            List<TMail> mailList = MongoDBHelper.Instance.SelectFilter<TMail>(builderFilter.Or(filter1, filter2));
            //mailList = mailList.OrderByDescending(t => t.sTime).ToList();

            FilterDefinitionBuilder<TMailSub> builderSub = Builders<TMailSub>.Filter;
            //全服邮件子项
            FilterDefinition<TMailSub> filter3 = builderSub.Eq(t => t.pId, player.ID);
            List<TMailSub> mailSubList = MongoDBHelper.Instance.SelectFilter<TMailSub>(filter3);

            player.SetPlayerMail(mailList, mailSubList);
        }

        /// <summary>
        /// 联盟数据
        /// </summary>
        /// <param name="player"></param>
//         public void readPlayerBlackList(Player player)
//         {
//             player.BlackList = MongoDBHelper.Instance.Select<TBlackList>(player.ID);
//             if (player.BlackList != null)
//             {
//                 foreach (var id in player.BlackList.blackList)
//                     player.ChatBlackList.Add(id.ToString());
//             }
//         }

    }
}
