using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RM
{
	public class UIDragObj : RMBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		Vector3 _DragStart;
		Vector3 _DragCur;
		Vector3 _StartPos;

		public void OnBeginDrag(PointerEventData eventData)
		{
			Deb.MethodLog();
			_DragStart = CameraMan.i._UICam.ScreenToWorldPoint(eventData.position);
			_StartPos = transform.position;

		}

		public void OnDrag(PointerEventData eventData)
		{
			_DragCur = CameraMan.i._UICam.ScreenToWorldPoint(eventData.position);
			transform.position = _StartPos + _DragCur - _DragStart;

		}

		public void OnEndDrag(PointerEventData eventData)
		{
			Deb.MethodLog();
			transform.position = _StartPos;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			CameraMan.i._DragCamera.enabled = false;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			CameraMan.i._DragCamera.enabled = true;
		}
	}
}


