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
    public class RZSet
    {
        public int rank { get; set; }
        public string value { get; set; }
        public int score { get; set; }

        public RZSet(int _rank,string _value, int _score)
        {
            rank = _rank;
            value = _value;
            score = _score;
        }
    }
}
