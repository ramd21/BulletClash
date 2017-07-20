using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class Tower : Chara
	{
		public UnitParam _Param;
		public UnitParam _ParamDef;
		public Canvas _CvsHp;
		public Image _ImgHp;
		bool _AngleSet;

		public Coll _Coll;

		public Transform _TraRot;
		public Transform _TraCannon;

		Unit _Tage;

		protected override void Start()
		{
			base.Start();
			_CvsHp.gameObject.SetActive(false);
			this.WaitForEndOfFrame(()=> 
			{
				gCharaMan._TowerList[_PlayerId].Add(this);
				ActivateReq(transform.position.ToVector2IntXZ() * GameMan.cDistDiv);
			});
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
				gB = gCharaMan.GetPoolOrNewBullet(_PlayerId, _Param.Bullet);
				gB.ActivateReq(_Tra._Pos, _Tage._Tra._Pos - _Tra._Pos);
				_Param.FireInter = _ParamDef.FireInter;
			}

			_Param.FireInter--;
		}

		public void SearchTage()
		{
			int min = int.MaxValue;
			int dist;

			_Tage = null;
			if (_PlayerId == 0)
				gVs = 1;
			else
				gVs = 0;

			gLen = gCharaMan._UnitList[gVs].Count;
			for (int i = 0; i < gLen; i++)
			{
				gU = gCharaMan._UnitList[gVs][i];
				if (gU._State == ActiveState.active)
				{
					dist = RMMath.GetApproxDist(_Tra._Pos, gU._Tra._Pos);
					if (dist < min)
					{
						_Tage = gU;
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
			transform.position = _Tra._Pos.ToVector3XZ() / GameMan.cDistDiv;

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


