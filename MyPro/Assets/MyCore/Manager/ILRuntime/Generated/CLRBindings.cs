using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3> s_UnityEngine_Vector3_Binding_Binder = null;
        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Quaternion> s_UnityEngine_Quaternion_Binding_Binder = null;
        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector2> s_UnityEngine_Vector2_Binding_Binder = null;

        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            UnityEngine_Debug_Binding.Register(app);
            CTaskExtension_Binding.Register(app);
            CTask_Binding.Register(app);
            RoutineBase_Binding.Register(app);
            System_Array_Binding.Register(app);
            System_Collections_Generic_List_1_IDisposable_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_GameObject_Binding.Register(app);
            UnityEngine_AddressableAssets_Addressables_Binding.Register(app);
            UnityEngine_ResourceManagement_AsyncOperations_AsyncOperationHandle_1_GameObject_Binding.Register(app);
            System_Threading_Tasks_Task_1_GameObject_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_GameObject_Binding.Register(app);
            System_String_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            System_Action_Binding.Register(app);
            ExtensionMethods_Binding.Register(app);
            UnityEngine_UI_Graphic_Binding.Register(app);
            UnityEngine_Color_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            UnityEngine_UI_Image_Binding.Register(app);
            UnityEngine_Resources_Binding.Register(app);
            UnityEngine_Renderer_Binding.Register(app);
            System_Collections_Generic_List_1_Transform_Binding.Register(app);
            System_Collections_IEnumerator_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            UnityEngine_ResourceManagement_AsyncOperations_AsyncOperationHandle_1_Texture_Binding.Register(app);
            System_Threading_Tasks_Task_1_Texture_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_Texture_Binding.Register(app);
            UnityEngine_UI_RawImage_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Transform_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Enum_Binding.Register(app);
            System_Collections_Generic_List_1_String_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncVoidMethodBuilder_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            DG_Tweening_DOTweenModuleUI_Binding.Register(app);
            DG_Tweening_TweenSettingsExtensions_Binding.Register(app);
            DG_Tweening_ShortcutExtensions_Binding.Register(app);
            UIOutlet_Binding.Register(app);
            System_Collections_Generic_List_1_UIOutlet_Binding_OutletInfo_Binding.Register(app);
            UIOutlet_Binding_OutletInfo_Binding.Register(app);
            UnityEngine_UI_Button_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Vector3));
            s_UnityEngine_Vector3_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3>;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Quaternion));
            s_UnityEngine_Quaternion_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Quaternion>;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Vector2));
            s_UnityEngine_Vector2_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector2>;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            s_UnityEngine_Vector3_Binding_Binder = null;
            s_UnityEngine_Quaternion_Binding_Binder = null;
            s_UnityEngine_Vector2_Binding_Binder = null;
        }
    }
}
