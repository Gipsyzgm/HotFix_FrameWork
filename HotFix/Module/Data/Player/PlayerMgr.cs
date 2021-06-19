using PbLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    /// <summary>
    /// 玩家数据管理
    /// </summary>
    public class PlayerMgr : BaseDataMgr<PlayerMgr>, IDisposable
    {
        /// <summary>
        /// 当前角色数据
        /// </summary>
        public static PlayerData MainPlayer;
        /// <summary>
        /// 当前角色战斗数据
        /// </summary>
        public static PlayerWarData PlayerWarData;
        /// <summary>
        /// 如果需要的话，玩家的唯一标识ID
        /// </summary>
        public static string UID = "000";
      
        //如果联网的话根据消息来获取数据
        public void CreateMainPlayer(SC_login_playerInfo msg)
        {
            TPlayer player = new TPlayer();
            player.Name = msg.Name;
            player.Exp = msg.Exp;
            player.Level = msg.Level;
            player.Gold = msg.Gold;
            player.Ticket = msg.Ticket;
            player.Power = msg.Power;                 
            MainPlayer = new PlayerData(player);
            PlayerWarData = new PlayerWarData();
            PlayerWarData.Recount();
        }


        //保存玩家数据
        public void SavePlayerData()
        {
            MainPlayer.Data.Save();
        }

        public override void Dispose()
        {
        }
    }
}
