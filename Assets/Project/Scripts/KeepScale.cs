using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class KeepScale : EditorUpdateBehaviour
	{
		public Camera _Cam;
		public int _TageX = 540;
		public int _TageY = 960;

		public float _Scale;

		public Vector2 _ScreenSize;
		float _TageRatio;
		float _CurRatio;

		void Awake()
		{
			SetScale();
		}

		void SetScale()
		{
			_ScreenSize = new Vector2(Screen.width, Screen.height);
			_TageRatio = (float)_TageX / _TageY;
			_CurRatio = (float)Screen.width / (float)Screen.height;

			if (_CurRatio > _TageRatio)
			{
				//‰¡‚ªL‚¢
				transform.localScale = Vector3.one * ((float)Screen.height / _TageY) * _Scale;
			}
			else
			{
				//c‚ªL‚¢
				transform.localScale = Vector3.one * ((float)Screen.width / _TageX) * _Scale;
			}
		}


#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			SetScale();
		}
#endif

	}
}




