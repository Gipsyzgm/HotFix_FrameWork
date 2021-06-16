using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    public class StorePack
    {
        private StorePackConfig _config;

        /// <summary>
        /// 固定物品奖励[物品Id,物品数量]
        /// </summary>
        public List<int[]> items { get; }

        /// <summary>
        /// 权重获得物品执行次数范围
        /// </summary>
        public int[] weightNum => _config.weightNum;

        /// <summary>
        /// 权重获得物品执行次数
        /// </summary>
        public int weightRunNum
        {
            get
            {
                if (weightItems.Count == 0)
                    return 0;
                if (_config.weightNum.Length == 1)
                    return _config.weightNum[0];
                else if (_config.weightNum.Length == 2)
                    return RandomHelper.Random(_config.weightNum[0], _config.weightNum[1]);
                return 1;
            }
        }

        /// <summary>
        /// 权重物品[物品Id,物品数量]
        /// </summary>
        public List<int[]> weightItems { get; }
        /// <summary>
        /// 权重物品的权重值
        /// </summary>
        public int[] weightItemVal { get; }

        ///// <summary>
        ///// 权重物品[物品Id,物品数量,权重值]
        ///// </summary>
        //public List<int[]> randomItems { get; }

        public StorePack(StorePackConfig config)
        {
            _config = config;
            items = config.items;

            weightItems = new List<int[]>();
            List<int> wightVals = new List<int>();
            foreach (int[] iteminfo in config.weightItems)
            {
                weightItems.Add(new int[] { iteminfo[0], iteminfo[1] });
                wightVals.Add(iteminfo[2]);
            }
            weightItemVal = wightVals.ToArray();

            //randomItems = new List<int[]>();
            //foreach (int[] iteminfo in config.randomItems)
            //{
            //    randomItems.Add(new int[] { iteminfo[0], iteminfo[1], iteminfo[2] });
            //}
        }
        /// <summary>
        /// 获得一次权重随机物品[物品Id,物品数量]
        /// </summary>
        /// <returns></returns>
        public int[] getWeightItemOne()
        {
            return weightItems[RandomHelper.WeightRandom(weightItemVal)];
        }

        /// <summary>
        /// 获得一次权重随机物品[物品Id,物品数量]
        /// </summary>
        /// <param name="wItems">指定道具集合</param>
        /// <param name="wItemVal">指定道具权重</param>
        /// <returns></returns>
        public int[] getWeightItemOne(List<int[]> wItems, List<int> wItemVal, out int index)
        {
            index = RandomHelper.WeightRandom(wItemVal.ToArray());
            return wItems[index];
        }

    }*/
}
