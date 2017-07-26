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
		public static bool gIsDrag;

		public Text			_TxtCost;
		public Transform	_TraDeckUnit;
		UnitParam _Param;

		public void Init(UnitType aType)
		{
			_Param = MasterMan.i._UnitParam[(int)aType];
			_TxtCost.text = _Param.Cost.ToString();

			Unit unit;
			unit = ResourceMan.i.GetUnit(_Param.Type);
			unit._PlayerId = 0;
			unit = Instantiate(unit);
			unit.gameObject.SetActive(true);
			unit.transform.parent = _TraDeckUnit;
			unit.transform.ResetLocalTransform();
			unit._CvsHp.gameObject.SetActive(false);
			unit.gameObject.SetLayer(LayerMask.NameToLayer("UI"), true);
		}


		protected override Camera _cam
		{
			get
			{
				return CameraMan.i._3DUICam;
			}
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			CameraMan.i._DragCamera.enabled = false;
			gIsDrag = true;
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			CameraMan.i._DragCamera.enabled = true;
			gIsDrag = false;

			if (PlayerMan.i._myPlayer.GetTP() >= _Param.Cost)
			{
				Ray r = CameraMan.i._MainCam.ScreenPointToRay(eventData.position);
				RaycastHit rh;
				if(Physics.Raycast(r, out rh))
				{
					PlayerMan.i._myPlayer.PlaceUnit(_Param, rh.point.ToVector2XZ() * GameMan.cDistDiv - FieldMan.i._Offset);
				}
			}
		}
#if UNITY_EDITOR
		public void EditorUpdate()
		{
			_TxtCost = GetComponentInChildren<Text>();
			_TraDeckUnit = transform.FindRecurcive("deck_unit", true);

		}
#endif

	}
}


