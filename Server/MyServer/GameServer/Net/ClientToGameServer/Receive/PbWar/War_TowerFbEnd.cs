using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求爬塔副本关卡结束
        void War_TowerFbEnd(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_TowerFbEnd msg = e.Msg as CS_war_TowerFbEnd;

            //发送数据
            //SC_war_TowerFbEnd sendMsg = new SC_war_TowerFbEnd();            
            //e.Send(sendMsg);
        }
    }
}
