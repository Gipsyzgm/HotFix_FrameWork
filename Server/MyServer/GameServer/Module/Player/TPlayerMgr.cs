using MongoDB.Bson;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using CommonLib;
using CommonLib.Comm.DBMgr;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家数据管理
    /// </summary>
    public class TPlayerMgr
    {
        ///// <summary>玩家数据列表</summary>
        public DictionarySafe<ObjectId, TPlayer> playerDataList;

        ///// <summary>玩家名对应玩家Id</summary>
        public DictionarySafe<PlayerUniqueName,ObjectId> userNamesList = new DictionarySafe<PlayerUniqueName, ObjectId>();

        public TPlayerMgr()
        {
            playerDataList = DBReader.Instance.SelectAll<TPlayer>();

            PlayerUniqueName playerUName;
            foreach (TPlayer pl in playerDataList.Values)
            {

                if (Glob.tAccountMgr.accountList.ContainsKey(pl.id))
                {
                    playerUName = new PlayerUniqueName(pl.name, Glob.tAccountMgr.accountList[pl.id].serverId);
                    if (userNamesList.ContainsKey(playerUName))
                        Logger.LogWarning("PlayerMgr.userNamesList 有相同玩家名:" + playerUName.ToString());
                    else
                        userNamesList.Add(playerUName, pl.id);
                }
                else
                {
                    Logger.LogWarning("PlayerMgr.payerList 找到不到对应账号信息:id:" + pl.id.ToString());
                }
            }
        }
        
        /// <summary>
        /// 判断玩家名是否重复
        /// </summary>
        /// <param name="name">玩家名</param>
        /// <returns></returns>
        public bool CheckNameExist(string name)
        {
            //PlayerUniqueName uname = new PlayerUniqueName(name, serverid);
            //if (userNamesList.ContainsKey(uname))
            //    return true;
            //else
            //{
            //    return false;
            //}
            FilterDefinition<TPlayer> filter1 = Builders<TPlayer>.Filter.Eq(t => t.name, name);
            List<TPlayer> list = MongoDBHelper.Instance.SelectFilter(filter1);
            if (list != null && list.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 获取玩家名
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public string GetPlayerName(ObjectId playerId)
        {
            //TPlayer player;
            //if (playerDataList.TryGetValue(playerId, out player))
            //    return player.name;
            return string.Empty;

            //RPlayer player = Glob.redis.PlayerInfo.Get<RPlayer>(playerId.ToString());
            //if (player == null)
            //    return string.Empty;
            //return player.name;
        }

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        //public ELangType GetPlayerLang(ObjectId playerId)
        //{
        //    TPlayer player;
        //    if (playerDataList.TryGetValue(playerId, out player))
        //        return (ELangType)player.lang;
        //    return Glob.langMgr.defaultType;
        //}


        ///// <summary>
        ///// 判断账号是否存在
        ///// 平台Id,服务id,平台类型相同则表示玩家账号信息已存在
        ///// </summary>
        ///// <returns></returns>
        //public bool CheckAccountExist(int platform,string platformid,int serverid)
        //{
        //    AccountUniqueID accUID = new AccountUniqueID(platform, platformid);
        //    if (Glob.tAccountMgr.accUIDList.ContainsKey(accUID))
        //        return true;
        //    return false;   
        //}

        /// <summary>
        /// 创建角色数据
        /// </summary>
        /// <param name="acc"></param>
        public TPlayer CreagePlayerData(TAccount acc, string playerName)
        {
           //PlayerInitConfig config = Glob.config.playerInitConfig;
            //创建角色数据
            TPlayer play = new TPlayer(acc.id)
            {
                name = /*playerName*/acc.pfId.ToString(),
                //icon = Glob.config.playerInitConfig.initIcon,
                //level = config.initLevel,
                //gold = config.initGold,
                //ticket = config.initTicket,

                lastAddApTime = DateTime.Now,
              
            };
            play.Insert();
          
            PlayerUniqueName playerUName = new PlayerUniqueName(play.name, acc.serverId);
            playerDataList.Add(play.id, play);
            userNamesList.Add(playerUName, play.id);
            return play;
        }

        /// <summary>
        /// 角色改名
        /// </summary>
        /// <param name="player">角色对象</param>
        /// <param name="strOld">老名字</param>
        /// <param name="strNew">新名字</param>
        public void ChangePlayerName(Player player, string strOld, string strNew)
        {
        
            player.Data.name = strNew;
        
        }
    }
}
