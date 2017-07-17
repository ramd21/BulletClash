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

		Light _Light;
		public Light _light
		{
			get
			{
				return SetAndGetComponent(_Light);
			}
		}

		T SetAndGetComponent<T>(T aT) where T : Component
		{
			if (!aT)
				aT = GetComponent<T>();
			return aT;
		}

		//[Button("AddEditorUpdate")]
		//public int _AddEditorUpdate;
		//void AddEditorUpdate()
		//{
		//	gameObject.GetOrAddComponent<EditorUpdate>();
		//}

	}
}


