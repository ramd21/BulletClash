using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BattleCameraMan : Singleton<BattleCameraMan>
	{
		public DragCamera _DragCamera;

		public Camera _BattleUICam;
		public Camera _BattleCam;

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
			if (_BattleCam.transform.position.z < _MainCamZMin)
				_BattleCam.transform.SetPositionZ(_MainCamZMin);

			if (_BattleCam.transform.position.z > _MainCamZMax)
				_BattleCam.transform.SetPositionZ(_MainCamZMax);
		}

		public void ShakeCam()
		{
			int cnt = 0;
			Vector3 pos = _BattleCam.transform.position;
			float rand = 0.25f;

			this.DoUntil(() => cnt == 5, () =>
			{
				_BattleCam.transform.position += new Vector2(Random.Range(-rand, rand), Random.Range(-rand, rand)).ToVector3XZ();
				cnt++;
			},
			() => 
			{
				_BattleCam.transform.position = pos;
			});
		}
	}
}






