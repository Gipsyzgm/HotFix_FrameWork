﻿using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //保存指引步骤
        void Save_guideStep(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_save_guideStep msg = e.Msg as CS_save_guideStep;

            //发送数据
            //SC_save_guideStep sendMsg = new SC_save_guideStep();            
            //e.Send(sendMsg);
        }
    }
}
