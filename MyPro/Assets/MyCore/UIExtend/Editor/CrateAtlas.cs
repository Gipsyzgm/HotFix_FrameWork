using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

public class CrateAtlas
{
    private static string rpath = AppSetting.UISpritePath;
    [MenuItem("�﹤�ߡ�/�Զ�����ͼ��")]
    static void AtlasCreate()
    {
        DirectoryInfo direction = new DirectoryInfo(rpath);
        DirectoryInfo[] directs = direction.GetDirectories();//�ļ���
        DirectoryInfo dir;
        for (int i = 0; i < directs.Length; i++)
        {
            dir = directs[i];
            //��Ҫ����ͼ�����ļ���
            string dataPath = dir.FullName;
            //����ͼ����·��,ʹ��Replaceȷ��·����ȷ�滻·����ȷ
            string atlas = dataPath.Replace("ArtRes","AddressableRes") + ".spriteatlas";
            if (File.Exists(atlas))
            {
                Debug.Log("ͼ���Ѵ����ҵ�" + atlas);
            }
            else
            {
                SpriteAtlas sa = new SpriteAtlas();
                SpriteAtlasPackingSettings packSet = new SpriteAtlasPackingSettings()
                {
                    blockOffset = 1,
                    //Allow Rotation������ͼ����ͼƬ��ת����ѡ��Unity��Ѱ��������Ʒ�����ͼ����(����������ͼ�����¸����ͼƬ������һ�����ǲ���ѡ����Ϊ�����ת��Ӱ�쵽ʵ����ʾ����
                    enableRotation = false,
                    //Tight Packing: ��ѡ��ʹ�þ��ȸ��ߵĲü�ͼ��ͼƬ��Mesh�ü����������Ǿ���ü���ͼƬ��һ�����ͼ�����������Ҫ�߾��Ȳü��ͱ𹴣�������ֲü�����ͼƬ����ʾ����ȷ���⣬�ǾͿ��Թ�ѡ���ˡ�
                    enableTightPacking = false,
                    //Padding��ͼ����һ�л�һ�����ͼƬ��������2��4��8ѡ��
                    padding = 4,
                };
                sa.SetPackingSettings(packSet);
                SpriteAtlasTextureSettings textureSet = new SpriteAtlasTextureSettings()
                {
                    //Read/Write Enabled��ͼ��ͼƬ��дȨ��
                    readable = true,
                    //Generate Mip Maps: ���ĸ�����ڴ�������õı��֣���Ҫ��Ը����ʱ�ɿ���������һ�㶼���ܲ�������
                    generateMipMaps = false,
                    sRGB = true,
                    //Fitler Mode: Point�����ؼ�����Bilinear�����䣩��Trilinear����ţ�Ƶģ�Ĭ��Bilinear��һ��Ҳ����������������Ϸ�����ط硢�����и��ߵ�Ҫ��ʱѡTrilinear��
                    filterMode = FilterMode.Bilinear,
                };
                sa.SetTextureSettings(textureSet);
                //����ͼ��
                atlas = rpath.Replace("ArtRes", "AddressableRes")+ dir.Name + ".spriteatlas";
                AssetDatabase.CreateAsset(sa, atlas);
                //ͼƬ���ļ��м���ͼ����
                string AssetPath = rpath + dir.Name;
                Object texture = AssetDatabase.LoadMainAssetAtPath(AssetPath);
                SpriteAtlasExtensions.Add(sa, new Object[] { texture });
                AssetDatabase.SaveAssets();
                Debug.Log("ͼ�������ɹ�:" + atlas);
            }
        }
    }
}
