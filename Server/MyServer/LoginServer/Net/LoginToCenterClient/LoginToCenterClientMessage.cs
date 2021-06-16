using CSocket;
using Google.Protobuf;
namespace  LoginServer.Net
{
    public class LoginToCenterClientMessage:CClientMessage
    {
        public LoginToCenterClientMessage(ushort protocol, IMessage data) : base(protocol, data)
        {
           
           
        }
    }
}
