using PbLogin;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求游客绑定
        void Login_bind(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_login_bind msg = e.Msg as CS_login_bind;

            //发送数据
            //SC_login_bind sendMsg = new SC_login_bind();            
            //e.Send(sendMsg);
        }
    }
}
