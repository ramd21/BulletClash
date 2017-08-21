using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

namespace RM
{
	public class FastList<T> /*where T : IEquatable<T>*/
	{
		int _Grow;
		int _Capacity;
		public int Count;
		T[] _Arr;

		public T this[int i] { get { return _Arr[i]; } }

		public FastList(int aCapacicy, int aGrow)
		{
			_Arr = new T[aCapacicy];
			_Capacity = aCapacicy;
			_Grow = aGrow;
		}

		public void Add(T aT)
		{
			if (_Capacity == Count)
			{
				_Capacity += _Grow;
				T[] temp = _Arr;

				_Arr = new T[_Capacity];
				for (int i = 0; i < temp.Length; i++)
				{
					_Arr[i] = temp[i];
				}
			}

			_Arr[Count] = aT;
			Count++;
		}

		public bool Contains(T aT)
		{
			for (int i = 0; i < Count; i++)
			{
				if (_Arr[i].Equals(aT))
					return true;
			}

			return false;
		}

		public void Clear()
		{
			Count = 0;
		}
	}
}


