using PbCenterPlayer;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //中央服务器通知GameServer玩家是否登录
        void Center_PlayerBan(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_PlayerBan msg = e.Msg as SC_Center_PlayerBan;

        }
    }
}
