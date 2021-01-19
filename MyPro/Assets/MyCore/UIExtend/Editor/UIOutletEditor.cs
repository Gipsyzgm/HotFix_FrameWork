using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(UIOutlet))]
public class UIOutletEditor : Editor
{
    static Dictionary<GameObject, string[]> _OutletObjects = new Dictionary<GameObject, string[]>();
    static UIOutletEditor()
    {
        //委派HierarchyWindow中每个可见列表项的OnGUI事件。   
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;    
    }
    /// <summary>
    /// 在HierarchyWindow中对应的GameObject标记类型
    /// </summary>
    /// <param name="instanceid"></param>
    /// <param name="selectionrect"></param>
    private static void HierarchyItemCB(int instanceid, Rect selectionrect)
    {
        //将实例ID转换为对对象的引用。
        var obj = EditorUtility.InstanceIDToObject(instanceid) as GameObject;
        if (obj != null)
        {
            //橙色后缀
            UIOutlet uiLua = obj.GetComponent<UIOutlet>();
            if (uiLua != null)
            {
                Rect r = new Rect(selectionrect);
                r.x = r.width - 10;
                r.y += 2;
                var style = new GUIStyle();
                style.normal.textColor = new Color(1, 0.3f, 0);
                GUI.Label(r, string.Format("Infos:{0}", uiLua.OutletInfos.Count), style);
            }
            //紫红色后缀
            if (_OutletObjects.ContainsKey(obj))
            {
                Rect r = new Rect(selectionrect);
                r.x = r.x+ GetStringWidth(obj.name)+25;
                r.y += 0;
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.magenta;
                GUI.Label(r, string.Format("[{0}]", _OutletObjects[obj][1]), style);
            }
        }
    }
    /// <summary>
    /// 获取字符的长度
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static float GetStringWidth(string str)
    {
        Font font = GUI.skin.font;
        //请求将字符添加到字体纹理（仅动态字体）。
        font.RequestCharactersInTexture(str, font.fontSize, FontStyle.Normal);
        //角色信息
        CharacterInfo characterInfo;
        float width = 0f;
        for (int i = 0; i < str.Length; i++)
        {
            //获取特定角色的渲染信息。
            font.GetCharacterInfo(str[i], out characterInfo, font.fontSize);
            //从此字符原点到下一个字符原点的水平距离。
            width += characterInfo.advance;
        }
        return width;
    }
    /// <summary>
    /// 重命名，剔除大小写字母和下划线之外的字符
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static string ToVariableName(string str)
    {
        str = str.Replace("(Clone)", "");
        str = str.Replace(" ", "_");
        StringBuilder retStr = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            char ch = str[i];
            if (ch >= 'a' && ch <= 'z')
            {
                retStr.Append(ch);
            }
            else if (ch >= 'A' && ch <= 'Z')
            {
                retStr.Append(ch);
            }
            else if (ch >= '0' && ch <= '9')
            {
                retStr.Append(ch);
            }
            else if (ch == '_')
            {
                retStr.Append(ch);
            }
        }

        return retStr.ToString();
    }

    GUIStyle GreenFont;
    GUIStyle RedFont;

    private HashSet<string> _CachedPropertyNames = new HashSet<string>();

    private string[] layer;

    //默认Ui层级
    public int templayer = 1;

    void OnEnable()
    {
        GreenFont = new GUIStyle();
        GreenFont.fontStyle = FontStyle.Bold;
        GreenFont.fontSize = 11;
        GreenFont.normal.textColor = Color.blue;
        RedFont = new GUIStyle();
        RedFont.fontStyle = FontStyle.Bold;
        RedFont.fontSize = 11;
        RedFont.normal.textColor = Color.red;

        layer = new string[System.Enum.GetValues(typeof(PanelLayer)).Length];
        int tempCount = 0;
        foreach (PanelLayer suit in System.Enum.GetValues(typeof(PanelLayer)))
        {
            layer[tempCount] = suit.ToString();
            tempCount++;
        }

    }
    /// <summary>
    /// 自定义对Inspector面板的绘制
    /// </summary>
    public override void OnInspectorGUI()
    {
        _CachedPropertyNames.Clear();

     
        //检查代码块中是否有任何控件被更改
        EditorGUI.BeginChangeCheck();
        UIOutlet outlet = target as UIOutlet;
        #region 扩展功能
        GUILayout.Space(10);

        templayer = EditorGUILayout.Popup("选择Layer",templayer, layer);
        outlet.Layer = templayer;

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        //拖拽添加
        var aEvent = Event.current;
        var dragArea = GUILayoutUtility.GetRect(105, 85);
        //在Inspector窗口上创建区域，向区域拖拽资源对象，获取到拖拽到区域的对象
        GUI.Box(dragArea, "\n\n可将UI组件拖至此处添加");

        switch (aEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dragArea.Contains(aEvent.mousePosition))
                {
                    break;
                }
                Object temp = null;
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (aEvent.type == EventType.DragPerform)
                {
                    //接受拖动操作。
                    DragAndDrop.AcceptDrag();
                    //记录执行RecordObject函数之后对对象所做的任何更改。
                    Undo.RecordObject(target, "Drag Insert");
                    //处理拖动的所有对象
                    for (int i = 0; i < DragAndDrop.objectReferences.Length; ++i)
                    {
                        temp = DragAndDrop.objectReferences[i];
                        if (temp == null)
                        {
                            break;
                        }
                        //改名并添加
                        outlet.OutletInfos.Insert(0, new UIOutlet.OutletInfo { Object = temp });
                    }
                }
                //使用事件后调用此方法。该事件的类型将设置为EventType.Used，从而导致其他GUI元素将其忽略。
                Event.current.Use();
                break;
            default:
                break;
        }
        GUILayout.Space(15);
        GUILayout.BeginVertical();

        EditorGUILayout.HelpBox("命名规则：禁止中文、特殊字符、空格", MessageType.Warning);
        GUILayout.Space(9);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("简易命名", new[] { GUILayout.Height(35) }))
        {
            if (outlet.OutletInfos != null || outlet.OutletInfos.Count != 0)
            {
                //记录执行 RecordObject 函数之后对对象所做的任何更改。
                Undo.RecordObject(target, "SimpleName");
                for (var j = outlet.OutletInfos.Count - 1; j >= 0; j--)
                {
                    if (outlet.OutletInfos[j].Object != null)
                    {
                        outlet.OutletInfos[j].Object.name = ToVariableName(outlet.OutletInfos[j].Object.name);
                    }
                }
            }
        }

        GUILayout.Space(5);

        if (GUILayout.Button("清理空项", new[] { GUILayout.Height(35) }))
        {
            if (outlet.OutletInfos != null || outlet.OutletInfos.Count != 0)
            {
                Undo.RecordObject(target, "RemoveAllNull");
                for (var j = outlet.OutletInfos.Count - 1; j >= 0; j--)
                {
                    if (outlet.OutletInfos[j].Object == null)
                    {
                        outlet.OutletInfos.RemoveAt(j);
                    }
                }
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        #endregion

        if (outlet.OutletInfos == null || outlet.OutletInfos.Count == 0)
        {
            if (GUILayout.Button("Add New Outlet"))
            {
                if (outlet.OutletInfos == null)
                    outlet.OutletInfos = new List<UIOutlet.OutletInfo>();
                else
                {
                    outlet.OutletInfos.Clear();
                    _OutletObjects.Clear();
                }
                Undo.RecordObject(target, "AddOutletInfo");
                outlet.OutletInfos.Add(new UIOutlet.OutletInfo());
            }
        }
        else
        {
            // OutletUI edit
            for (var j = outlet.OutletInfos.Count - 1; j >= 0; j--)
            {
                var currentTypeIndex = -1;
                UIOutlet.OutletInfo outletInfo = outlet.OutletInfos[j];
                //组件类型可选,包含物体本身和自身添加的UI组件
                string[] typesOptions = new string[0];
                //该UI组件不为空且不和其他组件重名
                var isValid = outletInfo.Object != null && !_CachedPropertyNames.Contains(outletInfo.Name);
                _CachedPropertyNames.Add(outletInfo.Name);

                if (outletInfo.Object != null)
                {
                    if (outletInfo.Object is GameObject)
                    {                       
                        var UIObj = outletInfo.Object as GameObject;
                        var components = UIObj.GetComponents<Component>();                        
                        if (components.Length == 1)
                            currentTypeIndex = 0;
                        else
                            currentTypeIndex = components.Length;// 设置默认类型,默认选中最后一个
                        string objTypeName = "";

                        typesOptions = new string[components.Length + 1];
                  
                        typesOptions[0] = UIObj.GetType().FullName;                   
                        if (typesOptions[0] == outletInfo.ComponentType)
                        {
                            currentTypeIndex = 0;
                            objTypeName = UIObj.GetType().Name;
                        }

                        for (var i = 1; i <= components.Length; i++)
                        {
                            var UiComponent = components[i - 1];
                            var typeName = typesOptions[i] = UiComponent.GetType().FullName;
                            if (typeName == outletInfo.ComponentType)
                            {
                                currentTypeIndex = i;
                                objTypeName = UiComponent.GetType().Name;
                            }
                        }
                        _OutletObjects[UIObj] = new string[] { outletInfo.Name, objTypeName };

                        if (string.IsNullOrEmpty(outletInfo.Name))
                            outletInfo.Name = UIObj.name;
                    }
                }
                //和上面UI分隔开
                EditorGUILayout.Separator();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(string.Format("Property: {0}", outletInfo.Name), isValid ? GreenFont : RedFont);
                EditorGUILayout.Space();
                if (GUILayout.Button("+"))
                {
                    Undo.RecordObject(target, "InsertOutletInfo");
                    outlet.OutletInfos.Insert(j, new UIOutlet.OutletInfo());
                }
                if (GUILayout.Button("-"))
                {
                    Undo.RecordObject(target, "RemoveOutletInfo");
                    if (outlet.OutletInfos[j].Object != null)
                    {
                        _OutletObjects.Remove(outlet.OutletInfos[j].Object as GameObject);
                    }
                    outlet.OutletInfos.RemoveAt(j);
                }
                if (GUILayout.Button("↑", GUILayout.MaxWidth(20))&&j < outlet.OutletInfos.Count-1)
                {
                    UIOutlet.OutletInfo curr = outlet.OutletInfos[j];
                    outlet.OutletInfos[j] = outlet.OutletInfos[j + 1];
                    outlet.OutletInfos[j + 1] = curr;
                }
                if (GUILayout.Button("↓", GUILayout.MaxWidth(20))&&j>0)
                {
                    UIOutlet.OutletInfo curr = outlet.OutletInfos[j];
                    outlet.OutletInfos[j] = outlet.OutletInfos[j - 1];
                    outlet.OutletInfos[j - 1] = curr;
                }
                GUILayout.Space(20);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                if (outletInfo.Object != null)
                {
                    outletInfo.Name = outletInfo.Object.name;
                    outletInfo.Object = EditorGUILayout.ObjectField("", outletInfo.Object, typeof(UnityEngine.Object), true);
                }
                else
                {
                    outletInfo.Name = "Select Object";
                    outletInfo.Object = EditorGUILayout.ObjectField("", outletInfo.Object, typeof(UnityEngine.Object), true);
                }

                if (currentTypeIndex >= 0)
                {
                    var typeIndex = EditorGUILayout.Popup("", currentTypeIndex, typesOptions);
                    outletInfo.ComponentType = typesOptions[typeIndex].ToString();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        //如果UI控件被更改
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "GUI Change Check");
        }
    }


}
//设置UI层级，需要和热更的PanelLayer对应
public enum PanelLayer
{
    /// <summary>UI主界面节点,如:人物头像，右上角地小图，功能栏按钮等</summary>
    UIWar = 0,
    /// <summary>UI窗口节点,如:商店,人物背包，任务面板，商城面板</summary>
    UIMain = 1,
    /// <summary>Tips节点,如:物品详细信息</summary>
    UITips = 2,
    /// <summary>剧情节点</summary>
    UIStory = 3,
    /// <summary>系统消息节点</summary>
    UIMsg = 4,
    /// <summary>页面等待</summary>
    UILoading = 5,
}
