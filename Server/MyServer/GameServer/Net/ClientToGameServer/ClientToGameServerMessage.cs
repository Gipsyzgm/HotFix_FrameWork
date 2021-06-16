using CSocket;
using Google.Protobuf;
using CommonLib;
using Telepathy;
namespace  GameServer.Net
{
    public class ClientToGameServerMessage:CServerMessage
    {
        public ClientToGameServerMessage(Server server, int clientid) : base(server,clientid)
        {
                
        }
        public ClientToGameServerMessage(Server server,int clientid, IMessage msg, ushort protocol) : base(server,clientid,msg,protocol)
        {
                     
        }

        public override void Send<M>(M data)
        {
            ushort protocol = ClientToGameServerProtocol.Instance.GetProtocolByType(data.GetType());
            if (protocol == 0)
            {
                Logger.MessageError(data.GetType());
                return;
            }
            byte[] body = data.ToByteArray();
            Logger.LogMsg(true, protocol, body.Length, data);
            SendMessage(body, protocol);                     
        }
    }    
}
