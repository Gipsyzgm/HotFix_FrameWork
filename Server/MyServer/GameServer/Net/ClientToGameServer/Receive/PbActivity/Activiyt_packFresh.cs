using PbActivity;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求钻石刷新活动礼包
        void Activiyt_packFresh(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Activiyt_packFresh msg = e.Msg as CS_Activiyt_packFresh;

            //发送数据
            //SC_Activiyt_packFresh sendMsg = new SC_Activiyt_packFresh();            
            //e.Send(sendMsg);
        }
    }
}
