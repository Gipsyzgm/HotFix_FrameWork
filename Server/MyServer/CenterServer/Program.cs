using CommonLib;
using System;
using System.Threading;

namespace CenterServer
{
    class Program
    {
        static bool isInit = false;
        static void Main(string[] args)
        {
            //控制台的一些初始化处理            
            ProgramUtil.Initialize(closeEvent, unhandledException);
            ProgramUtil.SetTitle();

            //开始服务器逻辑
            //初始化
            isInit = Glob.Initialize();
            //启动完成
            ProgramUtil.StartEnd(isInit);

            CommandState commState = CommandState.Continue;
            while (true)
            {
                commState = ServerCommand.Command(Console.ReadLine());
                if (commState == CommandState.Break)
                    break;
                else if (commState == CommandState.Continue)
                    continue;
            }
        }
        static void closeEvent()
        {
            Logger.Sys("正在关闭服务器...");
            //等待所有操作完成再执行关闭
            Thread.Sleep(5000);
            Environment.Exit(0);//关闭程序
            Console.ReadLine();
        }
        //服务器未知异常处理
        static void unhandledException()
        {

        }
    }
}
