using System;
using System.Collections.Generic;
using CommonLib;
using System.Threading;
using System.Linq;

namespace LoginServer
{
    public enum CommandState
    {
        Break,
        Continue,
    }
    /// <summary>
    /// 控制台命令解释器
    /// </summary>
    public partial class ServerCommand
    {      
        public static CommandState Command(string cmd)
        {
            try
            {
                if (cmd == null) return CommandState.Continue;
                List<string> cmdS = cmd.ToLower().Split(' ').ToList();
                cmdS.Add(string.Empty);  //加几个默认参数
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                switch (cmdS[0])
                {
                    case "-help":
                        showHelp();                       
                        break;
                    case "-updatev":
                        Glob.serverStateMgr.GetServInfo();
                        Logger.Sys("重新加载版本信息完成!");
                        break;
                    case "-updaten":
                        GC.Collect();
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex);
            }
            return CommandState.Continue;
        }
        /// <summary>
        /// 字符串参数转为Int类型
        /// </summary>
        /// <param name="cmdArg">字符串参数</param>
        /// <param name="defVal">默认值</param>
        /// <returns></returns>
        private static int tryParseInt(string cmdArg,int defVal = 0)
        {
            int value = defVal;
            if (cmdArg != string.Empty)
                Int32.TryParse(cmdArg, out value);
            return value;
        }
      
        /// <summary>
        /// 控制台帮助输出
        /// </summary>
        private static void showHelp()
        {
            string str = @"控制台命令帮助[命领名 参数1 参数2 参数3 ],参数""[]""里表示默认值:
-updatev             更新版本信息文件
-updaten             更新公告   
";
            Logger.Log(str);
        }
      
    }
}
