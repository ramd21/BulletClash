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
			_Param = _ParamDef;
			_Tra.SetDir(aDir, _Param.Spd);
		}

		public override void DeactivateReq()
		{
			_State = ActiveState.deactivate_req;
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();
			_Coll.Deactivate();
		}

		public void SetPos()
		{
			_Tra._Pos += _Tra._Move;

			if (_Tra._Pos.y > FieldMan.i._Size.y)
			{
				DeactivateReq();
				return;
			}

			if (_Tra._Pos.y < 0)
			{
				DeactivateReq();
				return;
			}

			if (_Tra._Pos.x > FieldMan.i._Size.x)
			{
				DeactivateReq();
				return;
			}

			if (_Tra._Pos.x < 0)
			{
				DeactivateReq();
				return;
			}
			_Coll.UpdatePos();
		}

		public void HitBulletCheck()
		{
			FastList<Coll> collList;
			Coll c;
			int len;
			for (int i = 0; i < 9; i++)
			{
				collList = CollMan.i.GetCollList(_VSPlayerId, CharaType.bullet, _Coll._CollBlock[i]);
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
			FastList<Coll> collList;
			Coll c;
			int len;
			for (int i = 0; i < 9; i++)
			{
				collList = CollMan.i.GetCollList(_VSPlayerId, CharaType.unit, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						DeactivateReq();
						(c._Chara as Unit).Dmg(1);
					}
				}
			}
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


