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

		public void HitCheck()
		{
			int len;
			Bullet b;
			if (_PlayerId == 0)
			{
				len = CharaMan.i._BulletList[1].Count;
				for (int i = 0; i < len; i++)
				{
					b = CharaMan.i._BulletList[1][i];
					if (b.IsHitBullet(this))
					{
					}
				}
			}
			else
			{
				len = CharaMan.i._BulletList[0].Count;
				for (int i = 0; i < len; i++)
				{
					b = CharaMan.i._BulletList[0][i];
					if (b.IsHitBullet(this))
					{
					}
				}
			}
		}

		protected bool IsHitBullet(Bullet aVS)
		{
			return _CollArr[0].IsHit(aVS._CollArr[0]);
		}



		public void Act()
		{
			_Param.Timer--;
		}

		public void OMFrameEnd()
		{
			if (_Param.Timer <= 0)
				DeactivateReq();
		}


		public override void UpdateView()
		{
			transform.position = _Tra._Pos.ToVector3XZ() / GameMan.i._DistDiv;

			if (!_AngleSet)
			{
				if (_PlayerId == 1)
					transform.SetEulerAnglesY(180);
				_AngleSet = true;
			}
			
		}
	}
}


