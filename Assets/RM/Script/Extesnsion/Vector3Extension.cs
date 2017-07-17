using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace RM
{
	static public class Vector3Extension
	{
		static public Vector3 Multiply(this Vector3 aV3, Vector3 aV3Mul)
		{
			return new Vector3
				(
					aV3.x * aV3Mul.x,
					aV3.y * aV3Mul.y,
					aV3.z * aV3Mul.z
				);
		}

		static public Vector3 Divide(this Vector3 aV3, Vector3 aV3Div)
		{
			return new Vector3
				(
					aV3.x / aV3Div.x,
					aV3.y / aV3Div.y,
					aV3.z / aV3Div.z
				);
		}
		static public Vector2 ToVector2XY(this Vector3 aV3)
		{
			return new Vector2(aV3.x, aV3.y);
		}

		static public Vector2 ToVector2XZ(this Vector3 aV3)
		{
			return new Vector2(aV3.x, aV3.z);
		}

		static public Vector2 ToVector2YZ(this Vector3 aV3)
		{
			return new Vector2(aV3.y, aV3.z);
		}

		static public Vector3 ToVector3XY(this Vector3 aV3)
		{
			return new Vector3(aV3.x, aV3.y, 0);
		}

		static public Vector3 ToVector3XZ(this Vector3 aV3)
		{
			return new Vector3(aV3.x, 0, aV3.y);
		}

		static public Vector3 ToVector3YZ(this Vector3 aV3)
		{
			return new Vector3(0, aV3.x, aV3.y);
		}
	}
}


