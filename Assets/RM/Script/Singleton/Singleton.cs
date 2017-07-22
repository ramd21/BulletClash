using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public abstract class Singleton<T> : RMBehaviour where T : Singleton<T>
	{
		public static T i;

		protected virtual void Awake()
		{
			i = (T)this;
		}
	}
}


