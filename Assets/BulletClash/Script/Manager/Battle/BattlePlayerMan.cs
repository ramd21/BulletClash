using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BattlePlayerMan : Singleton<BattlePlayerMan>
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
				BattleUIMan.i.UpdateTPUI(cur);
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

	[System.Serializable]
	public class Player
	{
		public int _Id;
		public UnitType[] _DeckUnitTypeArr;
		public int _TPTimerTotal;

		public Player(int aId)
		{
			_Id = aId;
		}

		public int GetTP()
		{
			return _TPTimerTotal / BattleGameMan.i._TPTimer;
		}

		public void PlaceUnit(UnitParam aParam, Vector2Int aPos)
		{
			_TPTimerTotal -= aParam.Cost * BattleGameMan.i._TPTimer;
			Unit unit = BattleCharaMan.i.GetPoolOrNewUnit(_Id, aParam.Type);
			unit.ActivateReq(aPos);
		}

		public void AI()
		{
			if (GetTP() >= 5)
			{
				if (_Id == 1)
				{
					Vector2Int pos = new Vector2Int(Random.Range(200, BattleFieldMan.i._Size.x - 200), Random.Range((BattleFieldMan.i._Size.y / 4) * 3 + 200, BattleFieldMan.i._Size.y - 200));
					int rand = Random.Range(0, 4);
					PlaceUnit(MasterMan.i._UnitParam[rand], pos);
				}
				else
				{
					Vector2Int pos = new Vector2Int(Random.Range(200, BattleFieldMan.i._Size.x - 200), Random.Range((BattleFieldMan.i._Size.y / 4) - 200, 200));
					int rand = Random.Range(0, 4);
					PlaceUnit(MasterMan.i._UnitParam[rand], pos);
				}
			}
		}

		public void Act()
		{
			if (_TPTimerTotal < BattleGameMan.i._TPMax * BattleGameMan.i._TPTimer)
				_TPTimerTotal++;

			//if (_Id == 1)
			{
				AI();
			}
		}
	}
}




