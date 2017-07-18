using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using System;

namespace BC
{
	public class Bullet : Chara
	{
		public BulletParam _Param;
		public BulletParam _ParamDef;
		bool _AngleSet;

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
			_Param.Timer--;
			if (_Param.Timer == 0)
				DeactivateReq();

			if (_PlayerId == 0)
				_Pos += Vector3.forward * _Param.Spd / 10;
			else
				_Pos -= Vector3.forward * _Param.Spd / 10;
		}

		public override void UpdateView()
		{
			transform.position = _Pos;

			if (!_AngleSet)
			{
				if (_PlayerId == 1)
					transform.SetEulerAnglesY(180);
				_AngleSet = true;
			}
			
		}
	}
}


