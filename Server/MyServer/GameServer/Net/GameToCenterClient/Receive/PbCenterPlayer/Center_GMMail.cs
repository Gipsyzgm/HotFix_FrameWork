using PbCenterPlayer;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //中央服务器通知游戏服务器玩家发送邮件
        void Center_GMMail(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_GMMail msg = e.Msg as SC_Center_GMMail;

        }
    }
}
