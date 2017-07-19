using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class Coll : RMBehaviour
	{
		public Vector2 _Size;
		public Vector2 _Offset;

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(transform.position + _Offset.ToVector3XZ(), _Size.ToVector3XZ());
		}
#endif

	}

}

