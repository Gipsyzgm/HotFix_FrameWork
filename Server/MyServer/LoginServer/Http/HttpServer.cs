using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System;
using System.Reflection;
using System.Configuration;
using System.Text;
using System.IO;
using CommonLib;

namespace LoginServer.Http
{
    public partial class HttpServer
    {
        /// <summary>为客户端提供连接的通讯服务对象</summary>
        HttpListener httpListener;
        /// <summary>游戏key</summary>
        public string gameKey = "";

        public HttpServer()
        {
            Start();
            gameKey = ConfigurationManager.AppSettings["GameKey"];
        }

        public void Start()
        {
            //开始监听配置的web端口
            string httpProt = ConfigurationManager.AppSettings["WebPort"];
            httpListener = new HttpListener();
            if (httpProt.Contains(","))
            {
                string[] ports = httpProt.Split(',');
                for (int i = 0; i < ports.Length; i++)
                {
                    httpListener.Prefixes.Add("http://+:" + ports[i] + "/");
                    Logger.Sys("启动web监听，http:" + ports[i]);
                }                         
            }
            else
            {
                httpListener.Prefixes.Add("http://+:" + httpProt + "/");
                Logger.Sys("启动web监听，端口:" + httpProt);
            }
            try
            {
                httpListener.Start();
            }
            catch 
            {
                throw new Exception("权限不足,尝试以管理员权限运行cmd，输入: netsh http add urlacl url=http://+:httpProt/ user=username");
            }
          
            httpListener.BeginGetContext(new AsyncCallback(OnGetContext), httpListener);  //开始异步接收request请求
        }
        void OnGetContext(IAsyncResult ar)
        {
            HttpListener httpListener = ar.AsyncState as HttpListener;
            HttpListenerContext context = httpListener.EndGetContext(ar);  //接收到的请求context（一个环境封装体）
            httpListener.BeginGetContext(new AsyncCallback(OnGetContext), httpListener);  //开始 第二次 异步接收request请求

            /*-------------------------开始处理请求-----------------------------*/
            HttpListenerRequest request = context.Request;  //接收的request数据
            HttpListenerResponse response = context.Response;  //用来向客户端发送回复
            try
            {
                switch (request.Url.AbsolutePath)
                {
                    case "/ServState"://GM通知重载服务器状态和白名单列表
                        HttpServState(request, response);
                        break;
                    case "/ServUrl"://客户端请求游戏服地址                  
                        HttpServUrl(context);
                        break;
                    case "/PayNotify"://sdk支付回调 http://192.168.0.108:7000/payNotify
                        HttpPayCall(context);
                        break;
                    case "/Aihelp"://接受AiHelp推送消息 userId={userId}&title={title}&body={body}&serverId={serverId}&key={key}
                        HttpAiHelp(context);
                        break;
                    case "/TestConnect"://测试连接
                        ResponseOutput(response, "Succeed");
                        break;
                    default:
                        ResponseOutput(response, "error");
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        public void ResponseOutput(HttpListenerResponse response, string str)
        {
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.ContentType = "text/html;charset=UTF-8";
            response.ContentEncoding = Encoding.UTF8;
            using (Stream output = response.OutputStream)  //发送回复
            {
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                output.Write(buffer, 0, buffer.Length);              
                output.Close();
            }
        }

        public void ResponseOutputJson(HttpListenerResponse response, string str)
        {
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            using (Stream output = response.OutputStream)  //发送回复
            {
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }


        /// <summary>
        /// 向会话的客户端发送消息
        /// </summary>
        /// <param name="data"></param>
        public void ResponseError(HttpListenerResponse response, string error)
        {
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.ContentType = "text/html;charset=UTF-8";
            response.ContentEncoding = Encoding.UTF8;
            string str = $@"{{""msg"":""{error}""}}";
            using (Stream output = response.OutputStream)
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
                //response.ContentLength64 = buffer.Length;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }


    }
}
