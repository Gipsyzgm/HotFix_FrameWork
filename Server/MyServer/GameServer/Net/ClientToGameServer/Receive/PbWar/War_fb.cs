using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求副本挑战
        void War_fb(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_fb msg = e.Msg as CS_war_fb;

            //发送数据
            //SC_war_fb sendMsg = new SC_war_fb();            
            //e.Send(sendMsg);
        }
    }
}
