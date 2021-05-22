using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using MongoDB.Driver;
using CommonLib.Comm.DBMgr;
using CommonLib;

namespace CenterServer
{
    /// <summary>
    /// 控制台命令解释器
    /// </summary>
    public partial class ServerCommand
    {
        /// <summary>
        /// 发送邮件
        /// 参数:读取全部玩家信息到Redis中
        /// </summary>
        public static void LoadPlayerRedis()
        {
            bool isConnect = MongoDBHelper.Instance.IsConnect;
            Stopwatch watch = new Stopwatch();
            //---------------------------------------------------------------
            watch.Start();
            var playerList = MongoDBHelper.Instance.Select<TPlayer>();
            watch.Stop();
            Logger.LogWarning($"同步读Player {playerList.Count}条记录，用时:{watch.ElapsedMilliseconds}毫秒");



            //for (int i = 0; i < playerList.Count; i++)
            //{
            //    RPlayer player = new RPlayer(playerList[i]);
            //    Glob.redis.PlayerInfo.SetAsync(player);
               
            //}

            watch.Stop();
            Logger.LogWarning($"用时:{watch.ElapsedMilliseconds}毫秒");
        }

        /// <summary>
        /// 发送邮件
        /// 参数:读取全部玩家信息到Redis中
        /// </summary>
        private static async void LoadPlayerRedis2()
        {
            bool isConnect = MongoDBHelper.Instance.IsConnect;
            Stopwatch watch = new Stopwatch();            //---------------------------------------------------------------
         
            watch.Start();
            var playerList = await MongoDBHelper.Instance.SelectAsync<TPlayer>();
            watch.Stop();
            Logger.LogWarning($"异步读Player {playerList.Count}条记录，用时:{watch.ElapsedMilliseconds}毫秒");
            //---------------------------------------------------------------
            watch.Start();
            for (int i = 0; i < 1000; i++)
                MongoDBHelper.Instance.SelectAsync2<TPlayer>(playerList[1000].id);         
            watch.Stop();
            Logger.LogWarning($"异步ID查询Player {1000}次，用时:{watch.ElapsedMilliseconds}毫秒");

            //---------------------------------------------------------------
            //watch.Start();
            //List<Task> list = new List<Task>();
            //for (int i = 0; i < 2000; i++)
            //    list.Add(MongoDBHelper.Instance.SelectAsync<TPlayer>(playerList[1000].id));

            //TaskFactory taskFactory = new TaskFactory();
            //list.Add(taskFactory.ContinueWhenAll(list.ToArray(), tArray =>
            //{
            //    watch.Stop();
            //    Logger.LogWarning($"异步ID查询Player {2000}次，用时:{watch.ElapsedMilliseconds}毫秒");
            //}));
        }

    }
}


//List<Task> list = new List<Task>();
//            foreach (var i in indexs)
//            {
//                User user = new User()
//                {
//                    Uid = $"TEST{i}",
//                    Level = RandomHelper.Random(1, 10),
//                    Score = 100 + i,
//                    Icon = RandomHelper.RandomGetNum(icons, 3, true).ToArray()
//                };
////RedisHelper.Instance.SetEntryInHash("playerTest", $"TEST{i}", user.ToString());
//var task = Task.Run(() =>
//{
//    RedisHelper.Instance.SetEntryInHash("playerTest", $"TEST{i}", user.ToString());
//});
//list.Add(task);
//            }

           
//            TaskFactory taskFactory = new TaskFactory();