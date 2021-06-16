using PbPay;
namespace  LoginServer.Net
{
    public partial class LoginToCenterClientAction
    {
        //收到中央服充值付款成功处理返回
        void Pay_Succeed(LoginToCenterClientMessage e)
        {
            //收到的数据
            SC_Pay_Succeed msg = e.Msg as SC_Pay_Succeed; 
           
        }
    }
}
