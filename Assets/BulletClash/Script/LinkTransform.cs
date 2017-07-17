using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class LinkTransform : EditorUpdateBehaviour
	{
		public Transform _Tage;

		public override void EditorUpdate()
		{
			transform.position = _Tage.position;
		}
	}
}




