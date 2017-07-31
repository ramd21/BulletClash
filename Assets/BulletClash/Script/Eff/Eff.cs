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
			LayerSet[] lsArr = GetComponentsInChildren<LayerSet>();
			for (int i = 0; i < lsArr.Length; i++)
			{
				lsArr[i].Set(false);
			}
		}

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


