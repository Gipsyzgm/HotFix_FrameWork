using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 广告开奖对象
    /// </summary>
    public class AdsAward
    {
        /// <summary>
        /// 必出奖励池权重物品[物品Id,物品数量]
        /// </summary>
        public List<List<int[]>> weightSureItems { get; }
        /// <summary>
        /// 必出奖励池权重物品的权重值
        /// </summary>
        public List<int[]> weightSureItemVal { get; }

        /// <summary>
        /// 随机奖励池权重物品[物品Id,物品数量]
        /// </summary>
        public List<List<int[]>> weightRandItems { get; }
        /// <summary>
        /// 随机奖励池权重物品的权重值
        /// </summary>
        public List<int[]> weightRandItemVal { get; }

        public AdsAwardConfig Config;
        public AdsAward(AdsAwardConfig config)
        {
            Config = config;

            weightSureItems = new List<List<int[]>>();
            weightSureItemVal = new List<int[]>();
            foreach (List<int[]> pool in config.awardSurePool)
            {
                List<int[]> weights = new List<int[]>();
                List<int> vals = new List<int>();
                foreach (int[] iteminfo in pool)
                {
                    weights.Add(new int[] { iteminfo[0], iteminfo[1] });
                    vals.Add(iteminfo[2]);
                }
                weightSureItems.Add(weights);
                weightSureItemVal.Add(vals.ToArray());
            }

            weightRandItems = new List<List<int[]>>();
            weightRandItemVal = new List<int[]>();
            foreach (List<int[]> pool in config.awardRandPool)
            {
                List<int[]> weights = new List<int[]>();
                List<int> vals = new List<int>();
                foreach (int[] iteminfo in pool)
                {
                    weights.Add(new int[] { iteminfo[0], 1 });
                    vals.Add(iteminfo[1]);
                }
                weightRandItems.Add(weights);
                weightRandItemVal.Add(vals.ToArray());
            }
        }

        /// <summary>
        /// 获得一次必出池权重物品[物品Id,物品数量](根据池类型)
        /// </summary>
        public int[] getSureItemOne(int poolType)
        {
            return weightSureItems[poolType][RandomHelper.WeightRandom(weightSureItemVal[poolType])];
        }

        /// <summary>
        /// 获得一次随机池权重物品[物品Id,物品数量](根据池类型)
        /// </summary>
        public int[] getRandItemOne(int poolType)
        {
            return weightRandItems[poolType][RandomHelper.WeightRandom(weightRandItemVal[poolType])];
        }

        /// <summary>
        /// 获得所有奖励
        /// </summary>
        /// <returns></returns>
        public List<int[]> getAllAward()
        {
            List<int[]> itemList = new List<int[]>();//int[道具id,数量]
            //必出奖励
            for (int k = 0; k < Config.awardSurePool.Count; k++)
            {
                int[] item = getSureItemOne(k);
                itemList.Add(item);
            }
            //随机奖励池
            for (int i = 0; i < Config.rands.Length; i++)
            {
                int num = Config.rands[i];
                if (num < 1)
                    continue;
                //是否命中
                if(RandomHelper.RandomPercentW(num))
                {
                    int[] item = getRandItemOne(i);
                    itemList.Add(item);
                }
            }
            return itemList;
        }
    }*/
}
