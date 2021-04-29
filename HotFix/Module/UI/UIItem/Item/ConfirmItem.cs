using HotFix.Module.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace HotFix
{
    public partial class ConfirmItem : BaseItem
    {

        private Action ConfirmCallBack;
        private Action CancelCallBack;

        /// <summary>添加按钮事件</summary>
        public override void Init()
        {
             //当前对象点击事件需添加Button组件
            CancelButton.onClick.AddListener(CancelButton_Click);   //
            ConfirmButton.onClick.AddListener(ConfirmButton_Click);   //

        }
        /// <summary>刷新Item</summary>
        public override void Refresh()
        {


           
        }

        public void ShowCustomConfirm(string content, string title = null,Action ConfirmCB = null, Action CancelCB = null)
        {
          
            //至少一个确认
            if (ConfirmCB == null)
            {
                ConfirmCallBack = null;
            }
            else 
            {
                ConfirmCallBack = ConfirmCB;
            }
            //没有取消事件的话就不显示按钮
            if (CancelCB == null)
            {
                CancelCallBack = null;
                CancelButton.gameObject.SetActive(false);
            }
            else
            {
                CancelCallBack = CancelCB;
                CancelButton.gameObject.SetActive(true);                      
            }
            if (title == null)
            {
                TitleText.text = HotMgr.Lang.Get("Com_AlertTitle");
            }
            else 
            {
                TitleText.text = title;
            }
            ContentText.text = content;
            CurObj.SetActive(true);
            //后出现的放后面
            CurObj.transform.SetAsLastSibling();
            ShowUIAnim(CurObj, UIAnim.ScaleIn);
        }

        public void SetMask(bool isShow) 
        {
            if (isShow)
            {
                Mask.SetActive(true);
            }
            else 
            {
                Mask.SetActive(false);
            }
        }


        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {


        }
        /// <summary></summary>
        void CancelButton_Click()
        {
            CancelCallBack?.Invoke();
            ShowUIAnim(CurObj, UIAnim.ScaleOut);
            Confirm.CacheConfirmsList.Enqueue(this);
        }
        /// <summary></summary>
        void ConfirmButton_Click()
        {
            ConfirmCallBack?.Invoke();
            ShowUIAnim(CurObj, UIAnim.ScaleOut);
            Confirm.CacheConfirmsList.Enqueue(this);
        }

        /// <summary>释放Item引用</summary>
        public override void Dispose()
        {

        }
    }
}

