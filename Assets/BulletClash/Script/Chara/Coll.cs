﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class Coll : EditorUpdateBehaviour
	{
		static int gCnt;

		public int _Id;
		public BCTra _Tra;
		public Vector2Int _Size;
		public Vector2Int _Offset;

		public int _PlayerId;
		public int[] _CollBlock;

		int _CollBlockCur;
		int _CollBlockLast = int.MaxValue;

		public Chara _Chara;

		public int _L;
		public int _R;
		public int _T;
		public int _B;

		void Awake()
		{
			_CollBlock = new int[9];
		}

		public void InstantiateInit(int aPlayerId, Chara aChara)
		{
			_PlayerId = aPlayerId;
			_Chara = aChara;
			_Id = gCnt;
			gCnt++;
		}

		public void Deactivate()
		{
			//CollMan.i._BlockCollList[_PlayerId, (int)_Chara._Type, _CollBlockCur].Remove(_Id);
			//CollMan.i._BlockCollList[_PlayerId, (int)_Chara._Type, _CollBlockCur].Remove(this);
			//_CollBlockLast = int.MaxValue;
		}

		public void UpdatePos()
		{
			Vector2Int pos = _Tra._Pos;
			UpdateLRTB(pos);
			UpdateCollMan(pos);
		}

		void UpdateLRTB(Vector2Int aPos)
		{
			int x = aPos.x + _Offset.x;
			int y = aPos.y + _Offset.y;
			int sX = _Size.x >> 1;
			int sY = _Size.y >> 1;

			_L = x - sX;
			_R = x + sX;
			_T = y + sY;
			_B = y - sY;
		}

		void UpdateCollMan(Vector2Int aPos)
		{
			CollMan collMan = CollMan.i;

			int x, y;
			x = aPos.x + _Offset.x + collMan._ZeroPosOffset.x;
			y = aPos.y + _Offset.y + collMan._ZeroPosOffset.y;

			_CollBlockCur = (x / collMan._DivDist) % collMan._XCnt + (y / collMan._DivDist) * collMan._XCnt;
			
			if (_CollBlockCur != _CollBlockLast)
			{
				int a, b, c;

				a = _CollBlockCur - 1;
				b = _CollBlockCur;
				c = _CollBlockCur + 1;

				_CollBlock[0] = a - collMan._XCnt;
				_CollBlock[1] = b - collMan._XCnt;
				_CollBlock[2] = c - collMan._XCnt;

				_CollBlock[3] = a;
				_CollBlock[4] = b;
				_CollBlock[5] = c;

				_CollBlock[6] = a + collMan._XCnt;
				_CollBlock[7] = b + collMan._XCnt;
				_CollBlock[8] = c + collMan._XCnt;

				//if (_CollBlockLast != int.MaxValue)
				//	collMan._BlockCollList[_PlayerId, (int)_Chara._Type, _CollBlockLast].Remove(_Id);
				//collMan._BlockCollList[_PlayerId, (int)_Chara._Type, _CollBlockCur].Add(_Id, this);

				//if (_CollBlockLast != int.MaxValue)
				//	collMan._BlockCollList[_PlayerId, (int)_Chara._Type, _CollBlockLast].Remove(this);
				
			}

			collMan._BlockCollList[_PlayerId, (int)_Chara._Type, _CollBlockCur].Add(this);
			_CollBlockLast = _CollBlockCur;
		}

		public bool IsHit(Coll aVS)
		{
			if (aVS._T < _B)
				return false;

			if (_T < aVS._B)
				return false;

			if (aVS._R < _L)
				return false;

			if (_R < aVS._L)
				return false;

			return true;
		}

#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;

			if (Application.isPlaying)
			{
				Vector3 pos = (_Tra._Pos + FieldMan.i._Offset).ToVector3XZ() / GameMan.cDistDiv;
				Gizmos.DrawWireCube(pos, _Size.ToVector3XZ() / GameMan.cDistDiv);
			}
			else
			{
				Gizmos.DrawWireCube(transform.position + _Offset.ToVector3XZ() / GameMan.cDistDiv, _Size.ToVector3XZ() / GameMan.cDistDiv);
			}
		}

		public override void EditorUpdate()
		{
			_Tra = GetComponent<BCTra>();
		}
#endif

	}

}

