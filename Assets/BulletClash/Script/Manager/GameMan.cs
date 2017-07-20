using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class GameMan : Singleton<GameMan>
	{
		public int _TPMax;
		public int _TPTimer;

		public const int cDistDiv = 100;



		private void Start()
		{
			UIMan.i.Init();
			PlayerMan.i.Init();
			CharaMan.i.Init();
		}

		void FixedUpdate()
		{
			PlayerMan.i.Act();
			CharaMan.i.Act();
		}

		void Update()
		{
			CharaMan.i.UpdateView();
		}
	}
}




