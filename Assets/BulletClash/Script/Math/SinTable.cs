using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BC
{
	public class SinTable
	{
		static int[] gXArr;
		static public void Init()
		{
			gXArr = new int[360 * GameMan.cDistDiv];

			for (int i = 0; i < 360 * GameMan.cDistDiv; i++)
			{
				double deg = i / GameMan.cDistDiv;
				double rad = deg * Mathf.Deg2Rad;
				double valD = Math.Sin(rad);
				int valI = (int)(valD * GameMan.cDistDiv);
				gXArr[i] = valI;
			}
		}

		static public int GetX(int aDeg)
		{
			return gXArr[aDeg];
		}
	}
}


