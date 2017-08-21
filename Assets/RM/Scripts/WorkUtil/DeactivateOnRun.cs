using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class DeactivateOnRun : RMBehaviour
	{
		public bool _Enabled;
		void Awake()
		{
			if (!_Enabled)
				return;

			gameObject.SetActive(false);			
		}
	}
}


