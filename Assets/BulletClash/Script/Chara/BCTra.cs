using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BCTra : RMBehaviour
	{
		public Vector2Int	_Pos;
		public Vector2Int	_Dir;
		public Vector2		_NormDir;

		public Vector2Int	_Move;

		public void SetMove(Vector2Int aDir, int aSpd)
		{
			_Dir = aDir;
			_NormDir = _Dir.normalized;
			_Move = _NormDir * aSpd;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
		}
#endif

	}

}


