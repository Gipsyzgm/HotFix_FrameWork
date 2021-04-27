//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{
    public partial class ConfirmItem : BaseItem
    {
        private GameObject Mask;
        private Text TitleText;
        private Text ContentText;
        private Button CancelButton;
        private Button ConfirmButton;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            Mask = Get("Mask");
            TitleText = Get<Text>("TitleText");
            ContentText = Get<Text>("ContentText");
            CancelButton = Get<Button>("CancelButton");
            ConfirmButton = Get<Button>("ConfirmButton");

        }
    }
}

