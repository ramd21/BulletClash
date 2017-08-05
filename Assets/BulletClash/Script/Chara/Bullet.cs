using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using System;

	

namespace BC
{
	public class Bullet : Chara, IEditorUpdate
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

			LayerSet[] lsArr = GetComponentsInChildren<LayerSet>(true);
			for (int i = 0; i < lsArr.Length; i++)
			{
				lsArr[i].Set(false);
			}
		}

		public override void OnFrameBegin()
		{
			base.OnFrameBegin();
			_Coll.UpdateBlock();
			_Coll.AddToCollMan();
		}

		public void ActivateReq(Vector2Int aPos, Vector2Int aDir)
		{
			base.ActivateReq(aPos);
			_Param = _ParamDef;
			_Tra.SetMove(aDir, _Param.Spd);
		}

		public void SetPos()
		{
			//if (_PlayerId == 0)
			//	_Tra._Pos.y += _Param.Spd;
			//else
			//	_Tra._Pos.y -= _Param.Spd;

			_Tra._Pos += _Tra._Move;


			if (_Tra._Pos.y > BattleFieldMan.i._Size.y)
			{
				DeactivateReq();
				return;
			}

			if (_Tra._Pos.y < 0)
			{
				DeactivateReq();
				return;
			}

			if (_Tra._Pos.x > BattleFieldMan.i._Size.x)
			{
				DeactivateReq();
				return;
			}

			if (_Tra._Pos.x < 0)
			{
				DeactivateReq();
				return;
			}
			
		}

		public void HitCheck()
		{
			FastList<Coll> collList;
			Coll c;
			Unit u;
			int len;
			for (int i = 0; i < 9; i++)
			{
				collList = BattleCollMan.i.GetCollList(_VSPlayerId, CharaType.bullet, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						DeactivateReq();
						(c._Chara as Bullet).DeactivateReq();
						BulletHit bh = BattleCharaMan.i.GetPoolOrNewBulletHit();
						bh.SetPos(_Tra._Pos + new Vector2Int((c._Tra._Pos.x - _Tra._Pos.x) / 2, (c._Tra._Pos.y - _Tra._Pos.y) / 2), 10);
					}
				}
			}

			for (int i = 0; i < 9; i++)
			{
				collList = BattleCollMan.i.GetCollList(_VSPlayerId, CharaType.unit, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						DeactivateReq();

						u = c._Chara as Unit;

						u.Dmg(1);

						u.AddForce(_Tra._Move, 5);

						BulletHit bh = BattleCharaMan.i.GetPoolOrNewBulletHit();
						bh.SetPos(_Tra._Pos + new Vector2Int((c._Tra._Pos.x - _Tra._Pos.x) / 2, (c._Tra._Pos.y - _Tra._Pos.y) / 2), 10);
					}
				}
			}

			for (int i = 0; i < 9; i++)
			{
				collList = BattleCollMan.i.GetCollList(_VSPlayerId, CharaType.tower, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						DeactivateReq();
						(c._Chara as Tower).Dmg(1);

						BulletHit bh = BattleCharaMan.i.GetPoolOrNewBulletHit();
						bh.SetPos(_Tra._Pos + new Vector2Int((c._Tra._Pos.x - _Tra._Pos.x) / 2, (c._Tra._Pos.y - _Tra._Pos.y) / 2), 10);
					}
				}
			}
		}

		public void DecTimer()
		{
			_Param.Timer--;
		}

		public override void OnFrameEnd()
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
		public void EditorUpdate()
		{
			_Coll = GetComponent<Coll>();
		}
#endif
	}
}


