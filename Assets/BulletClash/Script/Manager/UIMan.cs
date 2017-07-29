using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RM;

namespace BC
{
	public class UIMan : Singleton<UIMan>
	{
		public GameObject _GoDialog;
		public Canvas _2DCanvas;

		public GameObject _GoCurUI;


		public void OpenUI(string aUI, bool aClosePrevUI)
		{
			if (_GoCurUI && aClosePrevUI)
				DestroyImmediate(_GoCurUI);

			GameObject goUI = Resources.Load<GameObject>("UI/" + aUI);
			goUI = goUI.Instantiate();
			goUI.transform.parent = _2DCanvas.transform;
			goUI.transform.ResetLocalTransform();
			goUI.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

			_GoCurUI = goUI;
		}

		public void OpenDialog(string aDialogBody)
		{
			GameObject goDilaog = _GoDialog.Instantiate();
			goDilaog.transform.parent = _2DCanvas.transform;
			goDilaog.transform.ResetLocalTransform();

			//GameObject goBody = Resources.Load<GameObject>(aDialogBody);
			//goBody = Instantiate(goBody);
			//goBody.transform.parent = _GoDialog.transform.Find("body");
		}



#if UNITY_EDITOR
#endif
	}
}




