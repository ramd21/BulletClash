using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class LinkRectTransformToCameraRect : EditorUpdateBehaviour
	{
		public Camera _Tage;


		public override void EditorUpdate()
		{
			_rectTransform.sizeDelta = new Vector2(_Tage.rect.width * Screen.width, _Tage.rect.height * Screen.height);
		}
	}
}


