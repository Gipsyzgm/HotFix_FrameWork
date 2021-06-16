using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求章节关卡结束
        void War_fbStageEnd(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_fbStageEnd msg = e.Msg as CS_war_fbStageEnd;

            //发送数据
            //SC_war_fbStageEnd sendMsg = new SC_war_fbStageEnd();            
            //e.Send(sendMsg);
        }
    }
}
