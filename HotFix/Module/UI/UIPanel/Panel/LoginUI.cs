using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace HotFix
{
    public partial class LoginUI : BaseUI
    {      
        /// <summary>添加按钮事件</summary>
        public override void Init(params object[] _args)
        { 
            args = _args;
            LoginButton.onClick.AddListener(LoginButton_Click);   //
            SdkLogin.onClick.AddListener(SdkLogin_Click);   //
            ChangServerBtn.onClick.AddListener(ChangServerBtn_Click);   //
            StartGameButton.onClick.AddListener(StartGameButton_Click);   //
            LoginOutButton.onClick.AddListener(LoginOutButton_Click);   //

        }

         /// <summary>刷新</summary>
        public override void Refresh()
        {
        }

        /// <summary></summary>
        void LoginButton_Click()
        {
        }
        /// <summary></summary>
        void SdkLogin_Click()
        {
        }
        /// <summary></summary>
        void ChangServerBtn_Click()
        {
        }
        /// <summary></summary>
        void StartGameButton_Click()
        {
        }
        /// <summary></summary>
        void LoginOutButton_Click()
        {
        }



        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }
    }
}

