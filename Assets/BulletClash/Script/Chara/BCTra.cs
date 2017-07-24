using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BCTra : RMBehaviour
	{
		public Vector2Int		_Pos;
		public Vector2			_Dir;
		public Vector2Int		_Move;

		public void SetDir(Vector2Int aDir, int aSpd)
		{
			_Dir = aDir;
			_Dir = _Dir.normalized;
			_Move = Vector2Int.RoundToInt(_Dir * aSpd);
		}

		//public Vector2 _dirNorm
		//{
		//	get
		//	{
		//		if(_Update)

		//	}
		//}



#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
		}
#endif

	}

}


