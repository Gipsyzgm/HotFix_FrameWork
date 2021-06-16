using System.Collections.Generic;

namespace GameServer.Module
{
    /// <summary>
    /// 副本关卡掉落
    /// </summary>
    public class FBLevelStageDrop
    {
        /*
        //怪物箱子产出相关
        private List<int> dropBoxWeightSum;
        private List<int[]> dropBoxWeight;
        private List<int[]> dropBoxItems;

        //元素箱子产出相关
        private int symbolBoxWeightSum;
        private int[] symbolBoxWeight;
        private List<int[]> symbolBoxItems;
        public FBLevelStageConfig Config;
        public FBLevelStageDrop(FBLevelStageConfig config)
        {
            Config = config;
            //怪物箱子产出
            dropBoxWeightSum = new List<int>();
            dropBoxItems = new List<int[]>();
            dropBoxWeight = new List<int[]>();
            for (int i = 0; i < config.dropBoxItems.Count; i++)
            {
                List<int[]> oneList = config.dropBoxItems[i];
                int weightSum = 0;
                int[] weights = new int[oneList.Count];
                int[] items = new int[oneList.Count];
                for (int k = 0; k < oneList.Count; k++)
                {
                    weightSum += oneList[k][1];
                    weights[k] = oneList[k][1];
                    items[k] = oneList[k][0];
                }
                dropBoxWeightSum.Add(weightSum);
                dropBoxWeight.Add(weights);
                dropBoxItems.Add(items);
            }

            //元素宝箱产出
            symbolBoxItems = new List<int[]>();
            symbolBoxWeight = new int[config.symbolBoxItems.Count];
            for (int i = 0; i < config.symbolBoxItems.Count; i++)
            {
                symbolBoxWeightSum += config.symbolBoxItems[i][1];
                symbolBoxWeight[i] = config.symbolBoxItems[i][1];
                symbolBoxItems.Add(new []{ config.symbolBoxItems[i][0], config.symbolBoxItems[i][1] });
            }
        }
        //获取1个怪物箱子产出
        public int GetDropBoxItem(int index)
        {
            if (dropBoxWeightSum[index] == 0) return 0;
            return dropBoxItems[index][RandomHelper.WeightRandom(dropBoxWeight[index], dropBoxWeightSum[index])];
        }

        //获取1个元素宝箱产出
        public int[] GetSymbolBoxItem()
        {
            if (symbolBoxWeightSum == 0) return null;
            return symbolBoxItems[RandomHelper.WeightRandom(symbolBoxWeight, symbolBoxWeightSum)];
        }*/
    }
}