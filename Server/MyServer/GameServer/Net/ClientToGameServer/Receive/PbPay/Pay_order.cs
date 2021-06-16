using PbPay;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求充值下定单
        void Pay_order(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_pay_order msg = e.Msg as CS_pay_order;

            //发送数据
            //SC_pay_order sendMsg = new SC_pay_order();            
            //e.Send(sendMsg);
        }
    }
}
