using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	[System.Serializable]
	public struct Player
	{
		public List<Ship>	_ShipList;
		public int			_TacticsPoint;
	}

	public class PlayerMan : Singleton<PlayerMan>
	{

		public Player[] _PlayerArr = new Player[2];

		public void Init()
		{
			_PlayerArr[0] = new Player();
			_PlayerArr[1] = new Player();

			this.StartObsserve(()=>_PlayerArr[0]._TacticsPoint,
			(cur, last)=> 
			{
				UIMan.i.SetTacticsPoint(cur);
			}, true);
		}

		public void Act()
		{
			for (int i = 0; i < _PlayerArr.Length; i++)
			{
				if (_PlayerArr[i]._TacticsPoint < GameMan.i._MaxTacticsPoint)
				{
					_PlayerArr[i]._TacticsPoint++;
				}
			}
		}
	}
}


