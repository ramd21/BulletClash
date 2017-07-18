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

		void Awake()
		{
			_CvsHp.gameObject.SetActive(false);
		}

		public void Act()
		{
			_Param.Hp--;

			if (_Param.Hp == 0)
				CharaMan.i.UnitDeactivateReq(this);

			_Pos += Vector3.forward * _Param.Spd / 10;
		}

		public override void UpdateView()
		{
			transform.position = _Pos;

			_CvsHp.gameObject.SetActive(true);
			_ImgHp.fillAmount = (float)_Param.Hp / _ParamDef.Hp;

			if (!_AngleSet)
			{
				transform.SetEulerAnglesX(90);
				_AngleSet = true;
			}
		}

		public override void EditorUpdate()
		{
			base.EditorUpdate();

			_CvsHp = GetComponentInChildren<Canvas>();
			_ImgHp = transform.FindRecurcive<Image>("hp", true);
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 1f);
		}
	}
}


