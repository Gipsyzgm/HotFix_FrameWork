using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取章节宝箱
        void War_GetBox(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_GetBox msg = e.Msg as CS_war_GetBox;

            //发送数据
            //SC_war_GetBox sendMsg = new SC_war_GetBox();            
            //e.Send(sendMsg);
        }
    }
}
