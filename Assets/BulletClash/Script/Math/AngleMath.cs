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

				aDeg += 360 * GameMan.cDistDiv;
			}

			return aDeg % (360 * GameMan.cDistDiv);
		}

		static public int RoundToPlusMinus180(int aDeg)
		{
			while (true)
			{
				if (aDeg < 180 * GameMan.cDistDiv)
					break;

				aDeg -= 360 * GameMan.cDistDiv;
			}

			while (true)
			{
				if (-180 * GameMan.cDistDiv <= aDeg)
					break;

				aDeg += 360 * GameMan.cDistDiv;
			}

			return aDeg;
		}

		static public int MoveTowardsAngle360(int aCurDeg, int aTageDeg, int aMaxChange)
		{
			int diff = aTageDeg - aCurDeg;

			if (diff <= -180 * GameMan.cDistDiv)
			{
				diff += 360 * GameMan.cDistDiv;
			}

			if (diff > 180 * GameMan.cDistDiv)
			{
				diff -= 360 * GameMan.cDistDiv;
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


