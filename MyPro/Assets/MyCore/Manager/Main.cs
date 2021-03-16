using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    /// <summary>
    /// 是否进行资源更新
    /// </summary>
    public bool VerCheck = true;

    bool IsStart = false;
    void Awake()
    { 
        DontDestroyOnLoad(this);
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
    }
    // Start is called before the first frame update
    public void InitMgr()
    {
        MainMgr.Initialize();   
    }
    public async CTask StartTask()
    {
        MainMgr.UI.Initialize();
        //初始化ILR
        await MainMgr.ILR.Init();
        IsStart = true;
        MainMgr.ILR.StartHotFixPro();
    }


    // Update is called once per frame
    void Update()
    {
        if (IsStart)
            MainMgr.ILR.CallHotFixMainUpdate(Time.deltaTime);
    }
}
