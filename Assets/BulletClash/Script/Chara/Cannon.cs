using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class Cannon : RMBehaviour
	{
		public Chara _Chara;

		public int _FireCnt;
		public int _FireInter;
		public int _LoopInter;

		public int _WayCnt;
		public int _WayDegInter;


		int _FireCntCur;
		int _FireInterCur;
		int _LoopInterCur;

		public void Fire()
		{
			if (_LoopInterCur == 0)
			{
				_FireCntCur = _FireCnt;
				_FireInterCur = _FireInter;
				_LoopInterCur = _LoopInter;
			}

			if (_FireInterCur == 0)
			{
				if (_FireCntCur != 0)
				{
					int deg;
					if (_Chara._PlayerId == 0)
						deg = 0;
					else
						deg = 180 * BattleGameMan.cDistDiv;

					int offset = (_WayCnt - 1) * _WayDegInter / 2;
					deg -= offset;

					for (int i = 0; i < _WayCnt; i++)
					{
						Bullet b = BattleCharaMan.i.GetPoolOrNewBullet(_Chara._PlayerId, BulletType.shot);
						deg = AngleMath.RoundTo360(deg);
						b.ActivateReq(_Chara._Tra._Pos, new Vector2Int(SinTable.GetX(deg), CosTable.GetY(deg)));
						deg += _WayDegInter;
					}

					_FireCntCur--;
				}
				_FireInterCur = _FireInter;
			}

			_FireInterCur--;
			_LoopInterCur--;
		}
	}
}


