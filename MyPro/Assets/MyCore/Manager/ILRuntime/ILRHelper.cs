using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Utils;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class ILRHelper
{
    public static bool IsRunning = true;
    unsafe  public static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        //值类型绑定
        appdomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        appdomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
        appdomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
        //注册委托

        //注册UGUI事件委托
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction>((action) =>
        {
            return new UnityAction(() =>
            {
                ((System.Action)action)();
            });
        });

        appdomain.DelegateManager.RegisterMethodDelegate<BaseEventData>();
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<BaseEventData>>((action) =>
        {
            return new UnityAction<BaseEventData>((a) =>
            {
                ((System.Action<BaseEventData>)action)(a);
            });
        });
        appdomain.DelegateManager.RegisterMethodDelegate<PointerEventData>();
        appdomain.DelegateManager.RegisterDelegateConvertor<EventListener.PointerDataDelegate>((act) =>
        {
            return new EventListener.PointerDataDelegate((eventData) =>
            {
                ((Action<UnityEngine.EventSystems.PointerEventData>)act)(eventData); 
            });
        });
        appdomain.DelegateManager.RegisterMethodDelegate<AxisEventData>();
        appdomain.DelegateManager.RegisterDelegateConvertor<EventListener.AxisDataDelegate>((act) =>
        {
            return new EventListener.AxisDataDelegate((eventData) => 
            {
                ((Action<UnityEngine.EventSystems.AxisEventData>)act)(eventData);
            });
        });
        //其他事件委托
        appdomain.DelegateManager.RegisterMethodDelegate<System.Object>();
        appdomain.DelegateManager.RegisterFunctionDelegate<System.Object, UniTask, UniTask>();
        appdomain.DelegateManager.RegisterFunctionDelegate<System.Object, UniTask<object>, UniTask<object>>();
        appdomain.DelegateManager.RegisterFunctionDelegate<UniTask,UniTask>();
        appdomain.DelegateManager.RegisterFunctionDelegate<CancellationToken, UniTaskVoid, UniTaskVoid>();
        appdomain.DelegateManager.RegisterFunctionDelegate<UniTaskVoid, UniTaskVoid>();
        appdomain.DelegateManager.RegisterMethodDelegate<System.Object, ILTypeInstance>();
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
        appdomain.DelegateManager.RegisterFunctionDelegate<System.Single>();

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
        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOGetter<System.Single>>((act) =>
        {
            return new DG.Tweening.Core.DOGetter<System.Single>(() =>
            {
                return ((Func<System.Single>)act)();
            });
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



        //注册重定向函数
        appdomain.RegisterCLRMethodRedirection(typeof(Debug).GetMethod("Log", new System.Type[] {typeof(object) }), HotFixLog);
      

        //注册跨域继承适配器
        Register(appdomain);

        //支持LitJson
        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);

        //最后CLR绑定
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
    }

    #region 重定向方法
    //编写重定向方法对于刚接触ILRuntime的朋友可能比较困难，比较简单的方式是通过CLR绑定生成绑定代码，然后在这个基础上改，比如下面这个代码是从UnityEngine_Debug_Binding里面复制来改的
    unsafe static StackObject* HotFixLog(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
        StackObject* ptr_of_this_method;
        StackObject* __ret = ILIntepreter.Minus(__esp, 1);

        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
        System.Object @message = (System.Object)typeof(System.Object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
        __intp.Free(ptr_of_this_method);
        //在真实调用Debug.Log前，我们先获取DLL内的堆栈
        var stacktrace = __domain.DebugService.GetStackTrace(__intp);
        //我们在输出信息后面加上DLL堆栈
        UnityEngine.Debug.Log("<color=#FF00FF>[HotFix:]</color>"+message + "\n" + stacktrace);

        return __ret;
    }
    #endregion


    #region 自动注册程序集

    public static void Register(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        //获取Main所在的程序集
        Assembly assembly = typeof(Main).Assembly;
        //IsSubclassOf确定当前 Type 表示的类是否是从指定的 Type 表示的类派生的。
        foreach (Type type in assembly.GetTypes().ToList().FindAll(t => t.IsSubclassOf(typeof(CrossBindingAdaptor))))
        {
            object obj = Activator.CreateInstance(type);
            CrossBindingAdaptor adaptor = obj as CrossBindingAdaptor;
            if (adaptor == null)
            {
                continue;
            }
            Debug.Log("什么东西啊：" + adaptor.ToString());
            appdomain.RegisterCrossBindingAdaptor(adaptor);
        }
    }
    #endregion




}
