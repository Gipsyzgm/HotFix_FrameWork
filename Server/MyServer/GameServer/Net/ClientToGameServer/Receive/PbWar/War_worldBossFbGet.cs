using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求一键领取世界Boss副本奖励
        void War_worldBossFbGet(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_worldBossFbGet msg = e.Msg as CS_war_worldBossFbGet;

            //发送数据
            //SC_war_worldBossFbGet sendMsg = new SC_war_worldBossFbGet();            
            //e.Send(sendMsg);
        }
    }
}
