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

            Music.onValueChanged.AddListener((b) =>
            {
                Debug.Log("开启音乐：" + b);
                HotMgr.Sound.isPlayMusic = b;

            });
            Sound.onValueChanged.AddListener((b) =>
            {
                Debug.Log("开启音效：" + b);
                HotMgr.Sound.isPlayEffect = b;
            });
            ZH_CN.onValueChanged.AddListener((b) =>
            {
                HotMgr.Lang.LangType = ELangType.ZH_CN;

            });
            ZH_TW.onValueChanged.AddListener((b) =>
            {
                HotMgr.Lang.LangType = ELangType.ZH_TW;

            });
            EN.onValueChanged.AddListener((b) =>
            {
                HotMgr.Lang.LangType = ELangType.EN;

            });
            JA.onValueChanged.AddListener((b) =>
            {
                HotMgr.Lang.LangType = ELangType.JA;

            });
            KO.onValueChanged.AddListener((b) =>
            {
                HotMgr.Lang.LangType = ELangType.KO;

            });
            CloseBtn.AddClick(() =>
            {
                HideSelf(UIAnim.FadeOut);
            });
        }
        /// <summary>刷新</summary>
        public override void Refresh()
        {
            switch (HotMgr.Lang.LangType)
            {
                case ELangType.ZH_CN:
                    ZH_CN.isOn = true;
                    break;
                case ELangType.ZH_TW:
                    ZH_TW.isOn = true;
                    break;
                case ELangType.EN:
                    EN.isOn = true;
                    break;
                case ELangType.JA:
                    JA.isOn = true;
                    break;
                case ELangType.KO:
                    KO.isOn = true;
                    break;
                default:
                    break;
            }
        }
        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {

        }
    }
}

