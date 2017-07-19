using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class Link2DTo3D : EditorUpdateBehaviour
	{
		public Camera _Cam;
		public Transform _Tage;

		public float _BaseZ;

#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			Vector3 v3 = transform.position;

			v3.z = _BaseZ;

			_Tage.position = _Cam.ScreenToWorldPoint(v3);
		}
#endif

	}
}



