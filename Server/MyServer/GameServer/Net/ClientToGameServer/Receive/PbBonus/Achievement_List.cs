using PbBonus;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求成就列表
        void Achievement_List(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Achievement_List msg = e.Msg as CS_Achievement_List; 

            //发送数据
            //SC_Achievement_List sendMsg = new SC_Achievement_List();            
            //e.Send(sendMsg);
        }
    }
}
