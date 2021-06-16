using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求玩家改名
        void Player_changeName(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_player_changeName msg = e.Msg as CS_player_changeName;

            //发送数据
            //SC_player_changeName sendMsg = new SC_player_changeName();            
            //e.Send(sendMsg);
        }
    }
}
