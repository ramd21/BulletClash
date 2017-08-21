#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RM
{
	public class ScreenShot
	{
		[MenuItem("Tools/ScreenShot %&s")]
		static public void RunMenu()
		{
			string path = Directory.GetCurrentDirectory() + "/ScreenShot";
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			int last = 0;
			if (Directory.Exists(path))
			{
				string[] strFileArr;
				List<int> numList = new List<int>();
				strFileArr = Directory.GetFiles(path);
				if (strFileArr.Length != 0)
				{
					//strFileList.ForEach((a)=> a = a.Split('_')[0]);

					for (int i = 0; i < strFileArr.Length; i++)
					{
						strFileArr[i] = Path.GetFileNameWithoutExtension(strFileArr[i]);
						strFileArr[i] = strFileArr[i].Split('_')[1];

						numList.Add(strFileArr[i].ToInt());
					}

					numList.Sort();

					last = numList[numList.Count - 1];
					last++; 
				}
			}

			ScreenCapture.CaptureScreenshot("ScreenShot/ScreenShot_" + last + ".png");
			Debug.Log(Application.persistentDataPath);
		}
	}
}
#endif


