using PbTask;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求增加广告进度（加一次广告次数）
        void TaskLine_Add(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_taskLine_Add msg = e.Msg as CS_taskLine_Add;

            //发送数据
            //SC_taskLine_Add sendMsg = new SC_taskLine_Add();            
            //e.Send(sendMsg);
        }
    }
}
