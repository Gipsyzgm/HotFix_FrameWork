using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using CSF;
using UnityEngine.SceneManagement;

public class SceneTools
{
    //[MenuItem("GameObject/★场景扩展★/1.创建场景预制根节点", false, 21)]
    static void CreateSceneRootScript(MenuCommand menuCommadn)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        GameObject go = new GameObject(sceneName);        
        SceneLightMapSetting setting = go.AddComponent<SceneLightMapSetting>();
        Selection.activeObject = go;
        ToolsHelper.Log("场景预制根节点创建成功，请将场景下的模型移到"+ sceneName + "节点下");
    }

 
}