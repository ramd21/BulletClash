using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RM
{
	public class EditorUpdateMan : AutoSingleton<EditorUpdateMan>
	{
		[InitializeOnLoadMethod]
		static void Init()
		{
			Deb.MethodLog();
			EditorApplication.update += ForceUpdate;
		}

		static void ForceUpdate()
		{
			i.transform.position += Vector3.one;
			i.transform.position -= Vector3.one;
		}
	}
}


