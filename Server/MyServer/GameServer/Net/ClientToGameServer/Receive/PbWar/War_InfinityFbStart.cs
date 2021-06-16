using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求无尽副本关卡进入
        void War_InfinityFbStart(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_InfinityFbStart msg = e.Msg as CS_war_InfinityFbStart;

            //发送数据
            //SC_war_InfinityFbStart sendMsg = new SC_war_InfinityFbStart();            
            //e.Send(sendMsg);
        }
    }
}
