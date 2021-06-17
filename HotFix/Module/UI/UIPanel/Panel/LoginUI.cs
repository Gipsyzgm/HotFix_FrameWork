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
        //是否正在登陆
        bool IsLogingIng = false;
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
            //是否需要选择服务器
            ChangServerBtn.gameObject.SetActive(AppSetting.GameType==GameType.MoreServers);
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
                SetServerInfo();
            }    
        }
        /// <summary>
        /// 设置服务器信息
        /// </summary>
        public void SetServerInfo()
        {
            if (AppSetting.GameType == GameType.MoreServers)
            {   
                //多服则显示服务器信息
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
                    //如果多个服务器的话需要带上服务器ID                    
                    CurServerName.text = server.ServerId.ToString() + ":" + CurServerName.text;

                }
            }
            else 
            {
                //单服只显示开始游戏
                GoSelServer.transform.GetChild(0).gameObject.SetActive(false);
            }            
        }

        /// <summary>
        /// 重置登录状态
        /// </summary>
        public void ResetConnectInt()
        {
            IsLogingIng = false;
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
            LoginMgr.I.LoginSucc(userName);
            SetLoginState();
        }
        /// <summary>Sdk登陆</summary>
        void SdkLogin_Click()
        {
            SDKLogin().Run();
        }
        /// <summary>进入选服页面</summary>
        void ChangServerBtn_Click()
        {
            HotMgr.UI.Show<SelServerUI>(UIAnim.ScaleIn).Run();
        }
        /// <summary>开始游戏</summary>
        void StartGameButton_Click()
        {
            if (IsLogingIng) return;
            StartGame_Task().Run();
        }
        private async CTask StartGame_Task()
        {            
            if (IsLogingIng) return;
            IsLogingIng = true;
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
                IsLogingIng = false;
        }
        async CTask SDKLogin()
        {
            //await HotMgr.UI.GetPanel<MovieUI>().AwaitPlayEnd();         
            string error = await LoginMgr.I.SDKLogin(LoginMgr.I.UserName);
            if (!string.IsNullOrEmpty(error))
            {
                IsLogingIng = false;
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

