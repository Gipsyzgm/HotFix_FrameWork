using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoginServer.Http
{
    public struct ServUrlMsgData
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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
