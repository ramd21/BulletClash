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

		bool _Init;
		protected override void Awake()
		{
			base.Awake();

			this.WaitForEndOfFrame(()=> 
			{
				//ResourceMan.i.Init();
				SinTable.Init();
				CosTable.Init();

				UIMan.i.Init();
				PlayerMan.i.Init();
				CharaMan.i.Init();
				CollMan.i.Init();
				_Init = true;
			});
		}

		void FixedUpdate()
		{
			if (!_Init)
				return;

			PlayerMan.i.Act();
			CharaMan.i.Act();
		}

		void Update()
		{
			if (!_Init)
				return;

			CharaMan.i.UpdateView();
		}
	}
}




