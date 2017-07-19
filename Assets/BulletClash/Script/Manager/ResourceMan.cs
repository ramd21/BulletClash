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
		public Bullet[] _BulletArr;

		Dictionary<UnitType, Unit> _MeshBakedUnitDic = new Dictionary<UnitType, Unit>();


		public Unit GetUnit(UnitType aType)
		{
			if (!_MeshBakedUnitDic.ContainsKey(aType))
			{
				Unit unit = _UnitArr[(int)aType];
				unit = Instantiate(unit);
				unit.gameObject.SetActive(false);
				unit._Bat.StartBake();
				//DestroyImmediate(unit._Bat);

				_MeshBakedUnitDic.Add(aType, unit);
			}

			return _MeshBakedUnitDic[aType];
		}

		public Bullet GetBullet(BulletType aType)
		{
			return _BulletArr[(int)aType];
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


