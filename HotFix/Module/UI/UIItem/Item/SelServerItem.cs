using HotFix.Module.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace HotFix
{
    public partial class SelServerItem : BaseItem
    {

        public Action<SelServerItem> onClick;
        public ServerItemData Data;
        /// <summary>添加按钮事件</summary>
        public override void Init()
        {
             CurObj.GetComponent<Button>().onClick.AddListener(self_Click);  //当前对象点击事件

        }

        /// <summary>设置数据</summary><param name="data"></param>
        public void SetData(ServerItemData data)
        {
            Data = data;
            Refresh();
        }

        /// <summary>刷新Item</summary>
        public override void Refresh()
        {
            if (!IsInstance) return;
            ServerNameText.text = Data.ServerName;
            ServerID.text = Data.ServerId.ToString();
            switch (Data.Flag) //服务器标记
            {
                case 1: //新服 暂时只一个标记
                    FlagImage.SetSprite("Icon_New", UIAtlas.Common).Run();
                    FlagImage.gameObject.SetActive(true);
                    break;
                default:
                    FlagImage.gameObject.SetActive(false);
                    break;
            }
            if (Data.URL == ServerListMgr.I.ServerURL)
            {
                FlagImage.SetSprite("Icon_Last", UIAtlas.Common).Run();
                FlagImage.gameObject.SetActive(true);
            }
        }
        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {
            onClick?.Invoke(this);
        }

        /// <summary>释放Item引用</summary>
        public override void Dispose()
        {
            base.Dispose();
            Data = null;
            onClick = null;
        }
    }
}

