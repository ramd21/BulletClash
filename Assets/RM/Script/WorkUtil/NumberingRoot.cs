#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class NumberingRoot : RMBehaviour
	{
		public string _BaseName;

		public int _StartNum;


		[Button("Apply")]
		public int _Apply;

		void Apply()
		{
			int len = transform.childCount;

			for (int i = 0; i < len; i++)
			{
				transform.GetChild(i).name = _BaseName + (_StartNum + i);
			}
		}
	}
}
#endif

