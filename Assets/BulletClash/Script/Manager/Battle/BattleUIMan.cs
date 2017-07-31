using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RM;

namespace BC
{
	public class BattleUIMan : Singleton<BattleUIMan>, IEditorUpdate
	{
		public Image[] _ImgTPGaugeArr;
		public Text _TxtPoint;

		public UnitCard[] _UnitCardArr;

		public GameObject _GoDialog;

		public Canvas _2DCanvas;

		public Button _BtnSetting;


		public void Init()
		{
			//UIMan.i._MainUICam.enabled = false;

			InitTPGauge();
			InitTPCnt();
			InitUnitCard();

			_BtnSetting.onClick.AddListener(()=> 
			{
				//UIMan.i._MainUICam.enabled = true;
				UIMan.i.OpenCommonDialog("リタイア", "リタイアしますか？", 
				"はい", 
				(d)=> 
				{
					//UIMan.i.
					UIMan.i.GoToScene("home");
					UIMan.i.OpenUI("home", true);
					d.Close();
					//UIMan.i._MainUICam.enabled = false;
				},
				"いいえ",
				(d) => 
				{
					d.Close();
					//UIMan.i._MainUICam.enabled = false;
				});
			});
		}

		//void OnDestroy()
		//{
		//	UIMan.i._MainUICam.enabled = true;
		//}

		void InitUnitCard()
		{
			for (int i = 0; i < _UnitCardArr.Length; i++)
			{
				_UnitCardArr[i].Init(BattlePlayerMan.i._myPlayer._DeckUnitTypeArr[i]);
			}
		}

		void InitTPCnt()
		{
			_TxtPoint.text = "0";
			this.StartObsserve(() => BattlePlayerMan.i._myPlayer._TPTimerTotal / BattleGameMan.i._TPTimer,
			(cur, last) =>
			{
				_TxtPoint.text = cur.ToString();
				_TxtPoint.transform.DOScale(1.5f, 0.25f).OnComplete(() =>
				{
					_TxtPoint.transform.DOScale(1f, 0.25f);
				});

			}, true);
		}

		void InitTPGauge()
		{
			for (int i = 0; i < _ImgTPGaugeArr.Length; i++)
			{
				Image img = _ImgTPGaugeArr[i];
				img.fillAmount = 0;
				this.StartObsserve(() => img.fillAmount,
				(cur, last) =>
				{
					if (last != 0 && cur == 1)
					{
						img.transform.parent.DOScale(1.5f, 0.25f).OnComplete(() =>
						{
							img.transform.parent.DOScale(1f, 0.25f);
						});
					}
				}, true);
			}
		}

		public void UpdateTPUI(int aTPTimerTotal)
		{
			float fill;

			for (int i = 0; i < _ImgTPGaugeArr.Length; i++)
			{
				fill = (float)(aTPTimerTotal - i * BattleGameMan.i._TPTimer) / BattleGameMan.i._TPTimer;
				_ImgTPGaugeArr[i].fillAmount = fill;
			}
		}

#if UNITY_EDITOR
		public void EditorUpdate()
		{
			_ImgTPGaugeArr = transform.FindAllRecurcive<Image>("fill", true);
			_TxtPoint = transform.FindRecurcive<Text>("point", true);
			_UnitCardArr = GetComponentsInChildren<UnitCard>();
		}
#endif
	}
}




