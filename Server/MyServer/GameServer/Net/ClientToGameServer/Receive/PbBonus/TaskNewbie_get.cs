using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求新手活动任务奖励
        void TaskNewbie_get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_taskNewbie_get msg = e.Msg as CS_taskNewbie_get;

            //发送数据
            //SC_taskNewbie_get sendMsg = new SC_taskNewbie_get();            
            //e.Send(sendMsg);
        }
    }
}
