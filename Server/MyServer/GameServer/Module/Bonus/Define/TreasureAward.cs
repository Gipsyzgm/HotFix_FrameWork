using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 宝藏开奖对象
    /// </summary>
    public class TreasureAward
    {
        //开奖倍率产出相关

        /// <summary>权重总数</summary>
        //private int spinWeightSum;
        /// <summary>权重数组</summary>
        private int[] spinWeight = { };
        /// <summary>倍率数组</summary>
        public int[] spinOdds = { };

       // public TreasureConfig Config;
//         public TreasureAward(TreasureConfig config)
//         {
//             Config = config;
// 
//             spinWeight = new int[config.weights.Count];
//             spinOdds = new int[config.weights.Count];
//             for (int k = 0; k < config.weights.Count; k++)
//             {
//                 spinWeightSum += config.weights[k][1];
//                 spinWeight[k] = config.weights[k][1];
//                 spinOdds[k] = config.weights[k][0];
//             }
//         }

        //获取1个权重抽奖产出（返回倍数）
        public int GetSpin()
        {
            //             if (spinWeightSum == 0) return 0;
            //             return spinOdds[RandomHelper.WeightRandom(spinWeight, spinWeightSum)];
            return 0;
        }
    }
}
