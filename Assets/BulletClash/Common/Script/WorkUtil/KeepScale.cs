using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class KeepScale : EditorUpdateBehaviour
	{
		public Camera _Cam;
		public int _TageX = 540;
		public int _TageY = 960;

		public float _Scale;

		public Vector2Int _ScreenSize;
		float _TageRatio;
		float _CurRatio;

		public override void EditorUpdate()
		{
			_ScreenSize = new Vector2Int(Screen.width, Screen.height);
			_TageRatio = (float)_TageX / _TageY;
			_CurRatio = (float)Screen.width / (float)Screen.height;

			if (_CurRatio > _TageRatio)
			{
				//横が広い
				transform.localScale = Vector3.one * ((float)Screen.height / _TageY) * _Scale;
			}
			else
			{
				//縦が広い
				transform.localScale = Vector3.one * ((float)Screen.width / _TageX) * _Scale;
			}
		}
	}
}


