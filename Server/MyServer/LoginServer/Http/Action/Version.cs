using CommonLib.Comm.DBMgr;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /// 获取版本信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void HttpServVersion(HttpListenerContext context)
        {
            string PF = "";
            JObject jo;
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                jo = (JObject)JsonConvert.DeserializeObject(reader.ReadToEnd());
                reader.Close();
            }          
            PF = jo["PF"].ToString();
            List<TVersion> version = MongoDBHelper.Instance.Select<TVersion>();
            bool IsBack = false;
            foreach (var item in version)
            {
                if (PF.ToLower() == item.Platform.ToLower())
                {
                    IsBack = true;
                    VersionInfo info = new VersionInfo();
                    info.Platform = item.Platform;
                    info.AppVersion = item.AppVersion;
                    info.IsForcedUpdate = item.IsForcedUpdate;
                    info.AppDownloadURL = item.AppDownloadURL;
                    Glob.http.ResponseOutput(context.Response, HttpMsg.Message(info.ToString()));
                }
            }

            if (!IsBack)
            {               
                ResponseOutput(context.Response, HttpMsg.ErrorString("No server information found"));
            }


        }
    }
}
