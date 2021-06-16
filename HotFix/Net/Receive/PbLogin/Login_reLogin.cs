using PbLogin;
namespace  HotFix.Net
{
    public partial class ClientToGameClientAction
    {
        //收到断线重连数据
        void Login_reLogin(ClientToGameClientMessage e)
        {
            //收到的数据
            SC_login_reLogin msg = e.Msg as SC_login_reLogin; 
           
        }
    }
}
