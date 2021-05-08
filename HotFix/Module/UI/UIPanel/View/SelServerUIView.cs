//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{
    public partial class SelServerUI : BaseUI
    {
        private Button CloseBtn;
        private ContentSizeFitter Content;
        private GameObject SelServerItem;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            CloseBtn = Get<Button>("CloseBtn");
            Content = Get<ContentSizeFitter>("Content");
            SelServerItem = Get("SelServerItem");

        }
    }
}

