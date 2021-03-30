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
            System_String_Binding.Register(app);
            System_Text_ASCIIEncoding_Binding.Register(app);
            System_Text_Encoding_Binding.Register(app);
            System_Text_RegularExpressions_Regex_Binding.Register(app);
            System_Text_RegularExpressions_Group_Binding.Register(app);
            UnityEngine_Vector2_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            System_Single_Binding.Register(app);
            System_Int64_Binding.Register(app);
            System_Byte_Binding.Register(app);
            System_Text_UTF8Encoding_Binding.Register(app);
            UnityEngine_Color_Binding.Register(app);
            UnityEngine_ColorUtility_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            System_Int32_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            UnityEngine_Mathf_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            UnityEngine_Application_Binding.Register(app);
            UnityEngine_Screen_Binding.Register(app);
            CTaskExtension_Binding.Register(app);
            CTask_Binding.Register(app);
            RoutineBase_Binding.Register(app);
            System_Array_Binding.Register(app);
            System_Collections_Generic_List_1_IDisposable_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            CTask_1_GameObject_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            System_Collections_Generic_Stack_1_GameObject_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Stack_1_GameObject_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Stack_1_GameObject_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_Stack_1_GameObject_Binding.Register(app);
            System_Collections_Generic_Stack_1_GameObject_Binding_Enumerator_Binding.Register(app);
            UnityEngine_AddressableAssets_Addressables_Binding.Register(app);
            UnityEngine_ResourceManagement_AsyncOperations_AsyncOperationHandle_1_GameObject_Binding.Register(app);
            System_Threading_Tasks_Task_1_GameObject_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_GameObject_Binding.Register(app);
            UnityEngine_AudioSource_Binding.Register(app);
            UnityEngine_PlayerPrefs_Binding.Register(app);
            System_Collections_Generic_HashSet_1_String_Binding.Register(app);
            System_Random_Binding.Register(app);
            UnityEngine_ResourceManagement_AsyncOperations_AsyncOperationHandle_1_AudioClip_Binding.Register(app);
            System_Threading_Tasks_Task_1_AudioClip_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_AudioClip_Binding.Register(app);
            System_Threading_Monitor_Binding.Register(app);
            System_Action_Binding.Register(app);
            System_Action_1_Single_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_List_1_Int32_Binding.Register(app);
            System_Collections_Generic_Queue_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding_ValueCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding_ValueCollection_Binding_Enumerator_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_GameObject_Binding.Register(app);
            DG_Tweening_DOTweenModuleUI_Binding.Register(app);
            DG_Tweening_TweenSettingsExtensions_Binding.Register(app);
            DG_Tweening_ShortcutExtensions_Binding.Register(app);
            UnityEngine_UI_Button_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);
            UnityEngine_UI_Graphic_Binding.Register(app);
            UnityEngine_UI_Image_Binding.Register(app);
            UnityEngine_Resources_Binding.Register(app);
            UnityEngine_Renderer_Binding.Register(app);
            System_Collections_Generic_List_1_Transform_Binding.Register(app);
            System_Collections_IEnumerator_Binding.Register(app);
            UnityEngine_ResourceManagement_AsyncOperations_AsyncOperationHandle_1_SpriteAtlas_Binding.Register(app);
            System_Threading_Tasks_Task_1_SpriteAtlas_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_SpriteAtlas_Binding.Register(app);
            UnityEngine_U2D_SpriteAtlas_Binding.Register(app);
            UnityEngine_ResourceManagement_AsyncOperations_AsyncOperationHandle_1_Texture_Binding.Register(app);
            System_Threading_Tasks_Task_1_Texture_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_Texture_Binding.Register(app);
            UnityEngine_UI_RawImage_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Transform_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Enum_Binding.Register(app);
            System_Collections_Generic_List_1_String_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            MainMgr_Binding.Register(app);
            UIMgr_Binding.Register(app);
            UIOutlet_Binding.Register(app);
            System_Collections_Generic_List_1_UIOutlet_Binding_OutletInfo_Binding.Register(app);
            UIOutlet_Binding_OutletInfo_Binding.Register(app);
            UnityEngine_UI_Toggle_Binding.Register(app);
            UnityEngine_Events_UnityEvent_1_Boolean_Binding.Register(app);
            System_Boolean_Binding.Register(app);

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
