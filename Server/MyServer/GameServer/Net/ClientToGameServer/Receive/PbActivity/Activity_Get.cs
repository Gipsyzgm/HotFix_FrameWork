using PbActivity;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取活动任务奖励
        void Activity_Get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Activity_Get msg = e.Msg as CS_Activity_Get;

            //发送数据
            //SC_Activity_Get sendMsg = new SC_Activity_Get();            
            //e.Send(sendMsg);
        }
    }
}
