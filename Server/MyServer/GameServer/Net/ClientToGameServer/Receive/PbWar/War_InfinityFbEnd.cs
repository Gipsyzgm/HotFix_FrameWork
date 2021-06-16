using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求无尽副本关卡结束
        void War_InfinityFbEnd(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_InfinityFbEnd msg = e.Msg as CS_war_InfinityFbEnd;

            //发送数据
            //SC_war_InfinityFbEnd sendMsg = new SC_war_InfinityFbEnd();            
            //e.Send(sendMsg);
        }
    }
}
