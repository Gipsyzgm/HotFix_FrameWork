//批量修改图片属性
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class SetImageAttrib
{
    const string UIAtlasSourceDir = "Assets/GameRes/ArtRes/UIAtlas/";

    [MenuItem("Assets/★工具★/批量设置图片属性(全部)", false, 50)]
    static private void SetImageAttr()
    {
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        List<TextureImporter> listTexture = new List<TextureImporter>();
        foreach (string assetGuid in AssetDatabase.FindAssets("",new string[] { "Assets" }))
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            if (assetImporter != null)
            {
                TextureImporter importer = assetImporter as TextureImporter;
                if (importer != null)
                {
                    listTexture.Add(importer);
                }
            }
        }

        int startIndex = 0;
        int succCount = 0;
        Debug.Log($"开始修改图片属性,共找到{listTexture.Count}图片");
        EditorApplication.update = delegate ()
        {
            TextureImporter importer = listTexture[startIndex];
            bool isCancel = EditorUtility.DisplayCancelableProgressBar("修改中", importer.assetPath, (float)startIndex / (float)listTexture.Count);

            if (SetTexture(importer))
                succCount++;
            startIndex++;
            if (isCancel || startIndex >= listTexture.Count)
            {
                EditorUtility.ClearProgressBar();
                EditorApplication.update = null;
                startIndex = 0;
                watch.Stop();
                Debug.Log($"批量修改图片属性完成,共{listTexture.Count}图片, 执行修改{succCount},用时:{watch.ElapsedMilliseconds / 1000.0f}秒");
            }
        };
    }

    [MenuItem("Assets/★工具★/批量设置图片属性(多选)", false, 51)]
    static private void SetSelectImageAttr()
    {
        List<TextureImporter> listTexture = new List<TextureImporter>();

        if (Selection.assetGUIDs.Length == 0)
        {
            Debug.LogError("请选择要修改的资源!!");
            return;
        }      

        foreach (var guid in Selection.assetGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AssetImporter assetImporter = AssetImporter.GetAtPath(path);
            if (assetImporter != null)
            {
                TextureImporter importer = assetImporter as TextureImporter;
                if (importer != null)
                {
                    listTexture.Add(importer);
                }
            }
        }
        int startIndex = 0;
        int succCount = 0;
        EditorApplication.update = delegate ()
        {
            TextureImporter importer = listTexture[startIndex];

            bool isCancel = EditorUtility.DisplayCancelableProgressBar("修改中", importer.assetPath, (float)startIndex / (float)listTexture.Count);
            if (SetTexture(importer))
                succCount++;
            startIndex++;
            if (isCancel || startIndex >= listTexture.Count)
            {
                EditorUtility.ClearProgressBar();
                EditorApplication.update = null;
                startIndex = 0;               
                Debug.Log($"批量修改图片属性完成,共{listTexture.Count}图片, 执行修改{succCount}");
            }
        };
    }
    public static bool SetTexture(TextureImporter importer)
    {
        if (importer.assetPath.StartsWith(UIAtlasSourceDir)) return false; //图集里面的图片不需要修改
        bool isChange = false;
        if (importer.mipmapEnabled == true)
        {
            importer.mipmapEnabled = false;
            isChange = true;
        }

        TextureImporterPlatformSettings iosSetting = importer.GetPlatformTextureSettings("iOS");
        if (!iosSetting.overridden)
        {
            iosSetting.overridden = true;
            isChange = true;
        }

        TextureImporterPlatformSettings androidSetting = importer.GetPlatformTextureSettings("Android");
        if (!androidSetting.overridden)
        {
            androidSetting.overridden = true;
            isChange = true;
        }

        if (iosSetting.format != TextureImporterFormat.ASTC_6x6)
        {
            iosSetting.format = TextureImporterFormat.ASTC_6x6;
            isChange = true;
        }

        if (importer.DoesSourceTextureHaveAlpha())
        {
            if (androidSetting.format != TextureImporterFormat.ETC2_RGBA8)
            {
                androidSetting.format = TextureImporterFormat.ETC2_RGBA8;
                isChange = true;
            }
        }
        else
        {
            if (androidSetting.format != TextureImporterFormat.ETC2_RGB4)
            {
                androidSetting.format = TextureImporterFormat.ETC2_RGB4;
                isChange = true;
            }
        }
        if (isChange)
        {
            importer.SetPlatformTextureSettings(iosSetting);
            importer.SetPlatformTextureSettings(androidSetting);
            importer.SaveAndReimport();
        }
        return isChange;
    }
}
