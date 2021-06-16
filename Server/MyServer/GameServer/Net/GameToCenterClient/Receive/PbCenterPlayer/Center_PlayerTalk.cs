using PbCenterPlayer;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //中央服务器通知游戏服务器玩家是否禁言
        void Center_PlayerTalk(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_PlayerTalk msg = e.Msg as SC_Center_PlayerTalk;

        }
    }
}
