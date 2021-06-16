using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求玩家修改头像
        void Player_changeIcon(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_player_changeIcon msg = e.Msg as CS_player_changeIcon;

            //发送数据
            //SC_player_changeIcon sendMsg = new SC_player_changeIcon();            
            //e.Send(sendMsg);
        }
    }
}
