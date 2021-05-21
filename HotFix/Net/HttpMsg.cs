using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix.Net
{
    public class HttpMsg
    {
        public int code;        //错误码 0 正常
        public string data;    //返回数据Json格式 
        public string msg;    //消息  

        //将data序列化成对象
        public T DeserializeData<T>()
        {
            return JsonMapper.ToObject<T>(data);
        }

        public static HttpMsg Deserialize(string msg)
        {
            return JsonMapper.ToObject<HttpMsg>(msg);
        }
    }
}
