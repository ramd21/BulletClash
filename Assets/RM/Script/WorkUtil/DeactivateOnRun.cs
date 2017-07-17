using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class DeactivateOnRun : RMBehaviour
	{
		void Awake()
		{
			gameObject.SetActive(false);			
		}
	}
}


