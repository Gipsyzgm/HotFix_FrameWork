using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求五连抽英雄
        void Box_GetHerofive(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Box_GetHerofive msg = e.Msg as CS_Box_GetHerofive;

            //发送数据
            //SC_Box_GetHerofive sendMsg = new SC_Box_GetHerofive();            
            //e.Send(sendMsg);
        }
    }
}
