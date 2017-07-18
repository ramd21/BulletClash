using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BC
{
	public class UnitCard : UIDragObj, IEditorUpdate
	{
		public Text			_TxtCost;
		public Transform	_TraDeckUnit;

		public void Init(Transform aTraModel)
		{
			aTraModel.parent = _TraDeckUnit;
			aTraModel.ResetLocalTransform();
		}


		protected override Camera _cam
		{
			get
			{
				return CameraMan.i._UICam;
			}
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			CameraMan.i._DragCamera.enabled = false;
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			CameraMan.i._DragCamera.enabled = true;
		}

		public void EditorUpdate()
		{
			_TxtCost = GetComponentInChildren<Text>();
			_TraDeckUnit = transform.FindRecurcive("deck_unit", true);

		}
	}
}


