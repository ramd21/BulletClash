using System.Collections;
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

		public CollInfo[] _CollInfoArr;

		public Bounds2DInt _Bounds2D;

		public int _Flip;

		[Serializable]
		public struct CollInfo
		{
			public Vector2Int _Size;
			public Vector2Int _Offset;

			[System.NonSerialized]
			public int _L;
			[System.NonSerialized]
			public int _R;
			[System.NonSerialized]
			public int _T;
			[System.NonSerialized]
			public int _B;

			public bool IsHit(CollInfo aVS)
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


			public void UpdateLRTB(Vector2Int aPos)
			{
				int x = aPos.x + _Offset.x;
				int y = aPos.y + _Offset.y;
				int sX = _Size.x / 2;
				int sY = _Size.y / 2;

				_L = x - sX;
				_R = x + sX;
				_T = y + sY;
				_B = y - sY;
			}
		}

		public int _PlayerId;
		public int[] _CollBlock;

		int _CollBlockCur;
		int _CollBlockLast = int.MaxValue;

		public Chara _Chara;

		bool _UpdateLRTB;

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

		public void SetBounds(Vector2 aPos)
		{
			if (_CollInfoArr.Length == 1)
			{
				_Bounds2D.size = _CollInfoArr[0]._Size;
				_Bounds2D.center = _CollInfoArr[0]._Offset;
			}
			else
			{
				int minX = int.MaxValue;
				int maxX = int.MinValue;
				int minY = int.MaxValue;
				int maxY = int.MinValue;

				for (int i = 0; i < _CollInfoArr.Length; i++)
				{
					int l = -_CollInfoArr[i]._Size.x / 2 + _CollInfoArr[i]._Offset.x * _Flip;
					int r = _CollInfoArr[i]._Size.x / 2 + _CollInfoArr[i]._Offset.x * _Flip;
					int t = _CollInfoArr[i]._Size.y / 2 + _CollInfoArr[i]._Offset.y * _Flip;
					int b = -_CollInfoArr[i]._Size.y / 2 + _CollInfoArr[i]._Offset.y * _Flip;

					if (minX > l)
						minX = l;

					if (maxX < r)
						maxX = r;

					if (maxY < t)
						maxY = t;

					if (minY > b)
						minY = b;
				}

				_Bounds2D.size = new Vector2Int(maxX - minX, maxY - minY);
				_Bounds2D.center = new Vector2Int(minX + _Bounds2D.size.x / 2, minY + _Bounds2D.size.y / 2);
			}
		}

		void UpdateLRTB()
		{
			if (_UpdateLRTB)
			{
				for (int i = 0; i < _CollInfoArr.Length; i++)
					_CollInfoArr[i].UpdateLRTB(_Tra._Pos);

				_UpdateLRTB = false;
			}
		}

		public bool IsHit(Coll aVS)
		{
			if (_Id == aVS._Id)
				return false;

			UpdateLRTB();
			aVS.UpdateLRTB();

			for (int i = 0; i < _CollInfoArr.Length; i++)
			{
				for (int j = 0; j < aVS._CollInfoArr.Length; j++)
				{
					if (_CollInfoArr[i].IsHit(aVS._CollInfoArr[j]))
						return true;
				}
			}
			return false;
		}

		public void UpdateBlock()
		{
			_UpdateLRTB = true;


			SetBounds(_Tra._Pos);
			int x = _Tra._Pos.x + _Bounds2D.center.x + BattleCollMan.i._Offset.x;
			int y = _Tra._Pos.y + _Bounds2D.center.y + BattleCollMan.i._Offset.y;

			BattleCollMan collMan = BattleCollMan.i;


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
			}
			_CollBlockLast = _CollBlockCur;
		}

		public void AddToCollMan()
		{
			BattleCollMan.i.AddColl(this, _PlayerId, _Chara._Type, _CollBlockCur);
		}


#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;

			if (Application.isPlaying)
			{
				for (int i = 0; i < _CollInfoArr.Length; i++)
				{
					Vector3 pos;
					pos = (_Tra._Pos + BattleFieldMan.i._Offset + _CollInfoArr[i]._Offset * _Flip).ToVector3XZ();
					Gizmos.DrawWireCube(pos / BattleGameMan.cDistDiv, _CollInfoArr[i]._Size.ToVector3XZ() / BattleGameMan.cDistDiv);
				}

				Gizmos.color = Color.green;
				Gizmos.DrawWireCube((_Tra._Pos + BattleFieldMan.i._Offset + _Bounds2D.center).ToVector3XZ() / BattleGameMan.cDistDiv, _Bounds2D.size.ToVector3XZ() / BattleGameMan.cDistDiv);
			}
			else
			{
				for (int i = 0; i < _CollInfoArr.Length; i++)
				{
					Gizmos.DrawWireCube(transform.position - _CollInfoArr[i]._Offset.ToVector3XZ() * _Flip / BattleGameMan.cDistDiv, _CollInfoArr[i]._Size.ToVector3XZ() / BattleGameMan.cDistDiv);
				}

				Gizmos.color = Color.green;

				SetBounds(Vector2.zero);
				Gizmos.DrawWireCube(transform.position + _Bounds2D.center.ToVector3XZ() / BattleGameMan.cDistDiv, _Bounds2D.size.ToVector3XZ() / BattleGameMan.cDistDiv);
			}
		}

		public override void EditorUpdate()
		{
			_Tra = GetComponent<BCTra>();
		}
#endif

	}

}

