using CommonLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoginServer.Http
{
    public partial class HttpServer
    {
        /// <summary>
        /// 收受AIHelp推送消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void HttpAiHelp(HttpListenerContext context)
        {          
            //if (Glob.net.loginToCenterClient.IsConnecting)
            //{
            //    ResponseOutput(context.Response, "error");
            //    return;
            //}

            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                string str = reader.ReadToEnd();
                str = HttpUtility.UrlDecode(str);
                Logger.Log($"收到AIHelp推送：{str}");
                reader.Close();
            }

            NameValueCollection args = context.Request.QueryString;
            Logger.Log($"args:{args.Count}");
            string userId = args.Get("userId");
            Logger.Log($"userId:{userId}");
            string title = HttpUtility.UrlDecode(args.Get("title"));
            Logger.Log($"title:{title}");
            string body = HttpUtility.UrlDecode(args.Get("body"));
            Logger.Log($"body:{body}");
            string serverId = args.Get("serverId");
            Logger.Log($"serverId:{serverId}");
            string key = args.Get("key");
            Logger.Log($"key:{key}");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
            {
                ResponseOutput(context.Response, "params fail");
                return;
            }
            //string signStr = $"{userId}&{Glob.platformMgr.pfCySdk.aihelpKey}";
            string signStr = "userId&";
            Logger.Log($"signStr:{signStr}");
            string sign = StringHelper.MD5(signStr);
            Logger.Log($"sign:{sign}");
            if (sign != key)
            {
                ResponseOutput(context.Response, "verify fail");
                return;
            }

            ////向中央服务器请求
            //CS_AIHelp_Push msg = new CS_AIHelp_Push();
            //msg.PId = userId;
            //msg.Title = title;
            //msg.Content = body;
            //Glob.net.loginToCenterClient.Send(msg);

            ResponseOutput(context.Response, "ok");
        }
    }
}