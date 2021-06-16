using CommonLib;
using MongoDB.Bson;
using MongoDB.Driver;
using PbCom;
using PbRank;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.Module
{
    /// <summary>
    /// 排行管理
    /// </summary>
    public class RankMgr
    {
        /// <summary>
        /// 排行数据分类
        /// </summary>
        public Dictionary<Enum_Rank_type, List<RankItem>> rankList = new Dictionary<Enum_Rank_type, List<RankItem>>();

        /// <summary>
        /// 排行数据消息缓存
        /// </summary>
        public Dictionary<Enum_Rank_type, SC_Rank_List> rankMsgList = new Dictionary<Enum_Rank_type, SC_Rank_List>();

        /// <summary>
        /// 俱乐部排名
        /// </summary>
        public Dictionary<ObjectId, int> clubRankList = new Dictionary<ObjectId, int>();
        public RankMgr()
        {
            
        }

        /// <summary>
        /// 向Cross请求排行榜数据
        /// </summary>
        public void SendCrossGetRankMsg()
        {
            //CS_Cross_RankList crossMsg = new CS_Cross_RankList();
            //Glob.net.gameToCrossClient.Send(crossMsg);

        }
                
        /// <summary>
        /// 发送玩家消息对象（查看其他玩家信息用）
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public void SendOtherPlayerMsg(Player play, ObjectId otherId, Enum_Look_type look_Type = Enum_Look_type.LkNormal)
        {
            SC_rank_lookPlayer msg = new SC_rank_lookPlayer();
            Player player = null;
            bool isDB = false;
            if (Glob.playerMgr.onlinePlayerList.TryGetValue(otherId, out Player p))
                player = p;
            else
            {
                player = DBReaderPlayer.Instance.ReadPlayerTempData(otherId);
                isDB = true;
            }

            //Glob.rankMgr.rankMsgList.TryGetValue(Enum_Rank_type.RkArena, out SC_Rank_List rankList);
            //rankList.List.ToList().ForEach(t =>
            //{
            //    if (t.PID == player.ID.ToString())
            //    {
            //        player.Arena.Rank = player.Arena == null ? 0 : t.Rank;
            //    }
            //});
            
            One_PlayerInfo one = new One_PlayerInfo();
            one.PID = player.ID.ToString();
            one.Name = player.Data.name;
            one.Level = player.Data.level;
//             one.Score = player.Arena == null ? 0 : player.Arena.Score;
//             one.Rank = player.Arena == null ? 0 : player.Arena.Rank;



            if (look_Type == Enum_Look_type.LkLeague)
            {
                //one.Score = player.League == null ? 0 : player.League.Score;
                //one.Rank = player.League == null ? 0 : player.League.Rank;
            }
            one.Icon.Add(player.Data.icon);
//             Hero[] heroList = player.heroTeam.GetDefenseTeam();
//             if (look_Type == Enum_Look_type.LkLeague)
//                 heroList = player.heroTeam.GetLeagueDefTeam();
//             if (look_Type == Enum_Look_type.LkClubWar)
//                 heroList = player.heroTeam.GetClubWarDefTeam();
            //One_heroInfo oneHero;
//             foreach (Hero hero in heroList)
//             {
//                 if (hero == null)
//                 {
//                     one.HeroIds.Add(0);
//                     continue;
//                 }
//                 one.Heros.Add(hero.GetHeroInfo());
//                 one.HeroIds.Add(hero.SID);
//             }
//            One_bag_equip oneEquip;
//             ItemEquip[] equipList = player.heroTeam.GetTeamEquip(player.heroTeam.Data.defenseTeam);
//             if (look_Type == Enum_Look_type.LkLeague)
//                 equipList = player.heroTeam.GetLeagueDefEquip();
//             if (look_Type == Enum_Look_type.LkClubWar)
//                 equipList = player.heroTeam.GetClubWarDefEquip();
//             foreach (ItemEquip item in equipList)
//             {
//                 if (item != null && player.equipList.ContainsKey(item.SID))
//                 {
//                     oneEquip = item.GetEquipInfo();
//                     one.Equips.Add(oneEquip);
//                 }
//                 one.EquipsIds.Add(item == null ? 0 : item.SID);
//             }
            //if (player.Club != null && player.ClubMember != null)
            //{
            //    one.ClubID = player.Club.id.ToString();
            //    one.ClubName = player.Club.name;
            //    one.ClubRole = player.ClubMember.role;
            //    one.ClubIcon = player.Club.icon;
            //    one.ClubIconBg = player.Club.iconbg;
            //    one.ClubJoinTime = player.ClubMember.createTime.ToTimestamp();
            //}
            //one.ArenaScore = player.Arena == null ? 0 : player.Arena.Score;
            one.LastTime = 0;
            if (!Glob.playerMgr.onlinePlayerList.ContainsKey(player.ID))
            {
                one.LastTime = player.AccountData.lastLogoutDate > player.AccountData.lastLoginDate ?
                    player.AccountData.lastLogoutDate.ToTimestamp() : player.AccountData.lastLoginDate.ToTimestamp();
            }
            msg.PlayerInfo = one;
            msg.LookType = look_Type;
            if (isDB)
            {
                player.Dispose();
                player = null;
            }
            play.Send(msg);
        }
        

        /// <summary>
        /// 返回一个Player的消息对象
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public One_PlayerInfo GetPlayerMsg(Player player)
        {
            One_PlayerInfo one = new One_PlayerInfo();
            one.PID = player.ID.ToString();
            one.Name = player.Data.name;
            one.Level = player.Data.level;
//             one.Score = player.Arena == null ? 0 : player.Arena.Score;
//             one.Rank = player.Arena == null ? 0 : player.Arena.Rank;
            one.Icon.Add(player.Data.icon);
            //Hero[] heroList = player.heroTeam.GetDefenseTeam();
//             foreach (Hero hero in heroList)
//             {
//                 if (hero == null)
//                 {
//                     one.HeroIds.Add(0);
//                     continue;
//                 }
//                 one.Heros.Add(hero.GetHeroInfo());
//                 one.HeroIds.Add(hero.SID);
//             }
//            One_bag_equip oneEquip;
            //ItemEquip[] equipList = player.heroTeam.GetTeamEquip(player.heroTeam.Data.defenseTeam);
//             foreach (ItemEquip item in equipList)
//             {
//                 if (item != null && player.equipList.ContainsKey(item.SID))
//                 {
//                     oneEquip = item.GetEquipInfo();
//                     one.Equips.Add(oneEquip);
//                 }
//                 one.EquipsIds.Add(item == null ? 0 : item.SID);
//             }
            //if (player.Club != null && player.ClubMember != null)
            //{
            //    one.ClubID = player.Club.id.ToString();
            //    one.ClubName = player.Club.name;
            //    one.ClubRole = player.ClubMember.role;
            //    one.ClubIcon = player.Club.icon;
            //    one.ClubIconBg = player.Club.iconbg;
            //    one.ClubJoinTime = player.ClubMember.createTime.ToTimestamp();
            //}
            //one.ArenaScore = player.Arena == null ? 0 : player.Arena.Score;
            one.LastTime = 0;
            if (!Glob.playerMgr.onlinePlayerList.ContainsKey(player.ID))
            {
                one.LastTime = player.AccountData.lastLogoutDate > player.AccountData.lastLoginDate ? 
                    player.AccountData.lastLogoutDate.ToTimestamp() : player.AccountData.lastLoginDate.ToTimestamp();
            }
            //player = null;
            return one;
        }

    }
}
