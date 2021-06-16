using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求退出活动副本
        void War_ExitAcFb(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_ExitAcFb msg = e.Msg as CS_war_ExitAcFb;

            //发送数据
            //SC_war_ExitAcFb sendMsg = new SC_war_ExitAcFb();            
            //e.Send(sendMsg);
        }
    }
}
