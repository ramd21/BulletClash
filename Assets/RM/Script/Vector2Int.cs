using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace RM
{
	[Serializable]
	public struct Vector2Int
	{
		public int x;
		public int y;

		public Vector2Int(int aX, int aY)
		{
			x = aX;
			y = aY;
		}

		public Vector3 ToVector3XZ()
		{
			return new Vector3(x, 0, y);
		}

		public Vector2 normalized { get { return new Vector2(x, y).normalized; } }

		public static Vector2Int zero { get { return new Vector2Int(0, 0); } }
		public static Vector2Int up { get { return new Vector2Int(0, 1); } }
		public static Vector2Int down { get { return new Vector2Int(0, -1); } }


		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}
		//public static Vector2Int operator *(float d, Vector2Int a);
		public static Vector2Int operator *(Vector2Int a, int d)
		{
			return new Vector2Int(a.x * d, a.y * d);
		}
		//public static Vector2Int operator /(Vector2Int a, float d);
		//public static bool operator ==(Vector2Int lhs, Vector2Int rhs);
		//public static bool operator !=(Vector2Int lhs, Vector2Int rhs);

		public static implicit operator Vector2(Vector2Int v)
		{
			return new Vector2(v.x, v.y);
		}

		public static implicit operator Vector2Int(Vector2 v)
		{
			return new Vector2Int((int)v.x, (int)v.y);
		}

	}
}


