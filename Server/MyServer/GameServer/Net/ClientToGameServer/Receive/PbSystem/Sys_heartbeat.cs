using PbSystem;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求心跳包30秒一次
        void Sys_heartbeat(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_sys_heartbeat msg = e.Msg as CS_sys_heartbeat;

            //发送数据
            //SC_sys_heartbeat sendMsg = new SC_sys_heartbeat();            
            //e.Send(sendMsg);
        }
    }
}
