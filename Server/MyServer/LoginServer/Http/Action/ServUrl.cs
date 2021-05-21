using PbGetServer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

namespace LoginServer.Http
{
    public partial class HttpServer
    {
        /// <summary>
        /// 客户端请求游戏服地址
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void HttpServUrl(HttpListenerContext context)
        {
            if (!Glob.net._socket.Connected)
            {
                ResponseOutput(context.Response, HttpMsg.ErrorString("No server information found"));
                return;
            }
            //向中央服务器请求
            CS_Get_GameServer msg = new CS_Get_GameServer();
            msg.IP = context.Request.RemoteEndPoint.Address.ToString();
            msg.ReqUID = context.Request.RequestTraceIdentifier.ToString();
            Glob.net.Send(msg,msg.ReqUID,context);
        }
    }
}