using HotFix.Module.UI;
using PbLogin;
using UnityEngine;

namespace  HotFix.Net
{
    public partial class ClientToGameClientAction
    {
        //收到登录验证
        void Login_verify(ClientToGameClientMessage e)
        {
            //收到的数据
            SC_login_verify msg = e.Msg as SC_login_verify;
            Debug.Log("收到登录数据");
            switch (msg.Result)
            {
                case Enum_verify_result.VrUnknown:
                    Tips.ShowLang("Com_Unknown");
                    break;
                case Enum_verify_result.VrSucceed: //登录成功  
                    Tips.Show("登录成功");
                    return;
                case Enum_verify_result.VrFailure: //验证失败
                    Tips.ShowLang("LoginVerify.Failure");
                    break;
                case Enum_verify_result.VrServerIdError: //服务器Id错误
                    Tips.ShowLang("LoginVerify.ServerIdError");
                    break;
                case Enum_verify_result.VrChError: //渠道未开放
                    Tips.ShowLang("LoginVerify.ChError");
                    break;
            }

        }
    }
}
