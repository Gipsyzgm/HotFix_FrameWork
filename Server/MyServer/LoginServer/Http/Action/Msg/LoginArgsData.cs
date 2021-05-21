using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoginServer.Http
{
    public struct LoginArgsData
    {
        /// <summary>
        /// 平台类型 0账密登录 1 SDK  2 unity一健登录玩家账号
        /// </summary>
        public int PFType;
        /// <summary>
        /// 平台ID  (账号)
        /// </summary>
        public string PFId;
        /// <summary>
        /// 平台Token (密码)
        /// </summary>
        public string PFToken;

        /// <summary>
        /// （getChannel）4位
        /// </summary>
        public string ChannelId;

        /// <summary>
        /// （getMediaChannelId）10位
        /// </summary>
        public string MediaChannelId;

        /// <summary>
        /// 客户端自定义数据，作为事务唯一标识，建议设置为int类型随机数(不大于8位)
        /// </summary>
        public string Tag;
        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceId;
        /// <summary>
        /// Sdk版本号
        /// </summary>
        public string SdkVer;
        /// <summary>
        /// 客户端版本号
        /// </summary>
        public string ClientVer;

        public static LoginArgsData Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<LoginArgsData>(data);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
