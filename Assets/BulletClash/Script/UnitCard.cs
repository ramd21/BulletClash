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
		public int _Id;
		public int _PosId;

		public static bool gIsDrag;

		int _Cost;
		public Text			_TxtCost;
		public Text			_TxtNext;

		public Transform	_TraDeckUnit;
		UnitParam _Param;

		public void Init(UnitType aType)
		{
			Deb.MethodLog();
			_Param = MasterMan.i._UnitParam[(int)aType];

			_Cost = _Param.Cost;
			_TxtCost.text = _Cost.ToString();

			Unit unit;
			unit = ResourceMan.i.GetUnit(_Param.Type);
			unit._PlayerId = 0;
			unit = Instantiate(unit);

			unit._CvsHp = unit.GetComponentInChildren<Canvas>();
			unit._CvsHp.gameObject.SetActive(false);
			unit.transform.parent = _TraDeckUnit;
			unit.transform.ResetLocalTransform();

			BackFire[] bfArr = unit.GetComponentsInChildren<BackFire>();
			for (int i = 0; i < bfArr.Length; i++)
			{
				bfArr[i]._CamTage = BattleCameraMan.i._BattleUICam;
			}

			LayerSet[] lsArr = GetComponentsInChildren<LayerSet>(true);
			for (int i = 0; i < lsArr.Length; i++)
			{
				lsArr[i].Set(true);
			}
		}

		public override void ManagedUpdate()
		{
			base.ManagedUpdate();

			if (_PosId == 4)
			{
				if (_Cost <= BattlePlayerMan.i._myPlayer.GetTP())
					_image.color = Color.green;
				else
					_image.color = Color.green / 2;
			}
			else
			{
				if (_Cost <= BattlePlayerMan.i._myPlayer.GetTP())
					_image.color = Color.white;
				else
					_image.color = Color.gray;
			}
		}

		public void SetPosId(int aPosId)
		{
			_PosId = aPosId;

			if (_PosId == 4)
			{
				_TxtNext.gameObject.SetActive(true);
				if (_Cost <= BattlePlayerMan.i._myPlayer.GetTP())
					_image.color = Color.green;
				else
					_image.color = Color.green / 2;
			}
			else
			{ 
				_TxtNext.gameObject.SetActive(false);
				if (_Cost <= BattlePlayerMan.i._myPlayer.GetTP())
					_image.color = Color.white;
				else
					_image.color = Color.gray;
			}

			this.DoUntil(()=> 
			{
				transform.position = Vector3.MoveTowards(transform.position, BattleUIMan.i._TraPosArr[_PosId].position, 20);
				return Vector3.Distance(BattleUIMan.i._TraPosArr[_PosId].position, transform.position) < 0.001f;
			});
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
			if (_PosId >= 4)
				return;

			//BattleCameraMan.i._DragCamera.enabled = false;
			gIsDrag = true;
		}

		public override void OnDrag(PointerEventData eventData)
		{
			if (_PosId >= 4)
				return;

			base.OnDrag(eventData);
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			if (_PosId >= 4)
				return;

			//BattleCameraMan.i._DragCamera.enabled = true;
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
							
							BattleUIMan.i._ShuffledList[4].SetPosId(_PosId);
							BattleUIMan.i._ShuffledList[5].SetPosId(4);
							SetPosId(5);


							BattleUIMan.i._ShuffledList.Remove(this);
							BattleUIMan.i._ShuffledList.Add(this);
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
			_TxtNext = transform.Find("next").GetComponent<Text>();
			_TraDeckUnit = transform.FindRecurcive("deck_unit", true);
		}
#endif

	}
}


