﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class DebMan : Singleton<DebMan>
	{
		public Text _TxtFps;
		public float _FPSInter = 0.5f;

		float _Accum; // FPS accumulated over the interval
		int _Frame; // Frames drawn over the interval
		float _Time; // Left time for current interval

		protected override void Awake()
		{
			_Time = _FPSInter;
		}

		void Update()
		{
			_Time -= Time.deltaTime;
			_Accum += Time.timeScale / Time.deltaTime;
			++_Frame;

			// Interval ended - update GUI text and start new interval
			if (_Time <= 0.0)
			{
				float fps = _Accum / _Frame;

				// display two fractional digits (f2 format)
				_TxtFps.text = "fps:" + fps.ToString("f2");


				if (fps < 45)
				{
					_TxtFps.color = Color.yellow;
				}
				else if (fps < 30)
				{
					_TxtFps.color = Color.red + Color.yellow / 2;
				}
				else if (fps < 15)
				{
					_TxtFps.color = Color.red;
				}
				else
				{
					_TxtFps.color = Color.green;
				}

				_Time = _FPSInter;
				_Accum = 0.0f;
				_Frame = 0;
			}
		}
	}
}






