using CommonLib;
using PbRegister;

namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        //收到注册GameServer
        void Register_GameServer(GameToCenterClientMessage e)
        {
            //收到的数据
            SC_Register_GameServer msg = e.Msg as SC_Register_GameServer;
            Glob.net.ServerId = msg.ServerId;
            ProgramUtil.SetTitle(msg.ServerId, Glob.net.Port);
        }
    }
}
