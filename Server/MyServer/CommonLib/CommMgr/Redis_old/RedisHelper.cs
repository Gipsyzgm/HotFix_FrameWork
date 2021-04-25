using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Redis
{
    /// <summary>
    /// RedisBase类，是redis操作的基类
    /// </summary>
    public partial class RedisHelper
    {
        private static readonly RedisHelper instance = new RedisHelper();
        public static RedisHelper Instance => instance;

        /// <summary>
        /// 数据库连接字符串 redis://keen!~game#123@127.0.0.1:6379 [password@ip:port]
        /// </summary>
        private const string connectionString = "redis://keen!~game#123@192.168.0.108:6379"; //ConfigurationManager.ConnectionStrings["conn_redis"].ConnectionString;

        //public IRedisClient Core => RedisManager.GetClient();
        public IRedisClient Core { get; private set; }

        public RedisHelper()
        {
            if (RedisManager.prcm == null)
            {
                RedisManager.writeConn = new[] { connectionString };
                RedisManager.readConn = new[] { connectionString };
                RedisManager.connectionString = connectionString;
                RedisManager.CreateManager();
            }
            //Core = RedisManager.GetClient();
            //不用连接池
            //Core = new RedisClient(connectionString.ToRedisEndpoint());
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            Core.Save();
        }
        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            Core.SaveAsync();
        }
        /// <summary>
        /// 设置要操作的db索引（0-15）
        /// </summary>
        /// <param name="db"></param>
        public void SetDB(int db)
        {
            using (var c = RedisManager.GetClient())
                c.Db = db;
        }
    }
}
