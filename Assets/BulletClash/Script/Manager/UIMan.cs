using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RM;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace BC
{
	public class UIMan : Singleton<UIMan>
	{
		public Camera _MainUICam;
		public UIDialog _Dialog;
		public Canvas _2DCanvas;

		public GameObject _GoCurUI;
		public GameObject _GoHeader;

		public Text _TxtUserName;
		public Text _TxtUserLv;
		public Text _TxtGold;


		public void Init()
		{
			_TxtUserName.text = UserDataMan.i._UserData._Name;
			_TxtUserLv.text = "Lv " + UserDataMan.i._UserData._Lv.ToString();
			_TxtGold.text = UserDataMan.i._UserData._Gold.ToString();
		}

		public void SetHeaderActive(bool aActive)
		{
			_GoHeader.SetActive(aActive);
		}

		public void CloseCurUI()
		{
			if (_GoCurUI)
				DestroyImmediate(_GoCurUI);
		}

		public void OpenUI(string aUI, bool aClosePrevUI)
		{
			if (aUI.IsNullOrEmpty())
				return;

			if (_GoCurUI && aClosePrevUI)
				DestroyImmediate(_GoCurUI);

			GameObject goUI = Resources.Load<GameObject>("UI/" + aUI);
			goUI = goUI.Instantiate();
			goUI.transform.SetParent(_2DCanvas.transform);
			goUI.transform.SetSiblingIndex(0);
			goUI.transform.ResetLocalTransform();
			goUI.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

			_GoCurUI = goUI;

			Deb.MethodLog();
		}

		public void GoToScene(string aScene)
		{
			CloseCurUI();
			SceneManager.LoadScene(aScene);
		}

		public UIDialog OpenCommonDialog(string aTitle, string aBody, string aBtn1Label, Action<UIDialog> aOnClickBtn1, string aBtn2Label, Action<UIDialog> aOnClickBtn2)
		{
			UIDialog dilaog = _Dialog.Instantiate();
			dilaog.transform.parent = _2DCanvas.transform;
			dilaog.transform.ResetLocalTransform();
			dilaog.SetCntents(aTitle, aBody);
			dilaog.SetBtn(aBtn1Label, aOnClickBtn1, aBtn2Label, aOnClickBtn2);

			return dilaog;
		}

		public void OpenCommonDialog(string aTitle, string aBtn1Label, Action<UIDialog> aOnClickBtn1)
		{
			UIDialog dilaog = _Dialog.Instantiate();
			dilaog.transform.parent = _2DCanvas.transform;
			dilaog.transform.ResetLocalTransform();
		}

		public UIDialog OpenDialog(string aDialogBody)
		{
			UIDialog dilaog = _Dialog.Instantiate();
			dilaog.transform.parent = _2DCanvas.transform;
			dilaog.transform.ResetLocalTransform();
			return dilaog;
			//GameObject goBody = Resources.Load<GameObject>(aDialogBody);
			//goBody = Instantiate(goBody);
			//goBody.transform.parent = _GoDialog.transform.Find("body");
		}



#if UNITY_EDITOR
#endif
	}
}




