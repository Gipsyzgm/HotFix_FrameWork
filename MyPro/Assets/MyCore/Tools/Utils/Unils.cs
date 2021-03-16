using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unils
{
    /// <summary>
    /// 在list范围内选出n个不同对象
    /// </summary>
    /// <param name="list"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static List<int> RandomCount(List<int> list, int n)
    {
        if (list.Count <= n) return null;
        List<int> newList = new List<int>();
        int m = UnityEngine.Random.Range(0, list.Count);
        newList.Add(list[m]);
        while (newList.Count < n)
        {
            int a = UnityEngine.Random.Range(0, list.Count);
            int num = list[a];
            if (!newList.Contains(num))
                newList.Add(num);
        }
        return newList;
    }
    /// <summary>
    /// 判断是否点击在Ui上面
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverGameObject()
    {
        PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;
        List<RaycastResult> list = new List<RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, list);
        return list.Count > 0;
    }
    /// <summary>
    /// 判断手机语言
    /// </summary>
    /// <returns></returns>
    public static string GetUserLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.ChineseSimplified:
                return "CN";
            case SystemLanguage.ChineseTraditional:
                return "TW";
            case SystemLanguage.English:
                return "EN";
            case SystemLanguage.Japanese:
                return "JP";
            case SystemLanguage.Korean:
                return "KO";
            case SystemLanguage.German:
                return "DE";
            case SystemLanguage.French:
                return "FR";
            case SystemLanguage.Russian:
                return "RU";
            case SystemLanguage.Swedish:
                return "SV";
            case SystemLanguage.Portuguese:
                return "PT";
            case SystemLanguage.Spanish:
                return "ES";
            case SystemLanguage.Thai:
                return "TH";
            case SystemLanguage.Unknown:
                return "EN";
            default:
                return "EN";
        };
    }

    /// <summary>
    /// 世界坐标转UI坐标
    /// </summary>
    /// <param name="WorldPos"></param>
    /// <param name="tinkerUp"></param>
    /// <param name="uiCanvas"></param>
    /// <returns></returns>
    public static Vector3 WorldPosToUiPos(Vector3 WorldPos, Canvas uiCanvas)
    {
        Vector3 pt = Camera.main.WorldToScreenPoint(WorldPos);
        Vector2 v;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(uiCanvas.transform as RectTransform, pt, uiCanvas.worldCamera, out v);
        pt = new Vector3(v.x, v.y, 0);
        return pt;
    }
    /// <summary>
    /// 带颜色的文字
    /// </summary>
    /// <param name="info"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string GetColorText(string info, Color color)
    {
        string MyColor = ColorUtility.ToHtmlStringRGB(color);
        return "-><color=#" + MyColor + ">" + info + "</color>";
    }

    /// <summary>
    /// 给物体添加一个路径下所有同尾缀的Component
    /// </summary>
    /// <param name="obj">物体</param>
    /// <param name="path">路径</param>
    /// <param name="end">尾缀比如.cs</param>
    public static void AddFileComment(GameObject obj, string path, string end)
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (!files[i].Name.EndsWith(end)) continue;
            string tempName = files[i].Name.Split('.')[0];
            Type t = Type.GetType(tempName);
            obj.AddComponent(t);
        }
    }
    /// <summary>
    /// 世界坐标转局部坐标，任意位置都可以。
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public Vector3 WorldToLocalPos(Transform parent, Transform worldPos)
    {
        Vector3 LocalPos;
        LocalPos = parent.InverseTransformPoint(worldPos.position);
        return LocalPos;
    }
    //
    //世界坐标（targetTrans）转相对（parent）的世界坐标。
    //例：targetTrans的世界坐标是（1，1, 1）parent的世界坐标是（2,2,2）那么获得的相对坐标是以parent为坐标原点的（1,1,1）位置，即向量相加返回为（3,3,3）
    //可以这么理解，相对于target的局部坐标转化成世界坐标。就是target的子物体的localPosition转成世界坐标。(只能转子物体，孙物体不行。)
    //可以通过孙物体的世界坐标InverseTransformPoint转成相对于parent的子物体坐标。然后再TransformPoint转成世界坐标。（证明两个方法是互逆的）
    //嗯，我都能获取到孙物体的世界坐标了还转什么呢？没太清楚这个方法的具体应用场景。

    /// <summary>
    /// 局部坐标转世界坐标，
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="targetTrans"></param>
    /// <returns></returns>
    public Vector3 targetTransToWorldPos(Transform parent, Transform targetTrans)
    {
        Vector3 WorldPos;
        WorldPos = parent.TransformPoint(targetTrans.localPosition);
        return WorldPos;
    }


}
