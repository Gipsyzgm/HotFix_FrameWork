using PbPay;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取首充奖励
        void Pay_getFirstPay(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_pay_getFirstPay msg = e.Msg as CS_pay_getFirstPay;

            //发送数据
            //SC_pay_getFirstPay sendMsg = new SC_pay_getFirstPay();            
            //e.Send(sendMsg);
        }
    }
}
