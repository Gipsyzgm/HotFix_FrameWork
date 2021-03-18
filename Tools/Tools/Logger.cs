using System.Collections.Generic;
using System.Drawing;

namespace Tools
{
    //打印等级
    public enum ELogType
    {
        None = 0,
        Normal = 2 << 1,
        Warning = 2 << 2,
        Error = 2 << 3,
        All = Normal | Warning | Error
    }

    public class Logger
    {
        //日志输出类型
        private static ELogType logType = ELogType.All;

        private static Main _mainForm;
        public static Main mainForm
        {
            get { return _mainForm; }
            set
            {
                _mainForm = value;
                while (cacheLogs.Count > 0)
                {
                    object[] log = cacheLogs.Dequeue();
                    mainForm.Log(log[0] as string, (Color)log[1]);
                }
            }
        }
        public static void SetLogType(ELogType type)
        {
            logType = type;
        }

        private static Queue<object[]> cacheLogs = new Queue<object[]>();
        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="args"></param>
        public static void Log(params object[] args)
        {
            if ((logType & ELogType.Normal) == ELogType.Normal)
                log(getParamsStr(args), Color.White);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="args"></param>
        public static void LogWarning(params object[] args)
        {
            if ((logType & ELogType.Warning) == ELogType.Warning)
                log(getParamsStr(args), Color.Yellow);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogError(params object[] args)
        {
            if ((logType & ELogType.Error) == ELogType.Error)
                log(getParamsStr(args), Color.Red);
        }
        /// <summary>
        /// 操作行为日志
        /// </summary>
        /// <param name="args"></param>
        public static void LogAction(params object[] args)
        {
            log(getParamsStr(args), Color.LawnGreen);
        }
        /// <summary>
        /// 清除日志
        /// </summary>
        public static void Clean()
        {
            if (mainForm == null)
                cacheLogs.Enqueue(new object[] { null, Color.White });
            else
                mainForm.Log(null, Color.White);
        }
        private static void log(string str, Color color)
        {
            if (mainForm == null)
                cacheLogs.Enqueue(new object[] { str, color });
            else
                mainForm.Log(str, color);
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
    }
}
