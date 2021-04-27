using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace HotFix.Module.UI
{
    public class Confirm
    {
        /// <summary>
        /// 缓存ConfirmItem实例，留着下次使用
        /// </summary>
        public static Queue<ConfirmItem> CacheConfirmsList = new Queue<ConfirmItem>();

        /// <summary>
        /// 显示Confirm
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="cancelCB">取消回调</param>
        /// <param name="content">消息内容</param>
        /// <param name="title">标题,不传使用默认标题</param>
        /// <param name="ShowMask">是否使用遮罩</param>
        public static void Show(Action confirmCB, Action cancelCB, string content, string title = null, bool ShowMask = true) 
        {
            RunShow(confirmCB, cancelCB, content,title, ShowMask).Run();
        }
        /// <summary>
        /// 显示确认框
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="cancelCB">取消回调</param>
        /// <param name="content">消息内容</param>
        /// <param name="title">标题,不传使用默认标题</param>
        private static async CTask RunShow(Action confirmCB,Action cancelCB,string content,string title,bool ShowMask)
        {
            //title = title ?? Mgr.Lang.Get("Com_ConfirmTitle");
            ConfirmItem item;
            if (CacheConfirmsList.Count > 0)
            {
                item = CacheConfirmsList.Dequeue();
                item.SetMask(ShowMask);
                item.ShowCustomConfirm(content, title, confirmCB, cancelCB);
            }
            else
            {
                item = new ConfirmItem();
                await item.Instantiate(HotMgr.UI.layer_dict[PanelLayer.UITips]);
                item.SetMask(ShowMask);
                item.ShowCustomConfirm(content, title, confirmCB, cancelCB);
            }                
        }

    //    /// <summary>
    //    /// 显示确认框
    //    /// </summary>
    //    /// <param name="confirmCB">确认回调</param>
    //    /// <param name="cancelCB">取消回调</param>
    //    /// <param name="content">消息内容(语言Key)</param>
    //    /// <param name="title">标题,不传使用默认标题(语言Key)</param>
    //    public static void ShowLang(Action confirmCB, Action cancelCB, string content, string title = null,bool IsConfirmCB = false)
    //    {
    //        title = title == null ? Mgr.Lang.Get("Com_ConfirmTitle") : Mgr.Lang.Get(title);
    //        Mgr.UI.Show<Confirm>(confirmCB, cancelCB, Mgr.Lang.Get(content), title, false, IsConfirmCB);
    //    }

    //    /// <summary>
    //    /// 警告框（只有一个确定按钮）
    //    /// </summary>
    //    /// <param name="confirmCB">确认回调</param>
    //    /// <param name="content">消息内容</param>
    //    /// <param name="title">标题,不传使用默认标题</param>
    //    public static void Alert(Action confirmCB, string content, string title = null)
    //    {
    //        title = title ?? Mgr.Lang.Get("Com_AlertTitle");
    //        Mgr.UI.Show<Confirm>(confirmCB, null,content, title,true,false);
    //    }

    //    /// <summary>
    //    /// 警告框（只有一个确定按钮）
    //    /// </summary>
    //    /// <param name="confirmCB">确认回调</param>
    //    /// <param name="content">消息内容(语言Key)</param>
    //    /// <param name="title">标题,不传使用默认标题(语言Key)</param>
    //    public static void AlertLang(Action confirmCB, string content, string title = null)
    //    {
    //        title = title==null?Mgr.Lang.Get("Com_AlertTitle"): Mgr.Lang.Get(title);
    //        Mgr.UI.Show<Confirm>(confirmCB, null, Mgr.Lang.Get(content), title, true,false);
    //    }

    //    /// <summary>
    //    /// 断线提示 层级要调到最上面
    //    /// </summary>
    //    /// <param name="confirmCB">确认回调</param>
    //    /// <param name="content">消息内容(语言Key)</param>
    //    /// <param name="title">标题,不传使用默认标题(语言Key)</param>
    //    public static async CTask AlertLangTop(Action confirmCB, string content, string title = null)
    //    {
    //        title = title == null ? Mgr.Lang.Get("Com_AlertTitle") : Mgr.Lang.Get(title);
    //        Confirm confirm  = Mgr.UI.Show<Confirm>(confirmCB, null, Mgr.Lang.Get(content), title, true,false);
    //        await confirm.Await();
    //        Canvas can = confirm.gameObject.GetComponent<Canvas>();
    //        if (can == null)
    //        {
    //            can = confirm.gameObject.AddComponent<Canvas>();
    //            //can.overrideSorting = true;
    //            //can.sortingOrder = 60;
    //            confirm.gameObject.AddComponent<GraphicRaycaster>();
    //        }
    //    }
    }
}

