using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取开服基金
        void OpenFund_get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_openFund_get msg = e.Msg as CS_openFund_get;

            //发送数据
            //SC_openFund_get sendMsg = new SC_openFund_get();            
            //e.Send(sendMsg);
        }
    }
}
