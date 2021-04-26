//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{
    public partial class TipItem : BaseItem
    {
        private Text TipsText;
        private GameObject Bg;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            TipsText = Get<Text>("TipsText");
            Bg = Get("Bg");

        }
    }
}

