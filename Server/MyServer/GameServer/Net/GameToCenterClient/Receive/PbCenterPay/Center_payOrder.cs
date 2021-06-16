using PbCenterPay;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //收到中央服充值下单信息
        void Center_payOrder(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_payOrder msg = e.Msg as SC_Center_payOrder;

        }
    }
}
