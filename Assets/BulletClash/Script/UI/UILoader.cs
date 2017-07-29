using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BC
{
	public class UILoader : RMBehaviour
	{
		public string _UIOpen;

		void Start()
		{
			UIMan.i.OpenUI(_UIOpen, true);
		}


#if UNITY_EDITOR
#endif

	}
}


