using UnityEngine;
using UnityEditor;
using System.Linq;
using CSF;

public class DefineSymbolsTools
{
    //是否定义符号
    public static bool IsDefineSymbols(string name)
    {
        string define;
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        else
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
        string[] defineArr = define.Split(';');
        return defineArr.Contains(name);
    }
    //是否不包含指定符号，可以指定多个，有一个则反回false
    public static bool IsUnDefineSymbols(params string[] names)
    {
        string define;
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        else
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
        string[] defineArr = define.Split(';');

        for (int i = 0; i < names.Length; i++)
        {
            if (defineArr.Contains(names[i]))
                return false;
        }
        return true;
    }


    public static void SetDefineSymbols(string name, bool isAdd)
    {
        string define;
        BuildTargetGroup buildTargetGroup = BuildTargetGroup.Android;
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        else
        {
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
            buildTargetGroup = BuildTargetGroup.iOS;
        }
        string newDefine = string.Empty;
        if (isAdd)
            newDefine = define + ";" + name;
        else
        {
            newDefine = define.Replace(name, string.Empty);
            newDefine = newDefine.Replace(";;", ";");
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, newDefine);
        ToolsHelper.Log($"已经{(isAdd ? "添加" : "移除")}宏{name}");
    }
    //宏改变，有则删除，没有则增加
    public static void ChangeDefineSymbols(string name)
    {
        string define;
        BuildTargetGroup buildTargetGroup = BuildTargetGroup.Android;
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        else
        {
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
            buildTargetGroup = BuildTargetGroup.iOS;
        }
        string[] defineArr = define.Split(';');
        bool isAdd = !defineArr.Contains(name);
        string newDefine = string.Empty;
        if (isAdd)
            newDefine = define + ";" + name;
        else
        {
            newDefine = define.Replace(name, string.Empty);
            newDefine = newDefine.Replace(";;", ";");
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, newDefine);
        ToolsHelper.Log($"已经{(isAdd ? "添加" : "移除")}宏{name}");
    }

    //宏分组设置，一组只能指定一个宏
    public static void SetDefineSymbolsGroup(string name, string[] groups)
    {
        string define;
        BuildTargetGroup buildTargetGroup = BuildTargetGroup.Android;
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        else
        {
            define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
            buildTargetGroup = BuildTargetGroup.iOS;
        }
        string newDefine = define;
        for (int i = 0; i < groups.Length; i++)
            newDefine = newDefine.Replace(groups[i], string.Empty);
        newDefine = newDefine.Replace(";;", ";").TrimEnd(';');
        if (name != string.Empty)
            newDefine += ";" + name;


        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, newDefine);
        ToolsHelper.Log($"当前宏:{newDefine}");
    }
}