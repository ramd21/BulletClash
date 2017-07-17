using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RM;

namespace BC
{
	public abstract class UIDragObj : RMBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		protected abstract Camera _cam { get; }

		Vector3 _DragStart;
		Vector3 _DragCur;
		Vector3 _StartPos;

		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			_DragStart = _cam.ScreenToWorldPoint(eventData.position);
			_StartPos = transform.position;
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
			_DragCur = _cam.ScreenToWorldPoint(eventData.position);
			transform.position = _StartPos + _DragCur - _DragStart;
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
			transform.position = _StartPos;
		}

		public virtual void OnPointerDown(PointerEventData eventData)
		{
			
		}

		public virtual void OnPointerUp(PointerEventData eventData)
		{
			
		}
	}
}




