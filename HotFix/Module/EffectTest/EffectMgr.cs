using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace HotFix_Archer.Effect
{
    public class EffectMgr
    {

        private Dictionary<int, BaseEffect> dicEffects = new Dictionary<int, BaseEffect>();

        private Dictionary<int, Stack<BaseEffect>> EffectPool = new Dictionary<int, Stack<BaseEffect>>();


        /// <summary>
        /// 特效管理器
        /// </summary>
        public EffectMgr()
        {
        }

        /// <summary>
        /// 创建特效
        /// </summary>
        /// <param name="templId">特效配置Id</param>
        /// <param name="offset">显示位置</param>
        /// <param name="parent">父对象，没有设置统一放特效跟节点下</param>
        /// <param name="effectOrder">显示层级排序 -1自动排</param>
        /// <returns></returns>
        public int Show(int templId, Vector3 pos, Transform parent = null, int effectOrder = -1)
        {
            BaseEffect eff = CreateEffect<BaseEffect>(templId, pos, parent, effectOrder);
            dicEffects.Add(eff.UID, eff);
            eff.onComplete = (effect) =>
            {
                dicEffects.Remove(effect.UID);
            };
            return eff.UID;
        }
        public int Show(int templId, Transform parent = null)
        {
            return Show(templId, Vector3.zero, parent);
        }
        //public int Show(string effectName, Vector3 pos, Transform parent = null, int effectOrder = -1)
        //{
        //    EffectConfig config;
        //    if (!Mgr.Config.dicEffectByName.TryGetValue(effectName, out config))
        //    {
        //        CLog.Error("特效不存在,Name:" + effectName);
        //        return 0;
        //    }
        //    return Show(config.id, pos, parent, effectOrder);
        //}
        //public int Show(string effectName, Transform parent = null, int effectOrder = -1)
        //{
        //    return Show(effectName, Vector3.zero, parent, effectOrder);
        //}

        public BaseEffect Get(int uid)
        {
            BaseEffect eff;
            dicEffects.TryGetValue(uid, out eff);
            return eff;
        }

        /// <summary>
        /// 关闭特效
        /// </summary>
        /// <param name="uid"></param>
        public void Close(int uid)
        {
            BaseEffect eff;
            if (dicEffects.TryGetValue(uid, out eff))
            {
                dicEffects.Remove(uid);
                eff.Stop(); //停止特效
            }
        }

        /// <summary>
        /// 创建特效,不会进行统一管理
        /// </summary>
        /// <param name="templId"></param>
        /// <param name="pos"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public T CreateEffect<T>(int templId, Transform parent = null, int effectOrder = -1) where T : BaseEffect
        {
            return CreateEffect<T>(templId, Vector3.zero, parent, effectOrder);
        }
        public T CreateEffect<T>(int templId, Vector3 pos, Transform parent = null, int effectOrder = -1) where T : BaseEffect
        {
            BaseEffect effect = GetPoolEffect(templId);
            if (effect != null)
            {
                switch ((EffectType)effect.Config.type)
                {
                    case EffectType.UI:
                        if (parent == null) //加到UI根节点的特效都显示在顶层,指定了父节点的自动设置
                        {
                            parent = Mgr.Effect.UIEffectRoot;
                            if (effectOrder == -1)
                                effectOrder = 1000;
                        }
                        effect.RePlay(pos, parent);
                        if (effectOrder != -1)
                            ((UIEffect)effect).SetOrder(effectOrder);
                        break;
                    default:
                        if (parent == null) parent = Mgr.Effect.WorldEffectRoot;
                        effect.RePlay(pos, parent);
                        break;
                }
                return (T)effect;
            }
            else
            {
                EffectConfig config;
                if (!Mgr.Config.dicEffect.TryGetValue(templId, out config))
                    CLog.Error("未找到特效配置" + templId);
                if (pos == null)
                    pos = Vector3.zero;

                switch ((EffectType)config.type)
                {
                    case EffectType.UI:
                        if (parent == null) //加到UI根节点的特效都显示在顶层,指定了父节点的自动设置
                        {
                            parent = Mgr.Effect.UIEffectRoot;
                            if (effectOrder == -1)
                                effectOrder = 1000;
                        }
                        effect = new UIEffect(config, pos, parent);
                        if (effectOrder != -1)
                            ((UIEffect)effect).SetOrder(effectOrder);
                        break;
                    default:
                        if (parent == null) parent = Mgr.Effect.WorldEffectRoot;
                        effect = new BaseEffect(config, pos, parent);
                        break;
                }
                return (T)effect;
            }
        }

        private Transform _worldEffectRoot;
        private Transform _uiEffectRoot;
        /// <summary>特效世界位置跟节点</summary>
        private Transform WorldEffectRoot {
            get {
                if (_worldEffectRoot == null)
                {
                    _worldEffectRoot = new GameObject("__WorldEffectRoot").transform;
                    GameObject.DontDestroyOnLoad(_worldEffectRoot);
                }
                return _worldEffectRoot;
            }
        }
        /// <summary>特效UI位置跟节点</summary>
        private Transform UIEffectRoot {
            get {
                if (_uiEffectRoot == null)
                {
                    _uiEffectRoot = new GameObject("_UIEffectRoot").transform;
                    _uiEffectRoot.SetParent(CSF.Mgr.UI.canvas.transform, false);
                }
                return _uiEffectRoot;
            }
        }

        //从缓存中获取特效
        private BaseEffect GetPoolEffect(int templId)
        {
            Stack<BaseEffect> stack;
            if (EffectPool.TryGetValue(templId, out stack))
            {
                if (stack.Count > 0)
                {
                    BaseEffect effect = stack.Pop();
                    if (effect.gameObject != null)
                        return effect;

                }
            }
            return null;
        }
        public void Release(BaseEffect effect)
        {
            Stack<BaseEffect> stack;
            effect.gameObject.transform.SetParent(WorldEffectRoot, false);
            if (!EffectPool.TryGetValue(effect.Config.id, out stack))
            {
                stack = new Stack<BaseEffect>();                
                EffectPool.Add(effect.Config.id, stack);
            }
            stack.Push(effect);
        }


        public void Dispose()
        {

        }
    }
}
