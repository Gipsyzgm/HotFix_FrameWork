using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PbHero;

namespace GameServer.Module
{
    public class GuideData
    {
        /// <summary>
        /// 已经完成的指引Id
        /// </summary>
        HashSet<int> finishs = new HashSet<int>();
        Player Player;
        public GuideData(Player player)
        {
            Player = player;
            if (Player.Data.guides != null)
            {
                foreach (int id in Player.Data.guides)
                    finishs.Add(id);
            }
        }
    
  


     


        public void Dispose()
        {
            finishs = null;
            Player = null;
        }
    }
}
