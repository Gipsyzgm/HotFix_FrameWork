using CommonLib;
using System;
using System.Configuration;
using System.Threading;

namespace GameServer
{
    class Program
    {
        static bool isInit = false;
        static void Main(string[] args)
        {
            ProgramUtil.Initialize(closeEvent, unhandledException, true);
            ProgramUtil.SetTitle();
            //初始化
            isInit = Glob.Initialize();
            //启动完成
            ProgramUtil.StartEnd(isInit);

            while (true)
            {
                Thread.Sleep(1000/50);
                Glob.net.StartTick();
                MainThreadContext.Instance.Update();
            }
        }

        static void closeEvent()
        {
            Logger.Sys("正在关闭服务器...");
            //等待所有操作完成再执行关闭   
            if (isInit)
                ServerCommand.Command("-exit");
            else
                Environment.Exit(0);//关闭程序
            Console.ReadLine();
        }
        //服务器未知异常处理
        static void unhandledException()
        {
        }
    }
}
