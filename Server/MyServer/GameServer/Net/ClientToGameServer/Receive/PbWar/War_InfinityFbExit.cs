using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求退出无尽副本
        void War_InfinityFbExit(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_InfinityFbExit msg = e.Msg as CS_war_InfinityFbExit;

            //发送数据
            //SC_war_InfinityFbExit sendMsg = new SC_war_InfinityFbExit();            
            //e.Send(sendMsg);
        }
    }
}
