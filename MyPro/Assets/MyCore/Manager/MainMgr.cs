using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 管理器统一入口
/// </summary>
public class MainMgr
{
    /// <summary>网络管理器</summary>
    //public static NetMgr Net;
    public static UIMgr UI;
    /// <summary>ILRuntime管理器</summary>
    public static ILRMgr ILR;
    // <summary>任务(协成)管理器</summary>
    public static CTaskMgr Task;
    /// <summary>版本检测管理器，客户端唯一</summary>
    public static VersionCheckMgr VersionCheck;



    public static void Initialize()
    {
        UI = UIMgr.Create();
        ILR = ILRMgr.Create();
        Task = CTaskMgr.Create();
        VersionCheck = VersionCheckMgr.Create();


    }



    public static void Dispose()
    {
        ILR?.Dispose();
    }
}