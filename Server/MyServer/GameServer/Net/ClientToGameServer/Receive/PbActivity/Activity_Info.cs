using PbActivity;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求活动列表信息
        void Activity_Info(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Activity_Info msg = e.Msg as CS_Activity_Info;

            //发送数据
            //SC_Activity_Info sendMsg = new SC_Activity_Info();            
            //e.Send(sendMsg);
        }
    }
}
