using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class LinkRectTransformToCameraRect : EditorUpdateBehaviour
	{
		public Camera _Tage;

		public enum Mode
		{
			on_enable,
			start,
		}

		public Mode _Mode;

		void OnEnable()
		{
			if (_Mode == Mode.on_enable)
				SetRect();
		}

		void Start()
		{
			if (_Mode == Mode.start)
				SetRect();
		}

		void SetRect()
		{
			_rectTransform.sizeDelta = new Vector2(_Tage.rect.width * Screen.width, _Tage.rect.height * Screen.height);
		}

#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			SetRect();
		}
#endif
	}
}


