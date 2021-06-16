using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求三日奖励广告获取
        void Threeads_award(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Threeads_award msg = e.Msg as CS_Threeads_award;

            //发送数据
            //SC_Threeads_award sendMsg = new SC_Threeads_award();            
            //e.Send(sendMsg);
        }
    }
}
