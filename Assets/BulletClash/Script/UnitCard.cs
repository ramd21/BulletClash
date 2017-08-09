using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BC
{
	public class UnitCard : ManagedBehaviour
	{
		public int _Id;
		public int _PosId;

		public static bool gIsDrag;

		int _Cost;
		public Text			_TxtCost;
		public Text			_TxtNext;
		public SpriteRenderer _SPRend;


		public Transform	_TraDeckUnit;
		UnitParam _Param;

		Vector3 _DragStart;
		Vector3 _DragCur;
		Vector3 _StartPos;

		Ray _Ray;

		public void Init(UnitType aType)
		{
			_Param = MasterMan.i._UnitParam[(int)aType];

			_Cost = _Param.Cost;
			_TxtCost.text = _Cost.ToString();

			Unit unit;
			unit = ResourceMan.i.GetUnit(_Param.Type);
			unit._PlayerId = BattlePlayerMan.i._MyPlayerId;
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
			if (_PosId == 4)
			{
				_SPRend.enabled = true;
				_TxtNext.gameObject.SetActive(true);
				_TxtCost.gameObject.SetActive(true);
				transform.GetChild(0).gameObject.SetActive(true);
				if (_Cost <= BattlePlayerMan.i._myPlayer.GetTP())
					_SPRend.color = Color.green;
				else
					_SPRend.color = Color.green / 2;

			}
			else if (_PosId == 5)
			{
				_SPRend.enabled = false;
				_TxtNext.gameObject.SetActive(false);
				_TxtCost.gameObject.SetActive(false);
				transform.GetChild(0).gameObject.SetActive(false);
			}
			else
			{
				_SPRend.enabled = true;
				_TxtNext.gameObject.SetActive(false);
				_TxtCost.gameObject.SetActive(true);
				transform.GetChild(0).gameObject.SetActive(true);

				if (_Cost <= BattlePlayerMan.i._myPlayer.GetTP())
					_SPRend.color = Color.white;
				else
					_SPRend.color = Color.gray;
			}


			Vector2 curPos = RMInput.i.GetInputInfo(0)._V2ScreenInputCur;
			_Ray = BattleCameraMan.i._BattleUICam.ScreenPointToRay(curPos);
			RaycastHit rh;

			if (!gIsDrag)
			{
				if (RMInput.i.GetInptState(0) == RMInput.InputState.start)
				{
					if (Physics.Raycast(_Ray, out rh, float.MaxValue))
					{
						if (rh.transform == transform)
							OnPointerDown(curPos);
					}
				}
			}
			else
			{
				if (RMInput.i.GetInptSeq(0) == RMInput.InputSeq.drag)
				{
					if (Physics.Raycast(_Ray, out rh, float.MaxValue))
					{
						if (rh.transform == transform)
							OnDrag(curPos);
					}
				}

				if (RMInput.i.GetInptState(0) == RMInput.InputState.end)
				{
					if (Physics.Raycast(_Ray, out rh, float.MaxValue))
					{
						if (rh.transform == transform)
						{
							OnPointerUp(curPos);
						}
					}
				}
			}
		}

		public void SetPosId(int aPosId)
		{
			_PosId = aPosId;

			this.DoUntil(()=> 
			{
				transform.position = Vector3.MoveTowards(transform.position, BattleUIMan.i._TraPosArr[_PosId].position, 20);
				return Vector3.Distance(BattleUIMan.i._TraPosArr[_PosId].position, transform.position) < 0.001f;
			});
		}

		protected Camera _cam
		{
			get
			{
				return BattleCameraMan.i._BattleUICam;
			}
		}

		public void OnPointerDown(Vector2 aScreenPos)
		{
			if (_PosId >= 4)
				return;

			_DragStart = _cam.ScreenToWorldPoint(aScreenPos);
			_StartPos = transform.position;

			gIsDrag = true;
		}

		public void OnDrag(Vector2 aScreenPos)
		{
			if (_PosId >= 4)
				return;

			_DragCur = _cam.ScreenToWorldPoint(aScreenPos);
			transform.position = _StartPos + _DragCur - _DragStart;
		}

		public void OnPointerUp(Vector2 aScreenPos)
		{
			if (_PosId >= 4)
				return;

			transform.position = _StartPos;

			gIsDrag = false;

			if (BattlePlayerMan.i._myPlayer.GetTP() >= _Param.Cost)
			{
				Ray r = BattleCameraMan.i._BattleCam.ScreenPointToRay(aScreenPos);
				RaycastHit rh;
				if(Physics.Raycast(r, out rh))
				{
					Vector2Int pos = rh.point.ToVector2IntXZ() * BattleGameMan.cDistDiv - BattleFieldMan.i._Offset;

					for (int i = 0; i < BattleCharaMan.i._TowerList[BattlePlayerMan.i._MyPlayerId].Count; i++)
					{
						Tower tw = BattleCharaMan.i._TowerList[BattlePlayerMan.i._MyPlayerId][i];
						if (RMMath.GetApproxDist((int)tw._Tra._Pos.x, (int)tw._Tra._Pos.y, (int)pos.x, (int)pos.y) <= 15 * BattleGameMan.cDistDiv)
						{
							BattleGameMan.i.SendPlayerInput(_Param.Type, pos, ()=> 
							{
								BattleUIMan.i._ShuffledList[4].SetPosId(_PosId);
								BattleUIMan.i._ShuffledList[5].SetPosId(4);
								SetPosId(5);

								BattleUIMan.i._ShuffledList.Remove(this);
								BattleUIMan.i._ShuffledList.Add(this);
							});
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

		private void OnDrawGizmos()
		{
			Gizmos.DrawLine(_Ray.origin, _Ray.origin + _Ray.direction * 100);
		}


#endif

	}
}


