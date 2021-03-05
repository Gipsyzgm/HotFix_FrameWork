using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace HotFix
{
    public partial class TopItem : BaseItem
    {

        /// <summary>添加按钮事件</summary>
        public override void Init()
        {
             //当前对象点击事件需添加Button组件
            InfoBtn.onClick.AddListener(InfoBtn_Click);   //
            CoinsBtn.onClick.AddListener(CoinsBtn_Click);   //
            GemBtn.onClick.AddListener(GemBtn_Click);   //

        }

        /// <summary>刷新Item</summary>
        public override void Refresh()
        {
        }

        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {

        }
        /// <summary></summary>
        void InfoBtn_Click()
        {
        }
        /// <summary></summary>
        void CoinsBtn_Click()
        {
        }
        /// <summary></summary>
        void GemBtn_Click()
        {
        }

        /// <summary>释放Item引用</summary>
        public override void Dispose()
        {

        }
    }
}

