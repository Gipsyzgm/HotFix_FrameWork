using PbCenterPlayer;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //中央服务器通知GameServer玩家下线
        void Center_PlayerLogout(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_PlayerLogout msg = e.Msg as SC_Center_PlayerLogout;

        }
    }
}
