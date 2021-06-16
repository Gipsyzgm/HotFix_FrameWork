using CommonLib;
using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public class FriendMgr
    {

        public DictionarySafe<ObjectId, DictionarySafe<ObjectId, FriendData>> FriendList = new DictionarySafe<ObjectId, DictionarySafe<ObjectId, FriendData>>();
        
        public FriendMgr()
        {
            //List<TFriend> friendlist = DBReader.Instance.SelectAllList<TFriend>();
            //DictionarySafe<ObjectId, FriendData> list;
            //TPlayer friednP;
            //foreach (TFriend friend in friendlist)
            //{
            //    if (!FriendList.TryGetValue(friend.pId, out list))
            //    {
            //        list = new DictionarySafe<ObjectId, FriendData>();
            //        FriendList.Add(friend.pId, list);
            //    }
            //    if (Glob.tPlayerMgr.playerDataList.TryGetValue(friend.fId, out friednP))
            //    {
            //        list.Add(friend.fId,new FriendData(friend, friednP));
            //    }                
            //}
        }

        /// <summary>
        /// 每天重置制造次数
        /// </summary>
        public void ResertNum()
        {
            //已领取好把收到好友赠送设为false
            UpdateDefinition<TFriend>  update = Builders<TFriend>.Update.Set(t => t.isRecAC, false);
            FilterDefinition<TFriend> filter = Builders<TFriend>.Filter.Eq(t => t.isGetAC, true);
            MongoDBHelper.Instance.UpdateMany<TFriend>(null, update);

            update = Builders<TFriend>.Update.Set(t => t.isSendAC, false).Set(t => t.isGetAC, false);
            MongoDBHelper.Instance.UpdateMany<TFriend>(null, update);

            foreach (DictionarySafe<ObjectId, FriendData> list in FriendList.Values)
            {
                foreach (FriendData fd in list.Values)
                {
                    if (fd.Data.isGetAC)
                        fd.Data.isRecAC = false;

                    fd.Data.isSendAC = false;
                    fd.Data.isGetAC = false;
                }
            }
        }
        //向好友赠送体力
        public bool SendAC(Player player,FriendData friend)
        {
            if (friend.Data.isSendAC)  //已经赠送过
                return false;

            friend.Data.isSendAC = true;
            friend.Data.Update();

            //friend.Data.pId
            DictionarySafe<ObjectId, FriendData> list;
            if (FriendList.TryGetValue(friend.Data.fId, out list))      //好友的好友列表不存在
            {
                FriendData fd;
                if (list.TryGetValue(player.ID, out fd)) //好友的好友列表中没有自己
                {

                    fd.Data.isRecAC = true;
                    fd.Data.Update();
                }
            }
            return true;
        }
    }
}
