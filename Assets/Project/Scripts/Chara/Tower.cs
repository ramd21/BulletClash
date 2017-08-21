using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using System;

namespace BC
{
	public class Tower : UnitBase, IEditorUpdate, IManagedUpdate
	{
		static int gCnt;

		public Transform _TraRot;
		public Transform _TraCannon;

		public GameObject _GoUnitSpawnRange;


		Chara _Tage;
		int _TageDist;

		public struct FrameData
		{
			public ActiveState _State;
			public int _Hp;
		}

		public FrameData GetFrameData()
		{
			FrameData fd = new FrameData();
			fd._State = _State;
			fd._Hp = _Param.Hp;
			return fd;
		}

		public void Restore(FrameData aFrameData)
		{
			_State = aFrameData._State;
			_Param.Hp = aFrameData._Hp;

			switch (_State)
			{
				case ActiveState.active:
					gameObject.SetActive(true);
					break;
				case ActiveState.deactivate_req:
					gameObject.SetActive(true);
					break;
			}
		}

		void Start()
		{
			this.WaitForEndOfFrame(() =>
			{
				InstantiateInit(_PlayerId);
				BattleCharaMan.i._TowerList[_PlayerId].Add(this);
				transform.parent = BattleCharaMan.i._TraPlayerParent[_PlayerId];
				ActivateReq(transform.position.ToVector2IntXZ() * BattleGameMan.cDistDiv - BattleFieldMan.i._Offset);
				gameObject.SetActive(true);
				_Coll.UpdateBlock();
			});
		}

		protected virtual void OnEnable()
		{
			ManagedUpdateMan.i._ManagedList.Add(this);
		}

		protected virtual void OnDisable()
		{
			ManagedUpdateMan.i._RemoveList.Add(this);
		}

		public override void InstantiateInit(int aPlayerId)
		{
			base.InstantiateInit(aPlayerId);
			_Id = gCnt;
			gCnt++;
			_Type = CharaType.tower;
			_GoUnitSpawnRange.SetActive(false);
		}

		public override void OnFrameBegin()
		{
			base.OnFrameBegin();
		}

		public override void ActivateReq(Vector2Int aPos)
		{
			base.ActivateReq(aPos);
		}

		public void SearchTage()
		{
			_TageDist = int.MaxValue;
			int dist;
			int len;
			_Tage = null;

			Unit u;
			len = BattleCharaMan.i._UnitList[_VSPlayerId].Count;
			for (int i = 0; i < len; i++)
			{
				u = BattleCharaMan.i._UnitList[_VSPlayerId][i];
				if (u._State == ActiveState.active)
				{
					dist = RMMath.GetApproxDist(_Tra._Pos.x, _Tra._Pos.y, u._Tra._Pos.x, u._Tra._Pos.y);
					if (dist < _TageDist)
					{
						_Tage = u;
						_TageDist = dist;
					}
				}
			}
		}

		public void Fire()
		{
			if (!_Tage)
				return;

			if (_TageDist > _Param.Range)
				return;

			if (_Param.FireInter == 0)
			{
				Bullet b = BattleCharaMan.i.GetPoolOrNewBullet(_PlayerId, _Param.Bullet);
				b.ActivateReq(_Tra._Pos, _Tage._Tra._Pos - _Tra._Pos);
				_Param.FireInter = _ParamDef.FireInter;
			}

			_Param.FireInter--;
		}

		public override void OnFrameEnd()
		{
			base.OnFrameEnd();
		}

		public override void UpdateView()
		{
			base.UpdateView();
			if (_Tage)
				_TraCannon.LookAt(_Tage.transform, Vector3.up);

			if (_PlayerId == BattlePlayerMan.i._MyPlayerId)
			{
				if (UnitCard.gDragUnitCard)
				{
					_GoUnitSpawnRange.transform.localScale = Vector3.MoveTowards(_GoUnitSpawnRange.transform.localScale, Vector3.one * 30, 3);
				}
				else
				{
					_GoUnitSpawnRange.transform.localScale = Vector3.MoveTowards(_GoUnitSpawnRange.transform.localScale, Vector3.zero, 3);
				}

				_GoUnitSpawnRange.SetActive(_GoUnitSpawnRange.transform.localScale.magnitude != 0);
			}
		}

		public void ManagedUpdate()
		{
			_TraRot.AddEulerAnglesY(2);
		}

		public void ManagedLateUpdate()
		{
		}

#if UNITY_EDITOR
		public void EditorUpdate()
		{
			_Coll = GetComponent<Coll>();
			_Tra = GetComponent<BCTra>();
		}

		
#endif

	}
}


