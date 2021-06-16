using PbCenterPlayer;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //收到中央服更新活动
        void Center_Activity(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_Activity msg = e.Msg as SC_Center_Activity;

        }
    }
}
