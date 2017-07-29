using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BattleCameraMan : Singleton<BattleCameraMan>
	{
		public DragCamera _DragCamera;
		public Camera _3DUICam;
		public Camera _MainCam;

		public int _ScreenW = 540;
		public int _ScreenH = 960;

		public float _MainCamZMax;
		public float _MainCamZMin;


		//public int _ScreenW = 540;
		//public int _ScreenH = 960;

		//[RuntimeInitializeOnLoadMethod]
		//static void Init()
		//{
		//	Deb.MethodLog();
		//	Screen.SetResolution(i._ScreenW, i._ScreenH, true);
		//}

		void LateUpdate()
		{
			if (_MainCam.transform.position.z < _MainCamZMin)
				_MainCam.transform.SetPositionZ(_MainCamZMin);

			if (_MainCam.transform.position.z > _MainCamZMax)
				_MainCam.transform.SetPositionZ(_MainCamZMax);
		}
	}
}






