using PbMail;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取邮件附件奖励
        void Mail_getAward(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_mail_getAward msg = e.Msg as CS_mail_getAward;

            //发送数据
            //SC_mail_getAward sendMsg = new SC_mail_getAward();            
            //e.Send(sendMsg);
        }
    }
}
