using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

//用于刷新语言和适配屏幕
public class UIMgr : BaseMgr<UIMgr>
{
    public Canvas canvas;
    public RectTransform UIRoot;  
    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        UIRoot = canvas.transform.Find("UIRoot").GetComponent<RectTransform>();
        DontDestroyOnLoad(canvas);     
    }

    /// <summary>
    /// 刷新语言
    /// </summary>
    public void RefreshLang()
    {
        UILangText[] list = canvas.GetComponentsInChildren<UILangText>(true);
        for (int i = list.Length; --i >= 0;)
            list[i].Refresh();
    }
}
