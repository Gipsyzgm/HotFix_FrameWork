using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIExtendMenu
{
    [MenuItem("GameObject/★UI扩展★/创建UI", false, 10)]
    static void CreateUIObject(MenuCommand menuCommadn)
    {
        GameObject go = new GameObject("New UI");
        GameObject parent = GameObject.Find("UICanvas");
        if (parent == null)
            parent = menuCommadn.context as GameObject;
        GameObjectUtility.SetParentAndAlign(go, parent);
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        //go.AddComponent<SpriteAtlasList>();
        go.AddComponent<UIOutlet>();
        Selection.activeObject = go;
    }
    [MenuItem("GameObject/★UI扩展★/创建UIItem", false, 10)]
    static void CreateUIObjectItem(MenuCommand menuCommadn)
    {
        GameObject go = new GameObject("New Item");
        GameObject parent = GameObject.Find("UICanvas");
        if (parent == null)
            parent = menuCommadn.context as GameObject;
        GameObjectUtility.SetParentAndAlign(go, parent);
        RectTransform rect = go.AddComponent<RectTransform>();
        go.AddComponent<UIOutlet>();
        Selection.activeObject = go;
    }
    [MenuItem("GameObject/★UI扩展★/生成Item脚本", false, 21)]
    static void CreateItemScript(MenuCommand menuCommadn)
    {
        GameObject target = menuCommadn.context as GameObject;
        if (target != null && (target.name.EndsWith("Item") || target.name.EndsWith("Icon")))
        {
            UIOutlet uiObj = target.GetComponent<UIOutlet>();
            if (uiObj != null)
            {
                UIScriptExport.ExportItemScript(uiObj);
                ToolsHelper.Log("生成成功!!!");
                return;
            }
        }
        ToolsHelper.Log("请选择有效果的Item对象!!!,Item包含UIOutlet脚本，并且以Item(或Icon)命名结尾");
    }
    [MenuItem("GameObject/★UI扩展★/生成UI脚本", false, 21)]
    static void CreateUIScript(MenuCommand menuCommadn)
    {
        GameObject target = menuCommadn.context as GameObject;
        if (target != null && target.transform.parent.name == "UICanvas")
        {
            if (target.name.StartsWith("New UI"))
            {
                ToolsHelper.Log("请修改UI名称!!!");
                return;
            }
            UIOutlet uiObj = target.GetComponent<UIOutlet>();
            if (uiObj != null)
            {
                UIScriptExport.ExportUIScript(uiObj);
                ToolsHelper.Log("生成成功!!!");
                return;
            }
        }
        ToolsHelper.Log("请选择有效果的UI对象!!!");
    }
    [MenuItem("GameObject/★UI扩展★/刷新Lang", false, 9)]
    static void RefreshLang(MenuCommand menuCommadn)
    {
        GameObject target = menuCommadn.context as GameObject;
        UILangText[] langArry = target.transform.GetComponentsInChildren<UILangText>();
        foreach (var variable in langArry)
        {
            //if (!string.IsNullOrEmpty(variable.Key))
            //    variable.Value = LangService.Instance.Get(variable.Key);
        }
    }
}