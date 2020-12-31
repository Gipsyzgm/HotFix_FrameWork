//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEngine;

//public class TextureAutoSet : EditorWindow
//{
//    [MenuItem("Assets/设置文件下图片压缩格式", priority = 0)]
//    static void AutoSetASTC()
//    {
//        string[] guidArray = Selection.assetGUIDs;
//        foreach (var item in guidArray)
//        {
//            string selectFloder = AssetDatabase.GUIDToAssetPath(item);
//            DirectoryInfo root = new DirectoryInfo(selectFloder);
//            GetFloder(root);
//        }
//    }

//    static void GetFloder(DirectoryInfo root)
//    {
//        GetFile(root);
//        DirectoryInfo[] array = root.GetDirectories();
//        foreach (DirectoryInfo item in array)
//            GetFloder(item);
//    }

//    static void GetFile(DirectoryInfo root)
//    {
//        FileInfo[] fileDic = root.GetFiles();
//        int subLen = Application.dataPath.Length - 6;
//        foreach (var file in fileDic)
//        {
//            if (file.FullName.EndsWith(".png") || file.FullName.EndsWith(".jpg") || file.FullName.EndsWith(".tga") ||
//                file.FullName.EndsWith(".psd") || file.FullName.EndsWith(".PSD") || file.FullName.EndsWith(".exr") ||
//                file.FullName.EndsWith(".tif"))
//            { 
//                TextureImporter importer = AssetImporter.GetAtPath(file.FullName.Substring(subLen)) as TextureImporter;
//                ResImportEditor.SetTexture(importer, true);
//            }
//        }
//    }
//}