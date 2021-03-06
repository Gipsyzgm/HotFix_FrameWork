﻿/// <summary>
/// 文件名： AutoSetUISpriteAtlas用于创建和修改UI预制体时自动设置其引用的图集。
/// </summary>
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEditor.Experimental.SceneManagement;
using System.Threading.Tasks;
/// <summary>
/// 自动设置UI引用的SpriteAtlsas
/// </summary>
public class AutoSetUISpriteAtlas : UnityEditor.AssetModificationProcessor
{
    //UI目录等同于AppSetting.AllUIPrefabs
    private static string UI_DIR = AppSetting.AllUIPrefabs;
    #region Apply
    [InitializeOnLoadMethod]
    static void StartInitializeOnLoadMethod()
    { 
        // Prefab被保存之前回调
        PrefabStage.prefabSaving += SaveUIPrefabInHierachy;

        //注册Apply时的回调
        PrefabUtility.prefabInstanceUpdated = delegate (GameObject instance)
        {
            if (instance)
                SaveUIPrefabInProgect(instance);
        };

    }
    /// <summary>
    /// 保存UI预制
    /// 自动添加引用图集依赖
    /// </summary>
    /// <param name="instance"></param>
    public static void SaveUIPrefabInProgect(GameObject instance)
    {     
        string prefabPath = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(instance));
        if (!IsUIPrefab(prefabPath))
            return;
        GameObject go = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(instance) as GameObject;
        Image[] imgs = go.GetComponentsInChildren<Image>(true);       
        Dictionary<string, SpriteAtlas> saDict = new Dictionary<string, SpriteAtlas>();
        List<SpriteAtlas> saList = new List<SpriteAtlas>();
        string imgPath;
        string spriteAtlasPath;
        SpriteAtlas sa;        
        foreach (Image img in imgs)
        {
            imgPath = AssetDatabase.GetAssetPath(img.sprite);
            if (imgPath.IndexOf("/UIAtlas/") == -1) continue;
            imgPath = imgPath.Substring(0,imgPath.LastIndexOf("/"));
            spriteAtlasPath = imgPath.Replace("/ArtRes/UIAtlas/", "/AddressableRes/UIAtlas/") + ".spriteatlas";
            if (!saDict.TryGetValue(spriteAtlasPath, out sa))
            {
                sa = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(spriteAtlasPath);
                if (sa != null)
                {
                    saDict.Add(spriteAtlasPath, sa);
                    saList.Add(sa);
                }
                else
                {
                    Debug.LogWarning("SpriteAtlas未找到:"+spriteAtlasPath);
                }
            }
        }
        SpriteAtlasList compAtlas = go.GetComponent<SpriteAtlasList>();
        if (saList.Count > 0)
        {
            if (compAtlas == null)
                compAtlas = go.AddComponent<SpriteAtlasList>();
            compAtlas.AtlasList = saList.ToArray();
        }
        else
        {
            if (compAtlas != null)
                Component.DestroyImmediate(compAtlas, true);
        }
        PrefabUtility.RevertObjectOverride(instance, InteractionMode.AutomatedAction);
        AssetDatabase.Refresh();
    }

    public static void SaveUIPrefabInHierachy(GameObject instance)
    {
        if (instance.GetComponent<UIOutlet>()==null)
            return;
        Image[] imgs = instance.GetComponentsInChildren<Image>(true);
        Dictionary<string, SpriteAtlas> saDict = new Dictionary<string, SpriteAtlas>();
        List<SpriteAtlas> saList = new List<SpriteAtlas>();
        string imgPath;
        string spriteAtlasPath;
        SpriteAtlas sa;
        foreach (Image img in imgs)
        {
            imgPath = AssetDatabase.GetAssetPath(img.sprite);
            if (imgPath.IndexOf("/UIAtlas/") == -1) continue;
            imgPath = imgPath.Substring(0, imgPath.LastIndexOf("/"));
            spriteAtlasPath = imgPath.Replace("/ArtRes/UIAtlas/", "/AddressableRes/UIAtlas/") + ".spriteatlas";
            if (!saDict.TryGetValue(spriteAtlasPath, out sa))
            {
                sa = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(spriteAtlasPath);
                if (sa != null)
                {
                    saDict.Add(spriteAtlasPath, sa);
                    saList.Add(sa);
                }
                else
                {
                    Debug.LogWarning("SpriteAtlas未找到:" + spriteAtlasPath);
                }
            }
        }
        SpriteAtlasList compAtlas = instance.GetComponent<SpriteAtlasList>();
        if (saList.Count > 0)
        {
            if (compAtlas == null)
                compAtlas = instance.AddComponent<SpriteAtlasList>();
            compAtlas.AtlasList = saList.ToArray();
        }
        else
        {
            if (compAtlas != null)
                Component.DestroyImmediate(compAtlas, true);
        }
        AssetDatabase.Refresh();
    }
    #endregion

    /// <summary>
    /// 是否为UI预制
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    static bool IsUIPrefab(string path)
    {
        if (path.Contains(UI_DIR) && Path.GetExtension(path) == ".prefab")
            return true;
        return false;
    }
}