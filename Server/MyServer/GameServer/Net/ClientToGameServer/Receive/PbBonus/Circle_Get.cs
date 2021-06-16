using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求转盘奖励
        void Circle_Get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Circle_Get msg = e.Msg as CS_Circle_Get;

            //发送数据
            //SC_Circle_Get sendMsg = new SC_Circle_Get();            
            //e.Send(sendMsg);
        }
    }
}
