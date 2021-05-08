using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using HotFix.Module.Login;
using HotFix.Module.UI;

namespace HotFix
{
    public partial class LoginUI : BaseUI
    {
        bool isLogingIng = false;
        /// <summary>添加按钮事件</summary>
        public override void Init(params object[] _args)
        { 
            args = _args;
            LoginButton.onClick.AddListener(LoginButton_Click);   //登录
            SdkLogin.onClick.AddListener(SdkLogin_Click);   //SDK登录
            ChangServerBtn.onClick.AddListener(ChangServerBtn_Click);   //选择服务器
            StartGameButton.onClick.AddListener(StartGameButton_Click);   //开始游戏
            LoginOutButton.onClick.AddListener(LoginOutButton_Click);   //注销  
            SetLoginState();
        }

        /// <summary>跟据平台类型设置登录状态</summary>
        void SetLoginState()
        {
            if (AppSetting.PlatformType == EPlatformType.AccountPwd)
            {
                PasswordLogin.SetActive(!LoginMgr.I.IsLogin);
                GoSelServer.SetActive(LoginMgr.I.IsLogin);
                SdkLogin.gameObject.SetActive(false);        
                LoginUserName.text = LoginMgr.I.UserName;
            }
            else if (AppSetting.PlatformType == EPlatformType.OWN_GP)
            {
                PasswordLogin.SetActive(!LoginMgr.I.IsLogin);
                GoSelServer.SetActive(LoginMgr.I.IsLogin);
                SdkLogin.gameObject.SetActive(false);
                LoginUserName.text = LoginMgr.I.UserName;
            }       
            if (LoginMgr.I.IsLogin)
            {
                //如果已经登录，可能需要显示选择服务器
                GoSelServer.SetActive(true);
                SetServerInfo();
            }    
        }
        /// <summary>
        /// 设置服务器信息
        /// </summary>
        public void SetServerInfo()
        {
           
            ServerItemData server = ServerListMgr.I.GetSelectServer();
            CurServerName.text = server != null ? server.ServerName : string.Empty;
            if (server != null)
            {
                switch (server.Flag) //服务器标记
                {
                    case 1: //新服 暂时只一个标记
                        TagImage.gameObject.SetActive(true);
                        break;
                    default:
                        TagImage.gameObject.SetActive(false);
                        break;
                }
                CurServerName.text = server.ServerId.ToString() +":" + CurServerName.text;
            }
        }

        public void ResetConnectInt()
        {
            isLogingIng = false;
        }
        /// <summary>刷新</summary>
        public override void Refresh()
        {
        }

        /// <summary></summary>
        void LoginButton_Click()
        {
            string userName = InputFieldUserName.text.Trim();
            string password = InputFieldPassword.text.Trim();
            if (userName == string.Empty)
            {
                Tips.ShowLang("LoginUI.UserName.Placeholder"); //请输入用户名
                return;
            }

            if (password == string.Empty)
            {
                Tips.ShowLang("LoginUI.Password.Placeholder"); //请输入密码
                return;
            }
            //登录成功
            LoginMgr.I.LoginSucc(userName, password);
            SetLoginState();
        }
        /// <summary></summary>
        void SdkLogin_Click()
        {

        }
        /// <summary></summary>
        void ChangServerBtn_Click()
        {
            HotMgr.UI.Show<SelServerUI>().Run();
        }
        /// <summary></summary>
        void StartGameButton_Click()
        {
            StartGame_Task().Run();
        }
        private async CTask StartGame_Task()
        {            
            if (isLogingIng) return;
            isLogingIng = true;
            bool isSucc = false;
            if (AppSetting.PlatformType == EPlatformType.AccountPwd)
            {
                isSucc = await LoginMgr.I.Login(InputFieldUserName.text.Trim(), InputFieldPassword.text.Trim());
            }
            else
            {
                string error = await LoginMgr.I.SDKLogin(LoginMgr.I.UserName);
                if (!string.IsNullOrEmpty(error))
                {

                    Confirm.ShowConfirm(() => { SDKLogin().Run(); }, error,HotMgr.Lang.Get("Net.DisconnectTitle"));
                }
                else
                {
                    isSucc = true;
                }
            }
            if (!isSucc)
                isLogingIng = false;
        }
        async CTask SDKLogin()
        {
            //await HotMgr.UI.GetPanel<MovieUI>().AwaitPlayEnd();         
            string error = await LoginMgr.I.SDKLogin(LoginMgr.I.UserName);
            if (!string.IsNullOrEmpty(error))
            {
                isLogingIng = false;
                Confirm.ShowConfirm(() => { SDKLogin().Run(); }, error, HotMgr.Lang.Get("Net.DisconnectTitle"));
            }
        }

        /// <summary>注销登录</summary>
        void LoginOutButton_Click()
        {
            InputFieldUserName.text = string.Empty;
            InputFieldPassword.text = string.Empty;
            LoginMgr.I.Logout();
            SetLoginState();

        }



        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }
    }
}

