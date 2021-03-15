//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{
    public partial class SettingUI : BaseUI
    {
        private Toggle Music;
        private Toggle Sound;
        private Button CloseBtn;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            Music = Get<Toggle>("Music");
            Sound = Get<Toggle>("Sound");
            CloseBtn = Get<Button>("CloseBtn");

        }
    }
}

