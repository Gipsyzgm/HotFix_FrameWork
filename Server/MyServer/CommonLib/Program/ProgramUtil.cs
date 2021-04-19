using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Telepathy;

namespace CommonLib
{
    //控制台程序
    public class ProgramUtil
    {
        //关闭事件
        protected static Action closeEventAction;
        //未处理的异常事件
        protected static Action unhandledExceptionAction;
        //控制台控制（记录关闭和防止误关闭）
        protected static ConsoleCtrlDelegate newDategate;
        public static void Initialize(Action closeAction = null,Action unhandledAction = null,bool isMainThread = false)
        {
            Logger.LogWarning("正在启动服务器....[按Ctrl+Break关闭程序]");
            closeEventAction = closeAction;
            unhandledExceptionAction = unhandledAction;
            // 异步方法全部会回掉到主线程
            if(isMainThread)
                SynchronizationContext.SetSynchronizationContext(MainThreadContext.Instance);

            //Console.OutputEncoding = Encoding.GetEncoding(936);  
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            newDategate = new ConsoleCtrlDelegate(HandlerRoutine);
           
            bool re = SetConsoleCtrlHandler(newDategate, true);  //点击关闭按钮执行

            if (!Directory.Exists("ErrorLog"))//如果不存在就创建文件夹
                Directory.CreateDirectory("ErrorLog");
            
            //控制台输入长度限制
            Stream inputStream = Console.OpenStandardInput(1500);
            Console.SetIn(new StreamReader(inputStream, Encoding.Default));
            //移除关闭按钮
            RemoveClose();
        }

        public static void StartEnd(bool isSucce)
        {
            if (isSucce)
                Logger.Sys("服务器启动完成 Ctrl+Break 关闭程序");
            else
                Logger.LogError("服务器启动失败，Ctrl+Break 关闭程序!!!");
        }

        public static void SetTitle()
        {
            string serverName = ConfigurationManager.AppSettings["ServerName"];
            Console.Title = serverName;

            string debug = ConfigurationManager.AppSettings["DebugModel"];
            if (debug == "1")
                NetLog.SetLogType(ELogType.All);
        }

        public static void SetTitle(int serverid,int port)
        {
            string serverName = ConfigurationManager.AppSettings["ServerName"];
            Console.Title = $"{serverName} 服务器Id:{serverid} 端口:{port}";
            Logger.SetServerId(serverid);
        }

        /// <summary>
        /// 捕获未处理的异常信息,不会处理异常
        /// [注:不能捕获定时器中引发的异常,所有定时器中容易引发异常的加try catch处理]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            try
            {
                //记录重启日志
            }
            catch
            { }
            Logger.LogError(ex);
            unhandledExceptionAction?.Invoke();
        }

        #region 点击控制台关闭按钮或任务栏关闭程序        
        public delegate bool ConsoleCtrlDelegate(int dwCtrlType);
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        //一個Ctrl    +    C的信號被接收，該信號或來自鍵盤，或來自GenerateConsoleCtrlEvent    函數   
        private const int CTRL_C_EVENT = 0;
        //一個    Ctrl    +    Break    信號被接收，該信號或來自鍵盤，或來自GenerateConsoleCtrlEvent    函數   
        private const int CTRL_BREAK_EVENT = 1;
        //當用戶系統關閉Console時，系統會發送此信號到此   
        private const int CTRL_CLOSE_EVENT = 2;
        //當用戶退出系統時系統會發送這個信號給所有的Console程序。該信號不能顯示是哪個用戶退出。   
        private const int CTRL_LOGOFF_EVENT = 5;
        //當系統將要關閉時會發送此信號到所有Console程序   
        private const int CTRL_SHUTDOWN_EVENT = 6;

        //异常关闭
        static bool HandlerRoutine(int CtrlType)
        {
            switch (CtrlType)
            {
                case CTRL_CLOSE_EVENT:
                //case CTRL_C_EVENT:
                case CTRL_SHUTDOWN_EVENT:
                case CTRL_BREAK_EVENT:
                    closeEventAction?.Invoke();
                    break;
            }
            return false;
        }

        [DllImport("User32.dll ", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);
        //控制台程序禁用关闭
        public static void RemoveClose()
        {
            //string path = System.Environment.CurrentDirectory;
            //string exeName = path.Substring(path.LastIndexOf('\\'))+".exe";
            ////与控制台标题名一样的路径
            //string fullPath = System.Environment.CurrentDirectory + exeName;
        
            //根据控制台标题找控制台
            int WINDOW_HANDLER = FindWindow(null, Console.Title);
            //找关闭按钮
            IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
            int SC_CLOSE = 0xF060;
            //关闭按钮禁用
            RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);
        }
        #endregion
    }
}
