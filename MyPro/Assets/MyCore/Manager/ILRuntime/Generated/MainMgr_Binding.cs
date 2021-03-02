using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class MainMgr_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::MainMgr);

            field = type.GetField("UI", flag);
            app.RegisterCLRFieldGetter(field, get_UI_0);
            app.RegisterCLRFieldSetter(field, set_UI_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_UI_0, AssignFromStack_UI_0);


        }



        static object get_UI_0(ref object o)
        {
            return global::MainMgr.UI;
        }

        static StackObject* CopyToStack_UI_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::MainMgr.UI;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_UI_0(ref object o, object v)
        {
            global::MainMgr.UI = (global::UIMgr)v;
        }

        static StackObject* AssignFromStack_UI_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::UIMgr @UI = (global::UIMgr)typeof(global::UIMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            global::MainMgr.UI = @UI;
            return ptr_of_this_method;
        }



    }
}
