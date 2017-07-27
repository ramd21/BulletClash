using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BC
{
	public class CosTable
	{
		static int[] gYArr;
		static public void Init()
		{
			gYArr = new int[360 * GameMan.cDistDiv];

			for (int i = 0; i < 360 * GameMan.cDistDiv; i++)
			{
				double deg = i / GameMan.cDistDiv;
				double rad = deg * Mathf.Deg2Rad;
				double valD = Math.Cos(rad);
				int valI = (int)(valD * GameMan.cDistDiv);
				gYArr[i] = valI;
			}
		}

		static public int GetY(int aDeg)
		{
			return gYArr[aDeg];
		}
	}
}


