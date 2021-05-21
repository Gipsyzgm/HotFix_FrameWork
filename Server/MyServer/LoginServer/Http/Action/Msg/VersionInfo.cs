using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Http
{
    public class VersionInfo
    {
        /// <summary>平台 Android/IOS </summary>
        public string Platform { get; set; }
        /// <summary>客户端版本 </summary>
        public string AppVersion { get; set; }

        /// <summary>是否强制更新</summary>
        public bool IsForcedUpdate { get; set; }
        /// <summary>强更包路径</summary>
        public string AppDownloadURL { get; set; }


        public static VersionInfo Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<VersionInfo>(data);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
