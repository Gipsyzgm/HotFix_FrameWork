using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace HotFix
{
    public partial class SettingUI : BaseUI
    {      
        /// <summary>添加按钮事件</summary>
        public override void Init(params object[] _args)
        { 
            args = _args;



            Music.onValueChanged.AddListener((b)=> 
            {
                Debug.Log("开启音乐："+b);               
                HotMgr.Sound.isPlayMusic = b;
             
            });
            Sound.onValueChanged.AddListener((b) =>
            {
                Debug.Log("开启音效：" + b);
                HotMgr.Sound.isPlayEffect = b;
            });

            CloseBtn.AddClick(() =>
            {
                HideSelf();
            });
        }

         /// <summary>刷新</summary>
        public override void Refresh()
        {
        }




        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }
    }
}

