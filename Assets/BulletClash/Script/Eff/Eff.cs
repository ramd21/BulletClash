using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class Eff : BHObj
	{
		int _Timer;

		public void SetPos(Vector2Int aPos, int aTimer)
		{
			_Tra._Pos = aPos;
			_Timer = aTimer;
			_State = ActiveState.active;

			gameObject.SetActive(true);
		}

		public void Act()
		{
			if (_Timer == 0)
			{
				_State = ActiveState.inactive;
				gameObject.SetActive(false);
			}
			_Timer--;
		}

#if UNITY_EDITOR
#endif

	}
}


