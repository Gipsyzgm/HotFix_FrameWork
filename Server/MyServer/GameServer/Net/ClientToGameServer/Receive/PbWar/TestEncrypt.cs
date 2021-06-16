using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //测试消息加密
        void TestEncrypt(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_TestEncrypt msg = e.Msg as CS_TestEncrypt;

            //发送数据
            //SC_TestEncrypt sendMsg = new SC_TestEncrypt();            
            //e.Send(sendMsg);
        }
    }
}
