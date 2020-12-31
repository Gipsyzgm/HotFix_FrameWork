using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI跟跟随目标
/// </summary>
public class UIFollowTarget : MonoBehaviour
{
    //跟随目标对象
    public Transform Target;
    //位置偏移
    public Vector3 OffSet;

    protected RectTransform owner;
    protected Camera mainCamera;
    protected Camera uiCamera;
    private void Start()
    {
        owner = transform as RectTransform;
        mainCamera = Camera.main;
        //uiCamera = CSF.Mgr.UI.canvas.worldCamera;
    }
    Vector2 pos = Vector2.zero;
    void LateUpdate()
    {
        if (Target != null && mainCamera!=null)
        {
            var scenePos = mainCamera.WorldToScreenPoint(Target.position+OffSet);  //Target.TransformPoint(OffSet)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, scenePos, uiCamera, out pos);
            owner.anchoredPosition = pos;
        }
    }

    public void ResetMainCamera()
    {
        mainCamera = Camera.main;
    }
}
