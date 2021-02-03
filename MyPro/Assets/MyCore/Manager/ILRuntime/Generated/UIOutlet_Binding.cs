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
    unsafe class UIOutlet_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::UIOutlet);

            field = type.GetField("OutletInfos", flag);
            app.RegisterCLRFieldGetter(field, get_OutletInfos_0);
            app.RegisterCLRFieldSetter(field, set_OutletInfos_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_OutletInfos_0, AssignFromStack_OutletInfos_0);
            field = type.GetField("Layer", flag);
            app.RegisterCLRFieldGetter(field, get_Layer_1);
            app.RegisterCLRFieldSetter(field, set_Layer_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_Layer_1, AssignFromStack_Layer_1);


        }



        static object get_OutletInfos_0(ref object o)
        {
            return ((global::UIOutlet)o).OutletInfos;
        }

        static StackObject* CopyToStack_OutletInfos_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UIOutlet)o).OutletInfos;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OutletInfos_0(ref object o, object v)
        {
            ((global::UIOutlet)o).OutletInfos = (System.Collections.Generic.List<global::UIOutlet.OutletInfo>)v;
        }

        static StackObject* AssignFromStack_OutletInfos_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::UIOutlet.OutletInfo> @OutletInfos = (System.Collections.Generic.List<global::UIOutlet.OutletInfo>)typeof(System.Collections.Generic.List<global::UIOutlet.OutletInfo>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::UIOutlet)o).OutletInfos = @OutletInfos;
            return ptr_of_this_method;
        }

        static object get_Layer_1(ref object o)
        {
            return ((global::UIOutlet)o).Layer;
        }

        static StackObject* CopyToStack_Layer_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UIOutlet)o).Layer;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_Layer_1(ref object o, object v)
        {
            ((global::UIOutlet)o).Layer = (System.Int32)v;
        }

        static StackObject* AssignFromStack_Layer_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @Layer = ptr_of_this_method->Value;
            ((global::UIOutlet)o).Layer = @Layer;
            return ptr_of_this_method;
        }



    }
}
