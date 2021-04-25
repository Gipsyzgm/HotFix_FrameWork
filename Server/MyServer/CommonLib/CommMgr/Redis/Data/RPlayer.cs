using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Redis
{
    /// <summary>
    /// 角色缓存简单数据
    /// </summary>
    public class RPlayer : RBase
    {
        public ObjectId pId { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 角色等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 角色头像
        /// </summary>
        public int[] icon { get; set; }
        /// <summary>
        /// 竞技积分
        /// </summary>
        //public int arena { get; set; }
        ///// <summary>
        ///// 联赛积分
        ///// </summary>
        //public int league { get; set; }
        ///// <summary>
        ///// 联盟名字
        ///// </summary>
        //public string club { get; set; }
        public RPlayer()
        { 
        }

        public RPlayer(string key):base(key)
        {
        }

        public RPlayer(TPlayer player)
        {
            Update(player);
        }
        public void Update(TPlayer player, int _arena = 0, int _league = 0, string cname = "")
        {
            Key = player.id.ToString();
            pId = player.id;
            name = player.name;
            level = player.level;
            icon = player.icon;
            //arena = _arena;
            //league = _league;
            //club = cname;
        }

        //public void UpdateLeague(int _league = 0)
        //{
        //    league = _league;
        //}

        public override string ToString()
        {
            //return JsonConvert.SerializeObject(this);
            return $"{Key};{name};{level};{string.Join(",", icon)};";/*{arena};{league};{club}*/
        }

        public override void Deserialize(string str)
        {
            //if (string.IsNullOrEmpty(str)) return null;
            string[] s = str.Split(';');
            Key = s[0];
            pId = ObjectId.Parse(Key);
            name = s[1];
            level = Convert.ToInt32(s[2]);
            string[] sA = s[3].Split(',');
            icon = Array.ConvertAll(sA, int.Parse);
            //arena = Convert.ToInt32(s[4]);
            //league = Convert.ToInt32(s[5]);
            //club = s[6];
        }

    }
}
