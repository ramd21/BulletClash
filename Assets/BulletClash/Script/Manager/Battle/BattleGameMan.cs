using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class BattleGameMan : Singleton<BattleGameMan>
	{
		public int _TPMax;
		public int _TPTimer;

		public const int cDistDiv = 100;

		bool _Init;

		public int _Frame;

		public int _FrameHead;

		public struct FrameData
		{
			public int _Frame;

			public Random.State _RandSeed;

			public Player.FrameData[]	_PlayerDataArr;
			public Unit.FrameData[][]	_UnitDataList;
			public Bullet.FrameData[][]	_BulletDataList;
			public Tower.FrameData[][]	_TowerDataList;

			public FrameData
			(
				int aFrame,
				Random.State aRandSeed,
				Player[] aPlayerArr, 
				List<Unit>[] aUnitList, 
				List<Bullet>[] aBulletList, 
				List<Tower>[] aTowerList
			)
			{
				_Frame = aFrame;
				_RandSeed = aRandSeed;
				_PlayerDataArr = new Player.FrameData[2];
				for (int i = 0; i < 2; i++)
				{
					_PlayerDataArr[i] = aPlayerArr[i].GetFrameData();
				}

				int len;

				_BulletDataList = new Bullet.FrameData[2][];
				_BulletDataList[0] = new Bullet.FrameData[aBulletList[0].Count];
				_BulletDataList[1] = new Bullet.FrameData[aBulletList[1].Count];

				_UnitDataList = new Unit.FrameData[2][];
				_UnitDataList[0] = new Unit.FrameData[aUnitList[0].Count];
				_UnitDataList[1] = new Unit.FrameData[aUnitList[1].Count];

				_TowerDataList = new Tower.FrameData[2][];
				_TowerDataList[0] = new Tower.FrameData[aTowerList[0].Count];
				_TowerDataList[1] = new Tower.FrameData[aTowerList[1].Count];

				for (int i = 0; i < 2; i++)
				{
					len = aBulletList[i].Count;
					for (int j = 0; j < len; j++)
					{
						_BulletDataList[i][j] = aBulletList[i][j].GetFrameData();
					}

					len = aUnitList[i].Count;
					for (int j = 0; j < len; j++)
					{
						_UnitDataList[i][j] = aUnitList[i][j].GetFrameData();
					}

					len = aTowerList[i].Count;
					for (int j = 0; j < len; j++)
					{
						_TowerDataList[i][j] = aTowerList[i][j].GetFrameData();
					}
				}
			}

			public void Restore()
			{
				Random.state = _RandSeed;
				int len;
				for (int i = 0; i < 2; i++)
				{
					len = BattleCharaMan.i._BulletList[i].Count;
					for (int j = 0; j < len; j++)
					{
						BattleCharaMan.i._BulletList[i][j]._State = ActiveState.inactive;
						BattleCharaMan.i._BulletList[i][j].gameObject.SetActive(false);
					}

					len = BattleCharaMan.i._UnitList[i].Count;
					for (int j = 0; j < len; j++)
					{
						BattleCharaMan.i._UnitList[i][j]._State = ActiveState.inactive;
						BattleCharaMan.i._UnitList[i][j].gameObject.SetActive(false);
					}

					len = BattleCharaMan.i._TowerList[i].Count;
					for (int j = 0; j < len; j++)
					{
						BattleCharaMan.i._TowerList[i][j]._State = ActiveState.inactive;
						BattleCharaMan.i._TowerList[i][j].gameObject.SetActive(false);
					}
				}


				for (int i = 0; i < 2; i++)
				{
					BattlePlayerMan.i._PlayerArr[i].Restore(_PlayerDataArr[i]);

					len = _BulletDataList[i].Length;
					for (int j = 0; j < len; j++)
					{
						BattleCharaMan.i._BulletList[i][j].Restore(_BulletDataList[i][j]);
					}

					len = _UnitDataList[i].Length;
					for (int j = 0; j < len; j++)
					{
						BattleCharaMan.i._UnitList[i][j].Restore(_UnitDataList[i][j]);
					}

					len = _TowerDataList[i].Length;
					for (int j = 0; j < len; j++)
					{
						BattleCharaMan.i._TowerList[i][j].Restore(_TowerDataList[i][j]);
					}
				}
			}
		}

		public FrameData[] _FrameData;


		void Start()
		{
			SinTable.Init();
			CosTable.Init();

			BattleUIMan.i.Init();
			BattlePlayerMan.i.Init();
			BattleCharaMan.i.Init();
			BattleCollMan.i.Init();

			_FrameData = new FrameData[60 * 60 * 3];


			_Init = true;
		}

		void FixedUpdate()
		{
			if (!_Init)
				return;

			if (_Frame > _FrameHead)
				_Frame = _FrameHead;

			if (_Frame < _FrameHead)
			{
				if (_Frame < 0)
					_Frame = 0;

				_FrameData[_Frame].Restore();
			}
			else
			{
				_FrameData[_Frame] = new FrameData
				(
					_Frame,
					Random.state,
					BattlePlayerMan.i._PlayerArr,
					BattleCharaMan.i._UnitList,
					BattleCharaMan.i._BulletList,
					BattleCharaMan.i._TowerList
				);

				BattlePlayerMan.i.Act();
				BattleCharaMan.i.Act();

				_Frame++;
				_FrameHead++;
			}
		}

		void Update()
		{
			if (!_Init)
				return;

			BattleCharaMan.i.UpdateView();
		}
	}
}




