using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CSF
{
    public class UIExtendMenu
    {
        [MenuItem("GameObject/★UI扩展★/创建UI", false,10)]
        static void CreateUIObject(MenuCommand menuCommadn)
        {
            GameObject go = new GameObject("New UI");
            GameObject parent =  GameObject.Find("UICanvas");
            if (parent == null)
                parent = menuCommadn.context as GameObject;
            GameObjectUtility.SetParentAndAlign(go, parent);
            RectTransform rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            go.AddComponent<SpriteAtlasList>();
            go.AddComponent<UIOutlet>();
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★UI扩展★/创建UI Item", false, 10)]
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

        [MenuItem("GameObject/★UI扩展★/创建多语言 Text", false, 10)]
        static void CreateTextLang(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject go = new GameObject("New Lang Text");
                GameObjectUtility.SetParentAndAlign(go, parent);
                go.AddComponent<UILangText>();
                Text txt = go.AddComponent<Text>();
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(200, 22);
                txt.alignment = TextAnchor.MiddleLeft;
                txt.fontSize = 22;
                txt.text = "New Lang Text";
                txt.resizeTextForBestFit = true;
                txt.supportRichText = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/GameRes/BundleRes/Font/Default.otf");
                txt.font = font;
            }
            else
            {
                ToolsHelper.Log("只能在UI下创建LangText");
            }
        }

        [MenuItem("GameObject/★UI扩展★/创建 Text", false, 10)]
        static void CreateText(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject go = new GameObject("New Text");
                GameObjectUtility.SetParentAndAlign(go, parent);
                Text txt = go.AddComponent<Text>();
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = new Vector2(200, 22);
                txt.alignment = TextAnchor.MiddleLeft;
                txt.fontSize = 22;
                txt.text = "New Text";
                txt.resizeTextForBestFit = true;
                txt.supportRichText = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/GameRes/BundleRes/Font/Default.otf");
                txt.font = font;
            }
            else
            {
                ToolsHelper.Log("只能在UI下创建Text");
            }
        }

        [MenuItem("GameObject/★UI扩展★/创建多语言 Button", false, 10)]
        static void CreateButtonLang(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject goBtn = new GameObject("New Button");
                GameObjectUtility.SetParentAndAlign(goBtn, parent);
                Image image = goBtn.AddComponent<Image>();
                image.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameRes/ArtRes/UIAtlas/PublicButton/button-3-1.png");
                image.SetNativeSize();

                Button btn = goBtn.AddComponent<Button>();
                
                GameObject goTxt = new GameObject("Text");
                GameObjectUtility.SetParentAndAlign(goTxt, goBtn);
                goTxt.AddComponent<UILangText>();
                Text txt = goTxt.AddComponent<Text>();
                Color color = Color.black;
                ColorUtility.TryParseHtmlString("#00E3F6", out color);
                txt.color = color;
                goTxt.AddComponent<Outline>();
                RectTransform rect = goTxt.GetComponent<RectTransform>();

                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                txt.fontSize = 35;
                txt.alignment = TextAnchor.MiddleCenter;
                txt.text = "Lang Button";
                //txt.resizeTextForBestFit = true;
                //txt.supportRichText = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/GameRes/BundleRes/Font/Default.otf");
                txt.font = font;
            }
            else
            {
                ToolsHelper.Log("只能在UI下创建LangText");
            }
        }

        [MenuItem("GameObject/★UI扩展★/生成 Item脚本", false, 21)]
        static void CreateItemScript(MenuCommand menuCommadn)
        {
            GameObject target = menuCommadn.context as GameObject;
            if (target != null && (target.name.EndsWith("Item")|| target.name.EndsWith("Icon")))
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
        [MenuItem("GameObject/★UI扩展★/生成 Panel脚本", false, 21)]
        static void CreatePanelScript(MenuCommand menuCommadn)
        {
            GameObject target = menuCommadn.context as GameObject;
            if (target != null && (target.name.EndsWith("Panel")))
            {
                UIOutlet uiObj = target.GetComponent<UIOutlet>();
                if (uiObj != null)
                {
                    UIScriptExport.ExportPanelScript(uiObj);
                    ToolsHelper.Log("生成成功!!!");
                    return;
                }
            }
            ToolsHelper.Log("请选择有效果的Panel对象!!!,Panel包含UIOutlet脚本，并且以Panel命名结尾");
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

        [MenuItem("GameObject/★UI扩展工具★/刷新Lang", false, 9)]
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
        [MenuItem("GameObject/★UI扩展工具★/禁用选择对象全部Raycast", false, 10)]
        static void BanUIRaycast(MenuCommand menuCommadn)
        {
            GameObject target = menuCommadn.context as GameObject;
            if (target != null)
            {
                string name;
                Graphic[] graphicArray = target.GetComponentsInChildren<Graphic>();
                for (int k = 0; k < graphicArray.Length; k++)
                {
                    name = graphicArray[k].gameObject.name;
                    if (name.StartsWith("btn")
                        || name == "imgMask"
                        || name == "Viewport") continue;
                    graphicArray[k].raycastTarget = false;
                    EditorUtility.SetDirty(graphicArray[k]);
                }
            }
        }
    }
}