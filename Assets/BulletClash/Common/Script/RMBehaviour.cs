using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

namespace RM
{
	public abstract class RMBehaviour : MonoBehaviour
	{
		public void AutoName()
		{
			name = this.GetType().ToString();
		}


		Camera _Camera;
		public Camera _camera
		{

			get
			{
				if (!_Camera)
					_Camera = GetComponent<Camera>();

				return _Camera;
			}
		}
	}
}


