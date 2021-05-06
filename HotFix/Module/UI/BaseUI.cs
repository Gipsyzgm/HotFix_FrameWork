using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

namespace HotFix
{
    /// <summary>
    /// 引入layer概念
    /// </summary>
    public enum PanelLayer
    {
        /// <summary>UI主界面节点,如:人物头像，右上角地小图，功能栏按钮等</summary>
        UIWar = 0,
        /// <summary>UI窗口节点,如:商店,人物背包，任务面板，商城面板，属性面板</summary>
        UIMain = 1,
        /// <summary>UI二级页面</summary>
        UIWindow = 2,
        /// <summary>Tips节点,如:物品详细信息，提示面板</summary>
        UITips = 3,
        /// <summary>剧情节点</summary>
        UIStory = 4,
        /// <summary>系统消息节点</summary>
        UIMsg = 5,
        /// <summary>页面等待</summary>
        UILoading = 6,
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
        public TopItem topItem;

        /// <summary>
        /// 关闭界面执行
        /// </summary>
        public Action CloseAction;

        /// <summary>
        /// UI路径名,自动获取,跟据UI脚本名(如果不符合自己重写此方法)
        /// 对应Addressables的简单命名
        /// 获取结果为:BaseItem
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
            if (TempObj == null) 
            {
                Debug.Log("UI Load Fail,UIPath= " + CurViewPath);
                return;
            }          
            CurObj = GameObject.Instantiate(TempObj);         
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
        public virtual void OnShow(UIAnim uIAnim = UIAnim.None)
        {
            CurObj.SetActive(true);
            ShowUIAnim(CurObj, uIAnim);               
        }

        /// <summary>
        /// 可重写，页面隐藏的逻辑。动画等
        /// </summary>
        public virtual void OnHide(UIAnim uIAnim)
        {
            if (uIAnim == UIAnim.None)
            {                
                SetActive(false);
            }
            else
            {
                ShowUIAnim(CurObj, uIAnim);
            }
          
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
        public virtual void OnClose(UIAnim uIAnim = UIAnim.None)
        {         
            if (uIAnim == UIAnim.None)
            {
                topItem?.Dispose();
                Dispose();
                GameObject.DestroyImmediate(CurObj);
            }
            else 
            {          
                ShowUIAnim(CurObj, uIAnim,()=> 
                {
                    topItem?.Dispose();
                    Dispose();
                    GameObject.DestroyImmediate(CurObj);
                });
            }                             
        }

        /// <summary>
        /// 备用的自己隐藏自己的方法
        /// </summary>
        public virtual void HideSelf(UIAnim uIAnim = UIAnim.None)
        {      
            string name = this.GetType().ToString();
            HotMgr.UI.HidePanel(name, uIAnim);
        }

        /// <summary>
        /// 备用的自己关闭自己的方法
        /// </summary>
        public virtual void CloseSelf(UIAnim uIAnim = UIAnim.None)
        {
            CloseAction?.Invoke();
            string name = this.GetType().ToString();
            HotMgr.UI.ClosePanel(name, uIAnim);
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
                if (HotMgr.UI.Loading!=null)
                {
                    HotMgr.UI.Loading.SetActive(true);
                    float alpha = IsLoading == UILoading.Translucent ? 0.5f : 1f;
                    HotMgr.UI.LoadingMask.SetAlpha(alpha);
                }
            }
        }

        protected virtual void CloseLoading()
        {
            if (IsLoading != UILoading.None) 
            {
                if (HotMgr.UI.Loading!=null)
                {
                    HotMgr.UI.Loading.SetActive(false);
                }            
            }
        }
        /// <summary>
        /// 创建公用Top
        /// </summary>
        protected async CTask CreatTopItem()
        {
            if (topItem != null) return;
            topItem = new TopItem();
            await topItem.Instantiate(this.transform);
        }       
    }
}