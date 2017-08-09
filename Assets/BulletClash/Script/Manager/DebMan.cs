using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class DebMan : Singleton<DebMan>
	{
		public Text _TxtFps;
		public Text _TxtDeb;
		public Button _BtnDeb;


		FastList<string> _DebTxt = new FastList<string>(20, 10);


		public float _FPSInter = 0.5f;

		float _Accum; // FPS accumulated over the interval
		int _Frame; // Frames drawn over the interval
		float _Time; // Left time for current interval

		protected override void Awake()
		{
			_Time = _FPSInter;

			_BtnDeb.onClick.AddListener(OnClickDeb);
		}

		void OnClickDeb()
		{
			_TxtDeb.gameObject.SetActive(!_TxtDeb.gameObject.activeSelf);
		}

		public void AddDebTxt(string aStrDeb)
		{
			_DebTxt.Add(aStrDeb);
		}

		void Update()
		{
			_Time -= Time.deltaTime;
			_Accum += Time.timeScale / Time.deltaTime;
			++_Frame;

			// Interval ended - update GUI text and start new interval
			if (_Time <= 0.0)
			{
				float fps = _Accum / _Frame;

				// display two fractional digits (f2 format)
				_TxtFps.text = "fps:" + fps.ToString("f2");


				if (fps < 45)
				{
					_TxtFps.color = Color.yellow;
				}
				else if (fps < 30)
				{
					_TxtFps.color = Color.red + Color.yellow / 2;
				}
				else if (fps < 15)
				{
					_TxtFps.color = Color.red;
				}
				else
				{
					_TxtFps.color = Color.green;
				}

				_Time = _FPSInter;
				_Accum = 0.0f;
				_Frame = 0;
			}
		}

		void LateUpdate()
		{
			string strDeb = "";

			if (BattleGameMan.i)
				strDeb += BattleGameMan.i._StartTime + "\n";

			strDeb += PhotonNetwork.countOfPlayers + "\n";

			if (BattleGameMan.i)
				strDeb += BattleGameMan.i._FrameCur + "\n";
			if (BattleGameMan.i)
				strDeb += BattleGameMan.i._FrameExpected + "\n";
			strDeb += PhotonNetwork.connected + "\n";
			strDeb += PhotonNetwork.room + "\n";


			for (int i = 0; i < _DebTxt.Count; i++)
			{
				strDeb += _DebTxt[i] + "\n";
			}

			_TxtDeb.text = strDeb;

			_DebTxt.Clear();
		}
	}
}






