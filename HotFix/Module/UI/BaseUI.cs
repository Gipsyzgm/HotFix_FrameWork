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
            ShowUIAnim(CurObj, uIAnim);
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
        public virtual void OnClose(UIAnim uIAnim = UIAnim.None)
        {
            Debug.LogError("关闭的页面：" + CurViewPath.ToLower());
            ShowUIAnim(CurObj, uIAnim);         
            topItem?.Dispose();
            Dispose();
            GameObject.DestroyImmediate(CurObj);
        }

        /// <summary>
        /// 备用的自己隐藏自己的方法
        /// </summary>
        public virtual void HideSelf()
        {      
            string name = this.GetType().ToString();
            HotMgr.UI.HidePanel(name);
        }

        /// <summary>
        /// 备用的自己关闭自己的方法
        /// </summary>
        public virtual void CloseSelf()
        {
            CloseAction?.Invoke();
            string name = this.GetType().ToString();
            HotMgr.UI.ClosePanel(name);
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

        /// <summary>
        /// 渐现菜单
        /// </summary>
        /// <param name="targetGO">菜单游戏对象</param>
        public void ShowUIAnim(GameObject target, UIAnim anim)
        {
            float time = 0.5f;
            switch (anim)
            {
                case UIAnim.FadeOut:
                    time = 0.35f;
                    break;
                case UIAnim.ScaleOut:
                    time = 0.2f;
                    break;
            }
            if (anim == UIAnim.None || target == null)
            {
                return;
            }
            //UI淡入淡出效果
            if (anim == UIAnim.FadeIn || anim == UIAnim.FadeOut)
            {
                Graphic[] comps = target.GetComponentsInChildren<Graphic>();
                for (int i = comps.Length; --i >= 0;)
                {
                    if (anim == UIAnim.FadeIn)
                        comps[i].DOFade(0, time).SetUpdate(true).From();
                    else
                        comps[i].DOFade(0, time).SetUpdate(true);
                }
            }
            else if (anim == UIAnim.ScaleIn || anim == UIAnim.ScaleOut)
            {
                if (anim == UIAnim.ScaleIn)
                {
                    target.transform.DOScale(0, time).SetUpdate(true).SetEase(Ease.OutBack).From();
                }
                else
                {
                    target.transform.DOScale(0, time).SetUpdate(true).SetEase(Ease.InBack);
                }
            }
        }      
    }
}