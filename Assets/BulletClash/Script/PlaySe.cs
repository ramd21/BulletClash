using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BC
{
	public class PlaySe : RMBehaviour
	{
		public SeType _Type;
		public int _Channel;

		void OnEnable()
		{
			SoundMan.i.PlaySeReq(_Type, _Channel);
		}


#if UNITY_EDITOR
#endif

	}
}


