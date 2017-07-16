using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace RM
{
	static public class ObjectExtension
	{
#if UNITY_EDITOR
		static public string GetAssetPath(this Object aThis)
		{
			return AssetDatabase.GetAssetPath(aThis);
		}

		static public void LogAssetPath(this Object aThis)
		{
			Debug.Log(AssetDatabase.GetAssetPath(aThis));
		}
#endif
	}
}


