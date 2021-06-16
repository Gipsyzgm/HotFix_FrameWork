using MongoDB.Bson;
using PbPlayer;
using System.Collections.Generic;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家红点
    /// </summary>
    public class PlayerRedDotMgr
    {
        /// <summary>
        /// 发送红点数据
        /// </summary>
        public void SendRedDotMsg(Player player)
        {
            SC_player_redDot msg = new SC_player_redDot();

            ////竞技场数据
            //TArena arena;
            //if (!Glob.arenaMgr.playerArenaDataList.TryGetValue(player.ID, out arena))
            //{
            //    msg.RankRachWarNum = Glob.config.arenaSettingsConfig.ArenaDayWarNum;
            //}
            //else
            //{
            //    msg.RankRachWarNum = Glob.config.arenaSettingsConfig.ArenaDayWarNum;
            //    msg.RankRachLastWarTime = Glob.arenaMgr.GetArenaLastWarTime(arena);
            //    ArenaItemData arenaItem;
            //    if (Glob.arenaMgr.arenaRankList.TryGetValue(arena.rank, out arenaItem))
            //        msg.RankRachGetAward = arenaItem.CanGetAward();
            //}
            ////商业活动/矿山
            //if (player.BusinessData != null && player.BusinessData.Data != null)
            //{
            //    msg.MineLeftNum = player.BusinessData.Data.mineLeftNum;
            //    msg.MineCurrEndTime = player.BusinessData.Data.mineEndTime.ToTimestamp();
            //    msg.BusADLeftNum = player.BusinessData.Data.adLeftNum;
            //    msg.BusADCurrEndTime = player.BusinessData.Data.adEndTime.ToTimestamp();
            //}
            //else
            //{
            //    msg.MineLeftNum = player.VipRight(VipRightType.BusAdNum);
            //    msg.MineCurrEndTime = 0;
            //    msg.BusADLeftNum = player.VipRight(VipRightType.MineNum);
            //    msg.BusADCurrEndTime = 0;
            //}

            ////探索
            //if (player.Explore != null)
            //{
            //    msg.ExploreLeftNum = player.Explore.Data.leftNum;
            //    msg.ExploreCurrEndTime = player.Explore.EndTime;
            //}
            //else
            //{
            //    msg.ExploreLeftNum = player.VipRight(VipRightType.ExploreNum);
            //    msg.ExploreCurrEndTime = 0;
            //}
            ////msg.GuessEndTime = Glob.guessMgr.CurrGuess.EndTime;
            ////竞猜
            //if (player.GuessData != null)
            //{
            //    msg.GuessAward = player.GuessData.AwardGuess != null;
            //    //player.GuessData.AwardGuess
            //}

            //msg.JocDiningEatNum = player.Data.jocDiningNum;
            player.Send(msg);
        }
    }
}
