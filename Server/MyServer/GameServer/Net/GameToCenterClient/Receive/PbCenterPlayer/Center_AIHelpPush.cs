using PbCenterPlayer;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //通知游戏服玩家有客服推送消息
        void Center_AIHelpPush(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_AIHelpPush msg = e.Msg as SC_Center_AIHelpPush;

        }
    }
}
