using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class GameMan : Singleton<GameMan>
	{
		public bool _ForEditor;

		protected override void Awake()
		{
			if (_ForEditor)
			{
#if !UNITY_EDITOR
				DestroyImmediate(gameObject);
#else
				if (i)
				{
					DestroyImmediate(gameObject);
				}
				else
				{
					base.Awake();
					DontDestroyOnLoad(gameObject);
				}
#endif
			}
			else
			{
				base.Awake();
				DontDestroyOnLoad(gameObject);
			}
		}
	}
}




