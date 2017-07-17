using System;
using UnityEngine;


namespace RM
{
	static public class StringExtension
	{
		static public int ToInt(this string aThis)
		{
			return int.Parse(aThis);
		}

		static public T ToEnum<T>(this string aThis)
		{
			return (T)Enum.Parse(typeof(T), aThis);
		}

		static public bool IsNullOrEmpty(this string aThis)
		{
			return string.IsNullOrEmpty(aThis);
		}

		static public bool IsNotNullOrEmpty(this string aThis)
		{
			return !string.IsNullOrEmpty(aThis);
		}

		static public string Remove(this string aThis, string aRemove)
		{
			return aThis.Replace(aRemove, "");
		}

		static public string[,] TSVToStrMultiArr(this string aThis)
		{
			string work = aThis;
			string[] splitYArr;
			string[] splitXArr;
			int xCnt;
			int yCnt;

			work = work.Remove("\r");
			splitYArr = work.Split('\n');
			yCnt = splitYArr.Length;
			xCnt = splitYArr[0].Split('\t').Length;
			string[,] strMultiArr = new string[yCnt, xCnt];

			for (int i = 0; i < splitYArr.Length; i++)
			{
				splitXArr = splitYArr[i].Split('\t');
				for (int j = 0; j < splitXArr.Length; j++)
				{
					strMultiArr[i, j] = splitXArr[j];
				}
			}

			return strMultiArr;
		}
	}
}

