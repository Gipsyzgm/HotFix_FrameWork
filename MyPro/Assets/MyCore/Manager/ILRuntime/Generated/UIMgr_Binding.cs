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
    unsafe class UIMgr_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::UIMgr);

            field = type.GetField("canvas", flag);
            app.RegisterCLRFieldGetter(field, get_canvas_0);
            app.RegisterCLRFieldSetter(field, set_canvas_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_canvas_0, AssignFromStack_canvas_0);


        }



        static object get_canvas_0(ref object o)
        {
            return ((global::UIMgr)o).canvas;
        }

        static StackObject* CopyToStack_canvas_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UIMgr)o).canvas;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_canvas_0(ref object o, object v)
        {
            ((global::UIMgr)o).canvas = (UnityEngine.Canvas)v;
        }

        static StackObject* AssignFromStack_canvas_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Canvas @canvas = (UnityEngine.Canvas)typeof(UnityEngine.Canvas).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::UIMgr)o).canvas = @canvas;
            return ptr_of_this_method;
        }



    }
}
