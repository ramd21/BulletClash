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
		public Unit[]		_UnitArr;
		public Bullet[]		_BulletArr;
		public GameObject[] _EffectArr;

		public Unit GetUnit(UnitType aType)
		{
			return _UnitArr[(int)aType];
		}

		public Bullet GetBullet(BulletType aType)
		{
			return _BulletArr[(int)aType];
		}

		public GameObject GetEffect(int aId)
		{
			return _EffectArr[aId];
		}



#if UNITY_EDITOR
		[Button("Load")]
		public int _Load;

		void Load()
		{
			_UnitArr = Resources.LoadAll<Unit>("Unit");
			_BulletArr = Resources.LoadAll<Bullet>("Bullet");
		}
#endif
	}
}


