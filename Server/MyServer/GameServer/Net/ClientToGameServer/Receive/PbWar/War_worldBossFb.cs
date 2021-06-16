using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求进入世界Boss副本
        void War_worldBossFb(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_worldBossFb msg = e.Msg as CS_war_worldBossFb;

            //发送数据
            //SC_war_worldBossFb sendMsg = new SC_war_worldBossFb();            
            //e.Send(sendMsg);
        }
    }
}
