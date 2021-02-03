using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

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
        UIMsg = 4,
        /// <summary>页面等待</summary>
        UILoading = 5,
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

    public class BaseUI:UIObject
    {     
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
        public UILoading IsLoading = UILoading.None;

        /// <summary>top栏</summary>
        //public TopItem topItem;

        /// <summary>
        /// 关闭界面执行
        /// </summary>
        public Action CloseAction;


        /// <summary>
        /// 预制体路径
        /// </summary>
        public virtual string CurViewPath
        {
            get
            {
                Type type = this.GetType();
                return type.Name;
            }
        }

        public async CTask LoadGameObject()
        {
            ShowLoading();       
            //加载物体
            GameObject TempObj = await Addressables.LoadAssetAsync<GameObject>(CurViewPath).Task;
            CurObj = GameObject.Instantiate(TempObj);
            if (CurObj == null)
                Debug.Log("UI Load Fail,UIPath= " + CurViewPath);
            //初始化该物体
            InitGameObject(CurObj);    
            CloseLoading();
        }
        public virtual void Init(params object[] _args)
        {
            this.args = _args;
        }

        /// <summary>
        /// 显示页面
        /// </summary>
        public virtual void OnShow()
        {
            CurObj.SetActive(true);
        }

        /// <summary>
        /// 可重写，页面隐藏的逻辑。动画等
        /// </summary>
        public virtual void OnHide()
        {
            SetActive(false);

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
            GameObject.DestroyImmediate(CurObj);
        }
        /// <summary>
        /// 备用的自己关闭自己的方法
        /// </summary>
        protected virtual void CloseSelf()
        {
            CloseAction?.Invoke();
            string name = this.GetType().ToString();
            Mgr.UI.ClosePanel(name);
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        public override void Dispose()
        {
            args = null;
            base.Dispose();
           
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
        protected virtual void ShowLoading()
        {
            if (IsLoading != UILoading.None)
            {
                if (Mgr.UI.Loading!=null)
                {
                    Mgr.UI.Loading.SetVisible(true);
                    float alpha = IsLoading == UILoading.Translucent ? 0.5f : 1f;
                    Mgr.UI.LoadingMask.SetAlpha(alpha);
                }
            }
        }

        protected virtual void CloseLoading()
        {
            if (IsLoading != UILoading.None) 
            {
                if (Mgr.UI.Loading!=null)
                {
                    Mgr.UI.Loading.SetVisible(false);
                }
             
            }

        }

    }
}