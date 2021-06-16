using CommonLib.Comm.DBMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 好友数据
    /// </summary>
    public class FriendData : BaseEntity<TFriend>
    {
        public TPlayer Friend;
        public FriendData(TFriend data,TPlayer friend)
        {
            Data = data;
            Friend = friend;
        }
        //public One_friend_info GetFriendMsg()
        //{
        //    One_friend_info one = new One_friend_info();
        //    one.SID = Friend.shortId;
        //    one.Name = Friend.name;       
        //    one.IsGetAC = Data.isGetAC;
        //    one.IsRecAC = Data.isRecAC;
        //    one.IsSendAC = Data.isSendAC;
        //    one.IsOnline = Glob.playerMgr.onlinePlayerList.ContainsKey(Friend.id);
        //    return one;
        //}
    }
}
