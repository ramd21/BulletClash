using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using RM;

namespace BC
{
	public class ResourceMan : Singleton<ResourceMan>
	{
		public Unit[] _UnitArr;

		public Unit GetUnit(UnitType aType)
		{
			return _UnitArr[(int)aType];
		}



#if UNITY_EDITOR
		[Button("Load")]
		public int _Load;

		void Load()
		{
			_UnitArr = Resources.LoadAll<Unit>("Unit");
		}
#endif
	}
}


