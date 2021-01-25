using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 管理器统一入口
/// </summary>
public class Mgr
{
    /// <summary>网络管理器</summary>
    //public static NetMgr Net;
    /// <summary>UI管理器</summary>
    //public static UIMgr UI;
    /// <summary>ILRuntime管理器</summary>
    public static ILRMgr ILR;
    public static CTaskMgr Task;

    public static void Initialize()
    {

        //UI = UIMgr.Create();
        ILR = ILRMgr.Create();
        Task = CTaskMgr.Create();

    }

    public static void Dispose()
    {
        //UI?.Dispose();
        ILR?.Dispose();
    }
}