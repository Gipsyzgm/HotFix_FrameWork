using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace HotFix
{
    public partial class SelServerItem : BaseItem
    {

        /// <summary>添加按钮事件</summary>
        public override void Init()
        {
             CurObj.GetComponent<Button>().onClick.AddListener(self_Click);  //当前对象点击事件

        }

        /// <summary>刷新Item</summary>
        public override void Refresh()
        {
        }

        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {

        }

        /// <summary>释放Item引用</summary>
        public override void Dispose()
        {

        }
    }
}

