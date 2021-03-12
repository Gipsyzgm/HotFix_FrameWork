using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

public class CrateAtlas
{
    //代码目录
    static string scriptDir = AppSetting.ExportScriptDir;
    //图集资源目录
    static string atlasPath = AppSetting.UIAtlasPath;
    //图片资源目录
    static string spritePath = AppSetting.UISpritePath;
    [MenuItem("★工具★/创建图集及路径")]
    static void AtlasCreate()
    {
        DirectoryInfo direction = new DirectoryInfo(spritePath);
        DirectoryInfo[] directs = direction.GetDirectories();//文件夹
        DirectoryInfo dir;
        for (int i = 0; i < directs.Length; i++)
        {
            dir = directs[i];
            //需要创建图集的文件夹
            string dataPath = dir.FullName;
            //创建图集的路径,使用Replace确保路径正确替换路径正确
            string atlas = dataPath.Replace("ArtRes","AddressableRes") + ".spriteatlas";
            if (File.Exists(atlas))
            {
                Debug.Log("图集已存在找到" + atlas);
            }
            else
            {
                SpriteAtlas sa = new SpriteAtlas();
                SpriteAtlasPackingSettings packSet = new SpriteAtlasPackingSettings()
                {
                    blockOffset = 1,
                    //Allow Rotation：允许图集的图片旋转，勾选后Unity会寻找最好姿势放置在图集内(这样就能让图集容下更多的图片，但是一般我们不勾选，因为这个旋转会影响到实际显示。）
                    enableRotation = false,
                    //Tight Packing: 勾选后使用精度更高的裁剪图集图片（Mesh裁剪），否则是矩阵裁剪（图片是一块矩形图），如果不需要高精度裁剪就别勾，如果发现裁剪出的图片有显示不正确问题，那就可以勾选它了。
                    enableTightPacking = false,
                    //Padding：图集中一行或一列最大图片个数，有2、4、8选项
                    padding = 4,
                };
                sa.SetPackingSettings(packSet);
                SpriteAtlasTextureSettings textureSet = new SpriteAtlasTextureSettings()
                {
                    //Read/Write Enabled：图集图片读写权限
                    readable = true,
                    //Generate Mip Maps: 消耗更多的内存带来更好的表现，主要针对高配机时可开启，手游一般都可能不开启。
                    generateMipMaps = false,
                    sRGB = true,
                    //Fitler Mode: Point（像素级）、Bilinear（渐变）、Trilinear（更牛逼的）默认Bilinear（一般也是用它，除非你游戏是像素风、或者有更高的要求时选Trilinear）
                    filterMode = FilterMode.Bilinear,
                };
                sa.SetTextureSettings(textureSet);
                //创建图集
                atlas = spritePath.Replace("ArtRes", "AddressableRes")+ dir.Name + ".spriteatlas";
                AssetDatabase.CreateAsset(sa, atlas);
                //图片的文件夹加入图集。
                string AssetPath = spritePath + dir.Name;
                Object texture = AssetDatabase.LoadMainAssetAtPath(AssetPath);
                SpriteAtlasExtensions.Add(sa, new Object[] { texture });
                AssetDatabase.SaveAssets();
                Debug.Log("图集创建成功:" + atlas);          
            }
        }
        ExportAtlas();
    }

    public static void ExportAtlas()
    {
        if (!Directory.Exists(atlasPath))
        {
            Debug.LogError("不存在路径：" + atlasPath);
            return;
        }
        if (!Directory.Exists(scriptDir))
        {
            Debug.LogError("不存在路径：" + scriptDir);
            return;
        }
        StringBuilder sbPath = new StringBuilder();
        string csName = "UIAtlas";
        sbPath.AppendLine("//每次都会重新生成的脚本，不要删，覆盖就行了");
        sbPath.AppendLine("namespace HotFix");
        sbPath.AppendLine("{");
        sbPath.AppendLine("    public class " + csName);
        sbPath.AppendLine("    {");
        DirectoryInfo direction = new DirectoryInfo(atlasPath);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Name.EndsWith(".meta")) continue;
            string FileName = files[i].Name.Split('.')[0];
            sbPath.AppendLine("        public const string " + FileName + " = " + '"' + FileName + '"' + ";");
        }
        sbPath.AppendLine("    }");
        sbPath.AppendLine("}");
        string scriptFilePath = scriptDir + csName + ".cs";
        //如果文件不存在，则创建；存在则覆盖
        File.WriteAllText(scriptFilePath, sbPath.ToString(), Encoding.UTF8);
        Debug.LogError("导入图集路径成功:" + scriptFilePath);
    }
}
