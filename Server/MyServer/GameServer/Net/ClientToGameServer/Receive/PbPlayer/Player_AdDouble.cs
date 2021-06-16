using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取玩家广告双倍奖励
        void Player_AdDouble(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_player_AdDouble msg = e.Msg as CS_player_AdDouble;

            //发送数据
            //SC_player_AdDouble sendMsg = new SC_player_AdDouble();            
            //e.Send(sendMsg);
        }
    }
}
