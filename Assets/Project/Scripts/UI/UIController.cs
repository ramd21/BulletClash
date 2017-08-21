using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BC
{
	public class UIController : RMBehaviour
	{
		public bool _Header;

		void Start()
		{
			UIMan.i.SetHeaderActive(_Header);
		}


#if UNITY_EDITOR
#endif

	}
}


