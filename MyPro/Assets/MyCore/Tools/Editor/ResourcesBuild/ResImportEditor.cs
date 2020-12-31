using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CSF;
public class ResImportEditor : AssetPostprocessor
{
    const string UIAtlasSourceDir = "Assets/GameRes/ArtRes/UIAtlas/";
    const string TextrueDir = "Assets/GameRes/BundleRes/Textures";
    const string ArtTextrueDir = "Assets/GameRes/ArtRes/Textures";

    const string SkeletonAnimationDir = "Assets/GameRes/BundleRes/SkeletonAnimation";

    /// <summary>
    /// 自动设置Textures格式
    /// </summary>
    public void OnPreprocessTexture()
    {
        TextureImporter importer = assetImporter as TextureImporter;
        if (importer.assetPath.StartsWith(UIAtlasSourceDir))
            SetUIAtlasSource(importer);
        else if (importer.assetPath.StartsWith("Assets/")) // if (importer.assetPath.StartsWith(TextrueDir)|| importer.assetPath.StartsWith(ArtTextrueDir) || importer.assetPath.StartsWith(SkeletonAnimationDir))
            SetTexture(importer);
    }

    /// <summary>
    /// 设置UI图集图片属性
    /// </summary>
    /// <param name="importer"></param>
    static void SetUIAtlasSource(TextureImporter importer)
    {
        importer.textureType = TextureImporterType.Sprite;
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
        //iosSetting.name = "iOS"; //Android
        //iosSetting.maxTextureSize = Mathf.Min(importer.GetPlatformTextureSettings("iOS").maxTextureSize, 4096);

        TextureImporterPlatformSettings androidSetting = importer.GetPlatformTextureSettings("Android");
        if (!androidSetting.overridden)
        {
            androidSetting.overridden = true;
            isChange = true;
        }
        //androidSetting.name = "Android";
        //androidSetting.maxTextureSize = Mathf.Min(importer.GetPlatformTextureSettings("Android").maxTextureSize, 4096);

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

    static void OnPostprocessAllAssets(
       string[] importedAssets,
       string[] deletedAssets,
       string[] movedAssets,
       string[] movedFromAssetPaths)
    {
        if (movedAssets.Length != 0)
            autoSetAssetBundleName(movedAssets);
        else if (importedAssets.Length != 0)
            autoSetAssetBundleName(importedAssets);
    }

    /// <summary>
    /// 资源导入时自动设置AssetBundle name
    /// 只修改 BundleRes目录下的资源
    /// </summary>
    /// <param name="importedAssets"></param>
    static void autoSetAssetBundleName(string[] importedAssets)
    {
       
    }

}