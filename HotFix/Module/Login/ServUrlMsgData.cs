using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix.Module.Login
{
    public class ServUrlMsgData
    {
        /// <summary>
        /// 服务器Id
        /// </summary>
        public int ServerId;
        //连接IP
        public string IP;
        /// <summary>
        /// 连接端口
        /// </summary>
        public int Port;
        /// <summary>
        /// 时间戳
        /// </summary>
        public int Timestamp;
        /// <summary>
        /// 登录成功生成登录口令,GameServer进行验证
        /// </summary>
        public string Token;

        public static ServUrlMsgData Deserialize(string data)
        {
            return JsonMapper.ToObject<ServUrlMsgData>(data);
        }

        public override string ToString()
        {
            return JsonMapper.ToJson(this);
        }
    }
}
