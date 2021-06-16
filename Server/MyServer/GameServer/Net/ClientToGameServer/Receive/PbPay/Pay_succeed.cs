using PbPay;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求支付成功
        void Pay_succeed(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_pay_succeed msg = e.Msg as CS_pay_succeed;

            //发送数据
            //SC_pay_succeed sendMsg = new SC_pay_succeed();            
            //e.Send(sendMsg);
        }
    }
}
