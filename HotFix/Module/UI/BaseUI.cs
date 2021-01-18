using UnityEngine;
using System.Collections;
using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
namespace HotFix
{
    /// <summary>
    /// 引入layer概念
    /// </summary>
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
        UIMessage = 4,
    }

    /// <summary>
    /// UI效果 与主工程 EUIAnim保持一至
    /// </summary>
    public enum UIAnim
    {
        None,       //无效果
        FadeIn,     //渐入
        FadeOut,   //渐出
        ScaleIn,    //缩放进入
        ScaleOut,  //缩放退出
    }

    public enum UILoading
    {
        None,               //无
        Translucent,      //半透明  
        Mask,               //全遮档

    }

    public class BaseUI
    {
        /// <summary>
        /// 代表Panel面板在场景中的物体
        /// </summary>
        public GameObject curView;
        /// <summary>
        /// UI所属节点类型
        /// </summary>
        public PanelLayer layer = PanelLayer.UIMain;
        /// <summary>
        /// 打开UI传进来的参数
        /// </summary>
        public object[] args;
        /// <summary>
        /// 打开UI的效果
        /// </summary>
        public UIAnim OpenAnim = UIAnim.None;
        /// <summary>
        /// 关闭UI的效果
        /// </summary>
        public UIAnim CloseAnim = UIAnim.None;
        /// <summary>
        /// 是否启用加载等待动画(默认开启)
        /// </summary>
        protected UILoading Loading = UILoading.None;
        /// <summary>
        /// 面板下包含的已标记的子物体
        /// </summary>
        protected Dictionary<string, GameObject> ObjectList = new Dictionary<string, GameObject>();


        protected bool IsDispose = false;

        /// <summary>
        /// 预制体路径
        /// </summary>
        public virtual string CurViewPath
        {
            get
            {
                Type type = this.GetType();
                return "MyUI/View" + " / " + type.Name;
            }
        }

        public async UniTask InitGameObject()
        {
            ShowLoading();
            //加载物体
            curView = (GameObject)await Resources.LoadAsync<GameObject>(CurViewPath);
            if (curView == null)
                Debug.LogError("panelMgr.OpenPanelfail,skin is null,skinPath= " + CurViewPath);
            //赋值
            UIOutlet uiInfo = curView.GetComponent<UIOutlet>();
            for (int i = 0; i < uiInfo.OutletInfos.Count; i++)
            {
                ObjectList.Add(uiInfo.OutletInfos[i].Name, uiInfo.OutletInfos[i].Object as GameObject);
            }
            layer = (PanelLayer)uiInfo.Layer;
            InitComponent();

            CloseLoading();
        }
        protected virtual void InitComponent()
        {


        }
        public virtual void Init(params object[] _args)
        {
            this.args = _args;
        }


        /// <summary>
        /// 可重写，页面显示的逻辑。动画等
        /// </summary>
        public virtual void OnShow()
        {
            curView.SetActive(true);
        }

        /// <summary>
        /// 可重写，页面隐藏的逻辑。动画等
        /// </summary>
        public virtual void OnHide()
        {
            curView.SetActive(false);

        }

        /// <summary>
        ///可重写,刷新界面,页面打开过的话，每次Show都会刷新。
        /// </summary>
        public virtual void Refresh()
        {


        }
        /// <summary>
        /// 可重写，页面关闭的逻辑。动画等
        /// </summary>
        public virtual void OnClose()
        {
            Debug.LogError("关闭的页面：" + CurViewPath.ToLower());         
            Dispose();
            GameObject.DestroyImmediate(curView);
        }


        /// <summary>
        /// 备用的自己关闭自己的方法
        /// </summary>
        protected virtual void CloseSelf()
        {
            string name = this.GetType().ToString();
            //Mgr.UI.CloseForName(name);
        }


        /// <summary>
        /// 清除数据
        /// </summary>
        public virtual void Dispose()
        {
            IsDispose = true;
            ObjectList = null;
            curView = null;
            args = null;
        }

        /// <summary>
        /// 获得第一个指定类型的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>数据</returns>
        public T GetArg<T>()
        {
            T value = default(T);
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is T)
                    return (T)args[i];
            }
            return value;
        }


        /// <summary>
        /// 获取控件引用对象(在UIOutlet中定义的)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">控件名</param>
        /// <returns></returns>
        public T Get<T>(string name) where T : Component
        {
            GameObject obj = Get(name);
            if (obj == null)
                return null;
            return obj.GetComponent<T>();
        }
        /// <summary>
        /// 获取引用对象(在UIOutlet中定义的)
        /// </summary>
        public GameObject Get(string name)
        {
            GameObject obj;
            if (!ObjectList.TryGetValue(name, out obj))
            {
                Debug.Log($"未找到GameObject对象,请在UIOutlet中设置:{name}");
            }
            return obj;
        }
        protected virtual void ShowLoading()
        {
            if (Loading != UILoading.None)
            {
                //Mgr.UI.Loading.SetVisible(true);
                //float alpha = Loading == UILoading.Translucent ? 0.5f : 1f;
                //Mgr.UI.LoadingMask.SetAlpha(alpha);
            }
        }

        protected virtual void CloseLoading()
        {
            if (Loading != UILoading.None) 
            {
                //Mgr.UI.Loading.SetVisible(false);
            }

        }

    }
}