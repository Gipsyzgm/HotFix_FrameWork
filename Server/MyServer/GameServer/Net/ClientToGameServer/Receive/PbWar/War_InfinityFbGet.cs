using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求无尽副本进度奖励
        void War_InfinityFbGet(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_InfinityFbGet msg = e.Msg as CS_war_InfinityFbGet;

            //发送数据
            //SC_war_InfinityFbGet sendMsg = new SC_war_InfinityFbGet();            
            //e.Send(sendMsg);
        }
    }
}
