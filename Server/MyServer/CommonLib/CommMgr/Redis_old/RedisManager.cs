using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Redis
{
    public class RedisManager
    {
        /// <summary>
        /// 连接池管理对象
        /// </summary>
        public static RedisManagerPool prcm;

        /// <summary>最大写入线程数</summary>
        private static readonly int MaxWritePoolSize = 100;
        /// <summary>最大读取线程数</summary>
        private static readonly int MaxReadPoolSize = 100;
        private static readonly bool AutoStart = true;

        public static string[] writeConn;
        public static string[] readConn;

        public static string connectionString;

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        public static void CreateManager()
        {
            //RedisEndpoint redisEndpoint = connectionString.ToRedisEndpoint();
            //prcm = new PooledRedisClientManager(writeConn, readConn,
            //                 new RedisClientManagerConfig
            //                 {
            //                     MaxWritePoolSize = MaxWritePoolSize,
            //                     MaxReadPoolSize = MaxReadPoolSize,
            //                     AutoStart = AutoStart,
            //                 });

            prcm = new RedisManagerPool(connectionString, new RedisPoolConfig() { MaxPoolSize = 100 });
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IRedisClient GetClient()
        {
            if (prcm == null)
                CreateManager();
            return prcm.GetClient();
        }
    }
}
