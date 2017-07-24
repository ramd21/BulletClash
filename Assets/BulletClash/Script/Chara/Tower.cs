using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class Tower : Chara
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

		Unit _Tage;

		void Start()
		{
			this.WaitForEndOfFrame(() =>
			{
				InstantiateInit(_PlayerId);
				CharaMan.i._TowerList[_PlayerId].Add(this);
				transform.parent = CharaMan.i._TraPlayerParent[_PlayerId];
				ActivateReq(transform.position.ToVector2IntXZ() * GameMan.cDistDiv - FieldMan.i._Offset);
				_Coll.UpdatePos();
			});
		}
		public void InstantiateInit(int aPlayerId)
		{
			_Id = gCnt;
			gCnt++;

			_PlayerId = aPlayerId;
			_VSPlayerId = (_PlayerId + 1) % 2;
			_Type = CharaType.tower;

			_Coll.InstantiateInit(_PlayerId, this);
		}

		public void ActivateReq(Vector2Int aPos)
		{
			gameObject.SetActive(false);
			_State = ActiveState.activate_req;
			_Tra._Pos = aPos;
			_Param = _ParamDef;
		}

		public void DeactivateReq()
		{
			_State = ActiveState.deactivate_req;
		}

		public bool IsHitBullet(Bullet aVS)
		{
			return _Coll.IsHit(aVS._Coll);
		}

		public void Dmg(int aDmg)
		{
			_Param.Hp -= aDmg;
		}

		public void Fire()
		{
			if (!_Tage)
				return;


			if (_Param.FireInter == 0)
			{
				Bullet b = CharaMan.i.GetPoolOrNewBullet(_PlayerId, _Param.Bullet);
				b.ActivateReq(_Tra._Pos, _Tage._Tra._Pos - _Tra._Pos);
				_Param.FireInter = _ParamDef.FireInter;
			}

			_Param.FireInter--;
		}

		public void SearchTage()
		{
			int min = int.MaxValue;
			int dist;
			int len;
			int vs;

			_Tage = null;
			if (_PlayerId == 0)
				vs = 1;
			else
				vs = 0;

			Unit u;
			len = CharaMan.i._UnitList[vs].Count;
			for (int i = 0; i < len; i++)
			{
				u = CharaMan.i._UnitList[vs][i];
				if (u._State == ActiveState.active)
				{
					dist = RMMath.GetApproxDist(_Tra._Pos, u._Tra._Pos);
					if (dist < min)
					{
						_Tage = u;
						min = dist;
					}
				}
			}
		}


		public void OMFrameEnd()
		{
			if (_Param.Hp <= 0)
				DeactivateReq();
		}

		public override void UpdateView()
		{
			base.UpdateView();

			_CvsHp.gameObject.SetActive(true);
			_ImgHp.fillAmount = (float)_Param.Hp / _ParamDef.Hp;

			if (!_AngleSet)
			{
				if (_PlayerId == 1)
					transform.SetEulerAnglesY(180);
				_AngleSet = true;
			}

			if(_Tage)
				_TraCannon.LookAt(_Tage.transform, Vector3.up);

			_TraRot.AddEulerAnglesY(5);
		}

#if UNITY_EDITOR
		public override void EditorUpdate()
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


