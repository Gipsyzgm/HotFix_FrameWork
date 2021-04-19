using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    /// <summary>
    /// 管理员命令，用于停服、重载数据表
    /// <para>2020-08-18 20:00:00|stop</para>
    /// <para>2020-08-18 20:00:00|reload</para>
    /// </summary>
    public class AdminCmdBase
    {
        private static string FilePath = $"../../../Product/PublicFolder/adminCmd.txt";

        public void LoadFile()
        {
            if (!File.Exists(FilePath))
                return;
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string strconfig = sr.ReadLine().Trim(' ');
                    if (strconfig == string.Empty || !strconfig.Contains("|"))
                        return;
                    string[] param = strconfig.Split('|');
                    string timeStr = param[0];
                    string cmd = param[1];
                    if (string.IsNullOrEmpty(timeStr) || string.IsNullOrEmpty(cmd))
                        return;
                    if (!DateTime.TryParse(timeStr, out DateTime time))
                        return;
                    if (DateTime.Now.ToTimestamp() - time.ToTimestamp() >= 10)
                        return;
                    if (DateTime.Now.Minute != time.Minute)
                        return;

                    if (cmd == "reload")
                        ExecReload();
                    if (cmd == "stop")
                        ExecStop();
                }
            }
        }
        
        public virtual void ExecReload()
        {
            Logger.Sys("执行管理员命令：reload");
        }

        public virtual void ExecStop()
        {
            Logger.Sys("执行管理员命令：stop");
        }

    }
}
