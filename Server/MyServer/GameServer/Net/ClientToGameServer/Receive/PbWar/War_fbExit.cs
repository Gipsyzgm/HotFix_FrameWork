using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求退出章节
        void War_fbExit(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_fbExit msg = e.Msg as CS_war_fbExit;

            //发送数据
            //SC_war_fbExit sendMsg = new SC_war_fbExit();            
            //e.Send(sendMsg);
        }
    }
}
