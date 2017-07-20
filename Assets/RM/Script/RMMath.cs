using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace RM
{
	public static class RMMath
	{
		static public int GetFigureCnt(int aVal)
		{
			int fig = 0;
			while (true)
			{
				aVal /= 10;
				if (aVal == 0)
					return fig + 1;

				fig++;
			}
		}

		static public int GetFigure(int aVal, int aFigurePos)
		{
			for (int i = 0; i < aFigurePos - 1; i++)
			{
				aVal /= 10;
			}

			return aVal % 10;
		}

		static public int CompareInt(int aIA, int aIB)
		{
			return aIA - aIB;
		}

		static List<int> g32BitMaskList;
		/// <summary>
		///	1	1
		///	2	3
		///	3	7
		///	4	15
		///	5	31
		///	6	63
		///	7	127
		///	8	255
		///	9	511
		///	10	1023
		///	11	2047
		///	12	4095
		///	13	8191
		///	14	16383
		///	15	32767
		///	16	65535
		///	17	131071
		///	18	262143
		///	19	524287
		///	20	1048575
		///	21	2097151
		///	22	4194303
		///	23	8388607
		///	24	16777215
		///	25	33554431
		///	26	67108863
		///	27	134217727
		///	28	268435455
		///	29	536870911
		///	30	1073741823
		///	31	2147483647
		///	32	-1
		/// </summary>
		/// <param name="aBitCnt"></param>
		/// <returns></returns>
		static public long Get32BitMask(int aBitCnt)
		{
			if (g32BitMaskList == null)
			{
				g32BitMaskList = new List<int>();

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < 32; i++)
				{
					int n = 0;

					int oneCnt = i + 1;
					int zeroCnt = 64 - oneCnt;

					for (int j = 0; j < zeroCnt; j++)
						sb.Append("0");

					for (int k = 0; k < oneCnt; k++)
						sb.Append("1");

					n = Convert.ToInt32(sb.ToString(), 2);
					g32BitMaskList.Add(n);
				}

			}
			return g64BitMaskList[aBitCnt - 1];
		}

		static List<long> g64BitMaskList;
		/// <summary>
		///	1	1
		///	2	3
		///	3	7
		///	4	15
		///	5	31
		///	6	63
		///	7	127
		///	8	255
		///	9	511
		///	10	1023
		///	11	2047
		///	12	4095
		///	13	8191
		///	14	16383
		///	15	32767
		///	16	65535
		///	17	131071
		///	18	262143
		///	19	524287
		///	20	1048575
		///	21	2097151
		///	22	4194303
		///	23	8388607
		///	24	16777215
		///	25	33554431
		///	26	67108863
		///	27	134217727
		///	28	268435455
		///	29	536870911
		///	30	1073741823
		///	31	2147483647
		///	32	4294967295
		///	33	8589934591
		///	34	17179869183
		///	35	34359738367
		///	36	68719476735
		///	37	137438953471
		///	38	274877906943
		///	39	549755813887
		///	40	1099511627775
		///	41	2199023255551
		///	42	4398046511103
		///	43	8796093022207
		///	44	17592186044415
		///	45	35184372088831
		///	46	70368744177663
		///	47	140737488355327
		///	48	281474976710655
		///	49	562949953421311
		///	50	1125899906842623
		///	51	2251799813685247
		///	52	4503599627370495
		///	53	9007199254740991
		///	54	18014398509481983
		///	55	36028797018963967
		///	56	72057594037927935
		///	57	144115188075855871
		///	58	288230376151711743
		///	59	576460752303423487
		///	60	1152921504606846975
		///	61	2305843009213693951
		///	62	4611686018427387903
		///	63	9223372036854775807
		///	64	-1
		/// </summary>
		/// <param name="aBitCnt"></param>
		/// <returns></returns>
		static public long Get64BitMask(int aBitCnt)
		{
			if (g64BitMaskList == null)
			{
				g64BitMaskList = new List<long>();

				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < 64; i++)
				{
					long lon = 0;
					int oneCnt = i + 1;
					int zeroCnt = 64 - oneCnt;

					for (int j = 0; j < zeroCnt; j++)
						sb.Append("0");

					for (int k = 0; k < oneCnt; k++)
						sb.Append("1");

					lon = Convert.ToInt64(sb.ToString(), 2);
					g64BitMaskList.Add(lon);
				}
			}
			return g64BitMaskList[aBitCnt - 1];
		}

		static public int BoolArrayToInt(bool[] aBoolArr, bool aSign)
		{
			int ret = 0;
			if (aSign && aBoolArr[aBoolArr.Length - 1])
			{
				for (int i = 0; i < aBoolArr.Length; i++)
				{
					if (!aBoolArr[i])
						ret += 1 << i;
				}
				ret = -ret;
				ret--;
			}
			else
			{
				for (int i = 0; i < aBoolArr.Length; i++)
				{
					if (aBoolArr[i])
						ret += 1 << i;
				}
			}
			return ret;
		}

		//ok
		static public long BoolArrayToLong(bool[] aBoolArr, bool aSign)
		{
			long ret = 0;

			if (aSign && aBoolArr[aBoolArr.Length - 1])
			{
				for (int i = 0; i < aBoolArr.Length; i++)
				{
					if (!aBoolArr[i])
						ret += 1L << i;
				}

				ret = -ret;
				ret--;
			}
			else
			{
				for (int i = 0; i < aBoolArr.Length; i++)
				{
					if (aBoolArr[i])
						ret += 1L << i;
				}
			}

			return ret;
		}

		static public bool[] IntToBoolArray(int aVal, int aBitCnt)
		{
			bool[] boolArr = new bool[aBitCnt];
			for (int i = 0; i < aBitCnt; i++)
			{
				int mask = 1 << i;
				int chk = aVal & mask;
				boolArr[i] = chk == mask;
			}

			return boolArr;
		}

		//ok
		static public bool[] LongToBoolArray(long aVal, int aBitCnt)
		{
			bool[] boolArr = new bool[aBitCnt];
			for (int i = 0; i < aBitCnt; i++)
			{
				long mask = 1L << i;
				long chk = aVal & mask;
				boolArr[i] = chk == mask;
			}
			return boolArr;
		}

		static public long IntArrayToLong(int[] aIntArr, int[] aBitCntArr)
		{
			bool[] toLong = new bool[64];
			int bitCnt = 0;
			for (int i = 0; i < aIntArr.Length; i++)
			{
				bool[] fromInt = IntToBoolArray(aIntArr[i], aBitCntArr[i]);
				for (int j = 0; j < fromInt.Length; j++)
				{
					toLong[bitCnt] = fromInt[j];
					bitCnt++;
				}
			}
			return BoolArrayToLong(toLong, true);
		}

		static public int[] LongToIntArray(long aLong, int[] aBitCntArr, bool[] aSignArr)
		{
			int[] intArr = new int[aBitCntArr.Length];
			bool[] fromLong = LongToBoolArray(aLong, 64);

			int bitCnt = 0;
			for (int i = 0; i < aBitCntArr.Length; i++)
			{
				bool[] toInt = new bool[aBitCntArr[i]];
				for (int j = 0; j < toInt.Length; j++)
				{
					toInt[j] = fromLong[bitCnt];
					bitCnt++;
				}
				intArr[i] = BoolArrayToInt(toInt, aSignArr[i]);
			}
			return intArr;
		}

		static public string LongTo64BitStr(long aLong)
		{
			string strBit = Convert.ToString(aLong, 2);
			StringBuilder sb = StringExtension.gStringBuilder;
			sb.Length = 0;
			for (int i = 0; i < 64 - strBit.Length; i++)
			{
				sb.Append("0");
			}
			strBit = sb.Append(strBit).ToString();
			return strBit;
		}

		static public string IntTo32BitStr(int aN)
		{
			string strBit = Convert.ToString(aN, 2);
			StringBuilder sb = StringExtension.gStringBuilder;
			sb.Length = 0;
			for (int i = 0; i < 32 - strBit.Length; i++)
			{
				sb.Append("0");
			}
			strBit = sb.Append(strBit).ToString();
			return strBit;
		}

		static public int BitStrToInt(string aStrBit, bool aSign)
		{
			int ret;
			if (aSign)
			{
				bool minus = aStrBit[0] == '1';
				aStrBit = aStrBit.Remove(0, 1);

				ret = Convert.ToInt32(aStrBit, 2);
				if (minus)
					ret *= -1;
			}
			else
			{
				ret = Convert.ToInt32(aStrBit, 2);
			}

			return ret;
		}

		static public string IntToBitStr(int aInt, int aBitCnt, bool aSign)
		{
			int bitcnt = aBitCnt;
			if (aSign)
				bitcnt--;

			int val = Mathf.Abs(aInt);
			string strBit = Convert.ToString(val, 2);

			StringBuilder sb = StringExtension.gStringBuilder;
			sb.Length = 0;
			int zeroCnt = bitcnt - strBit.Length;

			for (int i = 0; i < zeroCnt; i++)
			{
				sb.Append("0");
			}

			if (aSign)
			{
				if (aInt < 0)
					return "1".Append(sb.ToString(), strBit);
				else
					return "0".Append(sb.ToString(), strBit);
			}
			else
			{
				return sb.Append(strBit).ToString();
			}
		}

		static public string LongToBitStr(long aLong, int aBitCnt, bool aSign)
		{
			int bitcnt = aBitCnt;
			if (aSign)
				bitcnt--;

			long absval = Math.Abs(aLong);
			string strBit = Convert.ToString(absval, 2);

			StringBuilder sb = StringExtension.gStringBuilder;
			sb.Length = 0;
			int zeroCnt = bitcnt - strBit.Length;

			for (int i = 0; i < zeroCnt; i++)
			{
				sb.Append("0");
			}

			if (aSign)
			{
				if (aLong < 0)
					return "1".Append(sb.ToString(), strBit);
				else
					return "0".Append(sb.ToString(), strBit);
			}
			else
			{
				return sb.Append(strBit).ToString();
			}
		}

		static public int RoundTo360(int aDeg)
		{
			while (true)
			{
				if (aDeg < 360)
					break;

				aDeg -= 360;
			}

			while (true)
			{
				if (0 <= aDeg)
					break;

				aDeg += 360;
			}

			return aDeg;
		}

		static public float RoundTo360(float aDeg)
		{
			while (true)
			{
				if (aDeg < 360f)
					break;

				aDeg -= 360f;
			}

			while (true)
			{
				if (0f <= aDeg)
					break;

				aDeg += 360f;
			}

			return aDeg;
		}

		static public double RoundTo360(double aDeg)
		{
			while (true)
			{
				if (aDeg < 360.0)
					break;

				aDeg -= 360.0;
			}

			while (true)
			{
				if (0.0 <= aDeg)
					break;

				aDeg += 360.0;
			}

			return aDeg;
		}

		static public int MoveTowards(int aCurrent, int aTarget, int aMaxChange)
		{
			if (aCurrent < aTarget)
			{
				aCurrent += Math.Abs(aMaxChange);
				if (aCurrent > aTarget)
					aCurrent = aTarget;
			}

			if (aCurrent > aTarget)
			{
				aCurrent -= Math.Abs(aMaxChange);
				if (aCurrent < aTarget)
					aCurrent = aTarget;
			}

			return aCurrent;
		}

		static public float MoveTowards(float aCurrent, float aTarget, float aMaxChange)
		{
			if (aCurrent < aTarget)
			{
				aCurrent += Math.Abs(aMaxChange);
				if (aCurrent > aTarget)
					aCurrent = aTarget;
			}

			if (aCurrent > aTarget)
			{
				aCurrent -= Math.Abs(aMaxChange);
				if (aCurrent < aTarget)
					aCurrent = aTarget;
			}

			return aCurrent;
		}

		static public double MoveTowards(double aCurrent, double aTarget, double aMaxChange)
		{
			if (aCurrent < aTarget)
			{
				aCurrent += Math.Abs(aMaxChange);
				if (aCurrent > aTarget)
					aCurrent = aTarget;
			}

			if (aCurrent > aTarget)
			{
				aCurrent -= Math.Abs(aMaxChange);
				if (aCurrent < aTarget)
					aCurrent = aTarget;
			}

			return aCurrent;
		}

		static public int Lerp(int aCurrent, int aTarget, double aRatio)
		{
			double diff = aTarget - aCurrent;
			aCurrent = aCurrent + (int)(diff * aRatio);
			return aCurrent;
		}
		static public float Lerp(float aCurrent, float aTarget, double aRatio)
		{
			double diff = aTarget - aCurrent;
			aCurrent = aCurrent + (float)(diff * aRatio);
			return aCurrent;
		}

		static public double Lerp(double aCurrent, double aTarget, double aRatio)
		{
			double diff = aTarget - aCurrent;
			aCurrent = aCurrent + (diff * aRatio);
			return aCurrent;
		}

		static System.Random gRand = new System.Random();
		static public int RandomRangeRestoreSeed(int aMin, int aMax)
		{
			UnityEngine.Random.State seedOld = UnityEngine.Random.state;
			UnityEngine.Random.InitState(gRand.Next());
			int rand = UnityEngine.Random.Range(aMin, aMax);
			UnityEngine.Random.state = seedOld;
			return rand;
		}

		static public float RandomRangeRestoreSeed(float aMin, float aMax)
		{
			UnityEngine.Random.State seedOld = UnityEngine.Random.state;
			UnityEngine.Random.InitState(gRand.Next());
			float rand = UnityEngine.Random.Range(aMin, aMax);
			UnityEngine.Random.state = seedOld;
			return rand;
		}


		public static int GetApproxDist(int aX1, int aY1, int aX2, int aY2)
		{
			// 精度はあまり高めでないが、高速で近似値を計算できる.
			int dx, dy;
			if ((dx = (aX1 > aX2) ? aX1 - aX2 : aX2 - aX1) < (dy = (aY1 > aY2) ? aY1 - aY2 : aY2 - aY1))
			{
				return (((dy << 8) + (dy << 3) - (dy << 4) - (dy << 1) +
						(dx << 7) - (dx << 5) + (dx << 3) - (dx << 1)) >> 8);
			}
			else
			{
				return (((dx << 8) + (dx << 3) - (dx << 4) - (dx << 1) +
						(dy << 7) - (dy << 5) + (dy << 3) - (dy << 1)) >> 8);
			}
		}

		public static int GetApproxDist(Vector2Int aV2A, Vector2Int aV2B)
		{
			// 精度はあまり高めでないが、高速で近似値を計算できる.
			int dx, dy;
			if ((dx = (aV2A.x > aV2B.x) ? aV2A.x - aV2B.x : aV2B.x - aV2A.x) < (dy = (aV2A.y > aV2B.y) ? aV2A.y - aV2B.y : aV2B.y - aV2A.y))
			{
				return (((dy << 8) + (dy << 3) - (dy << 4) - (dy << 1) +
						(dx << 7) - (dx << 5) + (dx << 3) - (dx << 1)) >> 8);
			}
			else
			{
				return (((dx << 8) + (dx << 3) - (dx << 4) - (dx << 1) +
						(dy << 7) - (dy << 5) + (dy << 3) - (dy << 1)) >> 8);
			}
		}

		static public int GetRandRange(int aMin, int aMax)
		{
			int rand;
			while (true)
			{
				rand = UnityEngine.Random.Range(-aMax, aMax);
				if (-aMax <= rand && rand <= -aMin)
					break;
				if (aMin <= rand && rand <= aMax)
					break;
			}
			return rand;
		}

	}
}


