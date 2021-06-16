using PbLogin;
namespace  HotFix.Net
{
    public partial class ClientToGameClientAction
    {
        //收到登录成功
        void Login_playerInfo(ClientToGameClientMessage e)
        {
            //收到的数据
            SC_login_playerInfo msg = e.Msg as SC_login_playerInfo;

            //收到登录成功

        }
    }
}
