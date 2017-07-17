//#if UNITY_EDITOR
//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using UnityEditor;
//using UnityEngine;

//namespace RM
//{
//	public class Build : EditorUpdateBehaviour
//	{
//		public string _OutFolder;

//		public string _FileName;

//		[Button("BuildAndroid")]
//		public int _BuildAndroid;
//		void BuildAndroid()
//		{
//			BuildPipeline.BuildPlayer
//				(
//					EditorBuildSettings.scenes.Where(i => i.enabled).ToArray(),
//					_OutFolder + "/" +_FileName + ".apk", 
//					BuildTarget.Android, 
//					BuildOptions.Development
//				);

//			Process.Start(_ThriftBat.GetAssetFullPath());
//		}

//		public override void EditorUpdate()
//		{
//		}
//	}
//}
//#endif
