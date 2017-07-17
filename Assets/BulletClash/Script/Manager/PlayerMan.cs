using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	[System.Serializable]
	public struct Player
	{
		public UnitType[]	_DeckUnitTypeArr;
		public int			_TPTimerTotal;
	}

	public class PlayerMan : Singleton<PlayerMan>
	{
		public int _MyPlayerId;
		public Player[] _PlayerArr = new Player[2];
		public Player _myPlayer { get { return _PlayerArr[_MyPlayerId]; } }

		public void Init()
		{
			_PlayerArr[0] = new Player();
			_PlayerArr[1] = new Player();

			this.StartObsserve(() => _myPlayer._TPTimerTotal,
			(cur, last) =>
			{
				UIMan.i.UpdateTPUI(cur);
			}, true);
		}

		public void Act()
		{
			for (int i = 0; i < _PlayerArr.Length; i++)
			{
				if (_PlayerArr[i]._TPTimerTotal < GameMan.i._TPMax * GameMan.i._TPTimer)
				{
					_PlayerArr[i]._TPTimerTotal++;
				}
			}
		}
	}
}




