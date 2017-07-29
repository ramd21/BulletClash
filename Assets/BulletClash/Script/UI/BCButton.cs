using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BC
{
	public class BCButton : RMBehaviour
	{
		public string _GoToScene;
		public string _OpenUI;
		public string _OpenDialog;

		public bool _ClosePrevUI;

		void Awake()
		{
			GetComponent<Button>().onClick.AddListener(OnClick);
		}

		void OnClick()
		{
			if (_GoToScene.IsNotNullOrEmpty())
				SceneManager.LoadScene(_GoToScene);

			if (_OpenUI.IsNotNullOrEmpty())
				UIMan.i.OpenUI(_OpenUI, _ClosePrevUI);

			if (_OpenDialog.IsNotNullOrEmpty())
				UIMan.i.OpenDialog("");

			


		}


#if UNITY_EDITOR
#endif

	}
}


