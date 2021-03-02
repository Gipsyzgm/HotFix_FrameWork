using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.EventSystems;
//按钮长按功能
public class LongPressOrClickEventTrigger : UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    public float durationThreshold = 0.7f;
    public float moveDisCancel = 10;
    public float ResponseTime = 0.1f;

    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onClick = new UnityEvent();

    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float timePressStarted;

    private float tempTime = 0;
    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
        {
            if (Time.time - timePressStarted > durationThreshold)
            {               
                longPressTriggered = true;
                onLongPress.Invoke();
            }
        }
        if (isPointerDown && longPressTriggered)
        {
            tempTime += Time.deltaTime;
            if (tempTime>ResponseTime)
            {
                tempTime = 0;
                onLongPress.Invoke();
            }

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        if (Vector2.Distance(eventData.delta, Vector2.zero) > moveDisCancel) 
        {
            isPointerDown = false;
        }       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!longPressTriggered)
        {
            onLongPress.Invoke();
            isPointerDown = false;
        }
    }
}