using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BattleCameraMan : Singleton<BattleCameraMan>
	{
		public Camera _BattleUICam;
		public Camera _BattleCam;

		public int _ScreenW = 540;
		public int _ScreenH = 960;

		public Transform _TraShake;

		public void ShakeCam()
		{
			int cnt = 0;
			Vector3 pos = _TraShake.position;
			float rand = 0.3f;

			this.DoUntil(() => cnt == 5, () =>
			{
				_TraShake.position += new Vector2(RMMath.RandomRangeRestoreSeed(-rand, rand), RMMath.RandomRangeRestoreSeed(-rand, rand)).ToVector3XZ();
				cnt++;
			},
			() => 
			{
				_TraShake.position = pos;
			});
		}
	}
}






