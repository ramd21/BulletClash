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


		public void OpenDialog(string aDialogBody)
		{
			GameObject goDilaog = Instantiate(_GoDialog);
			goDilaog.transform.parent = _2DCanvas.transform;
			goDilaog.transform.ResetLocalPosition();

			//GameObject goBody = Resources.Load<GameObject>(aDialogBody);
			//goBody = Instantiate(goBody);
			//goBody.transform.parent = _GoDialog.transform.Find("body");
		}



#if UNITY_EDITOR
#endif
	}
}




