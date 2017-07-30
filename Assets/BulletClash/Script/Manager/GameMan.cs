using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class GameMan : Singleton<GameMan>
	{
		protected override void Awake()
		{
			if (i)
			{
				DestroyImmediate(gameObject);
				return;
			}

			base.Awake();
			DontDestroyOnLoad(gameObject);
			
		}

		void Start()
		{
			UIMan.i.Init();
		}
	}
}




