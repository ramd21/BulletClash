using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class Unit : Chara
	{
		public UnitParam _Param;
		public UnitParam _ParamDef;
		public Canvas _CvsHp;
		public Image _ImgHp;
		bool _AngleSet;

		public Coll[] _CollArr;

		public bat.opt.Bake.BAT_DeepBaker _Bat;


		void Start()
		{
			_CvsHp.gameObject.SetActive(false);
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

		public void Move()
		{
			if (_PlayerId == 0)
				_Tra._Pos.y += _Param.Spd;
			else
				_Tra._Pos.y -= _Param.Spd;

			UpdateCollReq();
		}

		protected void UpdateCollReq()
		{
			int len;
			len = _CollArr.Length;
			for (int i = 0; i < len; i++)
			{
				_CollArr[i]._Update = true;
			}
		}

		public bool IsHitBullet(Bullet aVS)
		{
			int len;
			len = _CollArr.Length;
			for (int i = 0; i < len; i++)
			{
				if (_CollArr[i].IsHit(aVS._Coll))
					return true;
			}

			return false;
		}

		public void Dmg(int aDmg)
		{
			_Param.Hp -= aDmg;
		}

		public void Fire()
		{
			if (_Param.FireInter == 0)
			{
				Bullet b = CharaMan.i.GetPoolOrNewBullet(_PlayerId, _Param.Bullet);
				if (_PlayerId == 0)
					b.ActivateReq(_Tra._Pos, Vector2Int.up);
				else
					b.ActivateReq(_Tra._Pos, Vector2Int.down);
				_Param.FireInter = _ParamDef.FireInter;
			}

			_Param.FireInter--;
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
		}

#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			_CvsHp = GetComponentInChildren<Canvas>();
			_ImgHp = transform.FindRecurcive<Image>("hp", true);
			_Bat = GetComponentInChildren<bat.opt.Bake.BAT_DeepBaker>();

			_CollArr = GetComponents<Coll>();
			_Tra = GetComponent<BCTra>();
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 1.5f);
		}
#endif

	}
}


