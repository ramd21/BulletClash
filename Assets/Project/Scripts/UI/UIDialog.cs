using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

namespace BC
{
	public class UIDialog : RMBehaviour
	{
		public Button _Btn1;
		public Button _Btn2;
		public Text _TxtBtn1;
		public Text _TxtBtn2;
		public Text _TxtTitle;
		public Text _TxtBody;

		Action<UIDialog> _OnCLickBtn1;
		Action<UIDialog> _OnCLickBtn2;

		public void SetCntents(string aTxtTitle, string aTxtBody)
		{
			_TxtTitle.text = aTxtTitle;
			_TxtBody.text = aTxtBody; 
		}

		public void SetBtn(string aBtn1Label, Action<UIDialog> aOnClickBtn1, string aBtn2Label, Action<UIDialog> aOnClickBtn2)
		{
			_TxtBtn1.text = aBtn1Label;
			_Btn1.onClick.AddListener(OnClickBtn1);
			_OnCLickBtn1 = aOnClickBtn1;

			_Btn2.gameObject.SetActive(true);
			_TxtBtn2.text = aBtn2Label;
			_Btn2.onClick.AddListener(OnClickBtn2);
			_OnCLickBtn2 = aOnClickBtn2;
		}

		public void SetBtn(string aBtn1Label, UnityAction aOnClickBtn1)
		{
			_TxtBtn1.text = aBtn1Label;
			_Btn1.onClick.AddListener(aOnClickBtn1);

			_Btn2.gameObject.SetActive(false);
		}

		void OnClickBtn1()
		{
			_OnCLickBtn1(this);
		}

		void OnClickBtn2()
		{
			_OnCLickBtn2(this);
		}


		public void Close()
		{
			GetComponent<Animator>().Play("dialog_close");
			this.WaitUntil(
			()=> 
			{
				return transform.localScale.y == 0;
			}, 
			()=> 
			{
				DestroyImmediate(gameObject);
			});
		}
	}
}



