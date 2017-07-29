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

		public string _OpenDialog;

		void Awake()
		{
			GetComponent<Button>().onClick.AddListener(OnClick);
			
		}

		void OnClick()
		{
			if (_GoToScene.IsNotNullOrEmpty())
				SceneManager.LoadScene(_GoToScene);

			if (_OpenDialog.IsNotNullOrEmpty())
				BattleUIMan.i.OpenDialog("");


		}


#if UNITY_EDITOR
#endif

	}
}


