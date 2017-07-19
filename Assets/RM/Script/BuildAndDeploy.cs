using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace RM
{
	public class BuildAndDeploy : EditorUpdateBehaviour
	{
#if UNITY_EDITOR
		public string _DeplayGateUserName;
		public string _DeplayGateToken;
		public UnityEngine.Object _CurlExe;
		public bool _IL2CPP;

		string _filePath { get { return Directory.GetCurrentDirectory() + "/" + Application.productName + ".apk"; } }

		[Button("BuildAndroidAndDeploy")]
		public int _BuildAndroidAndDeploy;
		void BuildAndroidAndDeploy()
		{
			EditorPrefs.SetBool("build android and deploy", true);
			BuildAndroid();
		}

		[Button("BuildAndroid")]
		public int _BuildAndroid;
		void BuildAndroid()
		{
			EditorPrefs.SetBool("build_android", true);

			UnityEngine.Debug.Log("start build android");
			string filePath = Directory.GetCurrentDirectory() + "/" + Application.productName + ".apk";

			BuildOptions opt = BuildOptions.None;
			if (_IL2CPP)
				opt |= BuildOptions.Il2CPP;


			BuildPipeline.BuildPlayer
			(
				EditorBuildSettings.scenes.Where(i => i.enabled).ToArray(),
				filePath,
				BuildTarget.Android,
				opt
			);
		}

		[Button("Deploy")]
		public int _Deploy;
		void Deploy()
		{
			EditorPrefs.SetBool("deploy", true);

			UnityEngine.Debug.Log("start deploy");
			string arg = " -F \"token=" + _DeplayGateToken + "\" -F \"file=@" + _filePath + "\" https://deploygate.com/api/users/" + _DeplayGateUserName + "/apps";
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.FileName = _CurlExe.GetAssetFullPath();
			psi.Verb = "runas";
			psi.Arguments = arg;
			Process.Start(psi);
		}

		public override void EditorUpdate()
		{
			if (EditorPrefs.GetBool("build android and deploy", false))
			{
				Deploy();
				EditorPrefs.SetBool("build android and deploy", false);
			}

			if (EditorPrefs.GetBool("deploy", false))
			{
				UnityEngine.Debug.Log("done deploy");
				EditorPrefs.SetBool("deploy", false);
			}

			if (EditorPrefs.GetBool("build_android", false))
			{
				UnityEngine.Debug.Log("done build android");
				EditorPrefs.SetBool("build_android", false);
			}
		}
#endif
	}
}

