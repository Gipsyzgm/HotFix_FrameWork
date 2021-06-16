using PbLogin;
namespace  HotFix.Net
{
    public partial class ClientToGameClientAction
    {
        //收到游客绑定
        void Login_bind(ClientToGameClientMessage e)
        {
            //收到的数据
            SC_login_bind msg = e.Msg as SC_login_bind; 
           
        }
    }
}
