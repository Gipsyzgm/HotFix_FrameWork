using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//通用的EventListener脚本
//使用方法 EventListener.Get(gameObject).onClick += ObjOnClick;
public class EventListener : UnityEngine.EventSystems.EventTrigger
{
	public delegate void VoidDelegate(GameObject go);
	public VoidDelegate onClick;
	public VoidDelegate onDown;
	public VoidDelegate onEnter;
	public VoidDelegate onExit;
	public VoidDelegate onUp;
	public VoidDelegate onSelect;
	public VoidDelegate onUpdateSelect;
	public VoidDelegate onDrag;
	public VoidDelegate onEndDrag;
	public VoidDelegate onDragStart;
	public VoidDelegate onMove;


	static public EventListener Get(GameObject go)
	{
		EventListener listener = go.GetComponent<EventListener>();
		if (listener == null) listener = go.AddComponent<EventListener>();
		return listener;
	}
	public override void OnBeginDrag(PointerEventData eventData)
	{
		
		if (onDragStart != null) onDragStart(gameObject);

	}
	public override void OnPointerClick(PointerEventData eventData)
	{

		if (onClick != null) onClick(gameObject);
	}
	public override void OnPointerDown(PointerEventData eventData)
	{
		if (onDown != null) onDown(gameObject);
	}
	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (onEnter != null) onEnter(gameObject);
	}
	public override void OnPointerExit(PointerEventData eventData)
	{
		if (onExit != null) onExit(gameObject);
	}
	public override void OnPointerUp(PointerEventData eventData)
	{
		if (onUp != null) onUp(gameObject);
	}
	public override void OnSelect(BaseEventData eventData)
	{
		if (onSelect != null) onSelect(gameObject);
	}
	public override void OnUpdateSelected(BaseEventData eventData)
	{
		if (onUpdateSelect != null) onUpdateSelect(gameObject);
	}
	public override void OnDrag(PointerEventData eventData)
	{
		base.OnDrag(eventData);
		if (onDrag != null) onDrag(gameObject);

	}
	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		if (onEndDrag != null) onEndDrag(gameObject);
	}
	public override void OnMove(AxisEventData eventData)
	{
		if (onMove != null) onMove(gameObject);
	}

}