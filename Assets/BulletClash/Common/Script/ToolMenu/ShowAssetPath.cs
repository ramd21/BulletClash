using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RM
{
	public class LogAssetPath
	{
		[MenuItem("Tools/LogAssetPath")]
		static public void RunMenu()
		{
			int len = Selection.objects.Length;

			for (int i = 0; i < len; i++)
			{
				Selection.objects[i].LogAssetPath();
			}
		}
	}
}


