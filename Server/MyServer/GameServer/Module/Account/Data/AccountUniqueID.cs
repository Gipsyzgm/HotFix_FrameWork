using System;

namespace GameServer.Module
{
    public class AccountUniqueID : IEquatable<AccountUniqueID>
    {
        public int Platform { get; }
        public string PlatformID { get; }
       // public int ServerId { get; }
        /// <summary>
        /// 账号唯一ID
        /// </summary>
        /// <param name="platform">平台类型</param>
        /// <param name="platformid">平台Id</param>
        /// <param name="serverid">服务器Id</param>
        public AccountUniqueID(int platform, string platformid)
        {
            Platform = platform;
            PlatformID = platformid;
            //ServerId = serverid;
        }

        /// <summary>
        /// 平台Id,服务id,平台类型相同则表示玩家账号信息已存在
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AccountUniqueID other)
        {
            if (other == null)
                return false;
            return other.Platform.Equals(Platform) && other.PlatformID.Equals(PlatformID);
        }
        public override string ToString()
        {
            return $"Platform:{Platform},PlatformID:{PlatformID}";
        }
        public override int GetHashCode()
        {
            return (this.ToString()).GetHashCode();
        }
    }
}
