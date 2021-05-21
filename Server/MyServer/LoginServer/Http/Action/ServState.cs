using LoginServer.Http.Action.Msg;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoginServer.Http
{
    public partial class HttpServer
    {
        /// <summary>
        /// GM通知 登录服重载服务器状态
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void HttpServState(HttpListenerRequest request, HttpListenerResponse response)
        {
            NameValueCollection args = request.QueryString;

            string time = args.Get("t");
            string key = args.Get("k");
            if (string.IsNullOrEmpty(time) || string.IsNullOrEmpty(key))
            {
                ResponseOutput(response, $"params error");
                return;
            }

            if (!Glob.serverStateMgr.CheckKey(time, key))
            {
                ResponseOutput(response, $"key error");
                return;
            }

            Glob.serverStateMgr.GetServInfo();
            ResponseOutput(response, $"ok");

        }
    }
}