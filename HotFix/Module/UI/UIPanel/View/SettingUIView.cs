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
        private Toggle ZH_CN;
        private Toggle ZH_TW;
        private Toggle EN;
        private Toggle JA;
        private Toggle KO;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            Music = Get<Toggle>("Music");
            Sound = Get<Toggle>("Sound");
            CloseBtn = Get<Button>("CloseBtn");
            ZH_CN = Get<Toggle>("ZH_CN");
            ZH_TW = Get<Toggle>("ZH_TW");
            EN = Get<Toggle>("EN");
            JA = Get<Toggle>("JA");
            KO = Get<Toggle>("KO");

        }
    }
}

