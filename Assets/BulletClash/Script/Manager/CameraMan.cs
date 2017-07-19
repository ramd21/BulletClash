using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class CameraMan : Singleton<CameraMan>
	{
		public DragCamera _DragCamera;
		public Camera _3DUICam;
		public Camera _MainCam;

		//public int _ScreenW = 540;
		//public int _ScreenH = 960;

		//[RuntimeInitializeOnLoadMethod]
		//static void Init()
		//{
		//	Deb.MethodLog();
		//	Screen.SetResolution(i._ScreenW, i._ScreenH, true);
		//}
	}
}




