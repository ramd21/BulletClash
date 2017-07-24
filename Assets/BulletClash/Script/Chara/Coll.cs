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
			Vector2Int pos = _Tra._Pos;

			_L = pos.x - _Size.x / 2 + _Offset.x;
			_R = pos.x + _Size.x / 2 + _Offset.x;
			_T = pos.y + _Size.y / 2 + _Offset.y;
			_B = pos.y - _Size.y / 2 + _Offset.y;


			int x, y;
			x = pos.x + _Offset.x;
			y = pos.y + _Offset.y;

			int block = (x / collMan._DivDist) % collMan._XCnt + (y / collMan._DivDist) * collMan._XCnt;

			_CollBlock[0] = block - collMan._XCnt - 1;
			_CollBlock[1] = block - collMan._XCnt;
			_CollBlock[2] = block - collMan._XCnt + 1;

			_CollBlock[3] = block - 1;
			_CollBlock[4] = block;	
			_CollBlock[5] = block + 1;

			_CollBlock[6] = block + collMan._XCnt - 1;	
			_CollBlock[7] = block + collMan._XCnt;		
			_CollBlock[8] = block + collMan._XCnt + 1;	

			collMan._BlockCollList[_PlayerId, (int)_Chara._Type, block].Add(this);
		}

		public bool IsHit(Coll aVS)
		{
			//int dist = RMMath.GetApproxDist(aVS._Tra._Pos, _Tra._Pos);
			//if (dist < 200)
			//	return true;
			//else
			//	return false;


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

