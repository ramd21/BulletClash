using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public abstract class BaseUnit : Chara
	{
		public UnitParam _Param;
		public UnitParam _ParamDef;
		
		public Coll _Coll;

		public Canvas _CvsHp;
		protected Image _ImgHp;

		bool _AngleSet;

		public virtual void InstantiateInit(int aPlayerId)
		{
			_PlayerId = aPlayerId;
			_VSPlayerId = (_PlayerId + 1) % 2;

			_CvsHp = GetComponentInChildren<Canvas>();
			_ImgHp = transform.FindRecurcive("hp").GetComponent<Image>();

			GetComponentInChildren<HpBar>()._CamTage = BattleCameraMan.i._BattleCam;

			_CvsHp.gameObject.SetActive(false);

			BackFire[] bfArr = GetComponentsInChildren<BackFire>();
			for (int i = 0; i < bfArr.Length; i++)
			{
				bfArr[i]._CamTage = BattleCameraMan.i._BattleCam;
			}
			gameObject.SetLayer("battle");

			_Coll.InstantiateInit(_PlayerId, this);
		}

		public override void OnFrameBegin()
		{
			base.OnFrameBegin();
			_Coll.AddToCollMan();
		}

		public override void ActivateReq(Vector2Int aPos)
		{
			base.ActivateReq(aPos);
			_Param = _ParamDef;
		}

		public void Dmg(int aDmg)
		{
			_Param.Hp -= aDmg;
		}

		public override void OnFrameEnd()
		{
			if (_Param.Hp <= 0)
			{
				Explode ex = BattleCharaMan.i.GetPoolOrNewExplode();
				ex.SetPos(_Tra._Pos, 60);
				BattleCameraMan.i.ShakeCam();

				DeactivateReq();
			}
		}

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

			//_CvsHp.gameObject.SetActive(true);
			if (!_AngleSet)
			{
				if (_PlayerId == 1)
					transform.SetEulerAnglesY(180);
				_AngleSet = true;
			}
		}

#if UNITY_EDITOR
		protected virtual void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 1.5f);
		}
#endif

	}
}


