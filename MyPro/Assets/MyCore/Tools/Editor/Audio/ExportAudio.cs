using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
/*
 * 把音效放在Assets/GameRes/AddressableRes/Sound
 */
public class ExportAudio : MonoBehaviour {
    //代码目录
    static string scriptDir = AppSetting.ExportSoundDir;
    //音效目录
    static string clipPath = AppSetting.SoundPath;

    [MenuItem("★工具★/导入声音路径")]
    public static void Export()
    {
        if (!Directory.Exists(clipPath)) 
        {
            Debug.LogError("不存在路径："+clipPath);
            return;
        }
        if (!Directory.Exists(scriptDir)) 
        {
            Debug.LogError("不存在路径：" + scriptDir);
            return;
        }    
        StringBuilder sbPath = new StringBuilder();
        string csName = "SoundName";
        sbPath.AppendLine("//每次都会重新生成的脚本，不要删，覆盖就行了");
        sbPath.AppendLine("public class " + csName);
        sbPath.AppendLine("{");           
        DirectoryInfo direction = new DirectoryInfo(clipPath);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Name.EndsWith(".meta")) continue;
            string FileName = files[i].Name.Split('.')[0];
            sbPath.AppendLine("     public const string " + FileName + " = " + '"' + FileName + '"' + ";");
        }
        sbPath.AppendLine("}");
        string scriptFilePath = scriptDir + csName + ".cs";
        //如果文件不存在，则创建；存在则覆盖
        File.WriteAllText(scriptFilePath, sbPath.ToString(), Encoding.UTF8);
        Debug.LogError("导入声音路径成功:"+ scriptFilePath);
    }
}
