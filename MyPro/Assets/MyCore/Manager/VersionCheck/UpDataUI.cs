using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UpDataUI : MonoBehaviour
{
    public Text MainTitle;
    public Slider LoadProgress;
    public Text Status;
    //提示页面的素材
    public GameObject Tips;
    public Button ConfirmBtn;
    public Button CancelBtn;
    public Text ConfirmText;
    public Text CancelText;
    public Text VersionText;
    public Text ConfirmContent;
    public Text TipsTitle;

    private Action ConfirmCB;
    private Action CancelCB;

    void Awake()
    {
        LoadProgress.value = 0;
        Tips.SetActive(false);
        ConfirmBtn.onClick.AddListener(BtnConfirm_Click);
        CancelBtn.onClick.AddListener(BtnCancel_Click);
    }

    public void Confirm(Action confirmcb, Action cancelcb, string content, string title = null, bool ShowCancel = true)
    {
        if (Tips.activeSelf) return;
        ConfirmCB = confirmcb;
        CancelCB = cancelcb;
        Tips.SetActive(true);
        TipsTitle.text = title;
        ConfirmContent.text = content;
        ConfirmText.text = VerCheckLang.Confirm;
        CancelText.text = VerCheckLang.Cancel;
        CancelBtn.gameObject.SetActive(ShowCancel); //Alert 不显示取消       
        ShowUIAnim(Tips, "In", 0.2f);
    }
    /// <summary>确认</summary>
    void BtnConfirm_Click()
    {
        CloseConfirm(ConfirmCB);
    }
    /// <summary>取消</summary>
    void BtnCancel_Click()
    {
        CloseConfirm(CancelCB);
    }
    public void CloseConfirm(Action action)
    {        
        ShowUIAnim(Tips, "Out", 0.2f, action);     
    }


    public void ShowUIAnim(GameObject target, string anim, float UIAnimTime, Action action = null)
    {
        float time = UIAnimTime;
        if (anim == "In")
        {
            target.transform.DOScale(0, time).SetUpdate(true).SetEase(Ease.OutBack).From();
        }
        else
        {
            target.transform.DOScale(0, time).SetUpdate(true).SetEase(Ease.InBack).OnComplete(() =>
            {
                if (target.activeSelf)
                {
                    target.SetActive(false);
                    target.transform.localScale = Vector3.one;
                    action?.Invoke();
                }
            });
        }
    }
}
