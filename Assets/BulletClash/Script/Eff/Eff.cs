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

		protected override void Awake()
		{
			base.Awake();
			LayerSet[] lsArr = GetComponentsInChildren<LayerSet>(true);
			for (int i = 0; i < lsArr.Length; i++)
			{
				lsArr[i].Set(false);
			}
		}

		public void SetPos(Vector2Int aPos, int aTimer, int aSeId, float aVol)
		{
			_Tra._Pos = aPos;
			_Timer = aTimer;
			_State = ActiveState.active;

			gameObject.SetActive(true);

			SoundMan.i.PlaySeReq(aSeId, aVol);
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


