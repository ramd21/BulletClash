using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class BackFire : ManagedBehaviour
	{
		public Camera _CamTage;

		public override void ManagedLateUpdate()
		{
			if (!_CamTage)
				_CamTage = Camera.main;

			transform.LookAt(transform.position + transform.parent.forward, _CamTage.transform.forward);
		}
#if UNITY_EDITOR

		//private void OnDrawGizmos()
		//{
		//	if (_CamTage)
		//	{
		//		Gizmos.DrawLine(transform.position, _CamTage.transform.position);
		//		Gizmos.DrawLine(transform.position, transform.position - transform.parent.forward * 10);
		//	}
		//}

#endif

	}
}


