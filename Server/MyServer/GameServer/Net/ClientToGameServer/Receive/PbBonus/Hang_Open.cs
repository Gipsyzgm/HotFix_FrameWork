using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求打开挂机奖励页面
        void Hang_Open(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_hang_Open msg = e.Msg as CS_hang_Open;

            //发送数据
            //SC_hang_Open sendMsg = new SC_hang_Open();            
            //e.Send(sendMsg);
        }
    }
}
