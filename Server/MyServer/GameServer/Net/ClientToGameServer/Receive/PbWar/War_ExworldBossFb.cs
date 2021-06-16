using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求退出世界Boss副本
        void War_ExworldBossFb(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_ExworldBossFb msg = e.Msg as CS_war_ExworldBossFb;

            //发送数据
            //SC_war_ExworldBossFb sendMsg = new SC_war_ExworldBossFb();            
            //e.Send(sendMsg);
        }
    }
}
