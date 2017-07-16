using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class GameMan : Singleton<GameMan>
	{
		public int _MaxTacticsPoint;

		protected override void Awake()
		{
			base.Awake();

			UIMan.i.Init();
			PlayerMan.i.Init();

		}

		void FixedUpdate()
		{
			PlayerMan.i.Act();
		}

		void Update()
		{
		}
	}
}


