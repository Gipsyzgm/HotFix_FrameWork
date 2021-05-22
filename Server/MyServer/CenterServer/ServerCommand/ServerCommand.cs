using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;
using Google.Protobuf;
using CommonLib;

namespace CenterServer
{
    public enum CommandState
    {
        Break,
        Continue,
    }
    /// <summary>
    /// 控制台命令解释器
    /// </summary>
    public partial class ServerCommand
    {      
        public static CommandState Command(string cmd)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cmd)) return CommandState.Continue;
                string[] sendCmd = cmd.Split('@');
                List<string> cmdS = sendCmd[0].ToLower().Split(' ').ToList();
                cmdS.Add(string.Empty);  //加几个默认参数
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                switch (cmdS[0])
                {
                    case "-help":
                        showHelp();
                        break;
                    case "-online":
                        online();
                        break;
                    case "-game": //向游戏服发送命令行
                        int serId = tryParseInt(cmdS[1], 0);
                        if (sendCmd.Length == 2)
                            sendGameCommand(serId, sendCmd[1]);
                        break;
                    case "-t":
                        kickPlayer(cmdS[1]);
                        break;
                    case "-loadplayer":
                        LoadPlayerRedis();
                        break;
                    case "-test":
                        int num = tryParseInt(cmdS[1], 10000);
                        usedTimeTest(num);
                        break;
                    default:
                        if (cmdS[0].StartsWith("-"))
                            Logger.LogWarning("未找到命行:" + cmdS[0]);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex);
            }
            return CommandState.Continue;
        }
        /// <summary>
        /// 字符串参数转为Int类型
        /// </summary>
        /// <param name="cmdArg">字符串参数</param>
        /// <param name="defVal">默认值</param>
        /// <returns></returns>
        private static int tryParseInt(string cmdArg,int defVal = 0)
        {
            int value = defVal;
            if (cmdArg != string.Empty)
                Int32.TryParse(cmdArg, out value);
            return value;
        }
       
        /// <summary>
        /// 控制台帮助输出
        /// </summary>
        private static void showHelp()
        {
            string str = $@"控制台命令帮助[命领名 参数1 参数2 参数3 ],参数""[]""里表示默认值:
-online             服务器在线人数
-game   向GameServer发送命令 参数:1服务器Id(0全部服务器) 2命令,命令说明如下:
        @exit             关闭服务器
        @additem          增加物品 参数:1玩家id 2物品id 3数量[1]
        @addmoney         增加金钱 参数:1玩家id 2数量[10000]
        @addticket        增加钻石 参数:1玩家id 2数量[10000]
        @player     输出玩家信息  参数:1玩家id
-loadplayer     读取全部玩家数据到redis中
";
            Logger.Log(str);
        }

        /// <summary>
        /// 用时测试
        /// </summary>
        private static void usedTimeTest(int num)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();           
            watch.Start();
            //---------------------------------------------------------------  
            for (int i = 0; i < num; i++)
            {

            }
            //---------------------------------------------------------------
            watch.Stop();
            Logger.LogWarning($"执行了{num}次，用时:{watch.ElapsedMilliseconds}毫秒");
        }



    }
}
