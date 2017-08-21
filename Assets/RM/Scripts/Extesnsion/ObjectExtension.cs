using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;

namespace RM
{
	static public class ObjectExtension
	{
#if UNITY_EDITOR

		static public string GetAssetFullPath(this Object aThis)
		{
			return Directory.GetCurrentDirectory() + "/" + AssetDatabase.GetAssetPath(aThis);
		}

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


