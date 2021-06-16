using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取赛季令牌奖励
        void Player_SeasonGet(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_player_SeasonGet msg = e.Msg as CS_player_SeasonGet;

            //发送数据
            //SC_player_SeasonGet sendMsg = new SC_player_SeasonGet();            
            //e.Send(sendMsg);
        }
    }
}
