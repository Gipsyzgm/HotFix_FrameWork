using System;

namespace GameServer.Module
{
    public class PlayerUniqueName : IEquatable<PlayerUniqueName>
    {
        public string PlayerName { get; }
        public int ServerId { get; }
        /// <summary>
        /// 玩家唯一名字
        /// </summary>
        /// <param name="PlayerName">玩家名</param>
        /// <param name="ServerId">服务器Id</param>
        public PlayerUniqueName(string playername, int serverid)
        {
            PlayerName = playername;
            ServerId = serverid;
        }

        /// <summary>
        /// 同一个服务器ID只能一个同名玩家,合服重名不要紧
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PlayerUniqueName other)
        {
            if (other == null)
                return false;
            return other.PlayerName.Equals(PlayerName) && other.ServerId.Equals(ServerId);
        }
        public override string ToString()
        {
            return $"s{ServerId}.{PlayerName}";
        }
        public override int GetHashCode()
        {
            return (this.ToString()).GetHashCode();
        }
    }
}
