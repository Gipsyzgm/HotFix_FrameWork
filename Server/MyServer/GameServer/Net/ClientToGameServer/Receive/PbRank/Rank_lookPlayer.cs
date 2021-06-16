using PbRank;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求查看其他玩家信息
        void Rank_lookPlayer(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_rank_lookPlayer msg = e.Msg as CS_rank_lookPlayer;

            //发送数据
            //SC_rank_lookPlayer sendMsg = new SC_rank_lookPlayer();            
            //e.Send(sendMsg);
        }
    }
}
