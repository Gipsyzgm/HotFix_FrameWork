using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class VersionCheckMgr : BaseMgr<VersionCheckMgr>
{
    private UpDataUI CheckUI;

    public bool IsUpdateCheckComplete = false;
    // Start is called before the first frame update
    public async CTask CrateCheckUI()
    {
        //创建检测UI
        GameObject obj = Resources.Load<GameObject>("VersionCheck/UpDataUI");
        GameObject go = Instantiate(obj);
        go.SetActive(true);
        CheckUI = go.GetComponent<UpDataUI>();
        CheckUI.transform.SetParent(MainMgr.UI.canvas.transform);    
        await CTask.WaitForNextFrame();
        SetVersion();
        SetTitle(VerCheckLang.CheckResInfo);    //检测资源信息   
    }

    public void Check() 
    {
        //版本验证并更新
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            CheckUI.Confirm(() =>
            {
                Check();

            }, Application.Quit, VerCheckLang.Request_Version_Error, VerCheckLang.ErrorTitle);
            return;
        }
        else
        {
            StartCheck().Run();
        }
    }

    //跳过检测
    public void SkipCheck()
    {
        CheckUI.LoadProgress.value = 0.9f;
        CheckUI.Status.text = VerCheckLang.InitRes;
        MainMgr.VersionCheck.IsUpdateCheckComplete = true;


    }

    /// <summary>
    /// 设置版本号
    /// </summary>
    public void SetVersion(int resVersion = 0)
    {
        string ver = Application.version;
        if (resVersion > 0)
            ver += "." + resVersion;
        CheckUI.VersionText.text = ver;
    }
    /// <summary>
    /// 设置标题
    /// </summary>
    public void SetTitle(string title)
    {
        CheckUI.MainTitle.text = title;
    }

    public void CloseUpDataUI()
    {
        CheckUI.LoadProgress.value = 1.0f;
        CheckUI.Status.text = VerCheckLang.InitRes;
        ShowUIAnim(CheckUI.gameObject, 0.35f);
    }
    /// <summary>
    /// 仅针对关闭CheckUI的动画
    /// </summary>
    /// <param name="target"></param>
    /// <param name="UIAnimTime"></param>
    public void ShowUIAnim(GameObject target,float UIAnimTime)
    {
        float time = UIAnimTime;
        Graphic[] comps = target.GetComponentsInChildren<Graphic>();
        for (int i = comps.Length; --i >= 0;)
        {           
                if (i == 0)
                {
                    comps[i].DOFade(0, time).SetUpdate(true).OnComplete(() =>
                    {
                        target.SetActive(false);
                        GameObject.Destroy(target);
                    });
                }
                else
                {
                    comps[i].DOFade(0, time).SetUpdate(true);
                }            
        }
    }


 

}
