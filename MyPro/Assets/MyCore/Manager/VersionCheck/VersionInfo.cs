using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionInfo
{
    /// <summary>平台 Android/IOS </summary>
    public string Platform { get; set; }
    /// <summary>客户端版本 </summary>
    public string AppVersion { get; set; }

    /// <summary>是否强制更新</summary>
    public bool IsForcedUpdate { get; set; }
    /// <summary>强更包路径</summary>
    public string AppDownloadURL { get; set; }

    /// <summary>资源下载地址</summary>
    public string ResURL { get; set; }

    /// <summary>登陆验证地址</summary>
    public string LoginUrl { get; set; }

}