using CommonLib.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLib.Redis
{
    public class RedisHashDB:RedisDB
    {

        /// <summary>
        /// 初始化ReadDB
        /// </summary>      
        public RedisHashDB(RedisSetting setting):base(setting)
        {
        }

        public RedisHashDB(RedisSetting setting, int db, string key) : base(setting, db, key)
        {

        }

        #region 同步方法
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Exists(string dataKey)
        {
            return DoRead(db => db.HashExists(HashKey, dataKey));
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Set<T>(T t) where T : RBase, new()
        {
            return DoWrite(db =>
            {
                string json = ConvertJson(t);
                return db.HashSet(HashKey, t.Key, json);
            });
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Delete(string dataKey)
        {
            return DoWrite(db => db.HashDelete(HashKey, dataKey));
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long Delete(List<string> dataKeys)
        {
            //List<RedisValue> dataKeys1 = new List<RedisValue>() {"1","2"};
            return DoWrite(db => db.HashDelete(HashKey, ConvertRedisValues(dataKeys)));
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T Get<T>(string dataKey) where T : RBase, new()
        {
            return DoRead(db =>
            {
                string value = db.HashGet(HashKey, dataKey);
                return ConvertObj<T>(value);
            });
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public List<T> Get<T>(List<string> dataKeys) where T : RBase, new()
        {
            RedisValue[] values = DoRead(db => db.HashGet(HashKey, ConvertRedisValues(dataKeys)));
            return ConvetList<T>(values);
        }

        /// <summary>
        /// 从hash表获取 增量数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public long GetIncrementValue(string dataKey)
        {
            return DoRead(db =>
            {
                return (long)db.HashGet(HashKey, dataKey);
            });
        }

        /// <summary>
        /// 存储增量数值 到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool SetIncrementValue(string dataKey, long val = 0)
        {
            return DoWrite(db =>
            {
                return db.HashSet(HashKey, dataKey, val);
            });
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public long Increment(string dataKey, long val = 1)
        {
            return DoWrite(db => db.HashIncrement(HashKey, dataKey, val));
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public long Decrement(string dataKey, long val = 1)
        {
            return DoWrite(db => db.HashDecrement(HashKey, dataKey, val));
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> Keys<T>() where T : RBase, new()
        {
            return DoRead(db =>
            {
                RedisValue[] values = db.HashKeys(HashKey);
                return ConvetList<T>(values);
            });
        }
        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public List<T> GetPlayerList<T>(List<string> dataKeys) where T : RBase, new()
        {
            RedisValue[] values =  DoRead(db => db.HashGet(HashKey, ConvertRedisValues(dataKeys)));
            return ConvetList<T>(values);
        }
        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string dataKey)
        {
            return await DoRead(db => db.HashExistsAsync(HashKey, dataKey));
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async void SetAsync<T>(T t) where T : RBase, new()
        {
            await DoWrite(db =>
            {
                string json = ConvertJson(t);
                return db.HashSetAsync(HashKey, t.Key, json);
            });
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string dataKey)
        {
            return await DoWrite(db => db.HashDeleteAsync(HashKey, dataKey));
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(List<string> dataKeys)
        {
            //List<RedisValue> dataKeys1 = new List<RedisValue>() {"1","2"};
            return await DoWrite(db => db.HashDeleteAsync(HashKey, ConvertRedisValues(dataKeys)));
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string dataKey) where T : RBase, new()
        {
            string value = await DoRead(db => db.HashGetAsync(HashKey, dataKey));
            return ConvertObj<T>(value);
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAsync<T>(List<string> dataKeys) where T : RBase, new()
        {
            RedisValue[] values = await DoRead(db => db.HashGetAsync(HashKey, ConvertRedisValues(dataKeys)));
            return ConvetList<T>(values);
        }
        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<Dictionary<string,T>> GetDictionaryAsync<T>(List<string> dataKeys) where T : RBase, new()
        {
            RedisValue[] values = await DoRead(db => db.HashGetAsync(HashKey, ConvertRedisValues(dataKeys)));
            return ConvetDictionary<T>(values);
        }


        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<long> HashIncrementAsync(string dataKey, long val = 1)
        {
            return await DoWrite(db => db.HashIncrementAsync(HashKey, dataKey, val));
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<long> HashDecrementAsync(string dataKey, long val = 1)
        {
            return await DoWrite(db => db.HashDecrementAsync(HashKey, dataKey, val));
        }

        ///// <summary>
        ///// 获取hashkey所有Redis key
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public async Task<List<T>> HashKeysAsync<T>() where T : RBase, new()
        //{
        //    RedisValue[] values = await DoRead(db => db.HashKeysAsync(HashKey));
        //    return ConvetList<T>(values);
        //}

        #endregion 异步方法
    }
}