using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class ILRHelper
{
    public static bool IsRunning = true;
    public static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        appdomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        appdomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
        appdomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());

        // 注册重定向函数

        // 注册委托
        appdomain.DelegateManager.RegisterMethodDelegate<List<object>>();
        appdomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance>();
        appdomain.DelegateManager.RegisterFunctionDelegate<System.Boolean>();
        appdomain.DelegateManager.RegisterMethodDelegate<System.Int32>();
        appdomain.DelegateManager.RegisterMethodDelegate<System.Boolean>();
        appdomain.DelegateManager.RegisterMethodDelegate<Vector2>();
        appdomain.DelegateManager.RegisterMethodDelegate<System.Single>();
        appdomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.Int32>();
        appdomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.Single>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Vector3, System.Single>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Vector2, System.Single>();

        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Collider>();
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Collider2D>();
        appdomain.DelegateManager.RegisterMethodDelegate<System.Exception>();


        //注册UGUI事件委托
        appdomain.DelegateManager.RegisterMethodDelegate<BaseEventData>();
        appdomain.DelegateManager.RegisterMethodDelegate<PointerEventData>();
        appdomain.DelegateManager.RegisterMethodDelegate<AxisEventData>();



        appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction>((action) =>
        {
            return new UnityAction(() => { ((System.Action)action)(); });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<System.Int32>>((act) =>
        {
            return new UnityEngine.Events.UnityAction<System.Int32>((arg0) =>
            {
                ((Action<System.Int32>)act)(arg0);
            });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<System.Boolean>>((act) =>
        {
            return new UnityEngine.Events.UnityAction<System.Boolean>((arg0) =>
            {
                ((Action<System.Boolean>)act)(arg0);
            });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<System.Action<UnityEngine.Vector3, System.Single>>((act) =>
        {
            return new System.Action<UnityEngine.Vector3, System.Single>((arg1, arg2) =>
            {
                ((Action<UnityEngine.Vector3, System.Single>)act)(arg1, arg2);
            });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<System.Action<UnityEngine.Vector2, System.Single>>((act) =>
        {
            return new System.Action<UnityEngine.Vector2, System.Single>((arg1, arg2) =>
            {
                ((Action<UnityEngine.Vector2, System.Single>)act)(arg1, arg2);
            });
        });

        appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<BaseEventData>>((action) =>
        {
            return new UnityAction<BaseEventData>((a) => { ((System.Action<BaseEventData>)action)(a); });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<EventListener.PointerDataDelegate>((act) =>
        {
            return new EventListener.PointerDataDelegate((eventData) => { ((Action<UnityEngine.EventSystems.PointerEventData>)act)(eventData); });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<EventListener.AxisDataDelegate>((act) =>
        {
            return new EventListener.AxisDataDelegate((eventData) => { ((Action<UnityEngine.EventSystems.AxisEventData>)act)(eventData); });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback>((act) =>
        {
            return new DG.Tweening.TweenCallback(() =>
            {
                ((Action)act)();
            });
        });

        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<UnityEngine.Vector2>>((act) =>
        {
            return new UnityEngine.Events.UnityAction<UnityEngine.Vector2>((arg0) =>
            {
                ((Action<UnityEngine.Vector2>)act)(arg0);
            });
        });

        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOSetter<System.Single>>((act) =>
        {
            return new DG.Tweening.Core.DOSetter<System.Single>((pNewValue) =>
            {
                ((Action<System.Single>)act)(pNewValue);
            });
        });

        appdomain.DelegateManager.RegisterFunctionDelegate<System.Single>();
        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOGetter<System.Single>>((act) =>
        {
            return new DG.Tweening.Core.DOGetter<System.Single>(() =>
            {
                return ((Func<System.Single>)act)();
            });
        });

        //CLog.RegisterILRuntimeCLRRedirection(appdomain);
        //ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);

        //// 注册适配器
        //Assembly assembly = typeof(Main).Assembly;
        //foreach (Type type in assembly.GetTypes())
        //{
        //    object[] attrs = type.GetCustomAttributes(typeof(ILAdapterAttribute), false);
        //    if (attrs.Length == 0)
        //    {
        //        continue;
        //    }
        //    object obj = Activator.CreateInstance(type);
        //    CrossBindingAdaptor adaptor = obj as CrossBindingAdaptor;
        //    if (adaptor == null)
        //    {
        //        continue;
        //    }
        //    appdomain.RegisterCrossBindingAdaptor(adaptor);
        //}
        //LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);
        //ConfigUtils.RegisterILRuntimeCLRRedirection(appdomain);
 
    }

    //ILIntepreter4077 method.ExceptionHandler
    public static void MethodException(ILIntepreter intepreter, Exception ex, ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        if (!(ex is ILRuntimeException))
        {
            if (appdomain != null && appdomain.DebugService != null)
            {
                //CLog.Error("[ILR Error]:" + ex.Message + "\t\t" + appdomain.DebugService.GetStackTrace(intepreter) + "\n");
            }
        }
    }
}
