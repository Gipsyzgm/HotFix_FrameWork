using PbGameSerCommand;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //向游戏服发送命令行
        void Command(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Command msg = e.Msg as SC_Command;

        }
    }
}
