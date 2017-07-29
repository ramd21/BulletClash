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
			unit.gameObject.SetLayer("battle_ui");
			unit.gameObject.SetActive(true);

			unit.transform.parent = _TraDeckUnit;
			unit.transform.ResetLocalTransform();
			unit._CvsHp.gameObject.SetActive(false);

			BackFire[] bfArr = unit.GetComponentsInChildren<BackFire>();
			for (int i = 0; i < bfArr.Length; i++)
			{
				bfArr[i]._CamTage = BattleCameraMan.i._BattleUICam;
			}

		}


		protected override Camera _cam
		{
			get
			{
				return BattleCameraMan.i._BattleUICam;
			}
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			BattleCameraMan.i._DragCamera.enabled = false;
			gIsDrag = true;
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			BattleCameraMan.i._DragCamera.enabled = true;
			gIsDrag = false;

			if (BattlePlayerMan.i._myPlayer.GetTP() >= _Param.Cost)
			{
				Ray r = BattleCameraMan.i._BattleCam.ScreenPointToRay(eventData.position);
				RaycastHit rh;
				if(Physics.Raycast(r, out rh))
				{
					Vector2Int pos = rh.point.ToVector2IntXZ() * BattleGameMan.cDistDiv - BattleFieldMan.i._Offset;

					for (int i = 0; i < BattleCharaMan.i._TowerList[BattlePlayerMan.i._MyPlayerId].Count; i++)
					{
						Tower tw = BattleCharaMan.i._TowerList[BattlePlayerMan.i._MyPlayerId][i];
						if (RMMath.GetApproxDist((int)tw._Tra._Pos.x, (int)tw._Tra._Pos.y, (int)pos.x, (int)pos.y) <= 15 * BattleGameMan.cDistDiv)
						{
							BattlePlayerMan.i._myPlayer.PlaceUnit(_Param, pos);
							break;
						}
					}
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


