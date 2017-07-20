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
			if (_PlayerId == 0)
				gVs = 1;
			else
				gVs = 0;

			gBulletList = gCharaMan._BulletList[gVs];

			gLen = gBulletList.Count;
			for (int i = 0; i < gLen; i++)
			{
				gB = gBulletList[i];
				if (gB._State == ActiveState.active)
				{
					if (gB.IsHitBullet(this))
					{
						gB.DeactivateReq();
						DeactivateReq();
					}
				}
			}
		}

		public void HitUnitCheck()
		{
			if (_PlayerId == 0)
				gVs = 1;
			else
				gVs = 0;

			gUnitList = gCharaMan._UnitList[gVs];

			gLen = gUnitList.Count;
			for (int i = 0; i < gLen; i++)
			{
				gU = gUnitList[i];
				if (gU._State == ActiveState.active)
				{
					if (gU.IsHitBullet(this))
					{
						gU.Dmg(1);
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
			transform.position = _Tra._Pos.ToVector3XZ() / GameMan.cDistDiv;
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


