using System;
using System.Text;
using UnityEngine;


namespace RM
{
	static public class StringExtension
	{
		public static StringBuilder gStringBuilder = new StringBuilder();
		static public string Append(this string aThis, params string[] aStrArr)
		{
			gStringBuilder.Length = 0;
			gStringBuilder.Append(aThis);
			for (int i = 0; i < aStrArr.Length; i++)
			{
				gStringBuilder.Append(aStrArr[i]);
			}

			return gStringBuilder.ToString();
		}

		static public string LowerSnakeToUpperCamel(this string aThis)
		{
			string[] splitArr = aThis.Split('_');

			for (int i = 0; i < splitArr.Length; i++)
			{
				splitArr[i] = splitArr[i].ToUpperCamel();
			}

			return string.Join("", splitArr);
		}

		static public string ToUpperCamel(this string aThis)
		{
			string split = aThis;
			string upper = split[0].ToString().ToUpper();

			string upperCamel = split;

			upperCamel = upperCamel.Remove(0, 1);
			upperCamel = upperCamel.Insert(0, upper);
			return upperCamel;
		}

		static public int HexToInt(this string aThis)
		{
			return int.Parse(aThis, System.Globalization.NumberStyles.HexNumber);
		}

		static public Color HexToColor(this string aThis)
		{
			aThis = aThis.Replace("0x", "");//in case the string is formatted 0xFFFFFF
			aThis = aThis.Replace("#", "");//in case the string is formatted #FFFFFF
			byte a = 255;//assume fully visible unless specified in hex
			byte r = byte.Parse(aThis.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(aThis.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(aThis.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
			//Only use alpha if the string has enough characters
			if (aThis.Length == 8)
			{
				a = byte.Parse(aThis.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
			}
			return new Color32(r, g, b, a);
		}

		static public int ToInt(this string aThis)
		{
			return int.Parse(aThis);
		}
		static public float ToFloat(this string aThis)
		{
			return float.Parse(aThis);
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

