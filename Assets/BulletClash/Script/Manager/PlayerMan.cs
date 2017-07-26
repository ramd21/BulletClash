using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	[System.Serializable]
	public class Player
	{
		public int _Id;
		public UnitType[]	_DeckUnitTypeArr;
		public int			_TPTimerTotal;

		public Player(int aId)
		{
			_Id = aId;
		}

		public int GetTP()
		{
			return _TPTimerTotal / GameMan.i._TPTimer;
		}

		public void PlaceUnit(UnitParam aParam, Vector2 aPos)
		{
			_TPTimerTotal -= aParam.Cost * GameMan.i._TPTimer;
			Unit unit = CharaMan.i.GetPoolOrNewUnit(_Id, aParam.Type);
			unit.ActivateReq(aPos);
		}

		public void AI()
		{
			if (GetTP() >= 3)
			{
				Vector2 pos = new Vector2(Random.Range(200, FieldMan.i._Size.x - 200), Random.Range((FieldMan.i._Size.y / 4) * 3 + 200, FieldMan.i._Size.y - 200));
				PlaceUnit(MasterMan.i._UnitParam[0], pos);
			}
		}

		public void Act()
		{
			if (_TPTimerTotal < GameMan.i._TPMax * GameMan.i._TPTimer)
				_TPTimerTotal++;

			if (_Id == 1)
			{
				AI();
			}
		}
	}

	public class PlayerMan : Singleton<PlayerMan>
	{
		public int _MyPlayerId;
		public Player[] _PlayerArr = new Player[2];
		public Player _myPlayer { get { return _PlayerArr[_MyPlayerId]; } }

		public void Init()
		{
			_PlayerArr[0] = new Player(0);
			_PlayerArr[1] = new Player(1);

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
				_PlayerArr[i].Act();
			}
		}
	}
}




