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
        /// 当前角色对像
        /// </summary>
        public static PlayerData MainPlayer;


        public static PlayerWarData PlayerWarData;

        public static string UID = "000";

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
