using CommonLib;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoginServer
{
    //每个服务器有自己的一套逻辑
    //登录服务器，用于接收玩家信息并返回服务器列表信息。
    class Program
    {
        static bool IsInit = false;
        static void Main(string[] args)
        {
            //控制台的一些初始化处理            
            ProgramUtil.Initialize(CloseEvent, UnhandledException);
            ProgramUtil.SetTitle();
            //初始化
            IsInit = Glob.Initialize();
            //启动完成
            ProgramUtil.StartEnd(IsInit);

            //避免服务器直接关闭，直接while(true)
          
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

        static void CloseEvent()
        {
            Logger.Sys("正在关闭服务器...");
            //等待所有操作完成再执行关闭
            Thread.Sleep(1000);
            Environment.Exit(0);//关闭程序
            Console.ReadLine();
        }
        //服务器未知异常处理
        static void UnhandledException()
        {


        }
    }
}
