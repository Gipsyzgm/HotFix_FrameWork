// A simple logger class that uses Console.WriteLine by default.
// Can also do Logger.LogMethod = Debug.Log for Unity etc.
// (this way we don't have to depend on UnityEngine.DLL and don't need a
//  different version for every UnityEngine version here)
using CommonLib;
using Google.Protobuf;
using System;

namespace Telepathy
{
    public static class Log
    {
        public static Action<string> Info = Console.WriteLine;
        public static Action<string> Warning = Console.WriteLine;
        public static Action<string> Error = Console.Error.WriteLine;
    }
    //打印等级
    public enum ELogType
    {
        None = 0,
        Normal = 2 << 1,
        Warning = 2 << 2,
        Error = 2 << 3,
        Msg = 2 << 4,
        All = Normal | Warning | Error | Msg
    }
    //网络日志
    public static class NetLog
    {
        //日志输出类型
        private static ELogType logType = ELogType.Error | ELogType.Warning;

        public static void SetLogType(ELogType type)
        {
            logType = type;
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="args"></param>
        public static void Info(params object[] args)
        {
            if ((logType & ELogType.Normal) == ELogType.Normal)
                Logger.Log(args);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="args"></param>
        public static void Warning(params object[] args)
        {
            if ((logType & ELogType.Warning) == ELogType.Warning)
                Logger.LogWarning(args);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="args"></param>
        public static void Error(params object[] args)
        {
            if ((logType & ELogType.Error) == ELogType.Error)
                Logger.LogError(args);
        }

        public static void LogException(Exception exception)
        {
            if ((logType & ELogType.Error) == ELogType.Error)
                Logger.LogError(exception);
        }

        /// <summary>
        /// 通讯消息日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogMsg(bool isSend, ushort protocol, int length, IMessage msg)
        {
            if (protocol <= 100) return; //100以下的消息都过滤掉，一般为心跳包
            if ((logType & ELogType.Msg) == ELogType.Msg)
            {
                Logger.LogMsg(isSend, $"{(isSend ? "发送" : "收到")}[{protocol } L:{length} {msg.GetType().FullName}]:{msg.ToString()}");
            }
        }
    }

}
