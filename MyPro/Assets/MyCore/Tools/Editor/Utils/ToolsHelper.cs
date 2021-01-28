using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
public class ToolsHelper
{
    /// <summary>
    /// 编辑器的提示
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="title"></param>
    /// <param name="button"></param>
    /// <returns></returns>
    public static bool ShowDialog(string msg, string title = "提示", string button = "确定")
    {
        return EditorUtility.DisplayDialog(title, msg, button);
    }
    /// <summary>
    /// 编辑器的提示
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="yesCallback"></param>
    public static void ShowDialogSelection(string msg, Action yesCallback)
    {
        if (EditorUtility.DisplayDialog("确定吗", msg, "是!", "不！"))
        {
            yesCallback();
        }
    }
    /// <summary>
    /// 工具输出日志
    /// </summary>
    public static void Log(string str, bool isTips = true)
    {
        if (isTips)
        {
            SceneView myWindow = (SceneView)EditorWindow.GetWindow(typeof(SceneView));
            myWindow.ShowNotification(new GUIContent(str));
        }
    }
    /// <summary>
    /// 打开资源管理器
    /// </summary>
    /// <param name="path"></param>
    public static void ShowExplorer(string path)
    {
        System.Diagnostics.Process.Start(path);
    }
    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="dir"></param>
    public static void CreateDir(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    /// <summary>
    /// 打开场景
    /// </summary>
    /// <param name="scene"></param>
    public static void OpenScene(string scene)
    {
#if UNITY_5 || UNITY_2017_1_OR_NEWER
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            EditorSceneManager.OpenScene(scene);
#else
            if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
                EditorApplication.OpenScene(mainScene);
#endif
    }
    /// <summary>
    /// 显示进度
    /// </summary>
    /// <param name="val"></param>
    /// <param name="total"></param>
    /// <param name="cur"></param>
    public static void ShowProgress(string title, int total, int cur)
    {
        EditorUtility.DisplayProgressBar(title, string.Format("请稍等({0}/{1}) ", cur, total), cur / (float)total);
        if (total == cur)
            EditorUtility.ClearProgressBar();
    }
    /// <summary>
    /// 清理控制台日志
    /// </summary>
    public static void ClearConsole()
    {
#if UNITY_EDITOR
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type logEntries = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);
#endif
    }
    /// <summary>
    /// 项目是否是运行当中
    /// </summary>
    /// <returns></returns>
    public static bool IsPlaying()
    {
        if (EditorApplication.isPlaying)
        {
            EditorUtility.DisplayDialog("提示", "请先停止运行", "知道了...");
            return true;
        }
        return false;
    }
    /// <summary>
    /// 打开外部程序
    /// </summary>
    /// <param name="_exePathName">EXE所在绝对路径及名称带.exe</param>
    /// <param name="_exeArgus">启动参数</param>
    public static void OpenEXE(string filePath, string _exeArgus = null)
    {
        try
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                UnityEngine.Debug.LogError("文件不存在:" + file.FullName);
                return;
            }
            Process myprocess = new Process();
            //ProcessStartInfo startInfo = new ProcessStartInfo(file.FullName, _exeArgus);
            myprocess.StartInfo.FileName = file.FullName;
            myprocess.StartInfo.WorkingDirectory = file.DirectoryName;
            if (_exeArgus != null)
                myprocess.StartInfo.Arguments = _exeArgus;
            myprocess.StartInfo.UseShellExecute = false;
            myprocess.StartInfo.CreateNoWindow = true;
            myprocess.Start();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError("出错原因：" + ex.Message);
        }
    }
    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="path">保存路径</param>
    /// <param name="content">文件内容</param>
    /// <param name="iscover">存在是否进行覆盖,默认true</param>
    public static void SaveFile(string path, string content, bool iscover = true, bool isLog = true)
    {
        FileInfo info = new FileInfo(path);
        if (!iscover && info.Exists) //不覆盖
        {
            if (isLog)
                UnityEngine.Debug.LogWarning($"文件已存在，不进行覆盖操作!! {path}");
            return;
        }
        CreateDir(info.DirectoryName);
        FileStream fs = new FileStream(path, FileMode.Create);
        StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
        sWriter.WriteLine(content);
        sWriter.Flush();
        sWriter.Close();
        fs.Close();
        Log($"成功生成文件 {path}", false);
    }
    /// <summary>
    /// 物体存为预制体
    /// </summary>
    /// <param name="Prefab"></param>
    /// <param name="path"></param>
    public static void CratePrefab(GameObject Prefab,string path) 
    {
        //创建Prefab
        string dirObj = path + Prefab.name + ".prefab";
        if (!File.Exists(dirObj))
        {
            PrefabUtility.SaveAsPrefabAsset(Prefab, dirObj);
            UnityEngine.Debug.Log("生成预制体成功：" + dirObj);
        }
        else 
        {
            UnityEngine.Debug.LogWarning("已存在预制体："+ dirObj+",不进行覆盖，可直接在预制体上编辑。");
        }        
        //PrefabUtility.UnloadPrefabContents(Prefab);

    }
    /// <summary>
    /// 拷贝文件
    /// </summary>
    /// <param name="srcDir">起始文件夹</param>
    /// <param name="tgtDir">目标文件夹</param>
    public static void CopyDirectory(string srcDir, string tgtDir, bool copySubDirs)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(srcDir);
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + srcDir);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        if (!Directory.Exists(tgtDir))
            Directory.CreateDirectory(tgtDir);
        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(tgtDir, file.Name);
            file.CopyTo(temppath, true);
        }
        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(tgtDir, subdir.Name);
                CopyDirectory(subdir.FullName, temppath, true);
            }
        }
    }
    /// <summary>
    /// 删除文件夹
    /// </summary>
    /// <param name="dir"></param>
    public static void DeleteDir(string dir)
    {
        FileUtil.DeleteFileOrDirectory(dir);
    }
    /// <summary>
    /// 删除目标文件夹下面所有文件
    /// </summary>
    /// <param name="dir"></param>
    public static void CleanDirectory(string dir)
    {
        foreach (string subdir in Directory.GetDirectories(dir))
        {
            Directory.Delete(subdir, true);
        }
        foreach (string subFile in Directory.GetFiles(dir))
        {
            File.Delete(subFile);
        }
    }
    /// <summary>
    /// 给物体添加一个路径下所有同尾缀的组件
    /// </summary>
    /// <param name="obj">物体</param>
    /// <param name="path">路径</param>
    /// <param name="end">尾缀比如.cs</param>
    public static void AddFileComment(GameObject obj, string path, string end)
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (!files[i].Name.EndsWith(end)) continue;
            string tempName = files[i].Name.Split('.')[0];
            Type t = Type.GetType(tempName);
            obj.AddComponent(t);
        }
    }
    /// <summary>
    /// 复制到剪切板
    /// </summary>
    /// <param name="str"></param>
    public static void CopyString(string str)
    {
        TextEditor te = new TextEditor();
        te.text = str;
        te.SelectAll();
        te.Copy();
    }
}
