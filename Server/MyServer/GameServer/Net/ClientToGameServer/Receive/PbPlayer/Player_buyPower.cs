using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求钻石补满体力
        void Player_buyPower(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_player_buyPower msg = e.Msg as CS_player_buyPower;

            //发送数据
            //SC_player_buyPower sendMsg = new SC_player_buyPower();            
            //e.Send(sendMsg);
        }
    }
}
