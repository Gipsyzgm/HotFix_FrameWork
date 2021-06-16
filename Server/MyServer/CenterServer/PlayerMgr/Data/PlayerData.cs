using Google.Protobuf;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterServer.Player
{
    public class PlayerData
    {
        public ObjectId Id;
        public int ServerId;
        public int SessionId;

        public int LoginSessionId;

        public bool IsReLogin;

        public void Send<T>(T data) where T : IMessage
        {
            if(Glob.gameServer.serverList.TryGetValue(ServerId,out var server))
            {
                server.server.Send(data, LoginSessionId);
            }
                
        }
    }
}
