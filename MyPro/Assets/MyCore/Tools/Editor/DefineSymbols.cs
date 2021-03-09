using UnityEngine;
using UnityEditor;
using System.Linq;
public class DefineSymbolsTools
{
    /// <summary>
    /// 是否包含该定义
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 是否不包含指定符号，可以指定多个，有一个则反回false
    /// </summary>
    /// <param name="names"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 添加或移除宏定义
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isAdd"></param>
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
        string[] defineArr = define.Split(';');
        bool IsExist = !defineArr.Contains(name);
        if (!IsExist)
        {
            if (isAdd)
            {
                newDefine = define + ";" + name;
            }
            else 
            {
                ToolsHelper.Log($"不存在宏{name}，无法移除");
                return;
            }    
        }
        else
        {
            if (!isAdd)
            {
                newDefine = define.Replace(name, string.Empty);
                newDefine = newDefine.Replace(";;", ";");
            }
            else
            {
                ToolsHelper.Log($"已存在宏{name}，无需添加移除");
                return;
            }             
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
}