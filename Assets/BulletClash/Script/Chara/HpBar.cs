using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class HpBar : ManagedBehaviour
	{
		public Camera _CamTage;

		public override void ManagedLateUpdate()
		{
			if (!_CamTage)
				_CamTage = Camera.main;

			transform.LookAt(_CamTage.transform, Vector3.right);
		}
#if UNITY_EDITOR
#endif

	}
}


