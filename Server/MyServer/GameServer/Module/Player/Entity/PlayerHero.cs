using CommonLib;
using MongoDB.Bson;
using PbHero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public partial class Player
    {
        /// <summary>
        /// 英雄队伍
        /// </summary>
       // public HeroTeam heroTeam;

        /// <summary>
        /// 玩家全部英雄实体
        /// </summary>
        public DictionarySafe<int, Hero> heroList = new DictionarySafe<int, Hero>();



        /// <summary>
        /// 玩家可获取英雄上限
        /// </summary>
//        public int HeroLimit
//        {
//            get
//            {
////                 if (Glob.config.dicPlayerExp.TryGetValue(Level, out PlayerExpConfig expConf))
////                     return expConf.heroLimit + Data.extraNum[0];
////                else
//                    return /*Glob.config.dicPlayerExp[1].heroLimit*/0;
//            }
//        }

        /// <summary>
        /// 判断玩家拥有的英雄数量是否达到上限
        /// </summary>
        /// <returns></returns>
        //public bool CheckHeroNum()
        //{
        //    if (heroList.Count >= HeroLimit)
        //        return true;
        //    return false;
        //}       
     
        /// <summary>
        /// 召唤指引时，自动替换召唤到的英雄到队伍
        /// </summary>
        //public void AutoChangeSummonHero(int heroId)
        //{
//             if (!Glob.config.dicHero.TryGetValue(heroId, out HeroConfig config))
//                 return;
//             Hero summonHero = null;
//             List<Hero> hList = heroList.Values.Where(t => t.Config.id == config.id).ToList();
//             if (hList.Count > 0)
//                 summonHero = hList[0];
//             if (summonHero == null)
//                 return;
//            int index = -1;
//             for (int i = 0; i < heroTeam.Data.teamHeros[0].Length; i++)
//             {
//                 ObjectId teamHid = heroTeam.Data.teamHeros[0][i];
//                 if (!heroList.TryGetValue(teamHid, out Hero teamHero))
//                     continue;
//                 if (teamHero.Config.elemType == summonHero.Config.elemType)
//                 {
//                     index = i;
//                     break;
//                 }
//             }
//             if (index != -1)
//             {
//                 heroTeam.Data.teamHeros[0][index] = summonHero.ID;
//                 heroTeam.Data.Update();
//                 SC_teamInfo_change msg = new SC_teamInfo_change();
//                 msg.TeamId = 1;
//                 foreach (ObjectId id in heroTeam.Data.teamHeros[0])
//                     msg.Team1.Add(THero.ToShortId(id));
//                 Send(msg);
//             }
        //}
    }
}
