using MongoDB.Bson;
using CommonLib;
using System.Timers;
using System.Collections.Concurrent;
using System.Threading;
using System;

namespace CommonLib.Comm.DBMgr
{
    /// <summary>
    /// 数据库写入操作
    /// </summary>
    public partial class DBWrite
    {
        private static readonly DBWrite instance = new DBWrite();
        public static DBWrite Instance => instance;
           
        public bool Initialize()
        {
            Logger.Sys("正在连接数据库...");
            if (MongoDBHelper.Instance.IsConnect)
            {
                //IsLog();
                Logger.Sys("数据库连接成功!");
                return true;
            }
            return false;
        }

        /// <summary> 插入一条记录 </summary>
        public void Insert<T>(T data) where T : ITable
        {
            insertDB(data);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据源</param>
        /// <param name="isImmediately">是否立即执行</param>
        public void Update<T>(T data, bool isImmediately = true) where T : ITable
        {
            updateDB(data);
        }

        /// <summary> 删除一条记录 </summary>
        public void Delete<T>(T data) where T : ITable
        {
            deleteDB(data);
        }      
    }

}
