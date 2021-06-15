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
        /// 缓存ConfirmItem实例，留着下次使用，关闭的都移进来
        /// </summary>
        public static Queue<ConfirmItem> CacheConfirmsList = new Queue<ConfirmItem>();
        /// <summary>
        /// 需要手动关闭的ConfirmItem实例。
        /// </summary>
        public static Stack<ConfirmItem> ConfirmNeedCloseList = new Stack<ConfirmItem>();

        /// <summary>
        /// 显示Confirm
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="cancelCB">取消回调</param>
        /// <param name="content">消息内容</param>
        /// <param name="title">标题,不传使用默认标题</param>
        /// <param name="autoClose">是否点击按钮后自动关闭页面，不需要的话需要手动调用ManualClose()关闭</param>
        /// <param name="ShowMask">是否使用遮罩</param>
        /// <param name="SurperTips">是否在所有Ui上</param>
        public static void Show(Action confirmCB, Action cancelCB, string content, string title = null, bool autoClose = true, bool ShowMask = true,bool SurperTips = false)
        {
            if (SurperTips)
            {
                if (HotMgr.UI.layer_dict.TryGetValue(PanelLayer.UITips, out var transform))
                {
                    transform.GetComponent<Canvas>().sortingOrder = 2000;
                }
            }
            else
            {
                if (HotMgr.UI.layer_dict.TryGetValue(PanelLayer.UITips, out var transform))
                {
                    transform.GetComponent<Canvas>().sortingOrder = 200;
                }
            }           
            RunShow(confirmCB, cancelCB, content, title, autoClose, ShowMask).Run();
        }

        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="content">消息内容</param>
        /// <param name="title">标题,不传使用默认标题</param>
        /// <param name="ShowMask">是否使用遮罩</param>
        /// <param name="SurperTips">是否在所有Ui上</param>
        public static void ShowConfirm(Action confirmCB, string content, string title = null, bool autoClose = true, bool ShowMask = true, bool SurperTips = false)
        {
            if (SurperTips)
            {
                if (HotMgr.UI.layer_dict.TryGetValue(PanelLayer.UITips, out var transform))
                {
                    transform.GetComponent<Canvas>().sortingOrder = 2000;
                }
            }
            else
            {
                if (HotMgr.UI.layer_dict.TryGetValue(PanelLayer.UITips, out var transform))
                {
                    transform.GetComponent<Canvas>().sortingOrder = 200;
                }
            }
            RunShow(confirmCB, null, content, title, autoClose, ShowMask).Run();
        }

        /// <summary>
        /// 显示确认框
        /// </summary>
        /// <param name="confirmCB">确认回调</param>
        /// <param name="cancelCB">取消回调</param>
        /// <param name="content">消息内容</param>
        /// <param name="title">标题,不传使用默认标题</param>
        /// <param name="ShowMask">是否使用遮罩</param>
        private static async CTask RunShow(Action confirmCB,Action cancelCB,string content,string title,bool autoClose, bool ShowMask)
        {
            ConfirmItem item;
            if (CacheConfirmsList.Count > 0)
            {
                item = CacheConfirmsList.Dequeue();      
            }
            else
            {
                item = new ConfirmItem();
                await item.Instantiate(HotMgr.UI.layer_dict[PanelLayer.UITips]);             
            }
            item.SetMask(ShowMask);
            item.SetAutoClose(autoClose);
            item.ShowCustomConfirm(content, title, confirmCB, cancelCB);
            //把需要手动关闭的Item放进list
            if (autoClose == false)
            {
                ConfirmNeedCloseList.Push(item);
            }
            
        }
        //手动关闭最后一个需要关闭的Confirm
        public static void ManualClose() 
        {
            if (ConfirmNeedCloseList.Count>0)
            {
                ConfirmNeedCloseList.Pop().CloseSelf();
            }
        }

    }
}

