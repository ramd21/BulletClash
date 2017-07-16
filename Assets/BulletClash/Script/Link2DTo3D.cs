using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class Link2DTo3D : EditorUpdateBehaviour
	{
		public Camera _Cam;
		public Transform _Tage;

		public float _BaseZ;

		public override void EditorUpdate()
		{
			Vector3 v3 = transform.position;

			v3.z = _BaseZ;

			_Tage.position = _Cam.ScreenToWorldPoint(v3);
		}
	}
}


