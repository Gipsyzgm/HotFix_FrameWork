using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenterServer
{
    /// <summary>
    /// 管理员命令
    /// </summary>
    public class AdminCmd : AdminCmdBase
    {
        private static AdminCmd cmd;

        public static AdminCmd Command
        {
            get
            {
                if (cmd == null)
                    cmd = new AdminCmd();
                return cmd;
            }
        }

        public override void ExecReload()
        {
            base.ExecReload();
            Logger.LogWarning("CenterServer无需加载配置表，不执行命令");
        }

        public override void ExecStop()
        {
            base.ExecStop();
            Glob.net?.gameToCenterServer.Stop();
            Glob.net?.loginToCenterServer.Stop();
            //Glob.net?.gmToCenterServer.Stop();
            System.Timers.Timer exitTimer = new System.Timers.Timer(1000);
            exitTimer.Elapsed += new System.Timers.ElapsedEventHandler(exit_ElapsedCheck);
            exitTimer.Start();
        }

        private static void exit_ElapsedCheck(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                ((System.Timers.Timer)sender).Stop();
                Logger.Sys("服务器即将关闭..");
                Thread.Sleep(500);
                Environment.Exit(0);//关闭程序
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

    }
}
