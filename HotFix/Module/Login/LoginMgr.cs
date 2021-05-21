using HotFix.Module.UI;
using HotFix.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace HotFix.Module.Login
{
    /// <summary>
    /// 登录数据管理器
    /// </summary>
    public partial class LoginMgr : BaseDataMgr<LoginMgr>, IDisposable
    {
        private string Prefs_UserName = "Login_UserName";
        private string Prefs_VisitorId = "Prefs_VisitorId";
        /// <summary>用户名</summary>
        public string UserName = string.Empty;
        /// <summary>游客Id</summary>
        public string VisitorId = string.Empty;
        /// <summary>密码</summary>
        public string Password = string.Empty;
        /// <summary>参数1</summary>
        public string Param1 = string.Empty;     
        /// <summary>平台渠道Id</summary>
        public string PfChId = string.Empty;
        /// <summary>平台用户Id</summary>
        public string PlatformId = string.Empty;
        /// <summary>平台Token</summary>
        public string Token = string.Empty;
        /// <summary>
        /// 是否正在连接服务器
        /// </summary>
        public bool isConnectIng = false;

        private ServUrlMsgData reLoginServerInfo;

        private string reLoginPfId;

        public string nowServerId;
        public LoginMgr()
        {
            UserName = PlayerPrefs.GetString(Prefs_UserName, string.Empty);
            VisitorId = PlayerPrefs.GetString(Prefs_VisitorId, string.Empty);
        }
        /// <summary>是否登录</summary>
        public bool IsLogin => UserName != string.Empty;
        /// <summary>
        /// 登录成功后保存用户名和密码，仅用作辨别是否是登陆
        /// </summary>
        public void LoginSucc(string name)
        {
            if (AppSetting.PlatformType == EPlatformType.AccountPwd||AppSetting.PlatformType == EPlatformType.OWN_GP)
            {
                UserName = name;
                PlayerPrefs.SetString(Prefs_UserName, UserName);
                PlayerPrefs.Save();
            }
            else
            {
                //获取账号绑定信息

            }         
        }

        /// <summary>登出</summary>
        public void Logout()
        {
            UserName = string.Empty;   
            PlayerPrefs.SetString(Prefs_UserName, string.Empty);
            PlayerPrefs.Save();
        }


        public async CTask ShowNotice()
        {
            string versionFilesURL = AppSetting.HTTPServerURL + "Notice.txt";
            UnityWebRequest request = UnityWebRequest.Get(versionFilesURL);
            await request.SendWebRequest();
            if (request.error != null || request.downloadHandler.text == "")
            {
                return;
            }
            string[] line = request.downloadHandler.text.Split('\n');
            string title = line[0];
            string content = request.downloadHandler.text.Remove(0, title.Length + 1);
            //await HotMgr.UI.Show<NoticeUI>(line[0], content);

        }
        /// <summary>
        /// 清理游客登录缓存
        /// </summary>
        public void ClearVisitorCache()
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(Prefs_UserName)))
            {
                PlayerPrefs.DeleteKey(Prefs_UserName);
            }

            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(Prefs_VisitorId)))
            {
                PlayerPrefs.DeleteKey(Prefs_VisitorId);
            }
        }

        public override void Dispose()
        {
            Param1 = string.Empty;
            PfChId = string.Empty;
            PlatformId = string.Empty;
            Token = string.Empty;
            isConnectIng = false;
        }

    }
}
