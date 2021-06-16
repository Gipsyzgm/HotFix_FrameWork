using MongoDB.Bson;
using PbBag;
using PbCom;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PbHero;
using CommonLib;
using CommonLib.Comm.DBMgr;

namespace GameServer.Module
{
    /// <summary>
    /// 英雄逻辑管理器
    /// </summary>
    public class HeroMgr
    {
        ///// <summary>全部玩家英雄数据</summary>
        public DictionarySafe<ObjectId, DictionarySafe<int, THero>> playerHeroList = new DictionarySafe<ObjectId, DictionarySafe<int, THero>>();

        public HeroMgr()
        {
            List<THero> heroList = DBReader.Instance.SelectAllList<THero>();

            DictionarySafe<int, THero> playEquipList;
            foreach (THero hero in heroList)
            {
                if (!playerHeroList.TryGetValue(hero.pId, out playEquipList))
                {
                    playEquipList = new DictionarySafe<int, THero>();
                    playerHeroList.Add(hero.pId, playEquipList);
                }
                playEquipList.AddOrUpdate(hero.templId, hero);
            }

        }

        /// <summary>
        /// 玩家登陆检查玩家英雄列表
        /// </summary>
        public void PlayerCheckHeroList(Player player)
        {
            if (!playerHeroList.TryGetValue(player.ID, out var list))
                return;         
        }

        /// <summary>
        /// 创建玩家默认英雄
        /// </summary>
        /// <param name="player">玩家</param>
        public void PlayerCreateInitHero(Player player)
        {
            //PlayerInitConfig config = Glob.config.playerInitConfig;

            //int id = config.initHeros/* * 10 + 1*/;
            //if(!Glob.config.dicItemStarLevel.TryGetValue(id, out var itemStarLevelConfig))
            //{
            //    Logger.LogError(id +"英雄表没有找到id,请检查...");
            //    return;
            //}
            //if (!Glob.config.dicItem.TryGetValue(config.initHeros, out var itemConfig))
            //{
            //    Logger.LogError(config.initHeros + "物品表没有找到id,请检查...");
            //    return;
            //}
            ////默认创建所有英雄
            //foreach(var p in Glob.config.dicHeroList.Values)
            //{
            //    if (p.id == config.initHeros)
            //    {
            //        THero hero = new THero(true);
            //        hero.pId = player.ID;
            //        hero.templId = config.initHeros;
            //        hero.level = 1;
            //        hero.breakLv = 1;              
            //        hero.Insert();
            //        player.placeList.AddOrUpdate(EItemSubTypeEquipIndex.Hero, hero.templId);
            //        Glob.itemMgr.PlayerCreateInitInCut(player, hero, true);
            //        AddTHeroData(player, hero);
            //    }
            //    else
            //    {
            //        if(p.id % 10 == 1)
            //        {
            //            THero hero = new THero(true);
            //            hero.pId = player.ID;
            //            hero.templId = p.id;
            //            hero.level = 1;
            //            hero.breakLv = 1;              
            //            hero.Insert();
            //            AddTHeroData(player, hero);
            //        }

            //    }
               
            //}

            
        }

        /// <summary>
        /// 给玩家创建新英雄
        /// </summary>
        /// <param name="player"></param>
        /// <param name="templId">英雄模板Id</param>
        public int PlayerAddNewHero(Player player, int templId, Enum_bag_itemsType newType = Enum_bag_itemsType.BiNone)
        {

            int id = templId /** 10 + 1*/;
            int num = 0;
            if (!Glob.config.dicItem.TryGetValue(templId, out var itemConfig))
            {
                Logger.LogError(templId + "物品表没有找到id,请检查...");
                return -1;
            }

            if(!player.heroList.TryGetValue(templId,out var heros))
            {
                Logger.LogError(templId + "英雄列表没有找到id");
                return -1;
            }
          
            return num;
         
        }


        /// <summary>
        /// 增加英雄数据到集合，并创建英雄实体
        /// </summary>
        /// <param name="player"></param>
        /// <param name="hero"></param>
        public void AddTHeroData(Player player, THero hero)
        {
            if (!playerHeroList.TryGetValue(player.ID, out var list))
            {
                DictionarySafe<int, THero> pairs = new DictionarySafe<int, THero>();
                pairs.Add(hero.templId,hero);
                playerHeroList.Add(player.ID, pairs);
            }
            else
            {
                if(!list.TryGetValue(hero.templId,out var thero))
                {
                    list.Add(hero.templId, hero);
                }
               else
                {
                    thero.Update();
                }
            }
            PlayerAddHero(player, hero);
        }

        /// <summary>
        /// 读取玩家英雄数据
        /// </summary>
        /// <param name="player"></param>
        public void ReadPlayerHero(Player player)
        {
            if (playerHeroList.TryGetValue(player.ID, out var list))
            {
                foreach (var hero in list)
                {
                    PlayerAddHero(player, hero.Value);
                }
            }
        }


        /// <summary>
        /// 玩家新英雄实体
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="thero">英雄数据</param>
        public void PlayerAddHero(Player player, THero thero)
        {
            Hero hero = new Hero(player, thero);
            if (!hero.IsSucceed)
                return;
            player.heroList.AddOrUpdate(hero.Data.templId, hero);

//             if(player.PayPackPlayerData!=null)
//                 player.PayPackPlayerData.heroAddNum(hero);
        }
              

        /// <summary>
        /// 发送玩家英雄数据
        /// </summary>
        /// <param name="player"></param>
        //public void SendPlayerHero(Player player)
        //{
        //    SC_hero_list list = new SC_hero_list();
        //    foreach (Hero item in player.heroList.Values)
        //    {
        //        list.List.Add(item.GetHeroInfo());                
        //    }
        //    player.Send(list);
            
        //}
    }
}
