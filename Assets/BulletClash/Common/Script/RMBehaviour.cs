using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

namespace RM
{
	public abstract class RMBehaviour : MonoBehaviour
	{
		public void AutoName()
		{
			name = this.GetType().ToString();
		}

		Camera _Camera;
		public Camera _camera
		{
			get
			{
				return SetAndGetComponent(_Camera);
			}
		}

		RectTransform _RectTransform;
		public RectTransform _rectTransform
		{
			get
			{
				return SetAndGetComponent(_RectTransform);
			}
		}

		Image _Image;
		public Image _image
		{
			get
			{
				return SetAndGetComponent(_Image);
			}
		}

		T SetAndGetComponent<T>(T aT) where T : Component
		{
			if (!aT)
				aT = GetComponent<T>();
			return aT;
		}
	}
}


