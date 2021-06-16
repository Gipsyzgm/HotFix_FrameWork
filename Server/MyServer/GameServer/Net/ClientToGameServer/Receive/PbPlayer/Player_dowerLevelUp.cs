using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求天赋升级
        void Player_dowerLevelUp(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_player_dowerLevelUp msg = e.Msg as CS_player_dowerLevelUp;

            //发送数据
            //SC_player_dowerLevelUp sendMsg = new SC_player_dowerLevelUp();            
            //e.Send(sendMsg);
        }
    }
}
