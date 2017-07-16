#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class RemoveComponentRoot : RMBehaviour
	{
		public string _ComponentName;

		[Button("Remove")]
		public int _Remove;

		void Remove()
		{
			Component[] w = GetComponentsInChildren<Component>();

			for (int i = 0; i < w.Length; i++)
			{
				Deb.MethodLog(w[i].GetType().ToString());
				if (w[i].GetType().ToString() == _ComponentName)
					DestroyImmediate(w[i]);
			}
		}
	}
}
#endif


