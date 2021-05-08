//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{
    public partial class SelServerItem : BaseItem
    {
        private Image FlagImage;
        private Image StateImage;
        private Text ServerNameText;
        private Text ServerID;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            FlagImage = Get<Image>("FlagImage");
            StateImage = Get<Image>("StateImage");
            ServerNameText = Get<Text>("ServerNameText");
            ServerID = Get<Text>("ServerID");

        }
    }
}

