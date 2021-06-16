using PbCenterPay;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //收到中央服支付成功通知GS发货
        void Center_paySucceed(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_paySucceed msg = e.Msg as SC_Center_paySucceed;

        }
    }
}
