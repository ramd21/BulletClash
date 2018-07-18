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
		public Vector2Int	_Move;

		public void SetMove(Vector2Int aDir, int aSpd)
		{
			_Dir = aDir;
			_Move = (aDir.ToNrmalizedVector2() * aSpd).ToVector2Int();
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
		}
#endif

	}

}


