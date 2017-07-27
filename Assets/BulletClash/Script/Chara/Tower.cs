using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class Tower : Chara, IEditorUpdate
	{
		static int gCnt;
		public UnitParam _Param;
		public UnitParam _ParamDef;
		public Canvas _CvsHp;
		public Image _ImgHp;
		bool _AngleSet;

		public Coll _Coll;

		public Transform _TraRot;
		public Transform _TraCannon;

		public GameObject _GoUnitSpawnRange;


		Chara _Tage;
		int _TageDist;

		void Start()
		{
			this.WaitForEndOfFrame(() =>
			{
				InstantiateInit(_PlayerId);
				CharaMan.i._TowerList[_PlayerId].Add(this);
				transform.parent = CharaMan.i._TraPlayerParent[_PlayerId];
				ActivateReq(transform.position.ToVector2XZ() * GameMan.cDistDiv - FieldMan.i._Offset);
				_Coll.UpdatePos();
			});
		}
		public void InstantiateInit(int aPlayerId)
		{
			_Id = gCnt;
			gCnt++;

			_PlayerId = aPlayerId;
			_VSPlayerId = (_PlayerId + 1) % 2;
			_Type = CharaType.unit;

			_Coll.InstantiateInit(_PlayerId, this);

			_GoUnitSpawnRange.SetActive(false);
		}

		public void ActivateReq(Vector2 aPos)
		{
			gameObject.SetActive(false);
			_State = ActiveState.activate_req;
			_Tra._Pos = aPos;
			_Param = _ParamDef;
		}


		public void Dmg(int aDmg)
		{
			_Param.Hp -= aDmg;
		}


		public void SearchTage()
		{
			_TageDist = int.MaxValue;
			int dist;
			int len;
			_Tage = null;

			Unit u;
			len = CharaMan.i._UnitList[_VSPlayerId].Count;
			for (int i = 0; i < len; i++)
			{
				u = CharaMan.i._UnitList[_VSPlayerId][i];
				if (u._State == ActiveState.active)
				{
					dist = RMMath.GetApproxDist((int)_Tra._Pos.x, (int)_Tra._Pos.y, (int)u._Tra._Pos.x, (int)u._Tra._Pos.y);
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
				Bullet b = CharaMan.i.GetPoolOrNewBullet(_PlayerId, _Param.Bullet);
				b.ActivateReq(_Tra._Pos, _Tage._Tra._Pos - _Tra._Pos);
				_Param.FireInter = _ParamDef.FireInter;
			}

			_Param.FireInter--;
		}

		public void OnFrameEnd()
		{
			if (_Param.Hp <= 0)
				DeactivateReq();
		}

		Coroutine _Coroutine;

		public override void UpdateView()
		{
			base.UpdateView();

			if (_Param.Hp == _ParamDef.Hp)
			{
				_CvsHp.gameObject.SetActive(false);
			}
			else
			{
				_CvsHp.gameObject.SetActive(true);
				_ImgHp.fillAmount = (float)_Param.Hp / _ParamDef.Hp;
			}

			if (!_AngleSet)
			{
				if (_PlayerId == 1)
					transform.SetEulerAnglesY(180);
				_AngleSet = true;
			}

			if(_Tage)
				_TraCannon.LookAt(_Tage.transform, Vector3.up);

			if (UnitCard.gIsDrag)
			{
				_GoUnitSpawnRange.transform.localScale = Vector3.MoveTowards(_GoUnitSpawnRange.transform.localScale, Vector3.one * 30, 3);
			}
			else
			{
				_GoUnitSpawnRange.transform.localScale = Vector3.MoveTowards(_GoUnitSpawnRange.transform.localScale, Vector3.zero, 3);
			}

			_GoUnitSpawnRange.SetActive(_GoUnitSpawnRange.transform.localScale.magnitude != 0);


			_TraRot.AddEulerAnglesY(2);
		}

#if UNITY_EDITOR
		public void EditorUpdate()
		{
			_CvsHp = GetComponentInChildren<Canvas>();
			_ImgHp = transform.FindRecurcive<Image>("hp", true);

			_Coll = GetComponent<Coll>();
			_Tra = GetComponent<BCTra>();
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 1f);
		}
#endif

	}
}


