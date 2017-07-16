using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class PointLightScaler : EditorUpdateBehaviour
	{
		public float _Range;

		public override void EditorUpdate()
		{
			_light.range = _Range * transform.lossyScale.magnitude;

		}
	}
}


