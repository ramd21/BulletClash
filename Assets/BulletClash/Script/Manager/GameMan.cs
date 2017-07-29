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

		public string[] _StrUI;

		public int _UIId;

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
					Init();
				}
#endif
			}
			else
			{
				Init();
			}
		}

		void Init()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);

			GameObject go = Resources.Load<GameObject>("UI/" + _StrUI[_UIId]);
			go.Instantiate();
		}
	}
}




