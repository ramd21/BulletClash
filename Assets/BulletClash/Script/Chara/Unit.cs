using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class Unit : UnitBase, IEditorUpdate
	{
		static int gCnt;
		
		Chara _Tage;
		int _TageDist;

		Vector2Int _Force;
		Vector2Int _Correct;


		public Cannon _Cannon;

		


		public void InstantiateInit(int aPlayerId, UnitType aType)
		{
			base.InstantiateInit(aPlayerId);
			_Id = gCnt;
			gCnt++;
			_Type = CharaType.unit;
			_ParamDef = MasterMan.i._UnitParam[(int)aType];

			if (aPlayerId == 1)
				_Coll._Flip = -1;
			else
				_Coll._Flip = 1;
		}

		public override void OnFrameBegin()
		{
			if (_Frame < 60)
				return;

			base.OnFrameBegin();
			_Coll.UpdateBlock();
		}

		public override void ActivateReq(Vector2Int aPos)
		{
			base.ActivateReq(aPos);
		}

		public void SetPos()
		{
			if (_Frame < 60)
				return;


			if (_PlayerId == 0)
				_Tra._Pos.y += _Param.SpdY;
			else
				_Tra._Pos.y -= _Param.SpdY;


			_Tra._Pos += _Correct;
			_Correct = Vector2Int.zero;

			_Tra._Pos += _Force;

			if (_Tage)
			{
				if (_Tage._Tra._Pos.x > _Tra._Pos.x)
				{
					_Tra._Pos.x += _Param.SpdX;
					if (_Tra._Pos.x > _Tage._Tra._Pos.x)
						_Tra._Pos.x = _Tage._Tra._Pos.x;
				}
				else
				{
					if (_Tage._Tra._Pos.x < _Tra._Pos.x)
					{
						_Tra._Pos.x -= _Param.SpdX;
						if (_Tra._Pos.x < _Tage._Tra._Pos.x)
							_Tra._Pos.x = _Tage._Tra._Pos.x;
					}
				}
			}

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
		}

		public void HitCheck()
		{
			if (_Frame < 60)
				return;

			FastList<Coll> collList;
			Coll c;
			int len;
			for (int i = 0; i < 9; i++)
			{
				collList = BattleCollMan.i.GetCollList(_VSPlayerId, CharaType.unit, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						AddForce(_Tra._Pos - c._Tra._Pos, 5);
					}
					//hit = _Coll.GetOverLap(c);
					//if (hit != Vector2Int.zero)
					//{
					//	if (hit.x > hit.y)
					//	{
					//		_Correct.y -= (hit.y / 2);
					//	}
					//	else
					//	{
					//		_Correct.x -= (hit.x / 2);
					//	}


					//	AddForce(_Tra._Pos - c._Tra._Pos, 3);
					//}
				}

				collList = BattleCollMan.i.GetCollList(_PlayerId, CharaType.unit, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						AddForce(_Tra._Pos - c._Tra._Pos, 5);
					}
					//hit = _Coll.GetOverLap(c);
					//if (hit != Vector2Int.zero)
					//{
					//	if (hit.x > hit.y)
					//	{
					//		_Correct.y -= (hit.y / 2);
					//	}
					//	else
					//	{
					//		_Correct.x -= (hit.x / 2);
					//	}
					//	AddForce(_Tra._Pos - c._Tra._Pos, 3);
					//}
				}

				collList = BattleCollMan.i.GetCollList(_VSPlayerId, CharaType.tower, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						AddForce(_Tra._Pos - c._Tra._Pos, 5);
					}

					//hit = _Coll.GetOverLap(c);
					//if (hit != Vector2Int.zero)
					//{
					//	if (hit.x > hit.y)
					//	{
					//		_Correct.y -= (hit.y / 2);
					//	}
					//	else
					//	{
					//		_Correct.x -= (hit.x / 2);
					//	}
					//	AddForce(_Tra._Pos - c._Tra._Pos, 3);
					//}
				}

				collList = BattleCollMan.i.GetCollList(_PlayerId, CharaType.tower, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
					{
						AddForce(_Tra._Pos - c._Tra._Pos, 5);
					}
					//hit = _Coll.GetOverLap(c);
					//if (hit != Vector2Int.zero)
					//{
					//	if (hit.x > hit.y)
					//	{
					//		_Correct.y -= (hit.y / 2);
					//	}
					//	else
					//	{
					//		_Correct.x -= (hit.x / 2);
					//	}
					//	AddForce(_Tra._Pos - c._Tra._Pos, 3);
					//}
				}
			}

			if (_Tra._Pos.x > BattleFieldMan.i._Size.x)
			{
				_Tra._Pos.x = BattleFieldMan.i._Size.x;
				AddForce(Vector2Int.left, 5);
			}

			if (_Tra._Pos.x < 0)
			{
				_Tra._Pos.x = 0;
				AddForce(Vector2Int.right, 5);
			}
		}

		public void AddForce(Vector2Int aDir, int aPow)
		{
			int max = 15;
			_Force += (aDir.normalized * aPow);
			if (_Force.x > max)
				_Force.x = max;

			if (_Force.x < -max)
				_Force.x = -max;

			if (_Force.y > max)
				_Force.y = max;

			if (_Force.y < -max)
				_Force.y = -max;
		}

		public void SearchTage()
		{
			if (_Frame < 60)
				return;

			_TageDist = int.MaxValue;
			int dist;
			int len;
			_Tage = null;

			Unit u;
			len = BattleCharaMan.i._UnitList[_VSPlayerId].Count;
			for (int i = 0; i < len; i++)
			{
				u = BattleCharaMan.i._UnitList[_VSPlayerId][i];
				if (u._State == ActiveState.active && u._Frame >= 60)
				{
					if (_PlayerId == 0)
					{
						if (u._Tra._Pos.y <= _Tra._Pos.y)
							continue;
					}
					else
					{
						if (u._Tra._Pos.y >= _Tra._Pos.y)
							continue;
					}

					dist = RMMath.GetApproxDist(_Tra._Pos.x, _Tra._Pos.y, u._Tra._Pos.x, u._Tra._Pos.y);
					if (dist < _TageDist)
					{
						_Tage = u;
						_TageDist = dist;
					}
				}
			}

			Tower tw;
			len = BattleCharaMan.i._TowerList[_VSPlayerId].Count;
			for (int i = 0; i < len; i++)
			{
				tw = BattleCharaMan.i._TowerList[_VSPlayerId][i];
				if (tw._State == ActiveState.active)
				{
					if (_PlayerId == 0)
					{
						if (tw._Tra._Pos.y <= _Tra._Pos.y)
							continue;
					}
					else
					{
						if (tw._Tra._Pos.y >= _Tra._Pos.y)
							continue;
					}

					dist = RMMath.GetApproxDist(_Tra._Pos.x, _Tra._Pos.y, tw._Tra._Pos.x, tw._Tra._Pos.y);
					if (dist < _TageDist)
					{
						_Tage = tw;
						_TageDist = dist;
					}
				}
			}
		}

		public void Fire()
		{
			if (_Frame < 60)
				return;

			if (!_Tage)
				return;

			//if (_TageDist > _Param.Range)
			//	return;

			_Cannon.Fire();
		}

		public override void OnFrameEnd()
		{
			base.OnFrameEnd();
			_Force.x = (_Force.x * 975) / 1000;
			_Force.y = (_Force.y * 975) / 1000;
		}

		public override void UpdateView()
		{
			base.UpdateView();
		}

#if UNITY_EDITOR
		public void EditorUpdate()
		{
			_Coll = GetComponent<Coll>();
			_Tra = GetComponent<BCTra>();
		}
#endif

	}
}


