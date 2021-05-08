using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// UI脚本导出
/// </summary>
public class UIScriptExport
{
    /// <summary>导出UI</summary>
    /// <param name="ui"></param>
    public static void ExportUIScript(UIOutlet ui)
    { 
        string uiName = ui.gameObject.name;
        ToolsHelper.CratePrefab(ui.gameObject, AppSetting.UIPrefabsPath);
        CreateUI(uiName, ui);
        CreateUIView(uiName, ui);
    }

    /// <summary>导出Item</summary>
    public static void ExportItemScript(UIOutlet ui)
    {
        string uiName = ui.gameObject.name;
        ToolsHelper.CratePrefab(ui.gameObject, AppSetting.UIItemPrefabsPath);
        CreateItem(uiName, ui);
        CreateItemView(uiName, ui);
    }

    #region 创建UI       
    private static void CreateUI(string uiName, UIOutlet ui)
    {
        string saveFilePath = $"{AppSetting.ExportScriptDir}/UIPanel/Panel/{uiName}.cs";
        StringBuilder eventAddStrs = new StringBuilder();
        StringBuilder eventStrs = new StringBuilder();
        UIOutlet.OutletInfo info;
        string objType;
        for (int i = ui.OutletInfos.Count; --i >= 0;)
        {
            info = ui.OutletInfos[i];
            if (info == null) continue;
            objType = GetTypeName(info.ComponentType);
            if (objType.EndsWith("Button")) //自动生成按钮事件
            {
                eventAddStrs.AppendLine($"            {info.Object.name}.onClick.AddListener({info.Object.name}_Click);   //");
                eventStrs.AppendLine($@"        /// <summary></summary>
        void {info.Object.name}_Click()
        {{
        }}");

            }
        }
        string fieldStr = $@"using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace HotFix
{{
    public partial class {uiName} : BaseUI
    {{      
        /// <summary>添加按钮事件</summary>
        public override void Init(params object[] _args)
        {{ 
            args = _args;
{eventAddStrs}
        }}

         /// <summary>刷新</summary>
        public override void Refresh()
        {{
        }}

{eventStrs}


        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {{
        }}
    }}
}}
";
        ToolsHelper.SaveFile(saveFilePath, fieldStr, false);
    }
    #endregion

    #region 创建UIView 
    private static void CreateUIView(string uiName, UIOutlet ui)
    {
        string saveFilePath = $"{AppSetting.ExportScriptDir}/UIPanel/View/{uiName}View.cs";

        StringBuilder fieldStrs = new StringBuilder();
        StringBuilder getStrs = new StringBuilder();
        UIOutlet.OutletInfo info;
        string objType;
        for (int i = ui.OutletInfos.Count; --i >= 0;)
        {
            info = ui.OutletInfos[i];
            if (info == null) continue;
            objType = GetTypeName(info.ComponentType);
            fieldStrs.AppendLine($"        private {objType} {info.Name};");
            if (objType == "GameObject")
                getStrs.AppendLine($@"            {info.Name} = Get(""{info.Name}"");");
            else
                getStrs.AppendLine($@"            {info.Name} = Get<{objType}>(""{info.Name}"");");
        }

        string fieldStr = $@"//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{{
    public partial class {uiName} : BaseUI
    {{
{fieldStrs} 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {{
{getStrs}
        }}
    }}
}}
";
        ToolsHelper.SaveFile(saveFilePath, fieldStr, true);
    }
    #endregion

    #region 创建Item       
    private static void CreateItem(string uiName, UIOutlet ui)
    {
        string saveFilePath = $"{AppSetting.ExportScriptDir}/UIItem/Item/{uiName}.cs";
        StringBuilder eventAddStrs = new StringBuilder();
        StringBuilder eventStrs = new StringBuilder();
        UIOutlet.OutletInfo info;
        string objType;
        for (int i = ui.OutletInfos.Count; --i >= 0;)
        {
            info = ui.OutletInfos[i];
            if (info == null) continue;
            objType = GetTypeName(info.ComponentType);
            if (objType.EndsWith("Button")) //自动生成按钮事件
            {
                eventAddStrs.AppendLine($"            {info.Object.name}.onClick.AddListener({info.Object.name}_Click);   //");
                eventStrs.AppendLine($@"        /// <summary></summary>
        void {info.Object.name}_Click()
        {{
        }}");
            }
        }

        string selClick = "             //当前对象点击事件需添加Button组件";
        if (ui.GetComponent<Button>() != null)
           selClick = "             CurObj.GetComponent<Button>().onClick.AddListener(self_Click);  //当前对象点击事件";
        string fieldStr = $@"using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace HotFix
{{
    public partial class {uiName} : BaseItem
    {{

        /// <summary>添加按钮事件</summary>
        public override void Init()
        {{
{selClick}
{eventAddStrs}
        }}

        /// <summary>刷新Item</summary>
        public override void Refresh()
        {{
        }}

        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {{

        }}
{ eventStrs}
        /// <summary>释放Item引用</summary>
        public override void Dispose()
        {{

        }}
    }}
}}
";
        ToolsHelper.SaveFile(saveFilePath, fieldStr, false);
    }
    #endregion

    #region 创建ItemView 
    private static void CreateItemView(string uiName, UIOutlet ui)
    {
        string saveFilePath = $"{AppSetting.ExportScriptDir}/UIItem/View/{uiName}View.cs";

        StringBuilder fieldStrs = new StringBuilder();
        StringBuilder getStrs = new StringBuilder();
        UIOutlet.OutletInfo info;
        string objType;
        for (int i = ui.OutletInfos.Count; --i >= 0;)
        {
            info = ui.OutletInfos[i];
            if (info == null) continue;
            objType = GetTypeName(info.ComponentType);
            fieldStrs.AppendLine($"        private {objType} {info.Name};");
            if (objType == "GameObject")
                getStrs.AppendLine($@"            {info.Name} = Get(""{info.Name}"");");
            else
                getStrs.AppendLine($@"            {info.Name} = Get<{objType}>(""{info.Name}"");");
        }

        string fieldStr = $@"//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix
{{
    public partial class {uiName} : BaseItem
    {{
{fieldStrs} 
        /// <summary>初始化UI控件</summary>
        public override void InitComponent()
        {{
{getStrs}
        }}
    }}
}}
";
        ToolsHelper.SaveFile(saveFilePath, fieldStr, true);
    }
    #endregion


    private static string GetTypeName(string fullName)
    {
        return fullName.Substring(fullName.LastIndexOf(".") + 1);
    }
  
}

