using PbTask;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取任务线上的任务奖励
        void TaskLine_get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_taskLine_get msg = e.Msg as CS_taskLine_get;

            //发送数据
            //SC_taskLine_get sendMsg = new SC_taskLine_get();            
            //e.Send(sendMsg);
        }
    }
}
