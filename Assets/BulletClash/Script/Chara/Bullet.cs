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
		public Coll _Coll;

		public void ActivateReq(Vector2Int aPos, Vector2Int aDir)
		{
			gameObject.SetActive(false);
			_State = ActiveState.activate_req;
			_Tra._Pos = aPos;
			_Tra._Dir = aDir;
			_Param = _ParamDef;
		}

		public void DeactivateReq()
		{
			_State = ActiveState.deactivate_req;
		}

		public void Move()
		{
			_Tra._Pos += Vector2Int.RoundToInt(_Tra._Dir.normalized * _Param.Spd);
			UpdateCollReq();
		}

		protected void UpdateCollReq()
		{
			_Coll._Update = true;
		}

		public void HitBulletCheck()
		{
			int len;
			int vs;

			Bullet b;
			if (_PlayerId == 0)
				vs = 1;
			else
				vs = 0;

			len = CharaMan.i._BulletList[vs].Count;
			for (int i = 0; i < len; i++)
			{
				b = CharaMan.i._BulletList[vs][i];
				if (b._State == ActiveState.active)
				{
					if (b.IsHitBullet(this))
					{
						b.DeactivateReq();
						DeactivateReq();
					}
				}
			}
		}

		public void HitUnitCheck()
		{
			int len;
			int vs;

			Unit u;
			if (_PlayerId == 0)
				vs = 1;
			else
				vs = 0;

			len = CharaMan.i._UnitList[vs].Count;
			for (int i = 0; i < len; i++)
			{
				u = CharaMan.i._UnitList[vs][i];
				if (u._State == ActiveState.active)
				{
					if (u.IsHitBullet(this))
					{
						u.Dmg(1);
						DeactivateReq();
					}
				}
			}
		}

		public bool IsHitBullet(Bullet aVS)
		{
			return _Coll.IsHit(aVS._Coll);
		}

		public void DecTimer()
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
			transform.LookAt(transform.position + _Tra._Dir.ToVector3XZ(), Vector3.up);
		}

#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			_Coll = GetComponent<Coll>();
		}
#endif
	}
}


