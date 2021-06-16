using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求悬浮宝箱广告获取
        void FloatBox_award(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_FloatBox_award msg = e.Msg as CS_FloatBox_award;

            //发送数据
            //SC_FloatBox_award sendMsg = new SC_FloatBox_award();            
            //e.Send(sendMsg);
        }
    }
}
