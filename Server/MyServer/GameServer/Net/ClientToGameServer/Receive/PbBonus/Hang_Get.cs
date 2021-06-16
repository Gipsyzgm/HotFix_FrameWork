using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取挂机奖励
        void Hang_Get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_hang_Get msg = e.Msg as CS_hang_Get;

            //发送数据
            //SC_hang_Get sendMsg = new SC_hang_Get();            
            //e.Send(sendMsg);
        }
    }
}
