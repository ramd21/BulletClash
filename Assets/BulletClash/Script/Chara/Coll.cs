using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class Coll : EditorUpdateBehaviour
	{
		public BCTra _Tra;
		public Vector2Int _Size;
		public Vector2Int _Offset;

		public int _PlayerId;
		public int[] _CollBlock;

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
		}


		public void UpdatePos()
		{
			CollMan collMan = CollMan.i;

			_L = _Tra._Pos.x - _Size.x / 2 + _Offset.x;
			_R = _Tra._Pos.x + _Size.x / 2 + _Offset.x;
			_T = _Tra._Pos.y + _Size.y / 2 + _Offset.y;
			_B = _Tra._Pos.y - _Size.y / 2 + _Offset.y;


			int x, y;
			x = _Tra._Pos.x + _Offset.x;
			y = _Tra._Pos.y + _Offset.y;

			int block = (x / collMan._DivDist) % collMan._XCnt + (y / collMan._DivDist) * collMan._XCnt;

			bool isR = block % collMan._XCnt == collMan._XCnt - 1;
			bool isL = block % collMan._XCnt == 0;

			int val;

			val = block - collMan._XCnt - 1; 			_CollBlock[0] =	isL ? -1 : val >= collMan._BlockCnt ? -1 : val;
			val = block - collMan._XCnt;	 			_CollBlock[1] = val >= collMan._BlockCnt ? -1 : val;
			val = block - collMan._XCnt + 1;	 		_CollBlock[2] = isR ? -1 : val >= collMan._BlockCnt ? -1 : val;

			val = block - 1; 							_CollBlock[3] = isL ? -1 : val >= collMan._BlockCnt ? -1 : val;
			val = block;	 							_CollBlock[4] = val >= collMan._BlockCnt ? -1 : val;
			val = block + 1;	 						_CollBlock[5] = isR ? -1 : val >= collMan._BlockCnt ? -1 : val;

			val = block + collMan._XCnt - 1; 			_CollBlock[6] = isL ? -1 : val >= collMan._BlockCnt ? -1 : val;
			val = block + collMan._XCnt;	 			_CollBlock[7] = val >= collMan._BlockCnt ? -1 : val;
			val = block + collMan._XCnt + 1;	 		_CollBlock[8] = isR ? -1 : val >= collMan._BlockCnt ? -1 : val;

			collMan._BlockCollList[_PlayerId, (int)_Chara._Type, block].Add(this);
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

