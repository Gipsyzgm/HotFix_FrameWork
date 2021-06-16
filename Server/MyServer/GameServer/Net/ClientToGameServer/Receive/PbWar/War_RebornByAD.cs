using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求章节内看广告复活
        void War_RebornByAD(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_RebornByAD msg = e.Msg as CS_war_RebornByAD;

            //发送数据
            //SC_war_RebornByAD sendMsg = new SC_war_RebornByAD();            
            //e.Send(sendMsg);
        }
    }
}
