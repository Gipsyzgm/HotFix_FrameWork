using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求副本战斗重生
        void War_fbRebirth(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_fbRebirth msg = e.Msg as CS_war_fbRebirth;

            //发送数据
            //SC_war_fbRebirth sendMsg = new SC_war_fbRebirth();            
            //e.Send(sendMsg);
        }
    }
}
