/************************** * 文件名:AutoSetTextureUISprite.cs; * 文件描述:导入图片资源到Unity时，自动修改为UI 2D Sprite***************************/
using UnityEngine;
using System.Collections;
using UnityEditor;
public class AutoSetTextureUISprite : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        //自动设置类型;
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
    }
}