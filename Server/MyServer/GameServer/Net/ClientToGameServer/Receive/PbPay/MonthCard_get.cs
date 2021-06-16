using PbPay;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取月卡奖励
        void MonthCard_get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_monthCard_get msg = e.Msg as CS_monthCard_get;

            //发送数据
            //SC_monthCard_get sendMsg = new SC_monthCard_get();            
            //e.Send(sendMsg);
        }
    }
}
