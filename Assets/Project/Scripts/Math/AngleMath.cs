using UnityEngine;
using System.Collections;
using System;

namespace BC
{
	public static class AngleMath
	{
		static public int RoundTo360(int aDeg)
		{
			while (true)
			{
				if (0 <= aDeg)
					break;

				aDeg += 360 * BattleGameMan.cDistDiv;
			}

			return aDeg % (360 * BattleGameMan.cDistDiv);
		}

		static public int RoundToPlusMinus180(int aDeg)
		{
			while (true)
			{
				if (aDeg < 180 * BattleGameMan.cDistDiv)
					break;

				aDeg -= 360 * BattleGameMan.cDistDiv;
			}

			while (true)
			{
				if (-180 * BattleGameMan.cDistDiv <= aDeg)
					break;

				aDeg += 360 * BattleGameMan.cDistDiv;
			}

			return aDeg;
		}

		static public int MoveTowardsAngle360(int aCurDeg, int aTageDeg, int aMaxChange)
		{
			int diff = aTageDeg - aCurDeg;

			if (diff <= -180 * BattleGameMan.cDistDiv)
			{
				diff += 360 * BattleGameMan.cDistDiv;
			}

			if (diff > 180 * BattleGameMan.cDistDiv)
			{
				diff -= 360 * BattleGameMan.cDistDiv;
			}

			int change;
			if (Math.Abs(diff) < aMaxChange)
			{
				change = diff;
			}
			else
			{
				if (diff < 0)
					change = -aMaxChange;
				else
					change = aMaxChange;
			}
			return RoundTo360(aCurDeg + change);
		}
	}
}


