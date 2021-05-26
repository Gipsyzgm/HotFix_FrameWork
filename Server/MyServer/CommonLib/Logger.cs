using Google.Protobuf;
using System;
using System.IO;

namespace CommonLib
{
    public class Logger
    {
        public static bool MsgLogOnOff = true;
        public static int ServerId = 0;
        //是否输出日志
        public static bool IsDebug = true;
        public static void SetLogType(bool isShow)
        {
            IsDebug = isShow;
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="args"></param>
        public static void Log(string args)
        {
            logLine(args, ConsoleColor.White);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="args"></param>
        public static void LogWarning(string args)
        {
            logLine(args, ConsoleColor.Yellow);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogError(string args)
        {
            logLine(args, ConsoleColor.Red);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="args"></param>
        public static void MessageError(params object[] args)
        {
            string info = "协议号未定义, 协议类型:" + args[0].ToString();
            logLine(info, ConsoleColor.Red);
        }

        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="args"></param>
        public static void Sys(params object[] args)
        {
            logLine(GetParamsStr(args), ConsoleColor.Green);
        }
        /// <summary>
        /// 系统日志,一条日志开始显示(同一行)
        /// </summary>
        /// <param name="args"></param>
        public static void SysStart(params object[] args)
        {
            log(GetParamsStr(args), ConsoleColor.Green);
        }
        /// <summary>
        /// 系统日志,一条日志结束时显示(同一行)
        /// </summary>
        public static void SysEnd(params object[] args)
        {
            Console.Write(GetParamsStr(args) + "\n");
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="args"></param>
        public static void Log(params object[] args)
        {
            logLine(GetParamsStr(args), ConsoleColor.White);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="args"></param>
        public static void LogWarning(params object[] args)
        {
            logLine(GetParamsStr(args), ConsoleColor.Yellow);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogError(params object[] args)
        {
            logLine(GetParamsStr(args), ConsoleColor.Red);
        }

        /// <summary>
        /// 临时日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogTemporary(params object[] args)
        {
            logLine(GetParamsStr(args), ConsoleColor.Red);
        }

        public static void LogError(Exception ex)
        {
            LogError(ex.ToString() + ex.StackTrace);
            ErrorLog(ex.ToString() + ex.StackTrace);
        }

        /// <summary>
        /// 通讯消息日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogMsg(bool isSend, ushort protocol, int length, IMessage msg)
        {
            if (protocol <= 100) return; //100以下的消息都过滤掉，一般为心跳包           
            LogMsg(isSend, $"{(isSend ? "发送" : "收到")}[{protocol } L:{length} {msg.GetType().FullName}]:{msg.ToString()}");
        }

        /// <summary>
        /// 通讯消息日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogMsg(bool isSend, params object[] args)
        {
            logLine(GetParamsStr(args), isSend ? ConsoleColor.DarkCyan : ConsoleColor.Cyan);
        }

        /// <summary>
        /// 输出战斗日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogWar(params object[] args)
        {
            logLine(GetParamsStr(args), ConsoleColor.Yellow);
        }

        private static void logLine(string str, ConsoleColor color)
        {
            if (!IsDebug) return;
            Console.ForegroundColor = color;
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + str);
        }
        private static void log(string str, ConsoleColor color)
        {
            if (!IsDebug) return;
            Console.ForegroundColor = color;
            Console.Write(DateTime.Now.ToString("HH:mm:ss ") + str);
        }

        //把所有参数都转成String
        private static string GetParamsStr(params object[] args)
        {
            if (args == null)
            {
                return string.Empty;
            }
            string rtn = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                rtn += (args[i] == null ? "null" : args[i].ToString()) + ",";
            }
            return rtn.TrimEnd(',');
        }

        public static void SetServerId(int servId)
        {
            ServerId = servId;
            if (ServerId > 0)
            {
                string strSer = ServerId == 0 ? "" : $"/S{ServerId}";
                string errPath = $"ErrorLog/{strSer}";
                if (!Directory.Exists(errPath))
                    Directory.CreateDirectory(errPath);
            }
        }
        static object o = new object();
        private static void ErrorLog(string str)
        {
            lock (o)
            {
                string strSer = ServerId == 0 ? "" : $"/S{ServerId}/";
                StreamWriter sw = new StreamWriter($"ErrorLog/{strSer}Log{DateTime.Now.ToString("yyyy-MM-dd")}.txt", true);
                sw.Write($"DateTime:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} \r\n {str} \r\n\r\n");
                sw.Flush();
                sw.Close();
            }
        }
    
    }
}
