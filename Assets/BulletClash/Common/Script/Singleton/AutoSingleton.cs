using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public abstract class AutoSingleton<T> : RMBehaviour where T : AutoSingleton<T>
	{
		static public T gI;

		static public T i
		{
			get
			{
				if (gI == null)
					gI = (T)FindObjectOfType(typeof(T));

				if (gI == null)
				{
					GameObject go = new GameObject(typeof(T).ToString());
					gI = go.AddComponent<T>();
				}


				return gI;
			}
		}
	}
}


