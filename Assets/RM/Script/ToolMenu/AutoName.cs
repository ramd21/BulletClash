#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RM
{
	public class AutoName
	{
		[MenuItem("Tools/AutoName")]
		static public void RunMenu()
		{
			int len = Selection.gameObjects.Length;

			for (int i = 0; i < len; i++)
			{
				Type type = Selection.gameObjects[i].GetComponent<MonoBehaviour>().GetType();

				if (type.Namespace.IsNullOrEmpty())
					Selection.gameObjects[i].name = type.ToString();
				else
					Selection.gameObjects[i].name = type.ToString().Remove(type.Namespace + ".");
			}
		}
	}
}
#endif


