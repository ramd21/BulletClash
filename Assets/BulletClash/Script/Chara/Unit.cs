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

		public bat.opt.Bake.BAT_DeepBaker _Bat;

		void Awake()
		{
			//DestroyImmediate(_Bat);
		}

		void Start()
		{
			_CvsHp.gameObject.SetActive(false);
		}

		public void ActivateReq(Vector3 aPos)
		{
			gameObject.SetActive(false);
			_State = ActiveState.activate_req;
			_Pos = aPos;
			_Param = _ParamDef;
		}

		public void DeactivateReq()
		{
			_State = ActiveState.deactivate_req;
		}

		public void Act()
		{
			_Param.Hp--;
			if (_Param.Hp == 0)
				DeactivateReq();

			if (_PlayerId == 0)
				_Pos += Vector3.forward * _Param.Spd / 10;
			else
				_Pos -= Vector3.forward * _Param.Spd / 10;

			if (_Param.FireInter == 0)
			{
				Bullet bullet = CharaMan.i.GetPoolOrNewBullet(_PlayerId, _Param.Bullet);
				bullet.ActivateReq(_Pos);
				_Param.FireInter = _ParamDef.FireInter;
			}

			_Param.FireInter--;
		}

		public override void UpdateView()
		{
			transform.position = _Pos;

			_CvsHp.gameObject.SetActive(true);
			_ImgHp.fillAmount = (float)_Param.Hp / _ParamDef.Hp;

			if (!_AngleSet)
			{
				transform.SetEulerAnglesX(90);
				if (_PlayerId == 1)
					transform.SetEulerAnglesY(180);
				_AngleSet = true;
			}
		}

#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			base.EditorUpdate();

			_CvsHp = GetComponentInChildren<Canvas>();
			_ImgHp = transform.FindRecurcive<Image>("hp", true);
			_Bat = GetComponentInChildren<bat.opt.Bake.BAT_DeepBaker>();
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 1f);
		}
#endif

	}
}


