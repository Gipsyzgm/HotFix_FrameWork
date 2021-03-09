using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildAPKTools
{
    static string[] SCENES = FindEnabledEditorScenes();
    public static void BulidTarget(bool isRun = false)
    {
        BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
        if (buildTarget == BuildTarget.Android)
        {
            EditorKeyStore();
        }      
        string app_name =
            $"{Application.productName}.[{Application.version}]_{DateTime.Now.ToString("yyyyMMdd_HHmm")}";
        string target_dir = "";
        string target_name = ""; 
        string applicationPath = Application.dataPath.Replace("/Assets", "");
        Debug.Log("Start Bulid:" + app_name);
        if (buildTarget == BuildTarget.Android)
        {
            target_dir = applicationPath + $"/Builds/{Application.productName}_Android";
            target_name = app_name + ".apk";
        }
        else
        {
            target_dir = applicationPath + $"/Builds/{Application.productName}_iOS";
            target_name = app_name;
        }
        Directory.CreateDirectory(target_dir);        
        BuildPipeline.BuildPlayer(SCENES, target_dir + "/" + target_name, buildTarget,
            isRun ? BuildOptions.AutoRunPlayer : BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    public static void EditorKeyStore() 
    {
        //PlayerSettings.Android.keystoreName = "完整路径（包含文件后缀）";
        //PlayerSettings.Android.keystorePass = "moqikaka";
        //PlayerSettings.Android.keyaliasName = "dazhuzai";
        //PlayerSettings.Android.keyaliasPass = "mqkkdzz";
    }
}