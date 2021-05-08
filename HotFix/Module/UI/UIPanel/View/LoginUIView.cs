//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{
    public partial class LoginUI : BaseUI
    {
        private GameObject PasswordLogin;
        private InputField InputFieldUserName;
        private InputField InputFieldPassword;
        private Button LoginButton;
        private Button SdkLogin;
        private GameObject GoSelServer;
        private Text CurServerName;
        private Button ChangServerBtn;
        private Button StartGameButton;
        private Text LoginUserName;
        private Button LoginOutButton;
        private Image TagImage;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            PasswordLogin = Get("PasswordLogin");
            InputFieldUserName = Get<InputField>("InputFieldUserName");
            InputFieldPassword = Get<InputField>("InputFieldPassword");
            LoginButton = Get<Button>("LoginButton");
            SdkLogin = Get<Button>("SdkLogin");
            GoSelServer = Get("GoSelServer");
            CurServerName = Get<Text>("CurServerName");
            ChangServerBtn = Get<Button>("ChangServerBtn");
            StartGameButton = Get<Button>("StartGameButton");
            LoginUserName = Get<Text>("LoginUserName");
            LoginOutButton = Get<Button>("LoginOutButton");
            TagImage = Get<Image>("TagImage");

        }
    }
}

