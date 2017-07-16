using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
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


