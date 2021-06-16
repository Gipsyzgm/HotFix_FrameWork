using PbCenterPlayer;
namespace  GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //通知游戏服赛季更新消息
        void Center_SeasonUpdate(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_SeasonUpdate msg = e.Msg as SC_Center_SeasonUpdate; 
           
        }
    }
}
