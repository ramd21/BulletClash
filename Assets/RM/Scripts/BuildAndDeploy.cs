//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
//using UnityEngine;

//namespace RM
//{
//    public class BuildAndDeploy : RMBehaviour
//    {
//#if UNITY_EDITOR
//        public string _DeplayGateUserName;
//        public string _DeplayGateToken;
//        public UnityEngine.Object _CurlExe;

//        public TextAsset _Text;

//        public enum BuildEnvironment
//        {
//            develop,
//            staging,
//            release,
//        }

//        public BuildEnvironment _BuildEnvironment;

//        public bool _CPP;

//        string _filePath { get { return Directory.GetCurrentDirectory() + "/" + Application.productName + ".apk"; } }

//        [Button("BuildAndroidAndDeploy")]
//        public int _BuildAndroidAndDeploy;
//        void BuildAndroidAndDeploy()
//        {
//            EditorPrefs.SetBool("build android and deploy", true);
//            BuildAndroid();
//        }

//        [Button("BuildAndroid")]
//        public int _BuildAndroid;
//        void BuildAndroid()
//        {
//            File.WriteAllText(_Text.GetAssetFullPath(), DateTime.Now.ToString());
//            AssetDatabase.Refresh();

//            EditorPrefs.SetBool("build_android", true);

//            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/BuildHistory"))
//                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/BuildHistory");

//            if (File.Exists(_filePath))
//            {
//                string[] strFileArr = Directory.GetFiles(Directory.GetCurrentDirectory() + "/BuildHistory/");

//                List<int> numList = new List<int>();

//                int last = 0;
//                if (strFileArr.Length != 0)
//                {
//                    for (int i = 0; i < strFileArr.Length; i++)
//                    {
//                        strFileArr[i] = Path.GetFileNameWithoutExtension(strFileArr[i]);

//                        string[] split = strFileArr[i].Split('_');

//                        strFileArr[i] = split[split.Length - 1];

//                        numList.Add(strFileArr[i].ToInt());
//                    }

//                    numList.Sort();
//                    last = numList[numList.Count - 1];
//                }

//                File.Move(_filePath, Directory.GetCurrentDirectory() + "/BuildHistory/" + Application.productName + "_" + (last + 1) + ".apk");
//            }

//            UnityEngine.Debug.Log("start build android");
//            string filePath = Directory.GetCurrentDirectory() + "/" + Application.productName + ".apk";

//            BuildOptions opt = BuildOptions.None;
//            if (_CPP)
//                opt |= BuildOptions.Il2CPP;

//            switch (_BuildEnvironment)
//            {
//                case BuildEnvironment.develop:
//                    opt |= BuildOptions.Development;
//                    opt |= BuildOptions.ConnectWithProfiler;
//                    break;
//                case BuildEnvironment.staging:
//                    break;
//                case BuildEnvironment.release:
//                    break;
//            }

//            BuildPipeline.BuildPlayer
//            (
//                EditorBuildSettings.scenes.Where(i => i.enabled).ToArray(),
//                filePath,
//                BuildTarget.Android,
//                opt
//            );
//        }

//        [Button("Deploy")]
//        public int _Deploy;
//        void Deploy()
//        {
//            if (!File.Exists(_filePath))
//                return;

//            System.Diagnostics.Process process = new System.Diagnostics.Process();

//            EditorPrefs.SetBool("deploy", true);
//            UnityEngine.Debug.Log("start deploy " + DateTime.Now);
//            string arg = " -F \"token=" + _DeplayGateToken + "\" -F \"file=@" + _filePath + "\" https://deploygate.com/api/users/" + _DeplayGateUserName + "/apps";
//            ProcessStartInfo psi = new ProcessStartInfo();
//            psi.CreateNoWindow = true;
//            psi.UseShellExecute = false;
//            psi.FileName = _CurlExe.GetAssetFullPath();

//            psi.Verb = "runas";
//            psi.Arguments = arg;

//            process = Process.Start(psi);
//            process.EnableRaisingEvents = true;
//            process.Exited += new EventHandler(OnExit);
//        }

//        void OnExit(object aSender, EventArgs aEventArgs)
//        {
//            UnityEngine.Debug.Log("End deploy " + DateTime.Now);
//        }

//        public override void EditorAwake()
//        {
//            tag = "EditorOnly";
//        }

//        public override void EditorUpdate()
//        {
//            if (EditorPrefs.GetBool("build android and deploy", false))
//            {
//                Deploy();
//                EditorPrefs.SetBool("build android and deploy", false);
//            }

//            //if (EditorPrefs.GetBool("deploy", false))
//            //{
//            //	UnityEngine.Debug.Log("done deploy");
//            //	EditorPrefs.SetBool("deploy", false);
//            //}

//            if (EditorPrefs.GetBool("build_android", false))
//            {
//                UnityEngine.Debug.Log("done build android");
//                EditorPrefs.SetBool("build_android", false);
//            }

//            switch (_BuildEnvironment)
//            {
//                case BuildEnvironment.develop:
//#if !DEVELOP_BUILD
//                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "DEVELOP_BUILD");
//                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "DEVELOP_BUILD");
//#endif
//                    break;
//                case BuildEnvironment.staging:
//#if !STAGING_BUILD
//                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "STAGING_BUILD");
//                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "STAGING_BUILD");
//#endif
//                    break;
//                case BuildEnvironment.release:
//#if !RELEASE_BUILD
//                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "RELEASE_BUILD");
//                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "RELEASE_BUILD");
//#endif
//                    break;
//            }
//        }
//#endif
//    }
//}

