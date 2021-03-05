using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace HotFix
{
    public partial class MainUI : BaseUI
    {      
        /// <summary>添加按钮事件</summary>
        public override void Init(params object[] _args)
        {
            CreatTopItem().Run();
            args = _args;
            StartBtn.onClick.AddListener(StartBtn_Click);   //
            Setting.onClick.AddListener(Setting_Click);   //
        
        }
         /// <summary>刷新</summary>
        public override void Refresh()
        {

        }

        /// <summary></summary>
        void StartBtn_Click()
        {
            Debug.Log("StartBtn_Click");
        }
        /// <summary></summary>
        void Setting_Click()
        {
            Debug.Log("Setting_Click");
        }



        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }
    }
}

