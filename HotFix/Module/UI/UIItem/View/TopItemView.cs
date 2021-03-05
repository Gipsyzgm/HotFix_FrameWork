//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{
    public partial class TopItem : BaseItem
    {
        private Slider ExpSlider;
        private Text ExpText;
        private Button InfoBtn;
        private Text LvText;
        private Button CoinsBtn;
        private Text CoinText;
        private Button GemBtn;
        private Text GemText;
 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {
            ExpSlider = Get<Slider>("ExpSlider");
            ExpText = Get<Text>("ExpText");
            InfoBtn = Get<Button>("InfoBtn");
            LvText = Get<Text>("LvText");
            CoinsBtn = Get<Button>("CoinsBtn");
            CoinText = Get<Text>("CoinText");
            GemBtn = Get<Button>("GemBtn");
            GemText = Get<Text>("GemText");

        }
    }
}

