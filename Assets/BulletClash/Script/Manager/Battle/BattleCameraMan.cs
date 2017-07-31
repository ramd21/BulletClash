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
		public Transform _TraShake;

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
			Vector3 pos = _TraShake.position;
			float rand = 0.3f;

			this.DoUntil(() => cnt == 5, () =>
			{
				_TraShake.position += new Vector2(Random.Range(-rand, rand), Random.Range(-rand, rand)).ToVector3XZ();
				cnt++;
			},
			() => 
			{
				_TraShake.position = pos;
			});
		}
	}
}






