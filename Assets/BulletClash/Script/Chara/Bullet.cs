﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using System;

namespace BC
{
	public class Bullet : Chara
	{
		static int gCnt;
		public BulletParam _Param;
		public BulletParam _ParamDef;
		public Coll _Coll;

		public void InstantiateInit(int aPlayerId, BulletType aType)
		{
			_Id = gCnt;
			gCnt++;

			_PlayerId = aPlayerId;
			_VSPlayerId = (_PlayerId + 1) % 2;
			_Type = CharaType.bullet;
			_ParamDef = MasterMan.i._BulletParam[(int)aType];
			_Coll.InstantiateInit(_PlayerId, this);
		}

		public void ActivateReq(Vector2Int aPos, Vector2Int aDir)
		{
			gameObject.SetActive(false);
			_State = ActiveState.activate_req;
			_Tra._Pos = aPos;
			_Tra.SetDir(aDir);
			_Param = _ParamDef;
		}

		public void DeactivateReq()
		{
			_State = ActiveState.deactivate_req;
		}

		public void SetPos()
		{
			_Tra._Pos += Vector2Int.RoundToInt(_Tra._DirNorm * _Param.Spd);
			_Coll.UpdatePos();
		}

		public void HitBulletCheck()
		{
			List<Coll> collList;
			int len;
			Coll c;
			for (int i = 0; i < 9; i++)
			{
				if (_Coll._CollBlock[i] < 0)
					continue;

				collList = CollMan.i._BlockCollList[_VSPlayerId, (int)CharaType.bullet, _Coll._CollBlock[i]];
				len = collList.Count;

				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						DeactivateReq();
						(c._Chara as Bullet).DeactivateReq();
					}
				}
			}
		}

		public void HitUnitCheck()
		{
			List<Coll> collList;
			int len;
			Coll c;
			for (int i = 0; i < 9; i++)
			{
				if (_Coll._CollBlock[i] < 0)
					continue;

				collList = CollMan.i._BlockCollList[_VSPlayerId, (int)CharaType.unit, _Coll._CollBlock[i]];
				len = collList.Count;

				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						DeactivateReq();
						(c._Chara as Unit).DeactivateReq();
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
			base.UpdateView();
			transform.LookAt(transform.position + _Tra._DirNorm.ToVector3XZ(), Vector3.up);
		}

#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			_Coll = GetComponent<Coll>();
		}
#endif
	}
}


