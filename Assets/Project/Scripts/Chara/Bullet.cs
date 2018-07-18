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
		public BulletParam _ParamDef;
		public BulletParam _Param;
		public Coll _Coll;

		public struct FrameData
		{
			public ActiveState _State;
			public int _Id;
			public int _Timer;
			public Vector2Int	_Pos;
			public Vector2Int	_Dir;
			public Vector2Int	_Move;
		}

		public FrameData GetFrameData()
		{
			FrameData fd = new FrameData();
			fd._State = _State;
			fd._Id		= _Id;
			fd._Timer	= _Param.Timer;
			fd._Pos		= _Tra._Pos;
			fd._Dir		= _Tra._Dir;
			fd._Move	= _Tra._Move;
			return fd;
		}

		public void Restore(FrameData aFrameData)
		{
			_State				= aFrameData._State;
			_Id					= aFrameData._Id;
			_Param.Timer		= aFrameData._Timer;
			_Tra._Pos			= aFrameData._Pos;	
			_Tra._Dir			= aFrameData._Dir;
			_Tra._Move			= aFrameData._Move;

			switch (_State)
			{
				case ActiveState.active:
					gameObject.SetActive(true);
					break;
				case ActiveState.deactivate_req:
					gameObject.SetActive(true);
					break;
			}
		}


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
		public override void EditorUpdate()
		{
			_Coll = GetComponent<Coll>();
		}
#endif
	}
}


