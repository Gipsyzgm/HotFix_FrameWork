using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求购买金币
        void Player_buyGold(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_player_buyGold msg = e.Msg as CS_player_buyGold;

            //发送数据
            //SC_player_buyGold sendMsg = new SC_player_buyGold();            
            //e.Send(sendMsg);
        }
    }
}
