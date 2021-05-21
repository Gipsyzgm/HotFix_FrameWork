using CommonLib.Comm.DBMgr;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        /// 获取服务器信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void HttpServerList(HttpListenerContext context)
        {
            string PF = "";
            JObject jo;
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                jo = (JObject)JsonConvert.DeserializeObject(reader.ReadToEnd());
                reader.Close();
            }
            PF = jo["PF"].ToString();
            List<TServerInfo> version = MongoDBHelper.Instance.Select<TServerInfo>();
            List<ServerInfo> infos = new List<ServerInfo>();
            foreach (var item in version)
            {
                ServerInfo info = new ServerInfo();
                info.ServerId = item.ServerId;
                info.ServerName = item.ServerName;
                info.State = item.State;
                info.Flag = item.Flag;
                info.URL = item.URL;
                infos.Add(info);
            }      
            Glob.http.ResponseOutput(context.Response, HttpMsg.Message(JsonConvert.SerializeObject(infos)));         
        }
    }
}
