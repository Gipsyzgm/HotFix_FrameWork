using PbCenterPay;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //收到中央服更新礼包
        void Center_PayPack(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Center_PayPack msg = e.Msg as SC_Center_PayPack;

        }
    }
}
