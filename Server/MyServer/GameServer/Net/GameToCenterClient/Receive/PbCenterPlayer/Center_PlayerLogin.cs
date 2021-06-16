using PbCenterPlayer;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //返回中服务登录注册成功返回(成功T下线后才返回)
        void Center_PlayerLogin(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_PlayerLogin msg = e.Msg as SC_Center_PlayerLogin;

        }
    }
}
