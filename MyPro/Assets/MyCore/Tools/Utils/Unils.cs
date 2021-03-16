using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unils
{
    /// <summary>
    /// ��list��Χ��ѡ��n����ͬ����
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
    /// �ж��Ƿ�����Ui����
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
    /// �ж��ֻ�����
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
    /// ��������תUI����
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
    /// ����ɫ������
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
    /// ���������һ��·��������ͬβ׺��Component
    /// </summary>
    /// <param name="obj">����</param>
    /// <param name="path">·��</param>
    /// <param name="end">β׺����.cs</param>
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
    /// ��������ת�ֲ����꣬����λ�ö����ԡ�
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
    //�������꣨targetTrans��ת��ԣ�parent�����������ꡣ
    //����targetTrans�����������ǣ�1��1, 1��parent�����������ǣ�2,2,2����ô��õ������������parentΪ����ԭ��ģ�1,1,1��λ�ã���������ӷ���Ϊ��3,3,3��
    //������ô��⣬�����target�ľֲ�����ת�����������ꡣ����target���������localPositionת���������ꡣ(ֻ��ת�����壬�����岻�С�)
    //����ͨ�����������������InverseTransformPointת�������parent�����������ꡣȻ����TransformPointת���������ꡣ��֤�����������ǻ���ģ�
    //�ţ��Ҷ��ܻ�ȡ������������������˻�תʲô�أ�û̫�����������ľ���Ӧ�ó�����

    /// <summary>
    /// �ֲ�����ת�������꣬
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
