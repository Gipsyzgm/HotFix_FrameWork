//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{
    public partial class MainUI : BaseUI
    {
        private Button StartBtn;
        private Button Setting;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            StartBtn = Get<Button>("StartBtn");
            Setting = Get<Button>("Setting");

        }
    }
}

