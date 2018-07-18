using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class PointLightScaler : RMBehaviour
	{
		public float _Range;

		void Start()
		{
			SetScale();
		}

		void SetScale()
		{
			_light.range = _Range * transform.lossyScale.magnitude;
		}

#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			SetScale();
		}
#endif
	}
}


