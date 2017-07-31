using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BattleGameMan : Singleton<BattleGameMan>
	{
		public int _TPMax;
		public int _TPTimer;

		public const int cDistDiv = 100;

		bool _Init;

		void Start()
		{
			

			SinTable.Init();
			CosTable.Init();

			BattleUIMan.i.Init();
			BattlePlayerMan.i.Init();
			BattleCharaMan.i.Init();
			BattleCollMan.i.Init();
			_Init = true;
		}

		

		void FixedUpdate()
		{
			if (!_Init)
				return;

			BattlePlayerMan.i.Act();
			BattleCharaMan.i.Act();
		}

		void Update()
		{
			if (!_Init)
				return;

			BattleCharaMan.i.UpdateView();
		}
	}
}




