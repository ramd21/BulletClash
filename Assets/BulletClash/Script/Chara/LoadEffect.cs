using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace BC
{
	public class LoadEffect : RMBehaviour
	{
		public int _Id;

		void Awake()
		{
			GameObject go = ResourceMan.i.GetEffect(_Id);
			go = Instantiate(go);
			go.transform.parent = transform;
			go.transform.ResetLocalTransform();
			go.transform.SetScale(Vector3.one);
		}

#if UNITY_EDITOR
		public GameObject _GoPreview;
		public ResourceMan _ResourceMan;

		[Button("Preview")]
		public int _Preview;
		void Preview()
		{
			GameObject go = _ResourceMan.GetEffect(_Id);
			go = Instantiate(go);
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

#endif

	}
}


