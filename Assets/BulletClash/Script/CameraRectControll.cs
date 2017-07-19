using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class CameraRectControll : EditorUpdateBehaviour
	{
		public float _WidthRatio = 9;
		public float _HeightRatio = 16;

		float _TageRatio;
		float _CurRatio;

		public Vector2Int _ScreenSize;
#if UNITY_EDITOR
#endif
		public override void EditorUpdate()
		{
			_ScreenSize = new Vector2Int(Screen.width, Screen.height);
			_TageRatio = _WidthRatio / _HeightRatio;
			_CurRatio = (float)Screen.width / (float)Screen.height;

			float w, h;
			float x, y;

			if (_CurRatio > _TageRatio)
			{
				//横が広い
				w = _TageRatio / _CurRatio;
				x = (1 - w) / 2;

				_camera.rect = new Rect(new Vector2(x, 0), new Vector2(w, 1));

				if (_camera.orthographic)
					_camera.orthographicSize = Screen.height / 2;
			}
			else
			{
				//縦が広い
				h = _CurRatio / _TageRatio;
				y = (1 - h) / 2;

				_camera.rect = new Rect(new Vector2(0, y), new Vector2(1, h));

				if (_camera.orthographic)
					_camera.orthographicSize = (Screen.height * h) / 2;
			}
		}
	}
}




