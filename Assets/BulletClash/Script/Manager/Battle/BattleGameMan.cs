using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using ExitGames.Client.Photon;


namespace BC
{
	public class BattleGameMan : Singleton<BattleGameMan>
	{
		public int _TPMax;
		public int _TPTimer;

		public const int cDistDiv = 100;


		public bool _BattleStarted;

		public int _FrameCur;
		public int _FrameHead;
		public int _FrameExpected;
		public int _FrameDelta;

		public int _FrameMax;

		public int _frameRemain { get { return _FrameMax - _FrameCur; } }

		public int _StartTime;


		public struct FrameData
		{
			public Player.FrameData[]	_PlayerDataArr;
			public Unit.FrameData[][]	_UnitDataList;
			public Bullet.FrameData[][]	_BulletDataList;
			public Tower.FrameData[][]	_TowerDataList;

			public FrameData
			(
				Player[] aPlayerArr, 
				List<Unit>[] aUnitList, 
				List<Bullet>[] aBulletList, 
				List<Tower>[] aTowerList
			)
			{
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

			Random.InitState(0);
			BattleUIMan.i.Init();
			BattlePlayerMan.i.Init();
			BattleCharaMan.i.Init();
			BattleCollMan.i.Init();

			_FrameData = new FrameData[_FrameMax];

			PhotonMan.i.Connect();
		}

		void FixedUpdate()
		{
			if (!_BattleStarted)
				return;

			float frame = 1f / 60f;
			float elapsed = (float)(PhotonNetwork.ServerTimestamp - _StartTime) / 1000f;

			_FrameExpected = (int)(elapsed / frame) - 60 * 3;

			while (true)
			{
				_FrameDelta = _FrameExpected - _FrameCur;

				if (_FrameDelta > 0)
				{
					//_FrameDeltaがプラス
					//遅れている
					BattleGameMain();
				}
				else
				{
					break;	
				}
			}
		}

		void BattleGameMain()
		{
			if (_FrameCur > _FrameHead)
				_FrameCur = _FrameHead;

			if (_FrameCur < _FrameHead)
			{
				if (_FrameCur < 0)
					_FrameCur = 0;

				_FrameData[_FrameCur].Restore();
			}
			else
			{
				Random.InitState(_FrameCur);

				_FrameData[_FrameCur] = new FrameData
				(
					BattlePlayerMan.i._PlayerArr,
					BattleCharaMan.i._UnitList,
					BattleCharaMan.i._BulletList,
					BattleCharaMan.i._TowerList
				);

				BattlePlayerMan.i.Act();
				BattleCharaMan.i.Act();

				_FrameCur++;
				_FrameHead++;
			}
		}

		void Update()
		{
			//if (!_Init)
			//	return;

			BattleCharaMan.i.UpdateView();
		}
	}
}




