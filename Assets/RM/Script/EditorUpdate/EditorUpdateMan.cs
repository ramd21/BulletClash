using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RM
{
	public class EditorUpdateMan : AutoSingleton<EditorUpdateMan>
	{
#if UNITY_EDITOR
		public bool _Enabled;


		[InitializeOnLoadMethod]
		static void Init()
		{
			Deb.MethodLog();
			EditorApplication.update += ForceUpdate;
		}

		static void ForceUpdate()
		{
			if (!i._Enabled)
				return;

			i.transform.position += Vector3.one;
			i.transform.position -= Vector3.one;
		}
#endif
	}
}

