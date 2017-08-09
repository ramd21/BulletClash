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
		public Text _TxtTp;
		public Text _TxtTimer;
		public Text _TxtCountDown;
		public Text _Txtwaiting;

		public Animator _Animator;


		public UnitCard[] _UnitCardArr;
		public List<UnitCard> _ShuffledList;

		public Button _BtnSetting;

		public Transform[] _TraPosArr;

		public bool _CountDownEnd;

		void Start()
		{
			_BtnSetting.onClick.AddListener(() =>
			{
				UIMan.i.OpenCommonDialog("リタイア", "リタイアしますか？",
				"はい",
				(d) =>
				{
					UIMan.i.GoToScene("home");
					UIMan.i.OpenUI("home", true);
					d.Close();
				},
				"いいえ",
				(d) =>
				{
					d.Close();
				});
			});
		}

		public void Init()
		{
			InitTPGauge();
			InitTPCnt();
			InitTimer();
			
			InitUnitCard();

			
		}

		public void StartCountDown()
		{
			_Txtwaiting.gameObject.SetActive(false);
			InitCountDown();
		}

		void InitUnitCard()
		{
			for (int i = 0; i < 8; i++)
			{
				_UnitCardArr[i].Init(BattlePlayerMan.i._myPlayer._DeckUnitTypeArr[i]);
			}

			_ShuffledList = new List<UnitCard>();

			while (true)
			{
				int rand = UnityEngine.Random.Range(0, 8);

				if (!_ShuffledList.Find((a) => a._Id == rand))
					_ShuffledList.Add(_UnitCardArr[rand]);

				if (_ShuffledList.Count == 8)
					break;
			}

			_ShuffledList[0].SetPosId(0);
			_ShuffledList[1].SetPosId(1);
			_ShuffledList[2].SetPosId(2);
			_ShuffledList[3].SetPosId(3);
			_ShuffledList[4].SetPosId(4);
			_ShuffledList[5].SetPosId(5);
			_ShuffledList[6].SetPosId(5);
			_ShuffledList[7].SetPosId(5);
		}

		void InitTimer()
		{
			_TxtTimer.text = "3:00";
			this.StartObsserve(() => BattleGameMan.i._frameRemain / 60,
			(cur, last) =>
			{
				_TxtTimer.text = (cur / 60).ToString() + ":" + (cur % 60).ToString("D2");
				_TxtTimer.transform.DOScale(1.5f, 0.25f).OnComplete(() =>
				{
					_TxtTimer.transform.DOScale(1f, 0.25f);
				});

			}, true);
		}

		void InitCountDown()
		{
			CountDown(3);
		}

		void CountDown(int aCnt)
		{
			_TxtCountDown.text = aCnt.ToString();
			_TxtCountDown.transform.localScale = Vector3.one * 3;
			_TxtCountDown.transform.DOScale(1f, 1f).OnComplete(() =>
			{
				int next = --aCnt;

				if (next == 0)
				{
					_Animator.enabled = true;
					BattleGameMan.i._BattleStarted = true;
					_TxtCountDown.gameObject.SetActive(false);
				}
				else
				{ 
					CountDown(next);
				}
			});
		}

		void CountDownEnd()
		{
			_CountDownEnd = true;
		}

		void InitTPCnt()
		{
			_TxtTp.text = "0";
			this.StartObsserve(() => BattlePlayerMan.i._myPlayer._TPTimerTotal / BattleGameMan.i._TPTimer,
			(cur, last) =>
			{
				_TxtTp.text = cur.ToString();
				_TxtTp.transform.DOScale(1.5f, 0.25f).OnComplete(() =>
				{
					_TxtTp.transform.DOScale(1f, 0.25f);
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
			//_TxtTp = transform.FindRecurcive<Text>("point", true);
			_UnitCardArr = GetComponentsInChildren<UnitCard>();
		}
#endif
	}
}




