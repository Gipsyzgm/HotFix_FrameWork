using CommonLib.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLib.Redis
{
    public class RedisZSetDB : RedisDB
    {

        /// <summary>
        /// 初始化ReadDB
        /// </summary>      
        public RedisZSetDB(RedisSetting setting):base(setting)
        {
        }

        public RedisZSetDB(RedisSetting setting, int db, string key) : base(setting, db, key)
        {

        }

        #region 异步方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="score"></param>
        public async void AddAsync(string dataKey, int score)
        {
            await DoWrite(redis => redis.SortedSetAddAsync(HashKey, dataKey, score));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public async void RemoveAsync(string dataKey)
        {
            await DoWrite(redis => redis.SortedSetRemoveAsync(HashKey, dataKey));
        }


        /// <summary>
        /// 范围删除
        /// </summary>
        /// <param name="start">开始序号</param>
        /// <param name="stop">结束序号 -1 全部删除</param>
        public async void RemoveRangeAsync(long start, long stop)
        {
            await DoWrite(redis => redis.SortedSetRemoveRangeByRankAsync(HashKey,start,stop));
        }

        /// <summary>
        /// 全部删除
        /// </summary>
        public async void RemoveAllAsyncl()
        {
            await DoWrite(redis => redis.KeyDeleteAsync(HashKey));
        }

        /// <summary>
        /// 获取top排名列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<string>> GetRangeAsync(long start = 0, long stop = -1)
        {
            var values = await DoRead(redis => redis.SortedSetRangeByRankAsync(HashKey,start,stop, Order.Descending));
            return ConvetList(values);
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<string>> GetAllAsyncByKey()
        {
            var values = await DoRead(redis => redis.SortedSetRangeByRankWithScoresAsync(HashKey, 0, -1, Order.Descending));            
            List<string> strList = new List<string>();
            for (int i = 0; i < values.Length; i++)
                strList.Add(values[i].Element);
            return strList;
        }


        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<RZSet>> GetAllAsync() 
        {
            var values = await DoRead(redis => redis.SortedSetRangeByRankWithScoresAsync(HashKey, 0, -1, Order.Descending));
            List<RZSet> result = new List<RZSet>();
            for (int i=0;i<values.Length;i++)
            {
                var model = new RZSet(i+1,(string)values[i].Element, (int)values[i].Score);
                result.Add(model);
            }
            return result;
        }


 
        /// <summary>
        /// 根据key获得排名
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<long> GetRankAsync(string dataKey)
        {
            var re = await DoRead(redis => redis.SortedSetRankAsync(HashKey, dataKey, Order.Descending));
            if (re == null)
                return 0;
            return (long)re+1;
        }


        /// <summary>
        /// 获取积分范围对应的uids
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<string>> GetRangeByScoreAsync(long start = 0, long stop = -1)
        {
            var values = await DoRead(redis => redis.SortedSetRangeByScoreAsync(HashKey, start, stop, Exclude.None, Order.Ascending, 0, 20));
            return ConvetList(values);
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> GetLengthAsync()
        {
            return await DoRead(redis => redis.SortedSetLengthAsync(HashKey));
        }

        #endregion 异步方法


        #region 同步方法

        /// <summary>
        /// 全部删除
        /// </summary>
        public void RemoveAll()
        {
            DoWrite(redis => redis.KeyDelete(HashKey));
        }

        /// <summary>
        /// 根据key获得排名
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public long GetRank(string dataKey)
        {
            var re =  DoRead(redis => redis.SortedSetRank(HashKey, dataKey, Order.Descending));
            if (re == null)
                return 0;
            return (long)re + 1;
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<RZSet> GetAll()
        {
            var values = DoRead(redis => redis.SortedSetRangeByRankWithScores(HashKey, 0, -1, Order.Descending));
            List<RZSet> result = new List<RZSet>();
            for (int i = 0; i < values.Length; i++)
            {
                var model = new RZSet(i + 1, (string)values[i].Element, (int)values[i].Score);
                result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 获取积分范围对应的uids
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetRangeByScore(long start = 0, long stop = -1)
        {
            var values = DoRead(redis => redis.SortedSetRangeByScore(HashKey, start, stop, Exclude.None, Order.Ascending, 0, 20));
            return ConvetList(values);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="score"></param>
        public  void Add(string dataKey, int score)
        {
             DoWrite(redis => redis.SortedSetAdd(HashKey, dataKey, score));
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetAllByKey()
        {
            var values = DoRead(redis => redis.SortedSetRangeByRankWithScores(HashKey, 0, -1, Order.Descending));
            List<string> strList = new List<string>();
            for (int i = 0; i < values.Length; i++)
                strList.Add(values[i].Element);
            return strList;
        }

        #endregion 同步方法
    }
}