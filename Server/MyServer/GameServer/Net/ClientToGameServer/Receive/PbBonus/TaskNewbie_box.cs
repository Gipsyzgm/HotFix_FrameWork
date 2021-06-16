using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求新手活动宝箱进度奖励
        void TaskNewbie_box(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_taskNewbie_box msg = e.Msg as CS_taskNewbie_box;

            //发送数据
            //SC_taskNewbie_box sendMsg = new SC_taskNewbie_box();            
            //e.Send(sendMsg);
        }
    }
}
