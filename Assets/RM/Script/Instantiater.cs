using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class Instantiater : EditorUpdateBehaviour
	{
		public GameObject _GoInst;


		void Awake()
		{
			_GoInst = Instantiate(_GoInst);
			_GoInst.transform.SetParent(transform);
			_GoInst.transform.ResetLocalTransform();
		}

#if UNITY_EDITOR

		public GameObject _GoPreview;

		[Button("Preview")]
		public int _Preview;
		void Preview()
		{
			GameObject go;
			go = Instantiate(_GoInst);
			go.transform.parent = transform;
			go.transform.ResetLocalTransform();
			go.transform.SetScale(Vector3.one);
			_GoPreview = go;
		}

		[Button("RemovePreview")]
		public int _RemovePreview;
		void RemovePreview()
		{
			DestroyImmediate(_GoPreview);
		}

		public override void EditorUpdate()
		{
			_name = _GoInst.name;
		}
#endif
	}
}


