using CommonLib;
using System;
using System.Threading;
using System.Threading.Tasks;

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
          
            while (true)
            {
                Glob.net.StartTick();
                WaitCommand();
            }
        }
        public static async Task WaitCommand()         
        {
            await Task.Run(() => {
                ServerCommand.Command(Console.ReadLine());
            });          
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
