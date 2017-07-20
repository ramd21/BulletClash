using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public abstract class Singleton<T> : RMBehaviour where T : Singleton<T>
	{
		static T gI;

		static public T i
		{
			get
			{
				return gI;
			}
		}

		protected virtual void Awake()
		{
			gI = (T)this;
		}
	}
}


