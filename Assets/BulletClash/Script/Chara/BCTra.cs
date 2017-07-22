using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BCTra : RMBehaviour
	{
		public Vector2Int	_Pos;
		Vector2		_Dir;
		public Vector2		_DirNorm;


		public void SetDir(Vector2 aDir)
		{
			_Dir = aDir;
			_DirNorm = _Dir.normalized;
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


