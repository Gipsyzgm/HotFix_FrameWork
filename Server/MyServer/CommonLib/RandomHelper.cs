using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class RandomHelper
    {
        /// <summary>全局随机数</summary>
        private static readonly Random random = new Random(getRandomSeed());

        private static int getRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        private static int roundomNext(int maxValue)
        {
            lock (random)
                return random.Next(maxValue);
        }
        private static int roundomNext(int minValue, int maxValue)
        {
            lock (random)
                return random.Next(minValue, maxValue);
        }

        /// <summary>
        /// 返回一个范围内的随机数
        /// </summary>
        /// <param name="minValue">最小值(含)</param>
        /// <param name="maxValue">最大值(含)</param>
        /// <returns></returns>
        public static int Random(int minValue, int maxValue)
        {
            return roundomNext(minValue, maxValue + 1);
        }


        /// <summary>
        /// 返回一个范围内的随机数
        /// </summary>
        /// <param name="range">随机范围[最小值，最大值]</param>
        /// <returns></returns>
        public static int Random(int[] range)
        {
            if (range.Length < 2)
                return range[0];
            return Random(range[0], range[1]);
        }

        /// <summary>
        /// 判断百分比随机是否命中
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool RandomPercent(int val)
        {
            if (val >= 100) return true;
            return roundomNext(0, 100) < val;
        }

        /// <summary>
        /// 判断万分比随机是否命中
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool RandomPercentW(int val)
        {
            if (val >= 10000) return true;
            return roundomNext(0, 10000) < val;
        }

        /// <summary>
        /// 获取权重随机随到的索引
        /// </summary>
        /// <param name="weightArr">权重数组</param>
        /// <returns>返回权重数组随机到的索引值</returns>
        public static int WeightRandom(int[] weightArr)
        {
            int weightSum = 0;
            for (int i = 0; i < weightArr.Length; i++)
                weightSum += weightArr[i];
            return WeightRandom(weightArr, weightSum);
        }

        /// <summary>
        ///  获取权重随机随到的索引
        /// </summary>
        /// <param name="weightArr">权重数组</param>
        /// <param name="weightSum">总权重值</param>
        /// <returns>返回权重数组随机到的索引值</returns>
        public static int WeightRandom(int[] weightArr, int weightSum)
        {
            int randomVal = random.Next(weightSum);//0-weightSum-1
            //Logger.LogWarning($"随机值:{randomVal}-{weightSum}");
            int stepWeightSum = 0;
            for (int i = 0; i < weightArr.Length; i++)
            {
                stepWeightSum += weightArr[i];
                if (randomVal < stepWeightSum)
                    return i;
            }
            return 0;
        }
        /// <summary>
        ///  获取权重随机随到的索引,
        /// </summary>
        /// <param name="weightArr">权重数组</param>
        /// <param name="maxLen">权重数组最大长度有效值</param>
        /// <returns></returns>
        public static int WeightRandom(List<int> weightArr, int maxLen)
        {
            int weightSum = 0;
            for (int i = 0; i < maxLen; i++)
                weightSum += weightArr[i];

            int randomVal = random.Next(weightSum);//0-weightSum-1
            int stepWeightSum = 0;
            for (int i = 0; i < maxLen; i++)
            {
                stepWeightSum += weightArr[i];
                if (randomVal < stepWeightSum)
                    return i;
            }
            return 0;
        }

        /// <summary>
        /// 从数组中随机获得一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Random<T>(T[] list)
        {
            int randomVal = roundomNext(list.Length);
            return list[randomVal];
        }

        /// <summary>
        /// 从数组中随机获得一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Random<T>(List<T> list)
        {
            int randomVal = roundomNext(list.Count);
            return list[randomVal];
        }


        /// <summary>
        /// 数组随机排列
        /// </summary>
        public static void RandomSort<T>(this T[] list)
        {
            T temp;
            for (int i = 0; i < list.Length; i++)
            {
                int index = RandomHelper.Random(0, i);
                temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }
        }
        /// <summary>
        /// 数组随机排列
        /// </summary>
        public static void RandomSort<T>(this List<T> list)
        {
            T temp;
            for (int i = 0; i < list.Count; i++)
            {
                int index = RandomHelper.Random(0, i);
                temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }
        }

        /// <summary>
        /// 从数组中随机取几个不重复的
        /// 注：会修改源数据中的数据顺序，如果不想改变源数据，自行复制数据再传进来
        /// </summary>
        /// <param name="list">源数据</param>
        /// <param name="num">最多获取数量</param>
        /// <returns></returns>
        public static List<T> RandomGetNum<T>(this List<T> _list,int maxNum,bool isClone=false)
        {
            List<T> list;
            if (isClone)
            {
                list = new List<T>();
                foreach (T t in _list)
                    list.Add(t);
            }
            else
                list = _list;

            List<T> rtnList = new List<T>();
            int index;
            int arrLen = list.Count;
            for (int i = 0; i < maxNum; i++)
            {
                if (arrLen == 0)
                    break;
                index = RandomHelper.Random(0, arrLen - 1);
                rtnList.Add(list[index]);
                list[index] = list[arrLen - 1];
                arrLen -= 1;
            }
            return rtnList;
        }
    }
}
