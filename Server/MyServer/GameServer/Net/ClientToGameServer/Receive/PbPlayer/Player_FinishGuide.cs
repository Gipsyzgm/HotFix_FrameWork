using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求完成新手关卡
        void Player_FinishGuide(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_player_FinishGuide msg = e.Msg as CS_player_FinishGuide;

            //发送数据
            //SC_player_FinishGuide sendMsg = new SC_player_FinishGuide();            
            //e.Send(sendMsg);
        }
    }
}
