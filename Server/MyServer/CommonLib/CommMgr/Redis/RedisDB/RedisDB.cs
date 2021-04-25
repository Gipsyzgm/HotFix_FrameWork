using CommonLib.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLib.Redis
{
    public class RedisDB
    {
        protected int DBNo { get; }
        protected string HashKey { get; }
        protected readonly ConnectionMultiplexer _connRead;
        protected readonly ConnectionMultiplexer _connWrite;


        /// <summary>
        /// 初始化ReadDB
        /// </summary>      
        public RedisDB(RedisSetting setting)
        {
            DBNo = setting.DBNo;
            HashKey = setting.Name;
            _connRead = RedisConnection.GetConnectionMultiplexer(setting.ReadConnection);
            _connWrite = RedisConnection.GetConnectionMultiplexer(setting.WriteConnection);
        }

        /// <summary>
        /// 初始化ReadDB
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="db">指定db号</param>
        /// <param name="keyName">键名</param>
        public RedisDB(RedisSetting setting, int db, string keyName)
        {
            DBNo = db;
            HashKey = keyName;
            _connRead = RedisConnection.GetConnectionMultiplexer(setting.ReadConnection);
            _connWrite = RedisConnection.GetConnectionMultiplexer(setting.WriteConnection);
        }

        protected T DoRead<T>(Func<IDatabase, T> func)
        {
            var database = _connRead.GetDatabase(DBNo);
            return func(database);
        }

        protected T DoWrite<T>(Func<IDatabase, T> func)
        {
            var database = _connWrite.GetDatabase(DBNo);
            return func(database);
        }

        protected string ConvertJson<T>(T value) where T : RBase
        {
            return value.ToString();
        }

        protected T ConvertObj<T>(RedisValue value) where T : RBase, new()
        {           
            if (string.IsNullOrEmpty(value))
                return default(T);
            T t = new T();
            t.Deserialize(value.ToString());
            return t;
        }

        protected List<T> ConvetList<T>(RedisValue[] values) where T : RBase, new()
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ConvertObj<T>(item);
                result.Add(model);
            }
            return result;
        }
        protected List<string> ConvetList(RedisValue[] values)
        {
            return values.Select(redisValue => (string)redisValue).ToList();
        }
        protected Dictionary<string,T> ConvetDictionary<T>(RedisValue[] values) where T : RBase, new()
        {
            Dictionary<string,T> result = new Dictionary<string,T>();
            foreach (var item in values)
            {
                var model = ConvertObj<T>(item);
                result.Add(model.Key, model);
            }
            return result;
        }

        protected RedisKey[] ConvertRedisKeys(List<string> redisKeys)
        {
            return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
        }

        protected RedisValue[] ConvertRedisValues(List<string> redisValues)
        {
            return redisValues.Select(redisValue => (RedisValue)redisValue).ToArray();
        }
    }
}