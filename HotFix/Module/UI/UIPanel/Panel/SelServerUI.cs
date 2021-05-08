using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace HotFix
{
    public partial class SelServerUI : BaseUI
    {      
        /// <summary>添加按钮事件</summary>
        public override void Init(params object[] _args)
        { 
            args = _args;
            CloseBtn.onClick.AddListener(CloseBtn_Click);   //

        }

         /// <summary>刷新</summary>
        public override void Refresh()
        {

        }

        /// <summary></summary>
        void CloseBtn_Click()
        {
            CloseSelf();
        }



        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }
    }
}

