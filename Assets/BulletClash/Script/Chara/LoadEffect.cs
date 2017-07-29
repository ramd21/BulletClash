using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace BC
{
	public class LoadEffect : EditorUpdateBehaviour
	{
		public Chara _Chara;
		public int _Id;

		public GameObject _GoEff;

		void Awake()
		{
			if (_GoEff)
				return;

			_GoEff = ResourceMan.i.GetEffect(_Id + _Chara._PlayerId);
			_GoEff = Instantiate(_GoEff);
			_GoEff.SetLayer("battle");

			_GoEff.transform.parent = transform;
			_GoEff.transform.ResetLocalTransform();
			_GoEff.transform.SetScale(Vector3.one);
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

		public override void EditorUpdate()
		{
			if (!_ResourceMan)
				_ResourceMan = GameObject.FindObjectOfType<ResourceMan>();

			_Chara = transform.GetComponentInParent<Chara>();
		}
#endif

	}
}


