using CSocket;
using Google.Protobuf;
namespace  GameServer.Net
{
    public class GameToCenterClientMessage:CClientMessage
    {
        public GameToCenterClientMessage(ushort protocol, IMessage data) : base(protocol, data)
        {
           
           
        }
    }
}
