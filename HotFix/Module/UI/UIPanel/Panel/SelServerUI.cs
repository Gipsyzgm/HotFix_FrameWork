using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using HotFix.Module.Login;

namespace HotFix
{
    public partial class SelServerUI : BaseUI
    {
        /// <summary>服务器列表</summary>
        private List<SelServerItem> itemList = new List<SelServerItem>();
        /// <summary>
        /// 当前选择的服务器
        /// </summary>
        private string SelSeverName = string.Empty;
        /// <summary>添加按钮事件</summary>
        public override void Init(params object[] _args)
        { 
            args = _args;
            CloseBtn.onClick.AddListener(CloseBtn_Click);   //
            SetServerList();
        }
        //设置服务器列表
        void SetServerList()
        {
            SelServerItem item;
            foreach (ServerItemData data in ServerListMgr.I.dicServerList.Values)
            {
                item = new SelServerItem();
                item.Instantiate(SelServerItem, Content.transform);
                item.SetData(data);
                item.onClick = SelectServerItem;
                itemList.Add(item);                          
            }
        }

        /// <summary>
        /// 服务器列表Item点击回调事件
        /// </summary>
        /// <param name="item"></param>
        void SelectServerItem(SelServerItem item)
        {
            SelSeverName = item.Data.ServerName;
            ServerListMgr.I.SetServerId(SelSeverName);
            CloseSelf();
        }
        /// <summary>刷新</summary>
        public override void Refresh()
        {

        }

        /// <summary></summary>
        void CloseBtn_Click()
        {
            CloseSelf(UIAnim.ScaleOut);
        }



        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
            itemList.Dispose();
            itemList = null;
        }
    }
}

