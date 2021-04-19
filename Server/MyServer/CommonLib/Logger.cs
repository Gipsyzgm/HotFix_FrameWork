using System;
using System.IO;

namespace CommonLib
{
    public class Logger
    {
        public static bool MsgLogOnOff = true;
        public static int ServerId = 0;
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="args"></param>
        public static void Sys(params object[] args)
        {
            logLine(getParamsStr(args), ConsoleColor.Green);
        }
        /// <summary>
        /// 系统日志,一条日志开始显示(同一行)
        /// </summary>
        /// <param name="args"></param>
        public static void SysStart(params object[] args)
        {
            log(getParamsStr(args), ConsoleColor.Green);
        }
        /// <summary>
        /// 系统日志,一条日志结束时显示(同一行)
        /// </summary>
        public static void SysEnd(params object[] args)
        {
            Console.Write(getParamsStr(args) + "\n");
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="args"></param>
        public static void Log(params object[] args)
        {
            logLine(getParamsStr(args), ConsoleColor.White);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="args"></param>
        public static void LogWarning(params object[] args)
        {
            logLine(getParamsStr(args), ConsoleColor.Yellow);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogError(params object[] args)
        {
            logLine(getParamsStr(args), ConsoleColor.Red);
        }

        /// <summary>
        /// 临时日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogTemporary(params object[] args)
        {
            logLine(getParamsStr(args), ConsoleColor.Red);
        }

        public static void LogError(Exception ex)
        {
            LogError(ex.ToString() + ex.StackTrace);
            errorLog(ex.ToString() + ex.StackTrace);
        }

        /// <summary>
        /// 支付日志（打印并写文件）
        /// </summary>
        /// <param name="args"></param>
        public static void PayLog(params object[] args)
        {
            logLine(getParamsStr(args), ConsoleColor.White);
            payLog(getParamsStr(args));
        }

        /// <summary>
        /// 通讯消息日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogMsg(bool isSend, params object[] args)
        {
            logLine(getParamsStr(args), isSend ? ConsoleColor.DarkCyan : ConsoleColor.Cyan);
        }

        /// <summary>
        /// 输出战斗日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogWar(params object[] args)
        {
            logLine(getParamsStr(args), ConsoleColor.Yellow);
        }

        private static void logLine(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + str);
        }
        private static void log(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(DateTime.Now.ToString("HH:mm:ss ") + str);
        }

        private static string getParamsStr(params object[] args)
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
                //Logger.Log($"serverid:{servId}");
                string strSer = ServerId == 0 ? "" : $"/S{ServerId}";
                string errPath = $"ErrorLog/{strSer}";
                if (!Directory.Exists(errPath))
                    Directory.CreateDirectory(errPath);
            }
        }
        static object o = new object();
        private static void errorLog(string str)
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
        static object oo = new object();
        private static void payLog(string str)
        {
            if (!Directory.Exists("PayLog"))
                Directory.CreateDirectory("PayLog");
            lock (oo)
            {
                StreamWriter sw = new StreamWriter($"PayLog/Log{DateTime.Now.ToString("yyyy-MM-dd")}.txt", true);
                sw.Write($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\r {str}\r\n");
                sw.Flush();
                sw.Close();
            }
        }
    }
}
