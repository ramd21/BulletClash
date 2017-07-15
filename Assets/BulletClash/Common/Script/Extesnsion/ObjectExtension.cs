using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RM
{
	static public class ObjectExtension
	{
		static public string GetAssetPath(this Object aThis)
		{
			return AssetDatabase.GetAssetPath(aThis);
		}

		static public void LogAssetPath(this Object aThis)
		{
			Debug.Log(AssetDatabase.GetAssetPath(aThis));
		}
	}
}


