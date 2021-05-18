using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoginServer.Http
{
    public struct HttpMsg
    {
        public int code;        //错误码 0 正常
        public string data;    //返回数据Json格式 
        public string msg;    //消息  

        public static HttpMsg Error(string error,int code = 1)
        {
            HttpMsg msg = new HttpMsg();
            msg.code = code;
            msg.msg = error;
            return msg;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static string ErrorString(string error,int code = 1)
        {
            HttpMsg msg = new HttpMsg();
            msg.code = code;
            msg.msg = error;
            return msg.ToString();
        }

        public static string Message(string data)
        {
            HttpMsg msg = new HttpMsg();
            msg.code = 0;
            msg.data = data;
            msg.msg = string.Empty;
            return msg.ToString();
        }
    }
}
