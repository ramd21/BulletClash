using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	[ExecuteInEditMode]
	public class LinkTransform : RMBehaviour
	{
		public Transform _Tage;

		void Update()
		{
			transform.position = _Tage.position;
		}
	}
}




