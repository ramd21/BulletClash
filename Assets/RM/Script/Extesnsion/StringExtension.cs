using UnityEngine;


namespace RM
{
	static public class StringExtension
	{
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
	}
}

