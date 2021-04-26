using DG.Tweening;
using HotFix.Module.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace HotFix
{
    public partial class TipItem : BaseItem
    {

        /// <summary>添加按钮事件</summary>
        public override void Init()
        {
            //当前对象点击事件需添加Button组件
            InitContentY = Bg.transform.localPosition.y;
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

        private float InitContentY = 0;

        public void TipsAnim(string content)
        {
            CurObj.SetActive(true);
            Bg.transform.localPosition = Vector3.up * InitContentY;
            Bg.transform.localScale = Vector3.zero;
            TipsText.text = content;
            Sequence sequenc = DOTween.Sequence();
            sequenc.Append(Bg.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(true));
            sequenc.Append(Bg.transform.DOLocalMoveY(InitContentY + 250, 0.8f).SetEase(Ease.Linear)).SetUpdate(true).OnComplete(() => {
                TipsText.text = string.Empty;
                CurObj.SetActive(false);
                Tips.CacheTipsList.Enqueue(this);
            });
        }
    }
}

