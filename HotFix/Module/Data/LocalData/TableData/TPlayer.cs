using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    //需要保存的玩家信息，可扩展
    public class TPlayer : BaseTable
    {
        /// <summary>
        /// 玩家名称
        /// </summary>
        public string Name;

        /// <summary>等级</summary>
        public int Level;

        /// <summary>玩家当前经验</summary>
        public int Exp;

        /// <summary>金币</summary>
        public long Gold;

        /// <summary>(点券)钻石</summary>
        public int Ticket;

        /// <summary>体力</summary>
        public int Power = 20;

        /// <summary>天赋列表</summary>
        public List<int[]> TalentList = new List<int[]>();

    }
}
