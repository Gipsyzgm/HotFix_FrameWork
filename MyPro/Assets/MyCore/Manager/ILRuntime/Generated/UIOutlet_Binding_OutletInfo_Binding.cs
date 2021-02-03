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
    unsafe class UIOutlet_Binding_OutletInfo_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::UIOutlet.OutletInfo);

            field = type.GetField("Name", flag);
            app.RegisterCLRFieldGetter(field, get_Name_0);
            app.RegisterCLRFieldSetter(field, set_Name_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_Name_0, AssignFromStack_Name_0);
            field = type.GetField("Object", flag);
            app.RegisterCLRFieldGetter(field, get_Object_1);
            app.RegisterCLRFieldSetter(field, set_Object_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_Object_1, AssignFromStack_Object_1);


        }



        static object get_Name_0(ref object o)
        {
            return ((global::UIOutlet.OutletInfo)o).Name;
        }

        static StackObject* CopyToStack_Name_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UIOutlet.OutletInfo)o).Name;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_Name_0(ref object o, object v)
        {
            ((global::UIOutlet.OutletInfo)o).Name = (System.String)v;
        }

        static StackObject* AssignFromStack_Name_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @Name = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::UIOutlet.OutletInfo)o).Name = @Name;
            return ptr_of_this_method;
        }

        static object get_Object_1(ref object o)
        {
            return ((global::UIOutlet.OutletInfo)o).Object;
        }

        static StackObject* CopyToStack_Object_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UIOutlet.OutletInfo)o).Object;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_Object_1(ref object o, object v)
        {
            ((global::UIOutlet.OutletInfo)o).Object = (UnityEngine.Object)v;
        }

        static StackObject* AssignFromStack_Object_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Object @Object = (UnityEngine.Object)typeof(UnityEngine.Object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::UIOutlet.OutletInfo)o).Object = @Object;
            return ptr_of_this_method;
        }



    }
}
