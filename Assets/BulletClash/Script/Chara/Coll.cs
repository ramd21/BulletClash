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

		int _L;
		int _R;
		int _T;
		int _B;

		public bool _Update;

		public int _l
		{
			get
			{
				if (_Update)
					UpdateLRTB();
				return _L;
			}
		}
		public int _r
		{
			get
			{
				if (_Update)
					UpdateLRTB();
				return _R;
			}
		}
		public int _t
		{
			get
			{
				if (_Update)
					UpdateLRTB();
				return _T;
			}
		}
		public int _b
		{
			get
			{
				if (_Update)
					UpdateLRTB();
				return _B;
			}
		}

		void UpdateLRTB()
		{
			_L = _Tra._Pos.x - _Size.x / 2 + _Offset.x;
			_R = _Tra._Pos.x + _Size.x / 2 + _Offset.x;
			_T = _Tra._Pos.y + _Size.y / 2 + _Offset.y;
			_B = _Tra._Pos.y - _Size.y / 2 + _Offset.y;
			_Update = false;
		}

		public bool IsHit(Coll aVS)
		{
			if (aVS._R < _L)
				return false;

			if (_R < aVS._L)
				return false;

			if (aVS._T < _B)
				return false;

			if (_T < aVS._B)
				return false;

			return true;
		}



#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(transform.position + _Offset.ToVector3XZ() / GameMan.i._DistDiv, _Size.ToVector3XZ() / GameMan.i._DistDiv);
		}

		public override void EditorUpdate()
		{
			_Tra = GetComponent<BCTra>();
		}
#endif

	}

}

